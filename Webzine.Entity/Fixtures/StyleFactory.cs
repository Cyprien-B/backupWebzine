// <copyright file="StyleFactory.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity.Fixtures
{
    /// <summary>
    /// Class static permettant de générer et de gérer des styles.
    /// </summary>
    public static class StyleFactory
    {
        /// <summary>
        /// Obtient ou définit un attribut static retournant une liste de styles.
        /// </summary>
        public static List<Style> Styles { get; set; } = Factory.GenerateStyles(30).DistinctBy(s => s.Libelle).ToList();
    }
}
