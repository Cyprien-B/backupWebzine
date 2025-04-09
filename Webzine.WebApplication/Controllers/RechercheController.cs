// <copyright file="RechercheController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Contrôleur de recherche.
    /// </summary>
    public class RechercheController(ITitreRepository titreRepository, IArtisteRepository artisteRepository) : Controller
    {
        /// <summary>
        /// Gère la recherche et affiche les résultats.
        /// </summary>
        /// <param name="recherche">string de recherche.</param>
        /// <returns>Une vue avec les résultats de la recherche.</returns>
        [HttpPost]
        public IActionResult Index([FromForm] string recherche)
        {
            if (string.IsNullOrEmpty(recherche))
            {
                var refererUrl = this.Request.Headers.Referer.ToString();

                // Charger la vue active
                return this.Redirect(refererUrl); // Retourne la vue active avec les erreurs
            }

            RechercheModel model = new()
            {
                TermeRecherche = recherche,
                Artistes = artisteRepository.Search(recherche).ToList(),
                Titres = titreRepository.Search(recherche).ToList(),
            };

            return this.View(model);
        }
    }
}