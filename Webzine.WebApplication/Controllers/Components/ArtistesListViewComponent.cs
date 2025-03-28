// <copyright file="ArtistesListViewComponent.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers.Components
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;

    /// <summary>
    /// ViewComponent pour afficher une liste de artistes.
    /// </summary>
    public class ArtistesListViewComponent : ViewComponent
    {
        /// <summary>
        /// Invoque le ViewComponent.
        /// </summary>
        /// <param name="artistes">Liste des artistes à afficher.</param>
        /// <returns>La vue avec la liste des artistes.</returns>
        public IViewComponentResult Invoke(List<Artiste> artistes)
        {
            return this.View(artistes);
        }
    }
}
