// <copyright file="AdministrationCommentairesModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    using Webzine.Entity;

    /// <summary>
    /// Model pour la vue des Commentaires.
    /// </summary>
    public class AdministrationCommentairesModel
    {
        /// <summary>
        /// Obtient ou définit la liste des Commentaires existants.
        /// </summary>
        public List<Commentaire> Commentaires { get; set; } = [];

        /// <summary>
        /// Obtient ou définit un commentaire pour les displayfor.
        /// </summary>
        public Commentaire Commentaire { get; set; } = new();
    }
}
