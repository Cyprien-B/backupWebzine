using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webzine.Business.Contracts;
using Webzine.Entity;
using Webzine.Repository.Contracts;

namespace Webzine.Business
{
    /// <summary>
    /// Classe de service pour le dashboard.
    /// </summary>
    public class DashboardService : IDashboardService
    {
        private readonly IArtisteRepository artisteRepository;
        private readonly ITitreRepository titreRepository;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="DashboardService"/>.
        /// </summary>
        /// <param name="artisteRepository">Le repository d'artistes.</param>
        /// <param name="titreRepository">Le repository de titres.</param>
        public DashboardService(IArtisteRepository artisteRepository, ITitreRepository titreRepository)
        {
            artisteRepository = artisteRepository;
            titreRepository = titreRepository;
        }

        /// <summary>
        /// Trouve l'artiste qui compose le plus de titres.
        /// </summary>
        /// <returns>L'artiste qui compose le plus de titres, ou null si aucun artiste n'est trouvé.</returns>
        public Artiste? FindArtisteComposePlusTitre()
        {
            // Trouve l'artiste qui compose le plus de titres
            return artisteRepository.FindAll()
                .OrderByDescending(a => a.Titres.Count)
                .FirstOrDefault();
        }

        /// <summary>
        /// Trouve l'artiste qui est le plus chroniqué.
        /// </summary>
        /// <returns>L'artiste qui est le plus chroniqué, ou null si aucun artiste n'est trouvé.</returns>
        public Artiste? FindArtistePlusChronique()
        {
            // Trouve l'artiste qui est le plus chroniqué
            return artisteRepository.FindAll()
                .OrderByDescending(a => a.Titres.Count(t => !string.IsNullOrEmpty(t.Chronique)))
                .FirstOrDefault();
        }

        /// <summary>
        /// Compte le nombre de biographies d'artistes.
        /// </summary>
        /// <returns>Le nombre de biographies d'artistes.</returns>
        public int CountBiographies()
        {
            // Compte le nombre de biographies d'artistes
            return artisteRepository.FindAll()
                .Count(a => !string.IsNullOrEmpty(a.Biographie));
        }

        /// <summary>
        /// Trouve le titre le plus lu.
        /// </summary>
        /// <returns>Le titre le plus lu, ou null si aucun titre n'est trouvé.</returns>
        public Titre? FindTitresPlusLu()
        {
            // Trouve le titre le plus lu
            return titreRepository.FindAll()
                .OrderByDescending(t => t.NbLectures)
                .FirstOrDefault();
        }

        /// <summary>
        /// Compte le nombre total de lectures de titres.
        /// </summary>
        /// <returns>Le nombre total de lectures de titres.</returns>
        public long CountGlobalLectures()
        {
            // Compte le nombre total de lectures de titres
            return titreRepository.FindAll()
                .Sum(t => t.NbLectures);
        }

        /// <summary>
        /// Compte le nombre total de likes de titres.
        /// </summary>
        /// <returns>Le nombre total de likes de titres.</returns>
        public long CountGlobalLikes()
        {
            return titreRepository.FindAll()
                .Sum(t => t.NbLikes);
        }
    }
}
