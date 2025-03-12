// <copyright file="DashboardController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Controlleur de dashboard.
    /// </summary>
    [Area("Administration")]
    public class DashboardController : Controller
    {
        /// <summary>
        /// Obtient ou définit un générateur de fausse données.
        /// </summary>
        public Factory Factory { get; set; } = new();

        /// <summary>
        /// La page de dashboard et de métriques importantes du site.
        /// </summary>
        /// <returns>Une vue du dashboard.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            AdministrationDashboardModel model = new()
            {
                NbArtistes = (uint)new Random().Next(400, 600),
                ArtisteComposeLePlusTitres = this.Factory.GenerateArtiste(),
                ArtisteLePlusChronique = this.Factory.GenerateArtiste(),
                NbBiographies = (uint)new Random().Next(200, 500),
                TitreLePlusLu = this.Factory.GenerateTitre(),
                NbLectures = (uint)new Random().Next(10000, 50000),
                NbLikes = (uint)new Random().Next(9000, 25000),
                NbStyles = 18,
                NbTitres = (uint)new Random().Next(500, 2000),
            };

            return this.View(model);
        }
    }
}