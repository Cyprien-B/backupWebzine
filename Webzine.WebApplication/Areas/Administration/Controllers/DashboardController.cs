// <copyright file="DashboardController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Controlleur de dashboard.
    /// </summary>
    [Area("Administration")]
    public class DashboardController : Controller
    {
        /// <summary>
        /// La page de dashboard et de métriques importantes du site.
        /// </summary>
        /// <returns>Une vue du dashboard.</returns>
        public IActionResult Index()
        {
            AdministrationDashboardModel model = new()
            {
                NbArtistes = 405,
                ArtisteComposeLePlusTitres = new() { Nom = "Michel Blanc" },
                ArtisteLePlusChronique = new() { Nom = "Daft Punk" },
                NbBiographies = 387,
                TitreLePlusLu = new() { Libelle = "Le pouvoir des fleurs", Artiste = new() { Nom = "François Cabrel" }, },
                NbLectures = 48025,
                NbLikes = 20066,
                NbStyles = 18,
                NbTitres = 5412,
            };

            return this.View(model);
        }
    }
}