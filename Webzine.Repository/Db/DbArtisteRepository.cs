// <copyright file="DbArtisteRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Db
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
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
            // Vérifie si un artiste avec le même libellé existe déjà
            bool artisteExiste = this.NomAny(artiste);

            if (artisteExiste)
            {
                // Ignorer l'ajout si le artiste existe déjà
                return;
            }

            context.Artistes.Add(artiste);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public IEnumerable<Artiste> AdministrationFindArtistes(int offset, int limit)
        {
            return context.Artistes
                .OrderBy(a => a.Nom)
                .Skip(limit * (int)(offset - 1))
                .Take(limit)
                .AsNoTracking().ToList();
        }

        /// <inheritdoc/>
        public int Count()
        {
            return context.Artistes.AsNoTracking().Count();
        }

        /// <inheritdoc/>
        public int CountBiographies()
        {
            return context.Artistes.Where(a => a.Biographie != string.Empty).AsNoTracking().Count();
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
            var artisteFind = context.Artistes.AsNoTracking().Single(a => a.IdArtiste == id);

            return artisteFind;
        }

        /// <inheritdoc/>
        public IEnumerable<Artiste> FindAll()
        {
            return context.Artistes.Include(a => a.Titres).OrderBy(a => a.Nom).AsNoTracking().ToList();
        }

        /// <inheritdoc/>
        public Artiste? FindArtisteComposePlusTitre()
        {
            return context.Artistes
                .OrderByDescending(a => a.Titres.Count)
                .AsNoTracking()
                .FirstOrDefault();
        }

        /// <inheritdoc/>
        public Artiste? FindArtistePlusChronique()
        {
            return context.Artistes
                .OrderByDescending(a => a.Titres.Count(t => !string.IsNullOrEmpty(t.Chronique)))
                .AsNoTracking()
                .FirstOrDefault();
        }

        /// <inheritdoc/>
        public Artiste FindArtisteByName(string nom)
        {
            return context.Artistes.AsNoTracking().Single(a => a.Nom == nom);
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
                existingArtiste.Nom = artiste.Nom;
                existingArtiste.Biographie = artiste.Biographie;
                existingArtiste.Titres = artiste.Titres;
            }

            context.SaveChanges();
        }

        /// <inheritdoc/>
        public bool NomAny(Artiste artiste)
        {
            return context.Artistes.Any(a => a.Nom == artiste.Nom);
        }
    }
}
