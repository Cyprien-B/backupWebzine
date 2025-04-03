// <copyright file="IDashboardService.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>
namespace Webzine.Business.Contracts
{
    using Webzine.Entity;

    /// <summary>
    /// Interface définissant les services pour la gestion de la page dashboard.
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// Cherche l'artiste ayant composé le plus de titre.
        /// </summary>
        /// <returns>L'artiste ayant composé le plus de titre, ou un atiste nommer "Aucun artiste" si aucun artiste n'est trouvé.</returns>
        Artiste? FindArtisteComposePlusTitre();

        /// <summary>
        /// Cherche l'artiste ayant composé le plus de chronique.
        /// </summary>
        /// <returns>L'artiste qui est le plus chroniqué, ou un atiste nommer "Aucun artiste" si aucun artiste n'est trouvé.</returns>
        Artiste? FindArtistePlusChronique();

        /// <summary>
        /// Compte le nombre de biographie qui existe en tout, tout artiste confondu.
        /// </summary>
        /// <returns>Un entier de type <see cref="int"/> contenant les biographies totales.</returns>
        int CountBiographies();

        /// <summary>
        /// Cherche le titre le plus lu.
        /// </summary>
        /// <returns>Le titre le plus lu, ou un titre "Aucun titre" si aucun titre n'est trouvé.</returns>
        Titre? FindTitresPlusLu();

        /// <summary>
        /// Compte le nombre de lectures globaux pour tous les titres.
        /// </summary>
        /// <returns>Le nombre de lecture total de lecture, tout titres confondu.</returns>
        long CountGlobalLectures();

        /// <summary>
        /// Compte le nombre de likes globaux pour tous les titres.
        /// </summary>
        /// <returns>Le nombre de like total de lecture, tout titres confondu.</returns>
        long CountGlobalLikes();
    }
}
