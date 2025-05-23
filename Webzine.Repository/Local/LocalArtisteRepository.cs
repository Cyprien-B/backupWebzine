﻿// <copyright file="LocalArtisteRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class LocalArtisteRepository : IArtisteRepository
    {
        /// <inheritdoc/>
        public void Add(Artiste artiste)
        {
            // Vérifie si un style avec le même libellé existe déjà
            bool artisteExiste = this.NomAny(artiste);

            if (artisteExiste)
            {
                // Ignorer l'ajout si le style existe déjà
                return;
            }

            // Trouver le premier ID disponible
            int newId = 1;
            while (Factory.Artistes.Any(a => a.IdArtiste == newId))
            {
                newId++;
            }

            // Assigner le nouvel ID à l'artiste
            artiste.IdArtiste = newId;

            // Ajouter l'artiste à la collection
            Factory.Artistes.Add(artiste);
        }

        /// <inheritdoc/>
        public IEnumerable<Artiste> AdministrationFindArtistes(int offset, int limit)
        {
            return Factory.Artistes.OrderBy(a => a.Nom).Skip(limit * (int)(offset - 1)).Take(limit).ToList();
        }

        /// <inheritdoc/>
        public int Count()
        {
            return Factory.Artistes.Count;
        }

        /// <inheritdoc/>
        public void Delete(Artiste artiste)
        {
            Factory.Artistes.RemoveAll(a => a.IdArtiste == artiste.IdArtiste);
            Factory.Titres = Factory.Titres.Where(t => t.IdArtiste != artiste.IdArtiste).ToList();
        }

        /// <inheritdoc/>
        public Artiste? Find(int id)
        {
            return Factory.Artistes.SingleOrDefault(a => a.IdArtiste == id);
        }

        /// <inheritdoc/>
        public IEnumerable<Artiste> FindAll()
        {
            return [.. Factory.Artistes.OrderBy(a => a.Nom)];
        }

        /// <inheritdoc/>
        public Artiste FindArtisteByName(string nom)
        {
            return Factory.Artistes.Single(a => a.Nom == nom);
        }

        /// <inheritdoc/>
        public bool NomAny(Artiste artiste)
        {
            return Factory.Artistes.Any(a => a.Nom == artiste.Nom);
        }

        /// <inheritdoc/>
        public IEnumerable<Artiste> Search(string mot)
        {
            return Factory.Artistes
                .Where(a => a.Nom.Contains(mot, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        /// <inheritdoc/>
        public void Update(Artiste artiste)
        {
            Artiste? existingArtiste = Factory.Artistes.FirstOrDefault(a => a.IdArtiste == artiste.IdArtiste);
            if (existingArtiste == null)
            {
                this.Add(artiste);
            }
            else
            {
                // Vérifie si un style avec le même libellé existe déjà
                bool artisteExiste = Factory.Artistes.Any(a => a.Nom == artiste.Nom);

                if (artisteExiste)
                {
                    // Ignorer l'ajout si le style existe déjà
                    return;
                }

                // Mise à jour des propriétés de l'artiste existant
                existingArtiste.Nom = artiste.Nom;
                existingArtiste.Biographie = artiste.Biographie;
                existingArtiste.Titres = artiste.Titres;

                // Mise à jour des références à l'artiste dans tous les titres
                foreach (var titre in Factory.Titres)
                {
                    if (titre.IdArtiste == artiste.IdArtiste)
                    {
                        titre.IdArtiste = artiste.IdArtiste; // Ceci est redondant mais inclus pour la clarté
                        titre.Artiste = existingArtiste; // Utiliser existingArtiste pour maintenir la cohérence des références
                    }
                }
            }
        }
    }
}