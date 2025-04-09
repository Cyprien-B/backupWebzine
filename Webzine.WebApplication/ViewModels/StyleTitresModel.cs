// <copyright file="StyleTitresModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.ViewModels
{
    using System.Collections.Generic;
    using Webzine.Entity;

    /// <summary>
    /// Model pour servir les données nécessaires à la page des titres par style.
    /// </summary>
    public class StyleTitresModel
    {
        /// <summary>
        /// Obtient ou définit le style musical.
        /// </summary>
        public Style Style { get; set; } = new();

        /// <summary>
        /// Obtient ou définit la liste des titres du style.
        /// </summary>
        public List<Titre> Titres { get; set; } = [];
    }
}