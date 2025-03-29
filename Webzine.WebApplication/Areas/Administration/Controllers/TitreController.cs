// <copyright file="TitreController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur de titre.
    /// </summary>
    [Area("Administration")]
    public class TitreController(ITitreRepository titreRepository, IStyleRepository styleRepository, IArtisteRepository artisteRepository) : Controller
    {
        /// <summary>
        /// Administration principale des titre.
        /// </summary>
        /// <returns>Retourne une vue, avec la liste des titres pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.View(titreRepository.FindAll().OrderBy(t => t.Artiste.Nom).ThenBy(t => t.Libelle).ToList());
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
                Artistes = artisteRepository.FindAll().OrderBy(a => a.Nom).ToList(),
                Styles = styleRepository.FindAll().OrderBy(s => s.Libelle).ToList(),
            };
            return this.View(model);
        }

        /// <summary>
        /// Traite la soumission du formulaire de création d'un titre.
        /// </summary>
        /// <param name="titre">Les données du titre créer.</param>
        /// <param name="selectedStyleIds">Les id des styles du titre.</param>
        /// <returns>Le résultat de la création du titre.</returns>
        [HttpPost]
        public IActionResult Create([FromForm] Titre titre, [FromForm] List<int> selectedStyleIds)
        {
            if (this.ModelState.IsValid)
            {
                titre.UrlEcoute ??= string.Empty;

                // Récupérer les styles complets à partir des IDs sélectionnés
                titre.Styles = styleRepository.FindAll()
                    .Where(s => selectedStyleIds.Contains(s.IdStyle))
                    .ToList();

                // Ajouter le titre au dépôt ou à la base de données
                titreRepository.Add(titre);

                return this.RedirectToAction(nameof(this.Index));
            }

            CreationAndEditionTitreModel model = new()
            {
                Artistes = artisteRepository.FindAll().OrderBy(a => a.Nom).ToList(),
                Styles = styleRepository.FindAll().OrderBy(s => s.Libelle).ToList(),
                Titre = titre,
            };

            return this.View(model);
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression d'un artiste.
        /// </summary>
        /// <param name="id">identifiant du titre à supprimer.</param>
        /// <returns>La vue de confirmation de suppression.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return this.View(titreRepository.Find(id));
        }

        /// <summary>
        /// Traite la demande de suppression d'un artiste.
        /// </summary>
        /// <param name="titre">L'identifiant de l'artiste à supprimer.</param>
        /// <returns>Le résultat de la suppression de l'artiste.</returns>
        [HttpPost]
        public IActionResult Delete([FromForm] Titre titre)
        {
            titreRepository.Delete(titre);
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
                Artistes = artisteRepository.FindAll().OrderBy(a => a.Nom).ToList(),
                Styles = styleRepository.FindAll().OrderBy(s => s.Libelle).ToList(),
                Titre = titreRepository.Find(id),
            };
            return this.View(model);
        }

        /// <summary>
        /// Traite la soumission du formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="titre">L'identifiant de l'artiste à éditer.</param>
        /// <param name="selectedStyleIds">Les id des styles du titre.</param>
        /// <returns>Le résultat de l'édition de l'artiste.</returns>
        [HttpPost]
        public IActionResult Edit([FromForm] Titre titre, [FromForm] List<int> selectedStyleIds)
        {
            if (this.ModelState.IsValid)
            {
                titre.UrlEcoute ??= string.Empty;

                // Récupérer les styles complets à partir des IDs sélectionnés
                titre.Styles = styleRepository.FindAll()
                    .Where(s => selectedStyleIds.Contains(s.IdStyle))
                    .ToList();

                // Ajouter le titre au dépôt ou à la base de données
                titreRepository.Update(titre);

                return this.RedirectToAction(nameof(this.Index));
            }

            CreationAndEditionTitreModel model = new()
            {
                Artistes = artisteRepository.FindAll().OrderBy(a => a.Nom).ToList(),
                Styles = styleRepository.FindAll().OrderBy(s => s.Libelle).ToList(),
                Titre = titre,
            };

            return this.View(model);
        }
    }
}
