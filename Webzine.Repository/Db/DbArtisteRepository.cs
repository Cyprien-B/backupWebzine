// <copyright file="DbArtisteRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Db
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Webzine.Entity;
    using Webzine.EntityContext;
    using Webzine.Repository.Contracts;

    /// <summary>
    /// Implémentation du dépôt pour gérer les artistes avec une base de données.
    /// </summary>
    public class DbArtisteRepository(WebzineDbContext context) : IArtisteRepository
    {
        /// <inheritdoc/>
        public void Add(Artiste artiste)
        {
            // Vérifie si un style avec le même libellé existe déjà
            bool artisteExiste = context.Artistes.Any(a => a.Nom == artiste.Nom);

            if (artisteExiste)
            {
                // Ignorer l'ajout si le style existe déjà
                return;
            }

            context.Artistes.Add(artiste);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public IEnumerable<Artiste> AdministrationFindArtistes(int offset, int limit)
        {
            return context.Artistes.OrderBy(a => a.Nom).Skip(limit * (int)(offset - 1)).Take(limit).AsNoTracking().ToList();
        }

        /// <inheritdoc/>
        public void Delete(Artiste artiste)
        {
            context.Artistes.Remove(artiste);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public Artiste Find(int id)
        {
            var artisteFind = context.Artistes.Find(id);

            if (artisteFind == null)
            {
                throw new KeyNotFoundException($"L'artiste avec l'identifiant {id} n'a pas été trouvé.");
            }

            return artisteFind;
        }

        /// <inheritdoc/>
        public IEnumerable<Artiste> FindAll()
        {
            return context.Artistes.Include(a => a.Titres);
        }

        /// <inheritdoc/>
        public void Update(Artiste artiste)
        {
            var existingArtiste = context.Artistes.Find(artiste.IdArtiste);
            if (existingArtiste == null)
            {
                this.Add(artiste);
            }
            else
            {
                // Vérifie si un style avec le même libellé existe déjà
                bool artisteExiste = context.Artistes.Any(a => a.Nom == artiste.Nom);

                if (artisteExiste)
                {
                    // Ignorer l'ajout si le style existe déjà
                    return;
                }

                existingArtiste.Nom = artiste.Nom;
                existingArtiste.Biographie = artiste.Biographie;
                existingArtiste.Titres = artiste.Titres;
                context.SaveChanges();
            }
        }
    }
}
