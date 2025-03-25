// <copyright file="HomeController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Controlleur principal de base.
    /// </summary>
    public class HomeController(IConfiguration configuration) : Controller
    {
        /// <summary>
        /// Est la vue de la page d'accueil.
        /// </summary>
        /// <param name="page">Numéro de pagination pour les titres les plus chroniqué.</param>
        /// <returns>Retourne la vue la page principale.</returns>
        [HttpGet]
        public IActionResult Index(uint page = 1)
        {
            int nbTitresChroniques = configuration.GetValue<int>("App:Pages:Home:NbTitresChroniquesParPaginations");
            int nbTitresPopulaires = configuration.GetValue<int>("App:Pages:Home:NbTitresPopulaires");

            HomeModel model = new()
            {
                PaginationMax = (uint)Math.Ceiling((double)TitreFactory.Titres.Count / nbTitresChroniques),
                TitresRecemmentsChroniques = TitreFactory.Titres.Skip(nbTitresChroniques * (int)(page - 1)).Take(nbTitresChroniques).ToList(),
                TitresPopulaires = TitreFactory.Titres.OrderByDescending(t => t.NbLikes).Take(nbTitresPopulaires).ToList(),
                CaracteresChroniqueMax = 200,
                PaginationActuelle = page,
            };

            return this.View(model);
        }

        /// <summary>
        /// Gestion des erreur de pages.
        /// </summary>
        /// <returns>Retourne quand page est mauvaise.</returns>
        [HttpGet]
        public IActionResult NotFound404()
        {
            return this.View();
        }
    }
}
