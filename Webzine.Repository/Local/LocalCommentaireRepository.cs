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
            while (Factory.Commentaires.Any(c => c.IdCommentaire == newId))
            {
                newId++;
            }

            // Assigner le nouvel ID au commentaire
            commentaire.IdCommentaire = newId;
            commentaire.Titre = Factory.Titres.First(t => t.IdTitre == commentaire.IdTitre);

            // Ajouter le commentaire à la collection
            Factory.Commentaires.Add(commentaire);
            Factory.Titres.First(t => t.IdTitre == commentaire.IdTitre).Commentaires.Add(commentaire);
        }

        /// <inheritdoc/>
        public IEnumerable<Commentaire> AdministrationFindCommentaires(int offset, int limit)
        {
            return Factory.Commentaires.OrderByDescending(c => c.DateCreation).Skip(limit * (int)(offset - 1)).Take(limit).ToList();
        }

        /// <inheritdoc/>
        public void Delete(Commentaire commentaire)
        {
            // Trouver le vrai commentaire à partir de l'ID fourni
            Commentaire vraiCommentaire = Factory.Commentaires.First(c => c.IdCommentaire == commentaire.IdCommentaire);

            // Supprimer le commentaire de la liste globale des commentaires
            Factory.Commentaires.Remove(vraiCommentaire);

            // Trouver le titre associé au commentaire
            var titreConcerne = Factory.Titres.First(t => t.IdTitre == vraiCommentaire.IdTitre);

            // Supprimer le commentaire de la liste des commentaires du titre
            var commentaireASupprimer = titreConcerne.Commentaires.FirstOrDefault(c => c.IdCommentaire == vraiCommentaire.IdCommentaire);
            if (commentaireASupprimer != null)
            {
                titreConcerne.Commentaires.Remove(commentaireASupprimer);
            }
        }

        /// <inheritdoc/>
        public Commentaire Find(int id)
        {
            return Factory.Commentaires.FirstOrDefault(c => c.IdCommentaire == id) ?? new();
        }

        /// <inheritdoc/>
        public IEnumerable<Commentaire> FindAll()
        {
            return Factory.Commentaires;
        }
    }
}