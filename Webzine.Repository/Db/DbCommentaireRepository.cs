// <copyright file="DbCommentaireRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Repository.Db
{
    /// <inheritdoc/>
    public class DbCommentaireRepository : ICommentaireRepository
    {
        /// <inheritdoc/>
        public void Add(Commentaire commentaire)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(Commentaire commentaire)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Commentaire Find(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Commentaire> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
