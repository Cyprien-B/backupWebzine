// <copyright file="AdministrationDashboardModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    using Webzine.Entity;

    /// <summary>
    /// Model pour la vue du dashboard.
    /// </summary>
    public class AdministrationDashboardModel
    {
        /// <summary>
        /// Obtient ou définit le nombre d'Artistes total existant dans les sources (Local ou BDD).
        /// </summary>
        public uint NbArtistes { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit l'artiste dont les titres qu'il a composé ont été le plus chroniqués.
        /// </summary>
        public Artiste? ArtisteLePlusChronique { get; set; } = new();

        /// <summary>
        /// Obtient ou définit l'artiste avec le plus de titres provenants d'albums distincts.
        /// </summary>
        public Artiste? ArtisteComposeLePlusTitres { get; set; } = new();

        /// <summary>
        /// Obtient ou définit le nombre d'artiste possédant une biographie.
        /// </summary>
        public uint NbBiographies { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le titre donc la chronique est la plus lu.
        /// </summary>
        public Titre? TitreLePlusLu { get; set; } = new();

        /// <summary>
        /// Obtient ou définit le nombre de titres total existant dans les sources (Local ou BDD).
        /// </summary>
        public uint NbTitres { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le nombre de styles total existant dans les sources (Local ou BDD).
        /// </summary>
        public uint NbStyles { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le nombre de lecture total de tous les titres chroniqués réunis.
        /// </summary>
        public uint NbLectures { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le nombre de like total de tous les titres chroniqué réunis.
        /// </summary>
        public uint NbLikes { get; set; } = 0;
    }
}
