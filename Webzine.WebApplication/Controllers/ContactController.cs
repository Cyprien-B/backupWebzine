// <copyright file="ContactController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Contrôleur des contacts.
    /// </summary>
    public class ContactController : Controller
    {
        /// <summary>
        /// Contacts.
        /// </summary>
        /// <returns>Retourne la vue des contacts.</returns>
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
