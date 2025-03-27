// <copyright file="Factory.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity.Fixtures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;

    /// <summary>
    /// Classe statique pour générer des données fictives.
    /// </summary>
    public static class Factory
    {
        // Déclaration des générateurs de données fictives
        private static readonly Faker<Artiste> ArtisteFake;
        private static readonly Faker<Titre> TitreFake;
        private static readonly Faker<Commentaire> CommentaireFake;
        private static readonly Faker<Style> StyleFake;

        // Propriétés publiques pour accéder aux listes générées

        /// <summary>
        /// Obtient ou définit une liste de faux artistes.
        /// </summary>
        public static List<Artiste> Artistes { get; set; }

        /// <summary>
        /// Obtient ou définit une liste de faux styles.
        /// </summary>
        public static List<Style> Styles { get; set; }

        /// <summary>
        /// Obtient ou définit une liste de faux styles.
        /// </summary>
        public static List<Titre> Titres { get; set; }

        /// <summary>
        /// Obtient ou définit une liste de faux styles.
        /// </summary>
        public static List<Commentaire> Commentaires { get; set; }

        /// <summary>
        /// Constructeur statique pour initialiser les données.
        /// </summary>
        static Factory()
        {
            // Initialisation du générateur aléatoire
            Randomizer.Seed = new Random(123);

            // Configuration du générateur pour les artistes
            ArtisteFake = new Faker<Artiste>()
                .RuleFor(a => a.IdArtiste, f => f.IndexFaker + 1)
                .RuleFor(a => a.Nom, f => f.Name.FullName())
                .RuleFor(a => a.Biographie, f => f.Lorem.Sentences())
                .RuleFor(a => a.Titres, f => new List<Titre>());

            // Configuration du générateur pour les styles
            StyleFake = new Faker<Style>()
                .RuleFor(s => s.IdStyle, f => f.IndexFaker + 1)
                .RuleFor(s => s.Libelle, f => f.Music.Genre())
                .RuleFor(s => s.Titres, f => new List<Titre>());

            // Configuration du générateur pour les titres
            TitreFake = new Faker<Titre>()
                .RuleFor(t => t.IdTitre, f => f.IndexFaker + 1)
                .RuleFor(t => t.Libelle, f => f.Lorem.Sentence(1, 3))
                .RuleFor(t => t.Chronique, f => f.Lorem.Sentences(10))
                .RuleFor(t => t.DateCreation, f => f.Date.Recent())
                .RuleFor(t => t.Duree, f => f.Random.UInt(60, 300))
                .RuleFor(t => t.DateSortie, f => f.Date.Past())
                .RuleFor(t => t.UrlJaquette, f => f.Image.PicsumUrl())
                .RuleFor(t => t.UrlEcoute, f => "https://www.youtube.com/embed/" + f.Random.AlphaNumeric(11))
                .RuleFor(t => t.NbLectures, f => f.Random.UInt(0, 100))
                .RuleFor(t => t.NbLikes, f => f.Random.UInt(0, 100))
                .RuleFor(t => t.Album, f => f.Lorem.Word())
                .RuleFor(t => t.Commentaires, _ => new List<Commentaire>())
                .RuleFor(t => t.Styles, _ => new List<Style>());

            // Configuration du générateur pour les commentaires
            CommentaireFake = new Faker<Commentaire>()
                .RuleFor(c => c.IdCommentaire, f => f.IndexFaker + 1)
                .RuleFor(c => c.Contenu, f => f.Lorem.Paragraphs(1))
                .RuleFor(c => c.Auteur, f => f.Name.FirstName())
                .RuleFor(c => c.DateCreation, f => f.Date.Past());

            // Génération des données cohérentes
            GenerateAllData();
        }

        /// <summary>
        /// Génère toutes les entités avec des relations cohérentes.
        /// </summary>
        private static void GenerateAllData()
        {
            var random = new Random();

            // Générer les styles
            Styles = StyleFake.Generate(20);

            // Générer les artistes
            Artistes = ArtisteFake.Generate(30);

            // Générer les titres et associer artistes et styles
            Titres = TitreFake.Generate(60);
            foreach (var titre in Titres)
            {
                var artiste = Artistes[random.Next(Artistes.Count)];
                titre.Artiste = artiste;
                titre.IdArtiste = artiste.IdArtiste;
                artiste.Titres.Add(titre);

                var randomStyles = Styles.OrderBy(_ => random.Next()).Take(random.Next(1, 4)).ToList();
                foreach (var style in randomStyles)
                {
                    titre.Styles.Add(style);
                    style.Titres.Add(titre);
                }
            }

            // Générer les commentaires et associer aux titres
            Commentaires = CommentaireFake.Generate(100);
            foreach (var commentaire in Commentaires)
            {
                var titre = Titres[random.Next(Titres.Count)];
                commentaire.Titre = titre;
                commentaire.IdTitre = titre.IdTitre;
                titre.Commentaires.Add(commentaire);
            }
        }
    }
}