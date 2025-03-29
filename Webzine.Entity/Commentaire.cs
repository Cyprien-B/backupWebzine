// <copyright file="Commentaire.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity
{
    using System;
    using System.ComponentModel.DataAnnotations;
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
        [Required]
        [MaxLength(1000)]
        [MinLength(10)]
        [Display(Name = "Commentaire")]
        public string Contenu { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit l'auteur qui a écrit le message.
        /// </summary>
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [Display(Name = "Nom")]
        public string Auteur { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la date de publication du commentaire.
        /// </summary>
        [Required]
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
