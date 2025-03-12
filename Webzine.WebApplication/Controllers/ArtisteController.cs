// <copyright file="ArtisteController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Contrôleur pour gérer les actions liées aux artistes.
    /// </summary>
    public class ArtisteController : Controller
    {
        /// <summary>
        /// Affiche la page d'un artiste spécifique.
        /// </summary>
        /// <param name="artiste">Le nom de l'artiste à afficher.</param>
        /// <returns>La vue de l'artiste.</returns>
        public IActionResult Index(string nomArtiste)
        {
            var data = new Factory();
            var fakeArtiste = data.GenerateArtiste();

            var viewModel = new ArtisteViewModel.Artiste
            {
                IdArtiste = fakeArtiste.IdArtiste,
                Nom = fakeArtiste.Nom,
                Biographie = fakeArtiste.Biographie,
                Titres = fakeArtiste.Titres,
            };

            // Regrouper les titres par album
            var albums = fakeArtiste.Titres
                .GroupBy(t => t.Album)
                .Select(g => new ArtisteViewModel.Album
                {
                    Nom = g.Key,
                    ImageUrl = g.First().UrlJaquette,
                    Titres = g.ToList(),
                })
                .ToList();

            viewModel.Albums = albums;

            return this.View(viewModel);
        }
    }
}