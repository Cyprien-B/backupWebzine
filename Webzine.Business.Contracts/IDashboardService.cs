// <copyright file="IDashboardService.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webzine.Entity;

namespace Webzine.Business.Contracts
{
     /// <summary>
     /// Interface définissant les services pour la gestion de la page dashboard.
     /// </summary>
    public interface IDashboardService
    {

        /// <summary>
        /// Cherche l'artiste ayant composé le plus de titre.
        /// </summary>
        /// <returns>Retourne un artiste de type <see cref="Artiste"/> ayant composé le plus de titre sinon un artiste par défaut.</returns>
        Artiste? FindArtisteComposePlusTitre();

        /// <summary>
        /// Cherche l'artiste ayant composé le plus de chronique.
        /// </summary>
        /// <returns>Retourne un artiste de type <see cref="Artiste"/> soyant le plus chroniqué sinon un artiste par défaut.</returns>
        Artiste? FindArtistePlusChronique();

        /// <summary>
        /// Compte le nombre de biographie qui existe en tout, tout artiste confondu.
        /// </summary>
        /// <returns>Un entier de type <see cref="int"/> contenant les biographies totales.</returns>
        int CountBiographies();

        /// <summary>
        /// Cherche le titre le plus lu.
        /// </summary>
        /// <returns>Retourne le titre le plus lu de type <see cref="Titre"/>.</returns>
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
