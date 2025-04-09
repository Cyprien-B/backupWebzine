// <copyright file="HomeController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Controlleur principal de base.
    /// </summary>
    public class HomeController(IConfiguration configuration, ITitreRepository titreRepository) : Controller
    {
        /// <summary>
        /// Est la vue de la page d'accueil.
        /// </summary>
        /// <param name="page">Numéro de pagination pour les titres les plus chroniqué.</param>
        /// <returns>Retourne la vue la page principale.</returns>
        [HttpGet]
        public IActionResult Index(uint page = 1)
        {
            int nbTitresChroniques = configuration.GetValue<int>("App:Pages:Home:NbTitresChroniquesParPaginations");
            int nbTitresPopulaires = configuration.GetValue<int>("App:Pages:Home:NbTitresPopulaires");
            int descriptionMax = configuration.GetValue<int>("App:Pages:Home:DescriptionMax");

            HomeModel model = new()
            {
                PaginationMax = (uint)Math.Ceiling((double)titreRepository.Count() / nbTitresChroniques),
                TitresRecemmentsChroniques = titreRepository.FindTitres((int)page, nbTitresChroniques).ToList(),
                TitresPopulaires = titreRepository.FindTitresPopulaires(nbTitresPopulaires),
                CaracteresChroniqueMax = (uint)descriptionMax,
                PaginationActuelle = page,
            };

            return this.View(model);
        }

        /// <summary>
        /// Gestion des erreur de pages.
        /// </summary>
        /// <returns>Retourne quand la page est mauvaise.</returns>
        [HttpGet]
        public IActionResult NotFound404()
        {
            return this.View();
        }

        /// <summary>
        /// Gestion des erreurs exception.
        /// </summary>
        /// <returns>Retourne la page avec les information sur les exception et permettant de revenir à l'accueil.</returns>
        [HttpGet]
        public IActionResult Error()
        {
            var exceptionFeature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionFeature?.Error != null)
            {
                return this.View(exceptionFeature.Error); // Passe l'exception au modèle de la vue Error.cshtml
            }

            return this.View(); // Affiche la vue même si aucune exception n'est disponible
        }
    }
}
