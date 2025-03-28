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
                NbBiographies = (uint)artisteRepository.FindAll().ToList().Where(a => a.Biographie != string.Empty).ToList().Count,
                TitreLePlusLu = titreRepository.FindAll().OrderByDescending(t => t.NbLectures).First(),
                NbLectures = (uint)titreRepository.FindAll().ToList().Sum(t => t.NbLectures),
                NbLikes = (uint)titreRepository.FindAll().ToList().Sum(t => t.NbLikes),
                NbStyles = (uint)styleRepository.FindAll().ToList().Count,
                NbTitres = (uint)titreRepository.FindAll().ToList().Count,
            };

            return this.View(model);
        }
    }
}