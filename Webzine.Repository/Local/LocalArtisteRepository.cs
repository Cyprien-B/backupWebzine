// <copyright file="LocalArtisteRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
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
            // Trouver le premier ID disponible
            int newId = 1;
            while (ArtisteFactory.Artistes.Any(a => a.IdArtiste == newId))
            {
                newId++;
            }

            // Assigner le nouvel ID à l'artiste
            artiste.IdArtiste = newId;

            // Ajouter l'artiste à la collection
            ArtisteFactory.Artistes.Add(artiste);
        }

        /// <inheritdoc/>
        public void Delete(Artiste artiste)
        {
            ArtisteFactory.Artistes.RemoveAll(a => a.IdArtiste == artiste.IdArtiste);
        }

        /// <inheritdoc/>
        public Artiste Find(int id)
        {
            return ArtisteFactory.Artistes.FirstOrDefault(a => a.IdArtiste == id) ?? new();
        }

        /// <inheritdoc/>
        public IEnumerable<Artiste> FindAll()
        {
            return Factory.Artistes;
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
                existingArtiste.Nom = artiste.Nom; // Mise à jour des propriétés
                existingArtiste.Biographie = artiste.Biographie;
                existingArtiste.Titres = artiste.Titres;
            }
        }
    }
}