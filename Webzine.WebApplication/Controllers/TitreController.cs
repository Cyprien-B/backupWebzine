﻿// <copyright file="TitreController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Business.Contracts;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Contrôleur des titres.
    /// </summary>
    public class TitreController(ITitreService titreService, ITitreRepository titreRepository, IStyleRepository styleRepository, ICommentaireRepository commentaireRepository) : Controller
    {
        /// <summary>
        /// Titre en fonction de l'id.
        /// </summary>
        /// <param name="id">Identifiant du titre.</param>
        /// <returns>Renvois la vue du titre en fonction de l'id.</returns>
        [HttpGet]
        public IActionResult Index(int id)
        {
            var titreForIncrement = titreRepository.Find(id);
            titreService.IncrementNbLectures(titreForIncrement!);

            // ViewModel necessaire pour la soumission d'un commmentaire non valide
            var model = new TitreModel() { Titre = titreForIncrement! };

            return this.View(model);
        }

        /// <summary>
        /// Affiche les titres d'un style spécifique.
        /// </summary>
        /// <param name="nomStyle">Le nom du style.</param>
        /// <returns>La vue avec les titres du style.</returns>
        [HttpGet]
        public IActionResult Style(string nomStyle)
        {
            // TODO: J'aime pas trop le code, il faudrait trouver un moyen de faire mieux.
            if (string.IsNullOrWhiteSpace(nomStyle))
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var style = styleRepository.FindAll().First(s => s.Libelle == nomStyle);
            style.Libelle = nomStyle;

            var model = new StyleTitresModel()
            {
                Style = style,
                Titres = titreRepository.SearchByStyle(nomStyle).ToList(),
            };
            return this.View(model);
        }

        /// <summary>
        /// Incrementation de 1 pour les likes.
        /// </summary>
        /// <param name="idTitre">Identifie le titre à liker.</param>
        /// <returns>Un retour indiquant que tout s'est bien passé.</returns>
        [HttpPost]
        public IActionResult Liker([FromForm] int idTitre)
        {
            titreService.IncrementNbLikes(new() { IdTitre = idTitre });
            return this.RedirectToAction(nameof(this.Index), new { id = idTitre });
        }

        /// <summary>
        /// Ajoute un commentaire au titre.
        /// </summary>
        /// <param name="commentaire">Identifie le titre auquel le commentaire est à ajouter et son contenu.</param>
        /// <returns>Un retour positif du bon retour de la vue.</returns>
        [HttpPost]
        public IActionResult Commenter([FromForm] Commentaire commentaire)
        {
            if (this.ModelState.IsValid)
            {
                commentaireRepository.Add(commentaire);
                return this.RedirectToAction(nameof(this.Index), new { id = commentaire.IdTitre });
            }

            var titreModel = new TitreModel()
            {
                Titre = titreRepository.Find(commentaire.IdTitre)!,
                Commentaire = commentaire,
            };

            return this.View("Index", titreModel);
        }
    }
}