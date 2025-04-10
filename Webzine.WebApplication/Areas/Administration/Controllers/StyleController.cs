// <copyright file="StyleController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur de style.
    /// </summary>
    [Area("Administration")]
    public class StyleController(IStyleRepository styleRepository, IConfiguration configuration) : Controller
    {
        /// <summary>
        /// Administration principale des styles.
        /// </summary>
        /// <param name="page">Numéro de la page des styles dans la pagination.</param>
        /// <returns>Retourne une vue, avec la liste des styles pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var paginationLimitStyle = configuration.GetValue<int>("App:Pages:Administration:NbStylesParPagination");
            var administrationStyleModel = new AdministrationStyleModel()
            {
                PaginationActuelle = (uint)page,
                PaginationMax = (uint)Math.Ceiling((double)styleRepository.Count() / paginationLimitStyle),
                Styles = styleRepository.AdministrationFindStyles(page, paginationLimitStyle),
            };
            return this.View(administrationStyleModel);
        }

        /// <summary>
        /// Affiche le formulaire de création d'un artiste.
        /// </summary>
        /// <returns>La vue pour ajouter un artiste.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return this.View(new Style());
        }

        /// <summary>
        /// Traite la soumission du formulaire de création d'un artiste.
        /// </summary>
        /// <param name="style">Les données de l'artiste à créer.</param>
        /// <returns>Le résultat de la création de l'artiste.</returns>
        [HttpPost]
        public IActionResult Create([FromForm] Style style)
        {
            // Vérifie si le libellé existe déjà dans la base de données
            var styleExiste = styleRepository.LibelleAny(style);

            if (styleExiste)
            {
                // Ajoute une erreur au ModelState pour le champ "Libelle"
                this.ModelState.AddModelError("Libelle", "Le libellé de style existe déjà.");
            }

            if (this.ModelState.IsValid)
            {
                styleRepository.Add(style);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(style);
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression d'un artiste.
        /// </summary>
        /// <param name="id">identifiant du style à supprimer.</param>
        /// <returns>La vue de confirmation de suppression.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return this.View(styleRepository.Find(id));
        }

        /// <summary>
        /// Traite la demande de suppression d'un artiste.
        /// </summary>
        /// <param name="style">L'identifiant de l'artiste à supprimer.</param>
        /// <returns>Le résultat de la suppression de l'artiste.</returns>
        [HttpPost]
        public IActionResult Delete([FromForm] Style style)
        {
            styleRepository.Delete(style);

            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// Affiche le formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="id">Identifiant du style à modifier.</param>
        /// <returns>La vue pour éditer un artiste.</returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return this.View(styleRepository.Find(id));
        }

        /// <summary>
        /// Traite la soumission du formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="style">L'identifiant de l'artiste à éditer.</param>
        /// <returns>Le résultat de l'édition de l'artiste.</returns>
        [HttpPost]
        public IActionResult Edit([FromForm] Style style)
        {
            if (styleRepository.Find(style.IdStyle) == null)
            {
                this.ModelState.AddModelError("Libelle", "L'identifiant du style ne correspond à aucun style. Ne modifiez pas le code source SVP.");
            }
            else
            {
                // Vérifie si le libellé existe déjà dans la base de données
                var styleExiste = styleRepository.LibelleAny(style) && styleRepository.Find(style.IdStyle)!.Libelle != style.Libelle;

                if (styleExiste)
                {
                    // Ajoute une erreur au ModelState pour le champ "Libelle"
                    this.ModelState.AddModelError("Libelle", "Le libellé de style existe déjà.");
                }
            }

            if (this.ModelState.IsValid)
            {
                styleRepository.Update(style);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(style);
        }
    }
}
