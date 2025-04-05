// <copyright file="ICommentaireRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Contracts
{
    using Webzine.Entity;

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
        /// Compte le nombre de commentaires totaux.
        /// </summary>
        /// <returns>Retourne un entier sur le nombre de commentaire.</returns>
        int Count();

        /// <summary>
        /// Supprime un commentaire de la base de données.
        /// </summary>
        /// <param name="commentaire">L'objet commentaire à supprimer.</param>
        void Delete(Commentaire commentaire);

        /// <summary>
        /// Recherche un commentaire par son identifiant unique.
        /// </summary>
        /// <param name="id">L'identifiant du commentaire recherché.</param>
        /// <returns>Retourne l'objet Commentaire avec l'id correspondant.</returns>
        Commentaire Find(int id);

        /// <summary>
        /// Retourne une liste paginée de commentaires triés par date du plus récent au plus ancien.
        /// </summary>
        /// <param name="offset">Index de départ des résultats.</param>
        /// <param name="limit">Nombre de résultats à retourner.</param>
        /// <returns>Une collection de style triés.</returns>
        IEnumerable<Commentaire> AdministrationFindCommentaires(int offset, int limit);

        /// <summary>
        /// Retourne tous les commentaires disponibles dans la base de données.
        /// </summary>
        /// <returns>Une collection de commentaires.</returns>
        IEnumerable<Commentaire> FindAll();
    }
}