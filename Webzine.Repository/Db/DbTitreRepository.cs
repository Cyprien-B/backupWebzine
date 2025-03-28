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
    using Webzine.EntityContext.Dbcontext;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class DbTitreRepository(SQLiteContext context) : ITitreRepository
    {
        /// <inheritdoc/>
        public void Add(Titre titre)
        {
            try
            {
                // Vérifiez que l'ID de l'artiste est défini
                if (titre.IdArtiste != 0)  // Assurez-vous que le nom de la propriété est correct
                {
                    // Attachez l'artiste existant au contexte
                    var artisteExistant = new Artiste { IdArtiste = titre.IdArtiste };
                    context.Artistes.Attach(artisteExistant);

                    // Assurez-vous que la référence à l'artiste est correcte
                    titre.Artiste = artisteExistant;
                }

                context.Titres.Add(titre);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // TODO: Log exception.
            }
        }

        /// <inheritdoc/>
        public int Count()
        {
            return context.Titres.Count();
        }

        /// <inheritdoc/>
        public void Delete(Titre titre)
        {
            context.Titres.Remove(titre);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public Titre Find(int idTitre)
        {
            return context.Titres.Include(t => t.Artiste).Include(t => t.Styles).FirstOrDefault(t => t.IdTitre == idTitre) ?? new Titre();
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
                .ToList();
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
        public IEnumerable<Titre> Search(string mot)
        {
            return context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .Where(t => EF.Functions.Like(t.Libelle, $"%{mot}%"))
                .ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> SearchByStyle(string libelle)
        {
            return context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .Where(t => t.Styles.Any(s => EF.Functions.Like(s.Libelle, libelle)))
                .ToList();
        }

        /// <inheritdoc/>
        public void Update(Titre titre)
        {
            var existingTitre = context.Titres.Find(titre.IdTitre);
            if (existingTitre == null)
            {
                this.Add(titre);
            }
            else
            {
                context.Entry(existingTitre).CurrentValues.SetValues(titre);
                context.SaveChanges();
            }
        }
    }
}
