// <copyright file="DashboardController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Business.Contracts;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Controlleur de dashboard.
    /// </summary>
    [Area("Administration")]
    public class DashboardController(IStyleRepository styleRepository, ITitreRepository titreRepository, IArtisteRepository artisteRepository, IDashboardService dashboardService) : Controller
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
                NbArtistes = (uint)artisteRepository.Count(),
                NbStyles = (uint)styleRepository.Count(),
                NbTitres = (uint)titreRepository.Count(),
                ArtisteComposeLePlusTitres = dashboardService.FindArtisteComposePlusTitre(),
                ArtisteLePlusChronique = dashboardService.FindArtistePlusChronique(),
                NbBiographies = (uint)dashboardService.CountBiographies(),
                TitreLePlusLu = dashboardService.FindTitresPlusLu(),
                NbLectures = (uint)dashboardService.CountGlobalLectures(),
                NbLikes = (uint)dashboardService.CountGlobalLikes(),
            };

            return this.View(model);
        }
    }
}