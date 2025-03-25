// <copyright file="TitreFactory.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity.Fixtures
{
    /// <summary>
    /// Class static permettant de générer et de gérer des titres.
    /// </summary>
    public static class TitreFactory
    {
        /// <summary>
        /// Obtient ou définit un attribut static retournant une liste de titre.
        /// </summary>
        public static List<Titre> Titres { get; set; } = Factory.GenerateTitres(60, ArtisteFactory.Artistes, StyleFactory.Styles);
    }
}
