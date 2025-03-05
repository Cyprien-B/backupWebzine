// <copyright file="HomeController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index(int page = 1)
        {
            return this.View();
        }
    }
}
