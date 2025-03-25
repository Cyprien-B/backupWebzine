using System;
using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.Repository.Contracts
{
    /// <summary>
    /// Interface définissant les méthodes du repository pour la gestion des commentaires.
    /// </summary>
    public interface ICommentaireRepository
    {
        /// <summary>
        /// Ajoute un nouveau commentaire à la base de données.
        /// </summary>
        /// <param name="commentaire">L'objet commentaire à ajouter.</param>
        void Add(Commentaire commentaire);

        /// <summary>
        /// Supprime un commentaire de la base de données.
        /// </summary>
        /// <param name="commentaire">L'objet commentaire à supprimer.</param>
        void Delete(Commentaire commentaire);

        /// <summary>
        /// Recherche un commentaire par son identifiant unique.
        /// </summary>
        /// <param name="id">L'identifiant du commentaire recherché.</param>
        /// <returns>Retourne l'objet Commentaire s'il existe, sinon null.</returns>
        Commentaire Find(int id);

        /// <summary>
        /// Retourne tous les commentaires disponibles dans la base de données.
        /// </summary>
        /// <returns>Une collection de commentaires.</returns>
        IEnumerable<Commentaire> FindAll();
    }
}