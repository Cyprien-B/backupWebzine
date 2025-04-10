// <copyright file="ApiController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Classe API Controller.
    /// </summary>
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// Partie versionning de l'application et identification.
        /// </summary>
        /// <returns>Un string contenant le nom et l'api version.</returns>
        [HttpGet]
        public IActionResult Version()
        {
            var apiInfo = new
            {
                name = "webzine",
                version = "3.0",
            };

            return this.Ok(apiInfo);
        }
    }
}