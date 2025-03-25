using System;
using System.Collections.Generic;
using Webzine.Entity;

namespace Webzine.Repository.Contracts
{
    /// <summary>
    /// Interface définissant les méthodes du repository pour la gestion des styles de musique.
    /// </summary>
    public interface IStyleRepository
    {
        /// <summary>
        /// Ajoute un nouveau style de musique à la base de données.
        /// </summary>
        /// <param name="style">L'objet style à ajouter.</param>
        void Add(Style style);

        /// <summary>
        /// Supprime un style de musique de la base de données.
        /// </summary>
        /// <param name="style">L'objet style à supprimer.</param>
        void Delete(Style style);

        /// <summary>
        /// Recherche un style de musique par son identifiant unique.
        /// </summary>
        /// <param name="id">L'identifiant du style recherché.</param>
        /// <returns>Retourne l'objet Style s'il existe, sinon null.</returns>
        Style Find(int id);

        /// <summary>
        /// Retourne tous les styles de musique disponibles dans la base de données.
        /// </summary>
        /// <returns>Une collection de styles de musique.</returns>
        IEnumerable<Style> FindAll();

        /// <summary>
        /// Met à jour un style de musique existant dans la base de données.
        /// </summary>
        /// <param name="style">L'objet style avec les nouvelles informations.</param>
        void Update(Style style);
    }
}
