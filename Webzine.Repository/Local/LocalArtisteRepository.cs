// <copyright file="LocalArtisteRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class LocalArtisteRepository : IArtisteRepository
    {
        /// <inheritdoc/>
        public void Add(Artiste artiste)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Delete(Artiste artiste)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Artiste Find(int id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<Artiste> FindAll()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Update(Artiste artiste)
        {
            throw new NotImplementedException();
        }
    }
}