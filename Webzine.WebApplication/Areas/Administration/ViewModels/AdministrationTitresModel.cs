// <copyright file="AdministrationTitresModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    using Webzine.Entity;

    /// <summary>
    /// Model pour envoyer des données à la vue de titre.
    /// </summary>
    public class AdministrationTitresModel
    {
        /// <summary>
        /// Obtient ou définit la liste des titres existants.
        /// </summary>
        public required List<Titre> Titres { get; set; } = [];

        /// <summary>
        /// Obtient ou définit un titre juste pour le Display.
        /// </summary>
        public Titre? Titre { get; set; } = null;
    }
}
