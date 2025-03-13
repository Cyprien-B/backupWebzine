// <copyright file="TitreController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur de titre.
    /// </summary>
    [Area("Administration")]
    public class TitreController : Controller
    {
        /// <summary>
        /// Obtient ou définit un générateur de fausse données.
        /// </summary>
        public Factory Factory { get; set; } = new();

        /// <summary>
        /// Administration principale des titre.
        /// </summary>
        /// <returns>Retourne une vue, avec la liste des titres pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            AdministrationTitresModel model = new()
            {
                Titres = this.Factory.GenerateTitres(40),
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
            CreationAndEditionTitreModel model = new()
            {
                Artistes = this.Factory.GenerateArtistes(40).OrderBy(a => a.Nom).ToList(),
                Styles = this.Factory.GenerateStyles(30).DistinctBy(s => s.Libelle).OrderBy(s => s.Libelle).ToList(),
            };
            return this.View(model);
        }

        /// <summary>
        /// Traite la soumission du formulaire de création d'un artiste.
        /// </summary>
        /// <param name="result">Les données de l'artiste à créer.</param>
        /// <returns>Le résultat de la création de l'artiste.</returns>
        [HttpPost]
        public IActionResult Create([FromForm] Titre result)
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression d'un artiste.
        /// </summary>
        /// <param name="id">identifiant du titre à supprimer.</param>
        /// <returns>La vue de confirmation de suppression.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return this.View(this.Factory.GenerateTitre());
        }

        /// <summary>
        /// Traite la demande de suppression d'un artiste.
        /// </summary>
        /// <param name="result">L'identifiant de l'artiste à supprimer.</param>
        /// <returns>Le résultat de la suppression de l'artiste.</returns>
        [HttpPost]
        public IActionResult Delete([FromForm] Titre result)
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// Affiche le formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="id">identifiant du titre à modifier.</param>
        /// <returns>La vue pour éditer un artiste.</returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CreationAndEditionTitreModel model = new()
            {
                Artistes = this.Factory.GenerateArtistes(40),
                Styles = this.Factory.GenerateStyles(30).DistinctBy(s => s.Libelle).ToList(),
                Titre = this.Factory.GenerateTitre(),
            };
            return this.View(model);
        }

        /// <summary>
        /// Traite la soumission du formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="result">L'identifiant de l'artiste à éditer.</param>
        /// <returns>Le résultat de l'édition de l'artiste.</returns>
        [HttpPost]
        public IActionResult Edit([FromForm] Titre result)
        {
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
