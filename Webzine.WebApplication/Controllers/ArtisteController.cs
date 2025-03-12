// <copyright file="ArtisteController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Contrôleur pour gérer les actions liées aux artistes.
    /// </summary>
    public class ArtisteController : Controller
    {
        private Factory factory = new();

        /// <summary>
        /// Affiche la page d'un artiste spécifique.
        /// </summary>
        /// <param name="nomArtiste">Le nom de l'artiste à afficher.</param>
        /// <returns>La vue de l'artiste.</returns>
        [HttpGet]
        public IActionResult Index(string nomArtiste)
        {
            Artiste artiste = this.factory.GenerateArtiste();

            return this.View(artiste);
        }
    }
}