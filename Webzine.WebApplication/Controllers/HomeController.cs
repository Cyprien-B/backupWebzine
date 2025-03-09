// <copyright file="HomeController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Controlleur principal de base.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Est la vue de la page d'accueil.
        /// </summary>
        /// <param name="page">Numéro de pagination pour les titres les plus chroniqué.</param>
        /// <returns>Retourne la vue la page principale.</returns>
        [HttpGet]
        public IActionResult Index(uint page = 1)
        {
            DataGenerator dataGenerator = new();
            HomeModel model = new()
            {
                PaginationMax = (uint)new Random().Next(90, 100),
                TitresRecemmentsChroniques = [dataGenerator.GenerateTitre(), dataGenerator.GenerateTitre(), dataGenerator.GenerateTitre()],
                TitresPopulaires = [dataGenerator.GenerateTitre(), dataGenerator.GenerateTitre(), dataGenerator.GenerateTitre()],
                CaracteresChroniqueMax = 200,
                PaginationActuelle = page,
            };

            return this.View(model);
        }

        /// <summary>
        /// Retourne la vue de test pour les données fictives.
        /// </summary>
        /// <returns> Une vue avec les données. </returns>
        [HttpGet]
        public IActionResult BogusData()
        {
            return this.View();
        }
    }
}
