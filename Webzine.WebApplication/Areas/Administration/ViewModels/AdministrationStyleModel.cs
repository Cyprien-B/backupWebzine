// <copyright file="AdministrationStyleModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.ViewModels
{
    using Webzine.Entity;

    /// <summary>
    /// View model pour l'administration d'un titre.
    /// </summary>
    public class AdministrationStyleModel
    {
        /// <summary>
        /// Obtient ou définit la page actuelle dans la pagination.
        /// </summary>
        public uint PaginationActuelle { get; set; } = 1;

        /// <summary>
        /// Obtient ou définit la pagination maximale ou il n'y a plus de titres.
        /// </summary>
        public uint PaginationMax { get; set; } = 1;

        /// <summary>
        /// Obtient ou définit la liste de titres paginées.
        /// </summary>
        public IEnumerable<Style> Styles { get; set; } = [];
    }
}
