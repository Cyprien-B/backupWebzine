// <copyright file="RechercheController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Contrôleur de recherche.
    /// </summary>
    public class RechercheController : Controller
    {
        private readonly Factory factory;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RechercheController"/>.
        /// </summary>
        public RechercheController()
        {
            this.factory = new Factory();
        }

        /// <summary>
        /// Gère la recherche et affiche les résultats.
        /// </summary>
        /// <param name="recherche">string de recherche.</param>
        /// <returns>Une vue avec les résultats de la recherche.</returns>
        [HttpPost]
        public IActionResult Index([FromForm] string recherche)
        {
            if (string.IsNullOrWhiteSpace(recherche))
            {
                return this.RedirectToAction("Index", "Home");
            }

            // Générer 0-3 artistes aléatoires
            int nbArtistes = new Random().Next(0, 4);
            int nbTitres = new Random().Next(0, 5);

            RechercheModel model = new()
            {
                TermeRecherche = recherche,
                Artistes = nbArtistes > 0 ? this.factory.GenerateArtistes(nbArtistes) : [],
                Titres = nbTitres > 0 ? this.factory.GenerateTitres(nbTitres) : [],
            };

            return this.View(model);
        }
    }
}
