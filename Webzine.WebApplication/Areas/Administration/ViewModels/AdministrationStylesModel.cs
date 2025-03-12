// <copyright file="AdministrationStylesModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    using Webzine.Entity;

    /// <summary>
    /// Model pour la vue des styles.
    /// </summary>
    public class AdministrationStylesModel
    {
        /// <summary>
        /// Obtient ou définit la liste des styles existants.
        /// </summary>
        public List<Style> Styles { get; set; } = [];

        /// <summary>
        /// Obtient ou définit un style pour les displayfor.
        /// </summary>
        public Style Style { get; set; } = new();
    }
}
