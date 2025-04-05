// <copyright file="ArtisteController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;
    using Webzine.Repository.Db;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur des artistes.
    /// </summary>
    [Area("Administration")]
    public class ArtisteController(IArtisteRepository artisteRepository, IConfiguration configuration) : Controller
    {
        /// <summary>
        /// Administration principale des artistes.
        /// </summary>
        /// <param name="page">Numéro de la page des artistes dans la pagination.</param>
        /// <returns>Retourne une vue, avec la liste des artistes pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var paginationLimitArtiste = configuration.GetValue<int>("App:Pages:Administration:NbArtistesParPagination");

            AdministrationArtisteModel administrationArtisteModel = new AdministrationArtisteModel()
            {
                PaginationActuelle = (uint)page,
                PaginationMax = (uint)Math.Ceiling((double)artisteRepository.Count() / paginationLimitArtiste),
                Artistes = artisteRepository.AdministrationFindArtistes(page, paginationLimitArtiste),
            };

            return this.View(administrationArtisteModel);
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
        /// <param name="artiste">Les données de l'artiste à créer.</param>
        /// <returns>Le résultat de la création de l'artiste.</returns>
        [HttpPost]
        public IActionResult Create([FromForm] Artiste artiste)
        {
            // Vérifie si le libellé existe déjà dans la base de données
            // TODO: Modifier l'emplacement et/ou créer un service ou une méthode de repository qui vient vérifier l'existence de l'artiste.
            bool artisteExiste = artisteRepository.NomAny(artiste);

            if (artisteExiste)
            {
                // Ajoute une erreur au ModelState pour le champ "Libelle"
                this.ModelState.AddModelError("Nom", "Cette artiste existe déjà.");
            }

            if (this.ModelState.IsValid)
            {
                artiste.Biographie ??= string.Empty;
                artisteRepository.Add(artiste);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(artiste);
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression d'un artiste.
        /// </summary>
        /// <param name="id">identifiant de l'artiste à supprimer.</param>
        /// <returns>La vue de confirmation de suppression.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return this.View(artisteRepository.Find(id));
        }

        /// <summary>
        /// Traite la demande de suppression d'un artiste.
        /// </summary>
        /// <param name="artiste">L'identifiant de l'artiste à supprimer.</param>
        /// <returns>Le résultat de la suppression de l'artiste.</returns>
        [HttpPost]
        public IActionResult Delete([FromForm] Artiste artiste)
        {
            artisteRepository.Delete(artiste);
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
            return this.View(artisteRepository.Find(id));
        }

        /// <summary>
        /// Traite la soumission du formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="artiste">L'identifiant de l'artiste à éditer.</param>
        /// <returns>Le résultat de l'édition de l'artiste.</returns>
        [HttpPost]
        public IActionResult Edit([FromForm] Artiste artiste)
        {
            // Vérifie si le libellé existe déjà dans la base de données
            // TODO: Modifier l'emplacement et/ou créer un service ou une méthode de repository qui vient vérifier l'existence de l'artiste.
            bool artisteExiste = artisteRepository.NomAny(artiste);

            if (artisteExiste)
            {
                // Ajoute une erreur au ModelState pour le champ "Libelle"
                this.ModelState.AddModelError("Nom", "Cette artiste existe déjà.");
            }

            if (this.ModelState.IsValid)
            {
                artiste.Biographie ??= string.Empty;
                artisteRepository.Update(artiste);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(artiste);
        }
    }
}