// <copyright file="CommentaireController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur de commentaire.
    /// </summary>
    [Area("Administration")]
    public class CommentaireController(ICommentaireRepository commentaireRepository) : Controller
    {
        /// <summary>
        /// Administration principale des commentaires.
        /// </summary>
        /// <returns>Retourne une vue, avec la liste des commentaires pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return this.View(commentaireRepository.FindAll().OrderBy(c => c.DateCreation).ToList());
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression d'un commentaire.
        /// </summary>
        /// <param name="id">identifiant du commentaire à supprimer.</param>
        /// <returns>La vue de confirmation de suppression.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return this.View(commentaireRepository.Find(id));
        }

        /// <summary>
        /// Traite la demande de suppression d'un commentaire.
        /// </summary>
        /// <param name="commentaire">L'identifiant du commentaire à supprimer.</param>
        /// <returns>Le résultat de la suppression d'un commentaire.</returns>
        [HttpPost]
        public IActionResult Delete([FromForm] Commentaire commentaire)
        {
            commentaireRepository.Delete(commentaire);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
