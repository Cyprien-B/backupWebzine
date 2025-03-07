// <copyright file="TitreModel.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.ViewModels
{
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model pour servir les données nécessaires à la page d'un titre.
    /// </summary>
    public class TitreModel
    {
        /// <summary>
        /// Obtient ou définit le titre affiché.
        /// </summary>
        public Titre Titre { get; set; }

        /// <summary>
        /// Obtient ou définit les styles du titre.
        /// </summary>
        public List<Style> Styles { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="TitreModel"/>.
        /// </summary>
        /// <param name="dataGenerator">Le générateur de données fictives.</param>
        /// <param name="id">L'identifiant du titre à générer.</param>
        public TitreModel(DataGenerator dataGenerator, int id)
        {
            this.Titre = dataGenerator.GenerateTitre();
            this.Titre.IdTitre = id;
            
            // Générer entre 1 et 4 styles aléatoires
            var nbStyles = new Random().Next(1, 5);
            this.Styles = dataGenerator.GenerateStyles(nbStyles);
        }
    }
}