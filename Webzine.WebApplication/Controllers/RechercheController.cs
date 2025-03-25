// <copyright file="RechercheController.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.WebApplication.ViewModels;

    /// <summary>
    /// Contrôleur de recherche.
    /// </summary>
    public class RechercheController : Controller
    {
        /// <summary>
        /// Gère la recherche et affiche les résultats.
        /// </summary>
        /// <param name="recherche">string de recherche.</param>
        /// <returns>Une vue avec les résultats de la recherche.</returns>
        [HttpPost]
        public IActionResult Index([FromForm] string recherche)
        {
            if (string.IsNullOrWhiteSpace(recherche))
            {
                return this.RedirectToAction("Index", "Home");
            }

            // Générer 0-3 artistes aléatoires
            int nbArtistes = new Random().Next(0, 4);
            int nbTitres = new Random().Next(0, 5);

            List<Artiste> artistes = [];
            List<Titre> titres = [];

            Random random = new();

            for (int i = 0; i < nbArtistes; i++)
            {
                Artiste artiste = ArtisteFactory.Artistes[0];
                if (random.Next(2) == 0)
                {
                    artiste.Nom = recherche + " " + artiste.Nom;
                }
                else
                {
                    artiste.Nom = artiste.Nom + " " + recherche;
                }

                artistes.Add(artiste);
            }

            for (int i = 0; i < nbTitres; i++)
            {
                Titre titre = Factory.GenerateTitre();
                if (random.Next(2) == 0)
                {
                    titre.Libelle = recherche + " " + titre.Libelle;
                }
                else
                {
                    titre.Libelle = titre.Libelle + " " + recherche;
                }

                titres.Add(titre);
            }

            RechercheModel model = new()
            {
                TermeRecherche = recherche,
                Artistes = artistes,
                Titres = titres,
            };

            return this.View(model);
        }
    }
}
