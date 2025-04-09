// <copyright file="DbTitreRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
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
                .FirstOrDefault(a => a.IdArtiste == titre.IdArtiste);

            if (artisteExistant == null)
            {
                throw new InvalidDataException("L'artiste spécifié n'existe pas");
            }

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
            return context.Titres.Include(t => t.Artiste).OrderBy(t => t.Artiste.Nom).Skip(limit * (int)(offset - 1)).Take(limit).AsNoTracking().ToList();
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
                .FirstOrDefault(t => t.IdTitre == titre.IdTitre);

            if (titreExistant != null)
            {
                // Supprimer les commentaires associés
                context.Commentaires.RemoveRange(titreExistant.Commentaires);

                // Retirer le titre de la collection de titres de l'artiste
                if (titreExistant.Artiste != null)
                {
                    titreExistant.Artiste.Titres.Remove(titreExistant);
                }

                // Retirer le titre des collections de titres des styles
                foreach (var style in titreExistant.Styles.ToList())
                {
                    style.Titres.Remove(titreExistant);
                }

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
            return context.Titres.Include(t => t.Artiste).Include(t => t.Styles).ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> FindTitres(int offset, int limit)
        {
            return context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .OrderByDescending(t => t.DateCreation)
                .Skip(limit * (offset - 1))
                .Take(limit)
                .AsNoTracking()
                .ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> FindTitresPopulaires(int limit)
        {
            return context.Titres.Include(t => t.Artiste).OrderByDescending(t => t.NbLikes).Take(limit).AsNoTracking().ToList();
        }

        /// <inheritdoc/>
        public void IncrementNbLectures(Titre titre)
        {
            var titreToUpdate = context.Titres.Find(titre.IdTitre);
            if (titreToUpdate != null)
            {
                titreToUpdate.NbLectures++;
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public void IncrementNbLikes(Titre titre)
        {
            var titreToUpdate = context.Titres.Find(titre.IdTitre);
            if (titreToUpdate != null)
            {
                titreToUpdate.NbLikes++;
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public bool LibelleToArtisteAny(Titre titre)
        {
            return context.Titres.Any(t => t.Libelle == titre.Libelle && t.IdArtiste == titre.IdArtiste);
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> Search(string mot)
        {
            return context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .Where(t => t.Libelle.Contains(mot))
                .AsNoTracking()
                .ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> SearchByStyle(string libelle)
        {
            return context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .Where(t => t.Styles.Any(s => s.Libelle.Contains(libelle)))
                .ToList();
        }

        /// <inheritdoc/>
        public void Update(Titre titre)
        {
            var existingTitre = context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .Include(t => t.Commentaires)
                .FirstOrDefault(t => t.IdTitre == titre.IdTitre);

            if (existingTitre == null)
            {
                throw new InvalidDataException("Le titre à mettre à jour n'existe pas");
            }

            // Mise à jour de l'artiste
            var artisteExistant = context.Artistes.Find(titre.IdArtiste);
            if (artisteExistant == null)
            {
                throw new InvalidDataException("L'artiste spécifié n'existe pas");
            }

            existingTitre.Artiste = artisteExistant;

            // Mise à jour des styles
            var stylesExistants = context.Styles
                .Where(s => titre.Styles.Select(ts => ts.IdStyle).Contains(s.IdStyle))
                .ToList();

            if (stylesExistants.Count != titre.Styles.Count())
            {
                throw new InvalidDataException("Un ou plusieurs styles spécifiés n'existent pas");
            }

            existingTitre.Styles = [];
            foreach (var style in stylesExistants)
            {
                existingTitre.Styles.Append(style);
            }

            // Mise à jour des propriétés simples
            context.Entry(existingTitre).CurrentValues.SetValues(titre);

            // Les commentaires ne sont généralement pas mis à jour lors de la mise à jour d'un titre
            context.SaveChanges();
        }
    }
}