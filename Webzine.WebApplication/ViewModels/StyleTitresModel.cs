// <copyright file="StyleTitresModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;

    /// <summary>
    /// Model pour servir les données nécessaires à la page des titres par style.
    /// </summary>
    public class StyleTitresModel
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="StyleTitresModel"/>.
        /// </summary>
        /// <param name="dataGenerator">Le générateur de données fictives.</param>
        /// <param name="nomStyle">Le nom du style.</param>
        public StyleTitresModel(DataGenerator dataGenerator, string nomStyle)
        {
            // Générer un style avec le nom fourni
            this.Style = dataGenerator.GenerateStyle();
            this.Style.Libelle = nomStyle;

            // Générer entre 0 et 5 titres aléatoires
            this.Titres = new List<Titre>();
            var nbTitres = new Random().Next(6); // Génère un nombre entre 0 et 5 inclus
            for (int i = 0; i < nbTitres; i++)
            {
                var titre = dataGenerator.GenerateTitre();
                this.Titres.Add(titre);
            }
        }

        /// <summary>
        /// Obtient ou définit le style musical.
        /// </summary>
        public Style Style { get; set; }

        /// <summary>
        /// Obtient ou définit la liste des titres du style.
        /// </summary>
        public List<Titre> Titres { get; set; }
    }
} 