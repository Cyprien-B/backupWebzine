// <copyright file="RechercheController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contrôleur de recherche.
    /// </summary>
    public class RechercheController : Controller
    {
        /// <summary>
        /// Gère la recherche.
        /// </summary>
        /// <param name="recherche">string de recherche.</param>
        /// <returns>Une vue avec les résultats de la recherche.</returns>
        [HttpPost]
        public IActionResult Index([FromForm] string recherche)
        {
            return this.Ok("Not implemented");
        }
    }
}
