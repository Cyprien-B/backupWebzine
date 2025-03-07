// <copyright file="ArtisteViewModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Webzine.Entity;

    /// <summary>
    /// ViewModel pour l'entité Artiste.
    /// </summary>
    public class ArtisteViewModel
    {
        /// <summary>
        /// Représente un artiste avec ses propriétés.
        /// </summary>
        public class Artiste
        {
            /// <summary>
            /// Obtient ou définit l'identifiant unique de l'artiste.
            /// </summary>
            [Key]
            public int IdArtiste { get; set; }

            /// <summary>
            /// Obtient ou définit le nom de l'artiste.
            /// </summary>
            public required string Nom { get; set; }

            /// <summary>
            /// Obtient ou définit la biographie de l'artiste.
            /// </summary>
            public required string Biographie { get; set; }

            /// <summary>
            /// Obtient ou définit la collection des titres associés à cet artiste.
            /// </summary>
            public required ICollection<Titre> Titres { get; set; }

            /// <summary>
            /// Obtient ou définit la collection des albums associés à cet artiste.
            /// </summary>
            public ICollection<Album> Albums { get; set; } = new List<Album>();
        }

        /// <summary>
        /// Représente un album avec ses propriétés.
        /// </summary>
        public class Album
        {
            /// <summary>
            /// Obtient ou définit le nom de l'album.
            /// </summary>
            public required string Nom { get; set; }

            /// <summary>
            /// Obtient ou définit l'URL de l'image de l'album.
            /// </summary>
            public required string ImageUrl { get; set; }

            /// <summary>
            /// Obtient ou définit la collection des titres associés à cet album.
            /// </summary>
            public ICollection<Titre> Titres { get; set; } = new List<Titre>();
        }
    }
}