// <copyright file="RechercheModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.ViewModels
{
    using System.Collections.Generic;
    using Webzine.Entity;

    /// <summary>
    /// Model pour servir les données nécessaires à la page de recherche.
    /// </summary>
    public class RechercheModel
    {
        /// <summary>
        /// Obtient ou définit le terme de recherche.
        /// </summary>
        public string TermeRecherche { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la liste des artistes correspondant à la recherche.
        /// </summary>
        public List<Artiste> Artistes { get; set; } = [];

        /// <summary>
        /// Obtient ou définit la liste des titres correspondant à la recherche.
        /// </summary>
        public List<Titre> Titres { get; set; } = [];
    }
}