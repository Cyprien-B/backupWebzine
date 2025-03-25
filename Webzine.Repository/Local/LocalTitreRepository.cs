// <copyright file="LocalTitreRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class LocalTitreRepository : ITitreRepository
    {
        /// <inheritdoc/>
        public void Add(Titre titre)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int Count()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(Titre titre)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Titre Find(int idTitre)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> FindAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> FindTitres(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void IncrementNbLectures(Titre titre)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void IncrementNbLikes(Titre titre)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> Search(string mot)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> SearchByStyle(string libelle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Update(Titre titre)
        {
            throw new NotImplementedException();
        }
    }
}