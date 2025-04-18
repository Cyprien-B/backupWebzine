﻿// <copyright file="DbTitreRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Db
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Webzine.Entity;
    using Webzine.EntityContext;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class DbTitreRepository(WebzineDbContext context) : ITitreRepository
    {
        /// <inheritdoc/>
        public void Add(Titre titre)
        {
            // Vérification de l'artiste
            var artisteExistant = context.Artistes
                .Include(a => a.Titres) // Charger les titres existants
                .Single(a => a.IdArtiste == titre.IdArtiste);

            // Vérification des doublons
            if (context.Titres.Any(t => t.Libelle == titre.Libelle && t.IdArtiste == titre.IdArtiste))
            {
                return;
            }

            // Gestion des styles
            var idsStyles = titre.Styles.Select(s => s.IdStyle).ToList();
            var stylesExistants = context.Styles
                .Where(s => idsStyles.Contains(s.IdStyle))
                .ToList();

            if (stylesExistants.Count != idsStyles.Count)
            {
                throw new InvalidDataException("Un ou plusieurs styles spécifiés n'existent pas");
            }

            // Configuration des relations
            titre.Artiste = artisteExistant;
            titre.IdArtiste = artisteExistant.IdArtiste; // Assurer la cohérence de la FK
            titre.Styles = stylesExistants; // Remplacement direct de la collection

            // Ajout et sauvegarde
            context.Titres.Add(titre);
            artisteExistant.Titres.Add(titre); // Mise à jour bidirectionnelle
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> AdministrationFindTitres(int offset, int limit)
        {
            return [.. context.Titres.Include(t => t.Artiste).OrderBy(t => t.Artiste.Nom).ThenBy(t => t.Libelle).Skip(limit * (int)(offset - 1)).Take(limit).AsNoTracking()];
        }

        /// <inheritdoc/>
        public int Count()
        {
            return context.Titres.Count();
        }

        /// <inheritdoc/>
        public long CountGlobalLikes()
        {
            return context.Titres.AsNoTracking().Sum(t => t.NbLikes);
        }

        /// <inheritdoc/>
        public void Delete(Titre titre)
        {
            // Charger le titre existant avec toutes ses relations
            var titreExistant = context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .Include(t => t.Commentaires)
                .SingleOrDefault(t => t.IdTitre == titre.IdTitre);

            if (titreExistant != null)
            {
                // Supprimer le titre
                context.Titres.Remove(titreExistant);

                // Sauvegarder les changements
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public Titre? Find(int idTitre)
        {
            return context.Titres.Include(t => t.Artiste).Include(t => t.Styles).Include(t => t.Commentaires).AsNoTracking().SingleOrDefault(t => t.IdTitre == idTitre);
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> FindAll()
        {
            return [.. context.Titres.Include(t => t.Artiste).Include(t => t.Styles)];
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> FindTitres(int offset, int limit)
        {
            return [.. context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .OrderByDescending(t => t.DateCreation)
                .Skip(limit * (offset - 1))
                .Take(limit)
                .AsNoTracking()];
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> FindTitresPopulaires(int limit)
        {
            return [.. context.Titres.Include(t => t.Artiste).OrderByDescending(t => t.NbLikes).Take(limit).AsNoTracking()];
        }

        /// <inheritdoc/>
        public bool LibelleToArtisteAny(Titre titre)
        {
            return context.Titres.Any(t => t.Libelle == titre.Libelle && t.IdArtiste == titre.IdArtiste);
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> Search(string mot)
        {
            return [.. context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .Where(t => t.Libelle.Contains(mot))
                .AsNoTracking()];
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> SearchByStyle(string libelle)
        {
            return [.. context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .Where(t => t.Styles.Any(s => s.Libelle.Contains(libelle)))];
        }

        /// <inheritdoc/>
        public void Update(Titre titre)
        {
            var existingTitre = context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles) // Charger les relations existantes
                .Include(t => t.Commentaires)
                .Single(t => t.IdTitre == titre.IdTitre);

            // Mise à jour de l'artiste
            var artisteExistant = context.Artistes.Find(titre.IdArtiste)
                ?? throw new InvalidDataException("Artiste introuvable");
            existingTitre.Artiste = artisteExistant;

            // Gestion des styles
            var idsStyles = titre.Styles.Select(s => s.IdStyle).ToList();
            var stylesExistants = context.Styles
                .Where(s => idsStyles.Contains(s.IdStyle))
                .ToList();

            if (stylesExistants.Count != idsStyles.Count)
            {
                throw new InvalidDataException("Styles invalides");
            }

            existingTitre.Styles.Clear(); // Supprime les relations existantes
            foreach (var style in stylesExistants)
            {
                existingTitre.Styles.Add(style); // Ajoute les nouvelles relations
            }

            // Mise à jour des propriétés scalaires
            context.Entry(existingTitre).CurrentValues.SetValues(titre);
            context.SaveChanges();
        }
    }
}