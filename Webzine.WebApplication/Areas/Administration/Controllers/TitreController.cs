// <copyright file="TitreController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.WebApplication.Areas.Administration.ViewModels;

    /// <summary>
    /// Contrôleur de titre.
    /// </summary>
    [Area("Administration")]
    public class TitreController : Controller
    {
        /// <summary>
        /// Administration principale des titre.
        /// </summary>
        /// <returns>Retourne une vue, avec la liste des titres pouvant être modéré.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            int i = 2;
            List<Titre> list = new List<Titre>()
            {
                new Titre
                {
                    IdTitre = i,
                    IdArtiste = i,
                    Artiste = new Artiste()
                    {
                        Biographie = "Lorem ipsum literae para tuiro...",
                        Nom = $"Monsieur truc {i}",
                        IdArtiste = i,
                        Titres = [],
                    },
                    Libelle = $"Titre {i}",
                    Chronique = $"Chronique du titre {i}. Cette chronique est un exemple de texte pour le titre numéro {i}.",
                    DateCreation = DateTime.Now.AddDays(-i),
                    Duree = (uint)(180 + (i * 10)),
                    DateSortie = DateTime.Now.AddMonths(-i),
                    UrlJaquette = $"data:image/jpeg;base64,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",
                    UrlEcoute = $"https://example.com/ecoute{i}",
                    NbLectures = (uint)(1000 + (i * 100)),
                    NbLikes = (uint)(500 + (i * 50)),
                    Album = $"Album {i}",
                    Commentaires = new List<Commentaire>(),
                },
                new Titre
                {
                    IdTitre = ++i,
                    IdArtiste = i,
                    Artiste = new Artiste()
                    {
                        Biographie = "Lorem ipsum literae para tuiro...",
                        Nom = $"Monsieur truc {i}",
                        IdArtiste = i,
                        Titres = [],
                    },
                    Libelle = $"Titre {i}",
                    Chronique = $"Chronique du titre {i}. Cette chronique est un exemple de texte pour le titre numéro {i}.",
                    DateCreation = DateTime.Now.AddDays(-i),
                    Duree = (uint)(180 + (i * 10)),
                    DateSortie = DateTime.Now.AddMonths(-i),
                    UrlJaquette = $"data:image/jpeg;base64,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",
                    UrlEcoute = $"https://example.com/ecoute{i}",
                    NbLectures = (uint)(1000 + (i * 100)),
                    NbLikes = (uint)(500 + (i * 50)),
                    Album = $"Album {i}",
                    Commentaires = new List<Commentaire>(),
                },
            };

            AdministrationTitresModel model = new()
            {
                Titres = list,
            };
            return this.View(model);
        }

        /// <summary>
        /// Affiche le formulaire de création d'un artiste.
        /// </summary>
        /// <returns>La vue pour ajouter un artiste.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            CreationAndEditionTitreModel model = new()
            {
                Artistes = [new Artiste() { Nom = "Monsieur truc" },],
                Styles = [new Style() { Libelle = "Jazz" },],
            };
            return this.View(model);
        }

        /// <summary>
        /// Traite la soumission du formulaire de création d'un artiste.
        /// </summary>
        /// <param name="result">Les données de l'artiste à créer.</param>
        /// <returns>Le résultat de la création de l'artiste.</returns>
        [HttpPost]
        public IActionResult Create([FromForm] Titre result)
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// Affiche la page de confirmation de suppression d'un artiste.
        /// </summary>
        /// <param name="id">identifiant du titre à supprimer.</param>
        /// <returns>La vue de confirmation de suppression.</returns>
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Titre titre = new()
            {
                IdTitre = ++id,
                IdArtiste = id,
                Artiste = new Artiste()
                {
                    Biographie = "Lorem ipsum literae para tuiro...",
                    Nom = $"Monsieur truc {id}",
                    IdArtiste = id,
                    Titres = [],
                },
                Libelle = $"Titre {id}",
                Chronique = $"Chronique du titre {id}. Cette chronique est un exemple de texte pour le titre numéro {id}.",
                DateCreation = DateTime.Now.AddDays(-id),
                Duree = (uint)(180 + (id * 10)),
                DateSortie = DateTime.Now.AddMonths(-id),
                UrlJaquette = $"data:image/jpeg;base64,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",
                UrlEcoute = $"https://example.com/ecoute{id}",
                NbLectures = (uint)(1000 + (id * 100)),
                NbLikes = (uint)(500 + (id * 50)),
                Album = $"Album {id}",
                Commentaires = new List<Commentaire>(),
            };
            return this.View(titre);
        }

        /// <summary>
        /// Traite la demande de suppression d'un artiste.
        /// </summary>
        /// <param name="result">L'identifiant de l'artiste à supprimer.</param>
        /// <returns>Le résultat de la suppression de l'artiste.</returns>
        [HttpPost]
        public IActionResult Delete([FromForm] Titre result)
        {
            return this.RedirectToAction(nameof(this.Index));
        }

        /// <summary>
        /// Affiche le formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="id">identifiant du titre à modifier.</param>
        /// <returns>La vue pour éditer un artiste.</returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CreationAndEditionTitreModel model = new()
            {
                Artistes = [new Artiste() { Nom = "Monsieur truc" },],
                Styles = [new Style() { Libelle = "Jazz", IdStyle = 1 },],
                Titre = new Titre() { Duree = 12, UrlEcoute = "http://EditUrlEcoute.fr", UrlJaquette = "http://JaquetteUrlEcoute.fr", Styles = [new() { IdStyle = 1 },], Chronique = "Lorem ipsum icosaedrical guyatum fortram unical sum faritae...", Libelle = "Un super son", Album = "Démon de minuit" },
            };
            return this.View(model);
        }

        /// <summary>
        /// Traite la soumission du formulaire d'édition d'un artiste.
        /// </summary>
        /// <param name="result">L'identifiant de l'artiste à éditer.</param>
        /// <returns>Le résultat de l'édition de l'artiste.</returns>
        [HttpPost]
        public IActionResult Edit([FromForm] Titre result)
        {
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
