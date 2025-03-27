// <copyright file="DbCommentaireRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
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
    public class DbCommentaireRepository : ICommentaireRepository
    {
        private readonly SQLiteContext context;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="DbCommentaireRepository"/>.
        /// </summary>
        /// <param name="context">Le contexte de base de données SQLite.</param>
        public DbCommentaireRepository(SQLiteContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public void Add(Commentaire commentaire)
        {
            if (commentaire == null)
            {
                throw new ArgumentNullException(nameof(commentaire));
            }

            this.context.Commentaires.Add(commentaire);
            this.context.SaveChanges();
        }

        /// <inheritdoc/>
        public void Delete(Commentaire commentaire)
        {
            if (commentaire == null)
            {
                throw new ArgumentNullException(nameof(commentaire));
            }

            this.context.Commentaires.Remove(commentaire);
            this.context.SaveChanges();
        }

        /// <inheritdoc/>
        public Commentaire Find(int id)
        {
            return this.context.Commentaires
                .Include(c => c.Titre)
                .FirstOrDefault(c => c.IdCommentaire == id) ?? new Commentaire();
        }

        /// <inheritdoc/>
        public IEnumerable<Commentaire> FindAll()
        {
            return this.context.Commentaires
                .Include(c => c.Titre)
                .ToList();
        }
    }
}