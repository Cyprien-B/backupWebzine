// <copyright file="ApiController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Classe API Controller.
    /// </summary>
    public class ApiController : Controller
    {
        /// <summary>
        /// Partie versionning de l'application et identification.
        /// </summary>
        /// <returns>Un string contenant le nom et l'api version.</returns>
        [HttpGet]
        public string Version()
        {
            string jsonApiVersion = "{\n" +
                "\t\"name\" : \"webzine\",\n" +
                "\t\"version\" : \"1.0\"\n" +
                "}";
            return jsonApiVersion;
        }
    }
}
