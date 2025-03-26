// <copyright file="LocalCommentaireRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class LocalCommentaireRepository : ICommentaireRepository
    {
        /// <inheritdoc/>
        public void Add(Commentaire commentaire)
        {
            // Trouver le premier ID disponible
            int newId = 1;
            while (CommentaireFactory.Commentaires.Any(c => c.IdCommentaire == newId))
            {
                newId++;
            }

            // Assigner le nouvel ID au commentaire
            commentaire.IdCommentaire = newId;

            // Ajouter le commentaire à la collection
            CommentaireFactory.Commentaires.Add(commentaire);
            TitreFactory.Titres.First(t => t.IdTitre == commentaire.IdTitre).Commentaires.Add(commentaire);
        }

        /// <inheritdoc/>
        public void Delete(Commentaire commentaire)
        {
            CommentaireFactory.Commentaires.RemoveAll(c => c.IdCommentaire == commentaire.IdCommentaire);
            TitreFactory.Titres.First(t => t.IdTitre == commentaire.IdTitre).Commentaires.Remove(commentaire);
        }

        /// <inheritdoc/>
        public Commentaire Find(int id)
        {
            return CommentaireFactory.Commentaires.FirstOrDefault(c => c.IdCommentaire == id) ?? new();
        }

        /// <inheritdoc/>
        public IEnumerable<Commentaire> FindAll()
        {
            return CommentaireFactory.Commentaires;
        }
    }
}