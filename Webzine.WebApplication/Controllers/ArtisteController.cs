// <copyright file="ArtisteController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;

    /// <summary>
    /// Contrôleur pour gérer les actions liées aux artistes.
    /// </summary>
    public class ArtisteController : Controller
    {
        /// <summary>
        /// Affiche la page d'un artiste spécifique.
        /// </summary>
        /// <param name="nomArtiste">Le nom de l'artiste à afficher.</param>
        /// <returns>La vue de l'artiste.</returns>
        [HttpGet]
        public IActionResult Index(string nomArtiste)
        {
            Artiste artiste = ArtisteFactory.Artistes.FirstOrDefault(a => a.Nom == nomArtiste) ?? new Artiste();

            return this.View(artiste);
        }
    }
}