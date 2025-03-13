// <copyright file="TitresListViewComponent.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers.Components
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;

    /// <summary>
    /// ViewComponent pour afficher une liste de titres.
    /// </summary>
    public class TitresListViewComponent : ViewComponent
    {
        /// <summary>
        /// Invoque le ViewComponent.
        /// </summary>
        /// <param name="titres">Liste des titres à afficher.</param>
        /// <returns>La vue avec la liste des titres.</returns>
        public IViewComponentResult Invoke(List<Titre> titres)
        {
            return this.View(titres);
        }
    }
}
