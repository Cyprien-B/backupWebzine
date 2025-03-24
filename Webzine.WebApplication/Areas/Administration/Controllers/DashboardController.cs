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
        /// La page de dashboard et de métriques importantes du site.
        /// </summary>
        /// <returns>Une vue du dashboard.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            AdministrationDashboardModel model = new()
            {
                NbArtistes = (uint)ArtisteFactory.Artistes.Count,
                ArtisteComposeLePlusTitres = ArtisteFactory.Artistes
                    .OrderByDescending(a => a.Titres.Count)
                    .FirstOrDefault() ?? new(),
                ArtisteLePlusChronique = ArtisteFactory.Artistes
                    .OrderByDescending(a => a.Titres.Count(t => !string.IsNullOrEmpty(t.Chronique)))
                    .FirstOrDefault() ?? new(),
                NbBiographies = (uint)new Random().Next(200, 500),
                TitreLePlusLu = TitreFactory.Titres[0],
                NbLectures = (uint)new Random().Next(10000, 50000),
                NbLikes = (uint)new Random().Next(9000, 25000),
                NbStyles = (uint)StyleFactory.Styles.Count,
                NbTitres = (uint)new Random().Next(500, 2000),
            };

            return this.View(model);
        }
    }
}