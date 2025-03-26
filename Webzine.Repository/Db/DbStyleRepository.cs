// <copyright file="DbStyleRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using System.Linq;
    using Webzine.Entity;
    using Webzine.EntityContext.Dbcontext;
    using Webzine.Repository.Contracts;

    /// <summary>
    /// Implémentation du dépôt pour gérer les styles.
    /// </summary>
    public class DbStyleRepository(SQLiteContext context) : IStyleRepository
    {
        /// <inheritdoc/>
        public void Add(Style style)
        {
            context.Styles.Add(style);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public void Delete(Style style)
        {
            context.Styles.Remove(style);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public Style Find(int id)
        {
            var styleFind = context.Styles.Find(id);

            if (styleFind == null)
            {
                throw new KeyNotFoundException($"Le style avec l'identifiant {id} n'a pas été trouvé.");
            }

            return styleFind;
        }

        /// <inheritdoc/>
        public IEnumerable<Style> FindAll()
        {
            return context.Styles;
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
                existingStyle.Libelle = style.Libelle;
                context.SaveChanges();
            }
        }
    }
}
