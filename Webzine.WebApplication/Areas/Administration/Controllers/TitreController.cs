// <copyright file="TitreController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur de titre.
    /// </summary>
    [Area("Administration")]
    public class TitreController(IConfiguration configuration, ITitreRepository titreRepository, IStyleRepository styleRepository, IArtisteRepository artisteRepository) : Controller
    {
        /// <summary>
        /// Administration principale des titre.
        /// </summary>
        /// <param name="page">Numéro de la page des titres dans la pagination.</param>
        /// <returns>Retourne une vue, avec la liste des titres pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var paginationLimitTitre = configuration.GetValue<int>("App:Pages:Administration:NbTitresParPagination");

            AdministrationTitreModel administrationTitreModel = new AdministrationTitreModel()
            {
                PaginationActuelle = (uint)page,
                PaginationMax = (uint)Math.Ceiling((double)titreRepository.Count() / paginationLimitTitre),
                Titres = titreRepository.AdministrationFindTitres(page, paginationLimitTitre),
            };

            return this.View(administrationTitreModel);
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
                Artistes = artisteRepository.FindAll(),
                Styles = styleRepository.FindAll(),
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
            // Vérifie si un titre avec le même libellé existe déjà pour le même artiste
            bool titreExiste = titreRepository
                .LibelleToArtisteAny(titre);

            if (titreExiste)
            {
                // Ajoute une erreur au ModelState pour le champ "Libelle"
                this.ModelState.AddModelError("Titre.Libelle", "Un titre avec ce libellé existe déjà pour cet artiste.");
            }

            titre.Styles = styleRepository.FindStylesByIds(selectedStyleIds).ToList();

            if (this.ModelState.IsValid)
            {
                titre.UrlEcoute ??= string.Empty;

                // Ajouter le titre au dépôt ou à la base de données
                titreRepository.Add(titre);

                return this.RedirectToAction(nameof(this.Index));
            }

            CreationAndEditionTitreModel model = new()
            {
                Artistes = artisteRepository.FindAll(),
                Styles = styleRepository.FindAll(),
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
                Artistes = artisteRepository.FindAll(),
                Styles = styleRepository.FindAll(),
                Titre = titreRepository.Find(id)!,
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
            // Vérifie si un titre avec le même libellé existe déjà pour le même artiste
            bool titreExiste = titreRepository
                .LibelleToArtisteAny(titre);

            if (titreExiste)
            {
                // Ajoute une erreur au ModelState pour le champ "Libelle"
                this.ModelState.AddModelError("Titre.Libelle", "Un titre avec ce libellé existe déjà pour cet artiste.");
            }

            titre.Styles = styleRepository.FindStylesByIds(selectedStyleIds).ToList();

            if (this.ModelState.IsValid)
            {
                titre.UrlEcoute ??= string.Empty;

                // Ajouter le titre au dépôt ou à la base de données
                titreRepository.Update(titre);

                return this.RedirectToAction(nameof(this.Index));
            }

            CreationAndEditionTitreModel model = new()
            {
                Artistes = artisteRepository.FindAll(),
                Styles = styleRepository.FindAll(),
                Titre = titre,
            };

            return this.View(model);
        }
    }
}
