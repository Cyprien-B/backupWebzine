// <copyright file="TitreController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Contrôleur des titres.
    /// </summary>
    public class TitreController : Controller
    {
        private readonly Factory factory;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="TitreController"/>.
        /// </summary>
        /// <param name="factory">Le générateur de données fictives.</param>
        public TitreController(Factory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Titre en fonction de l'id.
        /// </summary>
        /// <param name="id">Identifiant du titre.</param>
        /// <returns>Renvois la vue du titre en fonction de l'id.</returns>
        [HttpGet]
        public IActionResult Index(int id)
        {
            TitreModel model = new()
            {
                Titre = this.factory.GenerateTitre(),
            };
            model.Titre.IdTitre = id;
            return this.View(model);
        }

        /// <summary>
        /// Affiche les titres d'un style spécifique.
        /// </summary>
        /// <param name="nomStyle">Le nom du style.</param>
        /// <returns>La vue avec les titres du style.</returns>
        [HttpGet]
        [Route("titres/style/{nomStyle}")]
        public IActionResult Style(string nomStyle)
        {
            if (string.IsNullOrWhiteSpace(nomStyle))
            {
                return this.RedirectToAction("Index", "Home");
            }

            var model = new StyleTitresModel(this.dataGenerator, nomStyle);
            return this.View(model);
        }

        /// <summary>
        /// Incrementation de 1 pour les likes.
        /// </summary>
        /// <param name="result">Identifie le titre à liker.</param>
        /// <returns>Un retour indiquant que tout s'est bien passé.</returns>
        [HttpPost]
        public IActionResult Liker([FromForm] object result)
        {
            return this.Ok("Likes are incremented");
        }

        /// <summary>
        /// Ajoute un commentaire au titre.
        /// </summary>
        /// <param name="commentaire">Identifie le titre auquel le commentaire est à ajouter et son contenu.</param>
        /// <returns>Un retour positif du bon retour de la vue.</returns>
        [HttpPost]
        public IActionResult Commenter([FromForm] Commentaire commentaire)
        {
            return this.RedirectToAction(nameof(this.Index), new { id = commentaire.IdTitre });
        }
    }
}