// <copyright file="IArtisteRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Contracts
{
    using System.Collections.Generic;
    using Webzine.Entity;

    /// <summary>
    /// Interface définissant les méthodes du repository pour la gestion des artistes.
    /// </summary>
    public interface IArtisteRepository
    {
        /// <summary>
        /// Ajoute un artiste dans la base de données.
        /// </summary>
        /// <param name="artiste">L'objet artiste à ajouter.</param>
        void Add(Artiste artiste);

        /// <summary>
        /// Supprime un artiste de la base de données.
        /// </summary>
        /// <param name="artiste">L'objet artiste à supprimer.</param>
        void Delete(Artiste artiste);

        /// <summary>
        /// Recherche un artiste par son identifiant unique.
        /// </summary>
        /// <param name="id">L'identifiant de l'artiste recherché.</param>
        /// <returns>Retourne l'objet Artiste s'il existe, sinon null.</returns>
        Artiste Find(int id);

        /// <summary>
        /// Récupère tous les artistes de la base de données.
        /// </summary>
        /// <returns>Une collection d'artistes.</returns>
        IEnumerable<Artiste> FindAll();

        /// <summary>
        /// Met à jour un artiste existant dans la base de données.
        /// </summary>
        /// <param name="artiste">L'objet artiste avec les nouvelles informations.</param>
        void Update(Artiste artiste);
    }
}
