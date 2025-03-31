// <copyright file="LocalTitreRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class LocalTitreRepository : ITitreRepository
    {
        /// <inheritdoc/>
        public void Add(Titre titre)
        {
            // Vérifier si un titre avec le même libellé existe déjà pour cet artiste
            bool titreExiste = Factory.Titres.Any(t => t.Libelle == titre.Libelle && t.IdArtiste == titre.IdArtiste);
            if (titreExiste)
            {
                // Le titre existe déjà, on ne fait rien
                return;
            }

            // Trouver le premier ID disponible
            int newId = 1;
            while (Factory.Titres.Any(t => t.IdTitre == newId))
            {
                newId++;
            }

            // Assigner le nouvel ID au style
            titre.IdTitre = newId;
            titre.Artiste = Factory.Artistes.FirstOrDefault(a => a.IdArtiste == titre.IdArtiste) ?? new();

            // Ajouter le style à la collection
            Factory.Titres.Add(titre);
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> AdministrationFindTitres(int offset, int limit)
        {
            return Factory.Titres.OrderBy(t => t.Artiste.Nom).Skip(limit * (int)(offset - 1)).Take(limit).ToList();
        }

        /// <inheritdoc/>
        public int Count()
        {
            return Factory.Titres.Count;
        }

        /// <inheritdoc/>
        public void Delete(Titre titre)
        {
            Factory.Titres.RemoveAll(s => s.IdTitre == titre.IdTitre);
        }

        /// <inheritdoc/>
        public Titre Find(int idTitre)
        {
            return Factory.Titres.FirstOrDefault(s => s.IdTitre == idTitre) ?? new();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> FindAll()
        {
            return Factory.Titres;
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> FindTitres(int offset, int limit)
        {
            return Factory.Titres.OrderByDescending(t => t.DateCreation).Skip(limit * (int)(offset - 1)).Take(limit).ToList();
        }

        /// <inheritdoc/>
        public void IncrementNbLectures(Titre titre)
        {
            Factory.Titres.Where(t => t.IdTitre == titre.IdTitre)
                       .ToList()
                       .ForEach(t => t.NbLectures++);
        }

        /// <inheritdoc/>
        public void IncrementNbLikes(Titre titre)
        {
            Factory.Titres.Where(t => t.IdTitre == titre.IdTitre)
                       .ToList()
                       .ForEach(t => t.NbLikes++);
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> Search(string mot)
        {
            return Factory.Titres
                .Where(t => t.Libelle.Contains(mot, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <inheritdoc/>
        public IEnumerable<Titre> SearchByStyle(string libelle)
        {
            return Factory.Titres
                .Where(t => t.Styles.Any(s => s.Libelle.Equals(libelle, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        /// <inheritdoc/>
        public void Update(Titre titre)
        {
            Titre? existingTitre = Factory.Titres.FirstOrDefault(t => t.IdTitre == titre.IdTitre);
            if (existingTitre == null)
            {
                this.Add(titre);
            }
            else
            {
                existingTitre.Libelle = titre.Libelle;
                existingTitre.IdArtiste = titre.IdArtiste;
                existingTitre.Artiste = Factory.Artistes.FirstOrDefault(a => a.IdArtiste == titre.IdArtiste) ?? new();
                existingTitre.NbLectures = titre.NbLectures;
                existingTitre.NbLikes = titre.NbLikes;
                existingTitre.Styles = titre.Styles;
            }
        }
    }
}