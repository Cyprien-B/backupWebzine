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
        public int IdStyle { get; set; }

        /// <summary>
        /// Obtient ou définit le libellé d'un style (rap, jazz, classique...).
        /// </summary>
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [Display(Name = "Libellé")]
        public required string Libelle { get; set; }
    }
}
