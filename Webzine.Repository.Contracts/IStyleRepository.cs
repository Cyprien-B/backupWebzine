// <copyright file="IStyleRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Contracts
{
    using Webzine.Entity;

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
        /// Compte le nombre total de Style dans la base de données.
        /// </summary>
        /// <returns>Le nombre total de Style.</returns>
        int Count();

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
        Style? Find(int id);

        /// <summary>
        /// Retourne tous les styles de musique disponibles dans la base de données.
        /// </summary>
        /// <returns>Une collection de styles de musique.</returns>
        IEnumerable<Style> FindAll();

        /// <summary>
        /// Retourne tous les styles de musique associés à leurs identifiants.
        /// </summary>
        /// <param name="styleIds">Enumerable <see cref="IEnumerable{int}"/> d'entier contenant la liste des styles à retourner.</param>
        /// <returns>Une collection de styles de musique correspondant aux identifiants.</returns>
        IEnumerable<Style> FindStylesByIds(IEnumerable<int> styleIds);

        /// <summary>
        /// Retourne une liste paginée de style triés par nom dans l'ordre alphanumérique.
        /// </summary>
        /// <param name="offset">Index de départ des résultats.</param>
        /// <param name="limit">Nombre de résultats à retourner.</param>
        /// <returns>Une collection de style triés.</returns>
        IEnumerable<Style> AdministrationFindStyles(int offset, int limit);

        /// <summary>
        /// Met à jour un style de musique existant dans la base de données.
        /// </summary>
        /// <param name="style">L'objet style avec les nouvelles informations.</param>
        void Update(Style style);
    }
}
