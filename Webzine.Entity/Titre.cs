// <copyright file="Titre.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    /// <summary>
    /// Entite de titre.
    /// </summary>
    public class Titre
    {
        /// <summary>
        /// Obtient ou définit un identifiant unique au titre.
        /// </summary>
        [Key]
        public int IdTitre { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit l'identifiant de l'Artiste qui a composé le titre.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "L'artiste est requis")]
        public int IdArtiste { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit l'Artiste qui a composé le titre.
        /// </summary>
        [ValidateNever]
        public Artiste Artiste { get; set; } = new();

        /// <summary>
        /// Obtient ou définit le libellé du titre.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [MinLength(1, ErrorMessage = "La longueur minimale autorisée pour le libellé est de 1 caractères.")]
        [MaxLength(200, ErrorMessage = "La longueur maximale autorisée pour le libellé est de 200 caractères.")]
        [Display(Name = "Titre")]
        public string Libelle { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la description de la chronique du titre.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [MinLength(10, ErrorMessage = "La longueur minimale autorisée pour une chronique est de 10 caractères.")]
        [MaxLength(4000, ErrorMessage = "La longueur maximale autorisée pour une chronique est de 4000 caractères.")]
        public string Chronique { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la date de création du titre.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [Display(Name = "Date de création")]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        /// <summary>
        /// Obtient ou définit la durée en secondes du titre.
        /// Ne doit pas être négatif.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "La durée doit être un nombre positif non nul inférieur à 2147483647.")]
        [Display(Name = "Durée en secondes")]
        public uint Duree { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit la date de sortie d'un titre.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [Display(Name = "Date de sortie")]
        public DateTime DateSortie { get; set; } = DateTime.Now;

        /// <summary>
        /// Obtient ou définit l'URL de la jaquette de l'album.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [MaxLength(250, ErrorMessage = "La longueur maximale autorisée pour l'URL de la jaquette est de 250 caractères.")]
        [Display(Name = "Jaquette de l'album")]
        public string UrlJaquette { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit l'URL d'écoute du titre.
        /// </summary>
        [MaxLength(250, ErrorMessage = "La longueur maximale autorisée pour l'URL d'écoute est de 250 caractères.")]
        [MinLength(13, ErrorMessage = "La longueur minimale autorisée pour l'URL d'écoute est de 13 caractères.")]
        [Display(Name = "URL d'écoute")]
        public string UrlEcoute { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit le nombre de fois que le titre a été écouté.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [Display(Name = "Nombre de lectures")]
        public uint NbLectures { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le nombre de "likes" reçus par le titre.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        [Display(Name = "Nombre de likes")]
        public uint NbLikes { get; set; } = 0;

        /// <summary>
        /// Obtient ou définit le nom de l'album auquel appartient le titre.
        /// </summary>
        [Required(ErrorMessage = "Ce champ est requis")]
        public string Album { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la collection des commentaires associés à ce titre.
        /// </summary>
        [ValidateNever]
        public ICollection<Commentaire> Commentaires { get; set; } = [];

        /// <summary>
        /// Obtient ou définit une liste de styles lié au titre automatiquement représentant le many to many.
        /// </summary>
        public IEnumerable<Style> Styles { get; set; } = [];
    }
}
