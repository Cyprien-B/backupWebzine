// <copyright file="StylesSidebarViewComponent.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers.Components
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Repository.Contracts;

    /// <summary>
    /// View component pour la liste des styles.
    /// </summary>
    public class StylesSidebarViewComponent(IStyleRepository styleRepository) : ViewComponent
    {
        /// <summary>
        /// Injecte les styles à la composante de vue.
        /// </summary>
        /// <returns>Une composante de vue.</returns>
        public IViewComponentResult Invoke()
        {
            var styles = styleRepository.FindAll()
                .OrderBy(s => s.Libelle)
                .ToList();
            return this.View(styles);
        }
    }
}
