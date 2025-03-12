// <copyright file="RechercheModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;

    /// <summary>
    /// Model pour servir les données nécessaires à la page de recherche.
    /// </summary>
    public class RechercheModel
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RechercheModel"/>.
        /// </summary>
        /// <param name="dataGenerator">Le générateur de données fictives.</param>
        /// <param name="termeRecherche">Le terme de recherche.</param>
        public RechercheModel(DataGenerator dataGenerator, string termeRecherche)
        {
            this.TermeRecherche = termeRecherche;

            // Générer 0-3 artistes aléatoires
            var nbArtistes = new Random().Next(0, 4);
            this.Artistes = nbArtistes > 0 ? dataGenerator.GenerateArtistes(nbArtistes) : new List<Artiste>();

            // Générer 5 titres aléatoires
            this.Titres = new List<Titre>();
            for (int i = 0; i < 5; i++)
            {
                var titre = dataGenerator.GenerateTitre();
                this.Titres.Add(titre);
            }
        }

        /// <summary>
        /// Obtient ou définit le terme de recherche.
        /// </summary>
        public string TermeRecherche { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des artistes correspondant à la recherche.
        /// </summary>
        public List<Artiste> Artistes { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des titres correspondant à la recherche.
        /// </summary>
        public List<Titre> Titres { get; set; }
    }
}