// <copyright file="CommentaireFactory.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity.Fixtures
{
    /// <summary>
    /// Class static permettant de générer et de gérer des titres.
    /// </summary>
    public static class CommentaireFactory
    {
        /// <summary>
        /// Obtient ou définit un attribut static retournant une liste de commentaire.
        /// </summary>
        public static List<Commentaire> Commentaires { get; set; } = Factory.GenerateCommentaires(50, TitreFactory.Titres);
    }
}