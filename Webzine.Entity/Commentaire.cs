// <copyright file="Commentaire.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    /// <summary>
    /// Entite des commentaire.
    /// </summary>
    public class Commentaire
    {
        /// <summary>
        /// Obtient ou définit un Identifiant unique au commentaire.
        /// </summary>
        [Key]
        public int IdCommentaire { get; set; }

        /// <summary>
        /// Obtient ou définit le contenu d'un commentaire.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [MaxLength(1000, ErrorMessage = "La longueur maximale autorisée pour le contenu du commentaire est de 1000 caractères.")]
        [MinLength(10, ErrorMessage = "La longueur minimale autorisée pour le contenu du commentaire est de 10 caractères.")]
        [Display(Name = "Commentaire")]
        public string Contenu { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit l'auteur qui a écrit le message.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [MinLength(2, ErrorMessage = "La longueur minimale autorisée pour l'auteur du commentaire est de 2 caractères.")]
        [MaxLength(30, ErrorMessage = "La longueur maximale autorisée pour l'auteur du commentaire est de 30 caractères.")]
        [Display(Name = "Nom")]
        public string Auteur { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la date de publication du commentaire.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [Display(Name = "Date de création")]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        /// <summary>
        /// Obtient ou définit l'identifiant du titre auquel le commentaire est associé.
        /// </summary>
        public int IdTitre { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le titre auquel le commentaire est associé.
        /// </summary>
        [ValidateNever]
        public Titre Titre { get; set; } = new();
    }
}
