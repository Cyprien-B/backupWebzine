// <copyright file="Factory.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity.Fixtures
{
    using Bogus; // Générateur de données fictives

    /// <summary>
    /// Classe statique pour générer des données fictives.
    /// </summary>
    public static class Factory
    {
        // Déclaration des générateurs de données fictives statiques
        private static readonly Faker<Artiste> ArtisteFake;
        private static readonly Faker<Titre> TitreFake;
        private static readonly Faker<Titre> TitreIndividuelFake;
        private static readonly Faker<Style> StyleFake;
        private static readonly Faker<Commentaire> CommentaireFake;

        /// <summary>
        /// Initialise les générateurs de données fictives.
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

            // Configuration du générateur pour les titres
            TitreFake = new Faker<Titre>()
                .RuleFor(t => t.IdTitre, f => f.IndexFaker + 1)
                .RuleFor(t => t.Artiste, f => f.PickRandom(ArtisteFake))
                .RuleFor(t => t.Libelle, f => f.Lorem.Sentence(1, 3))
                .RuleFor(t => t.Chronique, f => f.Lorem.Sentences(10))
                .RuleFor(t => t.DateCreation, f => f.Date.Recent().AddHours(f.Random.Int(0, 23)).AddMinutes(f.Random.Int(0, 59)))
                .RuleFor(t => t.Duree, f => f.Random.UInt(60, 300))
                .RuleFor(t => t.DateSortie, f => f.Date.Past())
                .RuleFor(t => t.UrlJaquette, f => f.Image.PicsumUrl())
                .RuleFor(t => t.UrlEcoute, f => f.Internet.Url())
                .RuleFor(t => t.NbLectures, f => f.Random.UInt(0, 100))
                .RuleFor(t => t.NbLikes, f => f.Random.UInt(0, 100))
                .RuleFor(t => t.Album, f => f.Lorem.Word())
                .RuleFor(t => t.Commentaires, f => new List<Commentaire>())
                .RuleFor(t => t.Styles, f => new List<Style>());

            // Configuration du générateur pour les titres individuels
            TitreIndividuelFake = new Faker<Titre>()
                .RuleFor(t => t.IdTitre, f => f.IndexFaker + 1)
                .RuleFor(t => t.Artiste, f => f.PickRandom(ArtisteFake))
                .RuleFor(t => t.Libelle, f => f.Lorem.Word())
                .RuleFor(t => t.Chronique, f => f.Lorem.Sentences(70))
                .RuleFor(t => t.DateCreation, f => f.Date.Past().AddHours(f.Random.Int(0, 23)).AddMinutes(f.Random.Int(0, 59)))
                .RuleFor(t => t.Duree, f => f.Random.UInt(60, 300))
                .RuleFor(t => t.DateSortie, f => f.Date.Past())
                .RuleFor(t => t.UrlJaquette, f => f.Image.PicsumUrl())
                .RuleFor(t => t.UrlEcoute, f => "https://www.youtube.com/embed/" + f.Random.AlphaNumeric(11))
                .RuleFor(t => t.NbLectures, f => f.Random.UInt(0, 100))
                .RuleFor(t => t.NbLikes, f => f.Random.UInt(0, 100))
                .RuleFor(t => t.Album, f => f.Lorem.Word())
                .RuleFor(t => t.Commentaires, f => new List<Commentaire>());

            // Configuration du générateur pour les commentaires
            CommentaireFake = new Faker<Commentaire>()
                .RuleFor(c => c.IdCommentaire, f => f.IndexFaker + 1)
                .RuleFor(c => c.Contenu, f => f.Lorem.Paragraphs(1, 3))
                .RuleFor(c => c.Auteur, f => f.Name.FirstName())
                .RuleFor(c => c.DateCreation, f => f.Date.Past())
                .RuleFor(c => c.Titre, f => new Titre
                {
                    Libelle = f.Lorem.Word(),
                    IdTitre = f.Random.Int(1, 100),
                });

            // Configuration du générateur pour les styles
            StyleFake = new Faker<Style>()
                .RuleFor(s => s.IdStyle, f => f.IndexFaker + 1)
                .RuleFor(s => s.Libelle, f => f.Music.Genre());
        }

        /// <summary>
        /// Génération d'un titre.
        /// </summary>
        /// <returns>Un titre généré de manière aléatoire.</returns>
        public static Titre GenerateTitre()
        {
            var titre = TitreIndividuelFake.Generate();
            titre.IdArtiste = titre.Artiste.IdArtiste;
            titre.Artiste.Titres.Add(titre);

            var nbCommentaires = new Random().Next(2, 4);
            var nbStyles = new Random().Next(1, 4);
            var commentaires = CommentaireFake.Generate(nbCommentaires);
            var styles = StyleFake.Generate(nbStyles);
            foreach (var commentaire in commentaires)
            {
                commentaire.Titre = titre;
                commentaire.IdTitre = titre.IdTitre;
                titre.Commentaires.Add(commentaire);
            }

            foreach (var style in styles)
            {
                titre.Styles.Add(style);
            }

            return titre;
        }

        /// <summary>
        /// Génère une liste de titres en associant chaque titre à un artiste et à des styles.
        /// </summary>
        /// <param name="count">Paramètre décrivant le nombre de titres à générer.</param>
        /// <param name="artistes">Liste d'artistes à associer aux titres.</param>
        /// <param name="styles">Liste de styles à associer aux titres.</param>
        /// <returns>Retourne une liste de titres.</returns>
        public static List<Titre> GenerateTitres(int count, IEnumerable<Artiste> artistes, IEnumerable<Style> styles)
        {
            var list = new List<Titre>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                // Générer un titre
                var titre = GenerateTitre();

                // Associer un artiste aléatoire au titre
                var artiste = artistes.ElementAt(random.Next(artistes.Count()));
                titre.Artiste = artiste;
                titre.IdArtiste = artiste.IdArtiste;
                artiste.Titres.Add(titre); // Ajouter le titre à la liste des titres de l'artiste

                // Associer 1 à 3 styles aléatoires au titre
                var stylesAssocies = styles.OrderBy(x => random.Next()).Take(random.Next(1, 4)).ToList();
                foreach (var style in stylesAssocies)
                {
                    titre.Styles.Add(style);
                }

                list.Add(titre);
            }

            return list;
        }

        /// <summary>
        /// Génère une fausse liste d'artistes.
        /// </summary>
        /// <param name="count">Le nombre d'artiste voulu dans la liste.</param>
        /// <returns>Une liste d'artistes.</returns>
        public static List<Artiste> GenerateArtistes(int count)
        {
            var artistes = ArtisteFake.Generate(count);
            foreach (var artiste in artistes)
            {
                var titres = TitreFake.Generate(3);
                foreach (var titre in titres)
                {
                    titre.Artiste = artiste;
                    titre.IdArtiste = artiste.IdArtiste;
                    artiste.Titres.Add(titre);
                }
            }

            return artistes;
        }

        /// <summary>
        /// Génère une fausse liste de style.
        /// </summary>
        /// <param name="count">Nombre de style voulu dans la liste.</param>
        /// <returns>Une liste de style.</returns>
        public static List<Style> GenerateStyles(int count)
        {
            return StyleFake.Generate(count);
        }

        /// <summary>
        /// Génère un faux commentaire.
        /// </summary>
        /// <returns>Un faux commentaire.</returns>
        public static Commentaire GenerateCommentaire()
        {
            return CommentaireFake.Generate();
        }

        /// <summary>
        /// Génère une liste de commentaires en les associant à des titres.
        /// </summary>
        /// <param name="count">Paramètre décrivant le nombre de commentaires à générer.</param>
        /// <param name="titres">Liste de titres à associer aux commentaires.</param>
        /// <returns>Retourne une liste de commentaires.</returns>
        public static List<Commentaire> GenerateCommentaires(int count, IEnumerable<Titre> titres)
        {
            var list = new List<Commentaire>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                // Générer un commentaire
                var commentaire = GenerateCommentaire();

                // Associer un titre aléatoire au commentaire
                var titre = titres.ElementAt(random.Next(titres.Count()));
                commentaire.Titre = titre;
                commentaire.IdTitre = titre.IdTitre;

                // Ajouter le commentaire au titre
                titre.Commentaires.Add(commentaire);

                list.Add(commentaire);
            }

            return list;
        }
    }
}