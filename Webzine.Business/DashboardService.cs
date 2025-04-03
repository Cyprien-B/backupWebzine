// <copyright file="DashboardService.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>
namespace Webzine.Business
{
    using Webzine.Business.Contracts;
    using Webzine.Entity;
    using Webzine.Repository.Contracts;

    /// <summary>
    /// Classe de service pour le dashboard.
    /// </summary>
    /// <remarks>
    /// Initialise une nouvelle instance de la classe <see cref="DashboardService"/>.
    /// </remarks>
    /// <param name="artisteRepository">Le repository d'artistes.</param>
    /// <param name="titreRepository">Le repository de titres.</param>
    public class DashboardService(IArtisteRepository artisteRepository, ITitreRepository titreRepository) : IDashboardService
    {
        /// <inheritdoc/>
        public Artiste FindArtisteComposePlusTitre()
        {
            var artistecomposeplustitre = artisteRepository.FindAll()
                .OrderByDescending(a => a.Titres.Count)
                .FirstOrDefault();

            if (artistecomposeplustitre == null)
            {
                artistecomposeplustitre = new Artiste()
                {
                    Nom = "Aucun artiste",
                };
            }

            return artistecomposeplustitre;
        }

        /// <inheritdoc/>
        public Artiste FindArtistePlusChronique()
        {
            var artistepluschronique = artisteRepository.FindAll()
                .OrderByDescending(a => a.Titres.Count(t => !string.IsNullOrEmpty(t.Chronique)))
                .First();

            if (artistepluschronique == null)
            {
                artistepluschronique = new Artiste()
                {
                    Nom = "Aucun artiste",
                };
            }

            return artistepluschronique;
        }

        /// <inheritdoc/>
        public int CountBiographies()
        {
            // Compte le nombre de biographies d'artistes
            return artisteRepository.FindAll()
                .Count(a => !string.IsNullOrEmpty(a.Biographie));
        }

        /// <inheritdoc/>
        public Titre FindTitresPlusLu()
        {
            var titrepluslu = titreRepository.FindAll()
                .OrderByDescending(t => t.NbLectures)
                .First();

            if (titrepluslu == null)
            {
                titrepluslu = new Titre()
                {
                    Libelle = "Aucun titre",
                };
            }

            return titrepluslu;
        }

        /// <inheritdoc/>
        public long CountGlobalLectures()
        {
            return titreRepository.FindAll()
                .Sum(t => t.NbLectures);
        }

        /// <inheritdoc/>
        public long CountGlobalLikes()
        {
            return titreRepository.FindAll()
                .Sum(t => t.NbLikes);
        }
    }
}
