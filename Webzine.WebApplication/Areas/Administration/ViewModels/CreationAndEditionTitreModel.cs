// <copyright file="CreationAndEditionTitreModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    using Webzine.Entity;

    /// <summary>
    /// Model pour la création et l'édition d'un titre.
    /// </summary>
    public class CreationAndEditionTitreModel
    {
        /// <summary>
        /// Obtient ou définit la liste des styles sélectionnables.
        /// </summary>
        public IEnumerable<Style> Styles { get; set; } = [];

        /// <summary>
        /// Obtient ou définit la liste des artistes sélectionnables.
        /// </summary>
        public IEnumerable<Artiste> Artistes { get; set; } = [];

        /// <summary>
        /// Obtient ou définit le titre à poster.
        /// </summary>
        public Titre Titre { get; set; } = new();
    }
}
