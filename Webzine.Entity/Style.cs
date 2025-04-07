// <copyright file="Style.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entite de style.
    /// </summary>
    public class Style
    {
        /// <summary>
        /// Obtient ou définit l'identifiant unique d'un style.
        /// </summary>
        [Key]
        public int IdStyle { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le libellé d'un style (rap, jazz, classique...).
        /// </summary>
        [Required(ErrorMessage = "Le libellé du style est obligatoire.")]
        [MaxLength(50, ErrorMessage = "La longueur maximale autorisée pour le libellé est de 50 caractères.")]
        [MinLength(2, ErrorMessage = "La longueur minimale requise pour le libellé est de 2 caractères.")]
        [Display(Name = "Libellé")]
        public string Libelle { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit une liste de titres lié au style automatiquement représentant le many to many.
        /// </summary>
        public List<Titre> Titres { get; set; } = [];
    }
}
