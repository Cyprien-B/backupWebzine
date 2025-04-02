// <copyright file="DbStyleRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Webzine.Entity;
    using Webzine.EntityContext;
    using Webzine.Repository.Contracts;

    /// <summary>
    /// Implémentation du dépôt pour gérer les styles.
    /// </summary>
    public class DbStyleRepository(WebzineDbContext context) : IStyleRepository
    {
        /// <inheritdoc/>
        public void Add(Style style)
        {
            // Vérifie si un style avec le même libellé existe déjà
            bool styleExiste = context.Styles.Any(s => s.Libelle == style.Libelle);

            if (styleExiste)
            {
                // Ignorer l'ajout si le style existe déjà
                return;
            }

            // Ajouter le style s'il n'existe pas
            context.Styles.Add(style);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public IEnumerable<Style> AdministrationFindStyles(int offset, int limit)
        {
            return context.Styles.OrderBy(s => s.Libelle).Skip(limit * (int)(offset - 1)).Take(limit).AsNoTracking().ToList();
        }

        /// <inheritdoc/>
        public void Delete(Style style)
        {
            context.Styles.Remove(style);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public int Count()
        {
            return context.Styles.AsNoTracking().Count();
        }

        /// <inheritdoc/>
        public Style? Find(int id)
        {
            return context.Styles.AsNoTracking().Single(s => s.IdStyle == id);
        }

        /// <inheritdoc/>
        public IEnumerable<Style> FindAll()
        {
            return context.Styles.OrderBy(s => s.Libelle).AsNoTracking().ToList();
        }

        /// <inheritdoc/>
        public void Update(Style style)
        {
            var existingStyle = context.Styles.Find(style.IdStyle);
            if (existingStyle == null)
            {
                this.Add(style);
            }
            else
            {
                // Vérifie si un style avec le même libellé existe déjà
                bool styleExiste = context.Styles.Any(s => s.Libelle == style.Libelle);

                if (styleExiste)
                {
                    return;
                }

                existingStyle.Libelle = style.Libelle;
            }

            context.SaveChanges();
        }

        /// <inheritdoc/>
        public IEnumerable<Style> FindStylesByIds(IEnumerable<int> styleIds)
        {
            return context.Styles
                    .Where(s => styleIds.Contains(s.IdStyle))
                    .ToList();
        }
    }
}
