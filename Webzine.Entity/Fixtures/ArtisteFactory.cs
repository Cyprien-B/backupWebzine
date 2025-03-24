// <copyright file="ArtisteFactory.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity.Fixtures
{
    /// <summary>
    /// Class static permettant de générer et de gérer des artistes.
    /// </summary>
    public static class ArtisteFactory
    {
        /// <summary>
        /// Obtient ou définit un attribut static retournant une liste d'artiste.
        /// </summary>
        public static List<Artiste> Artistes { get; set; } = Factory.GenerateArtistes(30);
    }
}
