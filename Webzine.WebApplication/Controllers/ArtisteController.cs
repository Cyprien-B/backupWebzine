// <copyright file="ArtisteController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contrôleur des Artistes.
    /// </summary>
    public class ArtisteController : Controller
    {
        /// <summary>
        /// Recherche artiste.
        /// </summary>
        /// <param name="artiste">Nom de l'artiste demandé.</param>
        /// <returns>Renvois la vue de l'artiste demandé.</returns>
        public IActionResult Index(string artiste)
        {
            return this.Ok("Not implemented");
        }
    }
}
