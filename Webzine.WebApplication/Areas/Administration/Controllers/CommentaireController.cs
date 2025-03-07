// <copyright file="CommentaireController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur de commentaire.
    /// </summary>
    [Area("Administration")]
    public class CommentaireController : Controller
    {
        /// <summary>
        /// Administration principale des commentaires.
        /// </summary>
        /// <returns>Retourne une vue, avec la liste des commentaires pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            AdministrationCommentairesModel model = new()
            {
                Commentaires = [new() { Auteur = "Jack", Contenu = "Trop cool", DateCreation = DateTime.Now, IdCommentaire = 1, IdTitre = 1, Titre = new() { Libelle = "Vivre Ailleur" } }],
            };
            return this.View(model);
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression d'un commentaire.
        /// </summary>
        /// <param name="id">identifiant du commentaire à supprimer.</param>
        /// <returns>La vue de confirmation de suppression.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Commentaire commentaire = new()
            {
                Auteur = "Jack", Contenu = "Trop cool", DateCreation = DateTime.Now, IdCommentaire = 1, IdTitre = 1, Titre = new() { Libelle = "Vivre Ailleur" },
            };
            return this.View(commentaire);
        }

        /// <summary>
        /// Traite la demande de suppression d'un commentaire.
        /// </summary>
        /// <param name="result">L'identifiant du commentaire à supprimer.</param>
        /// <returns>Le résultat de la suppression d'un commentaire.</returns>
        [HttpPost]
        public IActionResult Delete([FromForm] Commentaire result)
        {
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
