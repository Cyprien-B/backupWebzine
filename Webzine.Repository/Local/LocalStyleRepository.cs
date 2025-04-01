// <copyright file="LocalStyleRepository.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Repository.Local
{
    using System.Collections.Generic;
    using Webzine.Entity;
    using Webzine.Entity.Fixtures;
    using Webzine.Repository.Contracts;

    /// <inheritdoc/>
    public class LocalStyleRepository : IStyleRepository
    {
        /// <inheritdoc/>
        public void Add(Style style)
        {
            // Vérifie si un style avec le même libellé existe déjà
            bool styleExiste = Factory.Styles.Any(s => s.Libelle == style.Libelle);

            if (styleExiste)
            {
                // Ignorer l'ajout si le style existe déjà
                return;
            }

            // Trouver le premier ID disponible
            int newId = 1;
            while (Factory.Styles.Any(s => s.IdStyle == newId))
            {
                newId++;
            }

            // Assigner le nouvel ID au style
            style.IdStyle = newId;

            // Ajouter le style à la collection
            Factory.Styles.Add(style);
        }

        /// <inheritdoc/>
        public IEnumerable<Style> AdministrationFindStyles(int offset, int limit)
        {
            return Factory.Styles.OrderBy(s => s.Libelle).Skip(limit * (int)(offset - 1)).Take(limit).ToList();
        }

        /// <inheritdoc/>
        public void Delete(Style style)
        {
            // Supprimer le style de la liste globale des styles
            Factory.Styles.RemoveAll(s => s.IdStyle == style.IdStyle);

            // Parcourir tous les titres pour supprimer le style de leurs listes
            foreach (var titre in Factory.Titres)
            {
                // Supprimer le style de la liste des styles du titre si présent
                titre.Styles.RemoveAll(s => s.IdStyle == style.IdStyle);
            }
        }

        /// <inheritdoc/>
        public Style? Find(int id)
        {
            return Factory.Styles.FirstOrDefault(s => s.IdStyle == id);
        }

        /// <inheritdoc/>
        public int Count()
        {
            return Factory.Styles.Count();
        }

        /// <inheritdoc/>
        public IEnumerable<Style> FindAll()
        {
            return Factory.Styles.OrderBy(s => s.Libelle).ToList();
        }

        /// <inheritdoc/>
        public void Update(Style style)
        {
            // Trouver le style existant dans la liste globale
            Style? existingStyle = Factory.Styles.FirstOrDefault(s => s.IdStyle == style.IdStyle);

            if (existingStyle == null)
            {
                // Si le style n'existe pas, l'ajouter
                this.Add(style);
            }
            else
            {
                // Vérifie si un style avec le même libellé existe déjà
                bool styleExiste = Factory.Styles.Any(s => s.Libelle == style.Libelle);

                if (styleExiste)
                {
                    // Ignorer l'ajout si le style existe déjà
                    return;
                }

                // Mise à jour des propriétés du style existant
                existingStyle.Libelle = style.Libelle;

                // Mettre à jour les styles dans tous les titres associés
                foreach (var titre in Factory.Titres)
                {
                    // Trouver le style dans la liste des styles du titre
                    var titreStyle = titre.Styles.FirstOrDefault(s => s.IdStyle == style.IdStyle);
                    if (titreStyle != null)
                    {
                        // Mettre à jour les propriétés du style dans le titre
                        titreStyle.Libelle = style.Libelle;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<Style> FindStylesByIds(IEnumerable<int> styleIds)
        {
            return Factory.Styles
                    .Where(s => styleIds.Contains(s.IdStyle))
                    .ToList();
        }
    }
}