// <copyright file="ITitreRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Contracts
{
    using System.Collections.Generic;
    using Webzine.Entity;

    /// <summary>
    /// Interface définissant les méthodes du repository pour la gestion des titres musicaux.
    /// </summary>
    public interface ITitreRepository
    {
        /// <summary>
        /// Ajoute un nouveau titre à la base de données.
        /// </summary>
        /// <param name="titre">L'objet titre à ajouter.</param>
        void Add(Titre titre);

        /// <summary>
        /// Compte le nombre total de titres dans la base de données.
        /// </summary>
        /// <returns>Le nombre total de titres.</returns>
        int Count();

        /// <summary>
        /// Supprime un titre de la base de données.
        /// </summary>
        /// <param name="titre">L'objet titre à supprimer.</param>
        void Delete(Titre titre);

        /// <summary>
        /// Recherche un titre par son identifiant unique.
        /// </summary>
        /// <param name="idTitre">L'identifiant du titre recherché.</param>
        /// <returns>Retourne l'objet Titre s'il existe, sinon null.</returns>
        Titre? Find(int idTitre);

        /// <summary>
        /// Retourne une liste paginée de titres triés par date de création (du plus récent au plus ancien).
        /// </summary>
        /// <param name="offset">Index de départ des résultats.</param>
        /// <param name="limit">Nombre de résultats à retourner.</param>
        /// <returns>Une collection de titres triés.</returns>
        IEnumerable<Titre> FindTitres(int offset, int limit);

        /// <summary>
        /// Retourne une liste paginée de titre triés par nom d'artistes dans l'ordre alphanumérique.
        /// </summary>
        /// <param name="offset">Index de départ des résultats.</param>
        /// <param name="limit">Nombre de résultats à retourner.</param>
        /// <returns>Une collection de titre triés.</returns>
        IEnumerable<Titre> AdministrationFindTitres(int offset, int limit);

        /// <summary>
        /// Retourne les premiers titres les plus populaires dans la limite.
        /// </summary>
        /// <param name="limit">Limite de titres populaires.</param>
        /// <returns>Une liste de titres correspondant au premiers les plus populaires.</returns>
        IEnumerable<Titre> FindTitresPopulaires(int limit);

        /// <summary>
        /// Retourne tous les titres disponibles dans la base de données.
        /// </summary>
        /// <returns>Une collection de tous les titres.</returns>
        IEnumerable<Titre> FindAll();

        /// <summary>
        /// Incrémente le nombre de lectures d’un titre.
        /// </summary>
        /// <param name="titre">L'objet titre dont le compteur de lectures doit être incrémenté.</param>
        void IncrementNbLectures(Titre titre);

        /// <summary>
        /// Incrémente le nombre de likes d’un titre.
        /// </summary>
        /// <param name="titre">L'objet titre dont le compteur de likes doit être incrémenté.</param>
        void IncrementNbLikes(Titre titre);

        /// <summary>
        /// Cherche si un artiste possède le titre.
        /// </summary>
        /// <returns>Retourne un booléen <see cref="bool"/> indiquant si le titre existe déjà pour son artiste.</returns>
        bool LibelleToArtisteAny(Titre titre);

        /// <summary>
        /// Recherche les titres contenant un mot spécifique (recherche insensible à la casse).
        /// </summary>
        /// <param name="mot">Le mot clé à rechercher.</param>
        /// <returns>Une collection de titres correspondant à la recherche.</returns>
        IEnumerable<Titre> Search(string mot);

        /// <summary>
        /// Recherche les titres appartenant à un style de musique spécifique (recherche insensible à la casse).
        /// </summary>
        /// <param name="libelle">Le libellé du style de musique recherché.</param>
        /// <returns>Une collection de titres correspondant au style.</returns>
        IEnumerable<Titre> SearchByStyle(string libelle);

        /// <summary>
        /// Met à jour un titre existant dans la base de données.
        /// </summary>
        /// <param name="titre">L'objet titre avec les nouvelles informations.</param>
        void Update(Titre titre);
    }
}