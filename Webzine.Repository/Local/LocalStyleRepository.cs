// <copyright file="LocalStyleRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class LocalStyleRepository : IStyleRepository
    {
        /// <inheritdoc/>
        public void Add(Style style)
        {
            // Trouver le premier ID disponible
            int newId = 1;
            while (Factory.Styles.Any(s => s.IdStyle == newId))
            {
                newId++;
            }

            // Assigner le nouvel ID au style
            style.IdStyle = newId;

            // Ajouter le style à la collection
            Factory.Styles.Add(style);
        }

        /// <inheritdoc/>
        public void Delete(Style style)
        {
            Factory.Styles.RemoveAll(s => s.IdStyle == style.IdStyle);
        }

        /// <inheritdoc/>
        public Style Find(int id)
        {
            return Factory.Styles.FirstOrDefault(s => s.IdStyle == id) ?? new();
        }

        /// <inheritdoc/>
        public IEnumerable<Style> FindAll()
        {
            return Factory.Styles;
        }

        /// <inheritdoc/>
        public void Update(Style style)
        {
            Style? existingStyle = Factory.Styles.FirstOrDefault(s => s.IdStyle == style.IdStyle);
            if (existingStyle == null)
            {
                this.Add(style);
            }
            else
            {
                existingStyle.Libelle = style.Libelle; // Mise à jour des propriétés
            }
        }
    }
}