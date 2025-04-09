// <copyright file="CommentaireController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur de commentaire.
    /// </summary>
    [Area("Administration")]
    public class CommentaireController(ICommentaireRepository commentaireRepository, IConfiguration configuration) : Controller
    {
        /// <summary>
        /// Administration principale des commentaires.
        /// </summary>
        /// <param name="page">Numéro de la page des commentaires dans la pagination.</param>
        /// <returns>Retourne une vue, avec la liste des commentaires pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var paginationLimitCommentaire = configuration.GetValue<int>("App:Pages:Administration:NbCommentairesParPagination");

            AdministrationCommentaireModel administrationCommentaireModel = new()
            {
                PaginationActuelle = (uint)page,
                PaginationMax = (uint)Math.Ceiling((double)commentaireRepository.Count() / paginationLimitCommentaire),
                Commentaires = commentaireRepository.AdministrationFindCommentaires(page, paginationLimitCommentaire),
            };

            return this.View(administrationCommentaireModel);
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
