// <copyright file="HomeModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.ViewModels
{
    using Webzine.Entity;

    /// <summary>
    /// Model pour servir les données nécessaire à la page d'accueil.
    /// </summary>
    public class HomeModel
    {
        /// <summary>
        /// Obtient ou définit la liste de titre classé par popularité.
        /// </summary>
        public IEnumerable<Titre> TitresPopulaires { get; set; } = new List<Titre>();

        /// <summary>
        /// Obtient ou définit la liste de titre classé par chronique récente.
        /// </summary>
        public IEnumerable<Titre> TitresRecemmentsChroniques { get; set; } = new List<Titre>();

        /// <summary>
        /// Obtient ou définit le nombre de caractères maximum de début de chronique à afficher sur la page d'accueil.
        /// </summary>
        public uint CaracteresChroniqueMax { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le numéro de pagination actuel.
        /// </summary>
        public uint PaginationActuelle { get; set; } = 1;

        /// <summary>
        /// Obtient ou définit le numéro de pagination maximal.
        /// </summary>
        public uint PaginationMax { get; set; } = 1;
    }
}
