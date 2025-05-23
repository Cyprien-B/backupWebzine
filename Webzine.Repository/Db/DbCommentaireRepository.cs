﻿// <copyright file="DbCommentaireRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Db
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.EntityContext;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class DbCommentaireRepository(WebzineDbContext context) : ICommentaireRepository
    {
        /// <inheritdoc/>
        public void Add(Commentaire commentaire)
        {
            // Charger le titre existant sans inclure les commentaires
            Titre? titreExistant = context.Titres
                .Include(t => t.Artiste)
                .Include(t => t.Styles)
                .Include(t => t.Commentaires)
                .SingleOrDefault(t => t.IdTitre == commentaire.IdTitre);

            if (titreExistant != null)
            {
                // Ajouter le commentaire à la collection de commentaires du titre
                titreExistant.Commentaires.Add(commentaire);

                commentaire.Titre = titreExistant;

                // Ajouter le commentaire
                context.Commentaires.Add(commentaire);

                // Sauvegarder les changements
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<Commentaire> AdministrationFindCommentaires(int offset, int limit)
        {
            return [.. context.Commentaires
                .Include(c => c.Titre)
                .OrderByDescending(c => c.DateCreation)
                .Skip(limit * (int)(offset - 1))
                .Take(limit)
                .AsNoTracking()];
        }

        /// <inheritdoc/>
        public int Count()
        {
            return context.Commentaires.AsNoTracking().Count();
        }

        /// <inheritdoc/>
        public void Delete(Commentaire commentaire)
        {
            // Charger le commentaire existant avec son titre et les entités liées
            var commentaireExistant = context.Commentaires
                .Include(c => c.Titre)
                .ThenInclude(t => t.Artiste)
                .Include(c => c.Titre)
                .ThenInclude(t => t.Styles)
                .AsNoTracking()
                .FirstOrDefault(c => c.IdCommentaire == commentaire.IdCommentaire);

            if (commentaireExistant != null)
            {
                // Vérifier si le titre associé existe et retirer le commentaire de sa collection
                commentaireExistant.Titre?.Commentaires.Remove(commentaireExistant);

                // Supprimer le commentaire
                context.Commentaires.Remove(commentaireExistant);

                // Sauvegarder les changements
                context.SaveChanges();
            }
        }

        /// <inheritdoc/>
        public Commentaire Find(int id)
        {
            return context.Commentaires
                .Include(c => c.Titre)
                .AsNoTracking()
                .Single(c => c.IdCommentaire == id);
        }

        /// <inheritdoc/>
        public IEnumerable<Commentaire> FindAll()
        {
            return [.. context.Commentaires
                .Include(c => c.Titre)
                .AsNoTracking()];
        }
    }
}