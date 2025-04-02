// <copyright file="DashboardController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
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
            var artisteParDefaut = new Artiste()
            {
                Nom = "Aucun artiste",
            };

            var titreParDefaut = new Titre()
            {
                Libelle = "Aucun titre",
                Artiste = artisteParDefaut,
            };

            AdministrationDashboardModel model = new()
            {
                NbArtistes = (uint)artisteRepository.Count(),
                ArtisteComposeLePlusTitres = artisteRepository.FindArtisteComposePlusTitre() ?? artisteParDefaut,
                ArtisteLePlusChronique = artisteRepository.FindArtistePlusChronique() ?? artisteParDefaut,
                NbBiographies = (uint)artisteRepository.CountBiographies(),
                TitreLePlusLu = titreRepository.FindTitresPlusLu() ?? titreParDefaut,
                NbLectures = (uint)titreRepository.CountGlobalLectures(),
                NbLikes = (uint)titreRepository.CountGlobalLikes(),
                NbStyles = (uint)styleRepository.Count(),
                NbTitres = (uint)titreRepository.Count(),
            };

            return this.View(model);
        }
    }
}