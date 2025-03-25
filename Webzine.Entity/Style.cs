// <copyright file="Style.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity
{
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
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [Display(Name = "Libellé")]
        public string Libelle { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit une liste de titres lié au style automatiquement représentant le many to many.
        /// </summary>
        public List<Titre> Titres { get; set; } = [];
    }
}
