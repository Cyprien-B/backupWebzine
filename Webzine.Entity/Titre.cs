// <copyright file="Titre.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Entite de titre.
    /// </summary>
    public class Titre
    {
        /// <summary>
        /// Obtient ou définit un identifiant unique au titre.
        /// </summary>
        public int IdTitre { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant de l'Artiste qui a composé le titre.
        /// </summary>
        public int IdArtiste { get; set; }

        /// <summary>
        /// Obtient ou définit l'Artiste qui a composé le titre.
        /// </summary>
        public required Artiste Artiste { get; set; }

        /// <summary>
        /// Obtient ou définit le libellé du titre.
        /// </summary>
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        [Display(Name = "Titre")]
        public required string Libelle { get; set; }

        /// <summary>
        /// Obtient ou définit la description de la chronique du titre.
        /// </summary>
        [Required]
        [MinLength(10)]
        [MaxLength(4000)]
        public required string Chronique { get; set; }

        /// <summary>
        /// Obtient ou définit la date de création du titre.
        /// </summary>
        [Required]
        [Display(Name = "Date de création")]
        public DateTime DateCreation { get; set; }

        /// <summary>
        /// Obtient ou définit la durée en secondes du titre.
        /// Ne doit pas être négatif.
        /// </summary>
        [Display(Name = "Durée en secondes")]
        public uint Duree { get; set; }

        /// <summary>
        /// Obtient ou définit la date de sortie d'un titre.
        /// </summary>
        [Required]
        [Display(Name = "Date de sortie")]
        public DateTime DateSortie { get; set; }

        /// <summary>
        /// Obtient ou définit l'URL de la jaquette de l'album.
        /// </summary>
        [Required]
        [MaxLength(250)]
        [Display(Name = "Jaquette de l'album")]
        public required string UrlJaquette { get; set; }

        /// <summary>
        /// Obtient ou définit l'URL d'écoute du titre.
        /// </summary>
        [MaxLength(250)]
        [MinLength(13)]
        [Display(Name = "URL d'écoute")]
        public required string UrlEcoute { get; set; }

        /// <summary>
        /// Obtient ou définit le nombre de fois que le titre a été écouté.
        /// </summary>
        [Required]
        [Display(Name = "Nombre de lectures")]
        public uint NbLectures { get; set; }

        /// <summary>
        /// Obtient ou définit le nombre de "likes" reçus par le titre.
        /// </summary>
        [Required]
        [Display(Name = "Nombre de likes")]
        public uint NbLikes { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de l'album auquel appartient le titre.
        /// </summary>
        [Required]
        public required string Album { get; set; }

        /// <summary>
        /// Obtient ou définit la collection des commentaires associés à ce titre.
        /// </summary>
        public required ICollection<Commentaire> Commentaires { get; set; }
    }
}
