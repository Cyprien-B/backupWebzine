// <copyright file="ITitreService.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Business.Contracts
{
    using Webzine.Entity;

    /// <summary>
    /// Contrat assurant que les services sont capables de fournir les méthodes de logique métier d'un titre.
    /// </summary>
    public interface ITitreService
    {
        /// <summary>
        /// Incrémente de 1 le nombre de like d'un titre et le met à jour.
        /// </summary>
        /// <param name="titre">Titre à incrémenter.</param>
        public void IncrementNbLikes(Titre titre);

        /// <summary>
        /// Incrémente de 1 le nombre de vues d'un titre et le met à jour.
        /// </summary>
        /// <param name="titre">Titre à incrémenter.</param>
        public void IncrementNbLectures(Titre titre);
    }
}