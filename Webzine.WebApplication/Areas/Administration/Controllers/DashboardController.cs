// <copyright file="DashboardController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity.Fixtures;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Controlleur de dashboard.
    /// </summary>
    [Area("Administration")]
    public class DashboardController(IStyleRepository styleRepository, ITitreRepository titreRepository, IArtisteRepository artisteRepository) : Controller
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
                NbArtistes = (uint)artisteRepository.FindAll().ToList().Count,
                ArtisteComposeLePlusTitres = artisteRepository.FindAll()
                    .OrderByDescending(a => a.Titres.Count)
                    .FirstOrDefault() ?? new(),
                ArtisteLePlusChronique = artisteRepository.FindAll()
                    .OrderByDescending(a => a.Titres.Count(t => !string.IsNullOrEmpty(t.Chronique)))
                    .FirstOrDefault() ?? new(),
                NbBiographies = (uint)new Random().Next(200, 500),
                TitreLePlusLu = titreRepository.FindAll().OrderByDescending(t => t.NbLectures).First(),
                NbLectures = (uint)new Random().Next(10000, 50000),
                NbLikes = (uint)new Random().Next(9000, 25000),
                NbStyles = (uint)styleRepository.FindAll().ToList().Count,
                NbTitres = (uint)new Random().Next(500, 2000),
            };

            return this.View(model);
        }
    }
}