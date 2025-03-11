// <copyright file="TitreModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;

    /// <summary>
    /// Model pour servir les données nécessaires à la page d'un titre.
    /// </summary>
    public class TitreModel
    {
        /// <summary>
        /// Obtient ou définit le titre affiché.
        /// </summary>
        public Titre Titre { get; set; } = new();

        /// <summary>
        /// Obtient ou définit un commentaire pour les displaynamefor.
        /// </summary>
        public Commentaire Commentaire { get; set; } = new();
    }
}