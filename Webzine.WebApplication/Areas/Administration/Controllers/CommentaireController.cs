// <copyright file="CommentaireController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
            return this.Ok("Not implemented commentaire");
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression d'un commentaire.
        /// </summary>
        /// <param name="id">identifiant du commentaire à supprimer.</param>
        /// <returns>La vue de confirmation de suppression.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return this.View();
        }

        /// <summary>
        /// Traite la demande de suppression d'un commentaire.
        /// </summary>
        /// <param name="result">L'identifiant du commentaire à supprimer.</param>
        /// <returns>Le résultat de la suppression d'un commentaire.</returns>
        [HttpPost]
        public IActionResult Delete([FromForm] object result)
        {
            return this.Ok("Not implemented");
        }
    }
}
