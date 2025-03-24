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
            HomeModel model = new()
            {
                PaginationMax = (uint)Math.Ceiling((double)TitreFactory.Titres.Count / 3),
                TitresRecemmentsChroniques = TitreFactory.Titres.Skip(3 * (int)(page - 1)).Take(3).ToList(),
                TitresPopulaires = TitreFactory.Titres.OrderByDescending(t => t.NbLikes).Take(3).ToList(),
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
