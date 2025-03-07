// <copyright file="AdministrationArtistesModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    using Webzine.Entity;

    /// <summary>
    /// Model pour envoyer des données à la vue d'artistes administration.
    /// </summary>
    public class AdministrationArtistesModel
    {
        /// <summary>
        /// Obtient ou définit la liste des artiste existants.
        /// </summary>
        public List<Artiste> Artistes { get; set; } = [];

        /// <summary>
        /// Obtient ou définit un artiste pour les displayfor.
        /// </summary>
        public Artiste Artiste { get; set; } = new();
    }
}
