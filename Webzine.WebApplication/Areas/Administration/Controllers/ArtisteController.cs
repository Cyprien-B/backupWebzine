// <copyright file="ArtisteController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur des artistes.
    /// </summary>
    [Area("Administration")]
    public class ArtisteController : Controller
    {
        /// <summary>
        /// Obtient ou définit un générateur de fausse données.
        /// </summary>
        public DataGenerator DataGenerator { get; set; } = new();

        /// <summary>
        /// Administration principale des artistes.
        /// </summary>
        /// <returns>Retourne une vue, avec la liste des artistes pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            List<Artiste> artistes = [];
            AdministrationArtistesModel model = new()
            {
                Artistes = this.DataGenerator.GenerateArtistes(50),
            };
            return this.View(model);
        }

        /// <summary>
        /// Affiche le formulaire de création d'un artiste.
        /// </summary>
        /// <returns>La vue pour ajouter un artiste.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return this.View(new Artiste());
        }

        /// <summary>
        /// Traite la soumission du formulaire de création d'un artiste.
        /// </summary>
        /// <param name="result">Les données de l'artiste à créer.</param>
        /// <returns>Le résultat de la création de l'artiste.</returns>
        [HttpPost]
        public IActionResult Create([FromForm] object result)
        {
            return this.Ok("Not implemented");
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression d'un artiste.
        /// </summary>
        /// <param name="id">identifiant de l'artiste à supprimer.</param>
        /// <returns>La vue de confirmation de suppression.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return this.View(this.DataGenerator.GenerateArtiste().IdArtiste = id);
        }

        /// <summary>
        /// Traite la demande de suppression d'un artiste.
        /// </summary>
        /// <param name="result">L'identifiant de l'artiste à supprimer.</param>
        /// <returns>Le résultat de la suppression de l'artiste.</returns>
        [HttpPost]
        public IActionResult Delete([FromForm] Artiste result)
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// Affiche le formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="id">identifiant de l'artiste à modifier.</param>
        /// <returns>La vue pour éditer un artiste.</returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Artiste artiste = new()
            {
                Nom = "Daniel Balavoine",
                IdArtiste = id,
                Biographie = "Chanteur des années 90, est un chanteur avec un large timbre de voix, démontré avec SOS d'un terrien en détresse",
            };
            return this.View(artiste);
        }

        /// <summary>
        /// Traite la soumission du formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="result">L'identifiant de l'artiste à éditer.</param>
        /// <returns>Le résultat de l'édition de l'artiste.</returns>
        [HttpPost]
        public IActionResult Edit([FromForm] Artiste result)
        {
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
