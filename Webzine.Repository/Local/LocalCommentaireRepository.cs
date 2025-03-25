// <copyright file="LocalCommentaireRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class LocalCommentaireRepository : ICommentaireRepository
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