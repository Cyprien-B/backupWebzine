// <copyright file="StylesSidebarViewComponent.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers.Components
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;

    /// <summary>
    /// View component pour la liste des styles.
    /// </summary>
    public class StylesSidebarViewComponent : ViewComponent
    {
        /// <summary>
        /// Injecte les styles à la composante de vue.
        /// </summary>
        /// <returns>Une composante de vue.</returns>
        public IViewComponentResult Invoke()
        {
            List<Style> styles = [new Style { IdStyle = 0, Libelle = "Jazz", }, new Style { IdStyle = 1, Libelle = "Classique", }]; // Obtenez la liste des styles
            return this.View(styles);
        }
    }
}
