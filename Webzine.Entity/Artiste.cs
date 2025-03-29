// <copyright file="Artiste.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entite artiste.
    /// </summary>
    public class Artiste
    {
        /// <summary>
        /// Obtient ou définit l'identifiant unique de l'artiste.
        /// Cet identifiant sert de clé primaire dans la base de données.
        /// </summary>
        [Key]
        public int IdArtiste { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le nom de l'artiste.
        /// Le nom est obligatoire et doit contenir entre 2 et 50 caractères.
        /// </summary>
        [Required(ErrorMessage = "Le nom de l'artiste est obligatoire.")]
        [MaxLength(50, ErrorMessage = "La longueur maximale autorisée pour le nom d'artiste est de 50 caractères.")]
        [MinLength(2, ErrorMessage = "La longueur minimale requise pour le nom d'artiste est de 2 caractères.")]
        [Display(Name = "Nom de l'artiste")]
        public string Nom { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la biographie de l'artiste.
        /// Ce champ est optionnel et peut contenir une description détaillée de l'artiste.
        /// </summary>
        public string Biographie { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la collection des titres associés à cet artiste.
        /// Cette propriété représente une relation one-to-many entre Artiste et Titre.
        /// </summary>
        public ICollection<Titre> Titres { get; set; } = [];
    }
}
