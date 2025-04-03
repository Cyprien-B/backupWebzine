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
        /// Retourne tous les style qui ressemble à l'artiste.
        /// </summary>
        /// <param name="mot"^>Mot de recherche.</param>
        /// <returns>Retourne un énumérable d'artiste contenant mot en nom.</returns>
        IEnumerable<Artiste> Search(string mot);

        /// <summary>
        /// Confirme si le nom d'artiste est existant.
        /// </summary>
        /// <param name="artiste">Artiste dont le nom est recherché.</param>
        /// <returns><see cref="true"/> si l'artiste existe sinon <see cref="false"/>.</returns>
        bool NomAny(Artiste artiste);

        /// <summary>
        /// Compte le nombre d'artistes total existant.
        /// </summary>
        /// <returns>Un entier de type <see cref="int"/> contenant les artistes totaux.</returns>
        int Count();

        /// <summary>
        /// Supprime un artiste de la base de données.
        /// </summary>
        /// <param name="artiste">L'objet artiste à supprimer.</param>
        void Delete(Artiste artiste);

        /// <summary>
        /// Recherche un artiste par son identifiant unique.
        /// </summary>
        /// <param name="id">L'identifiant de l'artiste recherché.</param>
        /// <returns>Retourne l'objet Artiste avec l'id correspondant.</returns>
        Artiste Find(int id);

        /// <summary>
        /// Retourne une liste paginée d'artiste triés par nom dans l'ordre alphanumérique.
        /// </summary>
        /// <param name="offset">Index de départ des résultats.</param>
        /// <param name="limit">Nombre de résultats à retourner.</param>
        /// <returns>Une collection d'artiste triés.</returns>
        IEnumerable<Artiste> AdministrationFindArtistes(int offset, int limit);

        /// <summary>
        /// Récupère tous les artistes de la base de données.
        /// </summary>
        /// <returns>Une collection d'artistes.</returns>
        IEnumerable<Artiste> FindAll();

        /// <summary>
        /// Récupère l'artiste qui possède le bon nom.
        /// </summary>
        /// <param name="nom">Nom de l'artiste à obtenir.</param>
        /// <returns>Un artiste avec le nom recherché.</returns>
        Artiste FindArtisteByName(string nom);

        /// <summary>
        /// Met à jour un artiste existant dans la base de données.
        /// </summary>
        /// <param name="artiste">L'objet artiste avec les nouvelles informations.</param>
        void Update(Artiste artiste);
    }
}
