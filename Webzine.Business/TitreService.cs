// <copyright file="TitreService.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Business
{
    using Webzine.Business.Contracts;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class TitreService(ITitreRepository titreRepository) : ITitreService
    {
        /// <inheritdoc/>
        public void IncrementNbLikes(Titre titre)
        {
            var newtitre = titreRepository.Find(titre.IdTitre);
            if (newtitre != null)
            {
                newtitre.NbLikes++;
                titreRepository.Update(newtitre);
            }
        }

        /// <inheritdoc/>
        public void IncrementNbLectures(Titre titre)
        {
            var newtitre = titreRepository.Find(titre.IdTitre);
            if (newtitre != null)
            {
            newtitre.NbLectures++;
            titreRepository.Update(newtitre);
            }
        }
    }
}
