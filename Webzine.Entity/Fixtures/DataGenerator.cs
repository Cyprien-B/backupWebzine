// <copyright file="DataGenerator.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity.Fixtures
{
    using Bogus; // Générateur de données fictives

    /// <summary>
    /// Classe pour générer des données fictives.
    /// </summary>
    public class DataGenerator
    {
        // Déclaration des générateurs de données fictives
        private readonly Faker<Artiste> artisteFake;
        private readonly Faker<Titre> titreFake;
        private readonly Faker<Titre> titreIndividuelFake;
        private readonly Faker<Style> styleFake;
        private readonly Faker<Commentaire> commentaireFake;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="DataGenerator"/>.
        /// </summary>
        public DataGenerator()
        {
            // Initialisation du générateur aléatoire
            Randomizer.Seed = new Random(123);

            // Configuration du générateur pour les artistes
            this.artisteFake = new Faker<Artiste>()
                .RuleFor(a => a.IdArtiste, f => f.IndexFaker + 1) // ID unique
                .RuleFor(a => a.Nom, f => f.Name.FullName()) // Nom complet
                .RuleFor(a => a.Biographie, f => f.Lorem.Sentences()) // Biographie aléatoire
                .RuleFor(a => a.Titres, f => new List<Titre>()); // Liste de titres vide

            // Configuration du générateur pour les titres
            this.titreFake = new Faker<Titre>()
                .RuleFor(t => t.IdTitre, f => f.IndexFaker + 1) // ID unique
                .RuleFor(t => t.Artiste, f => f.PickRandom(this.artisteFake)) // Artiste aléatoire
                .RuleFor(t => t.Libelle, f => f.Lorem.Sentence(1, 3)) // Libellé aléatoire
                .RuleFor(t => t.Chronique, f => f.Lorem.Sentences(10)) // Chronique aléatoire
                .RuleFor(t => t.DateCreation, f => f.Date.Recent().AddHours(f.Random.Int(0, 23)).AddMinutes(f.Random.Int(0, 59))) // Date et heure de création passée
                .RuleFor(t => t.Duree, f => f.Random.UInt(60, 300)) // Durée aléatoire entre 60 et 600 secondes
                .RuleFor(t => t.DateSortie, f => f.Date.Past()) // Date de sortie passée
                .RuleFor(t => t.UrlJaquette, f => f.Image.PicsumUrl()) // URL de jaquette aléatoire
                .RuleFor(t => t.UrlEcoute, f => f.Internet.Url()) // URL d'écoute aléatoire
                .RuleFor(t => t.NbLectures, f => f.Random.UInt(0, 100)) // Nombre de lectures aléatoire
                .RuleFor(t => t.NbLikes, f => f.Random.UInt(0, 100)) // Nombre de likes aléatoire
                .RuleFor(t => t.Album, f => f.Lorem.Word()) // Album aléatoire
                .RuleFor(t => t.Commentaires, f => new List<Commentaire>()) // Liste de commentaires vide
                .RuleFor(t => t.Styles, f => new List<Style>());

            // Configuration du générateur pour les titres individuels
            this.titreIndividuelFake = new Faker<Titre>()
                .RuleFor(t => t.IdTitre, f => f.IndexFaker + 1) // ID unique
                .RuleFor(t => t.Artiste, f => f.PickRandom(this.artisteFake)) // Artiste aléatoire
                .RuleFor(t => t.Libelle, f => f.Lorem.Word()) // Libellé aléatoire
                .RuleFor(t => t.Chronique, f => f.Lorem.Sentences(70)) // Chronique aléatoire avec 15 phrases
                .RuleFor(t => t.DateCreation, f => f.Date.Past().AddHours(f.Random.Int(0, 23)).AddMinutes(f.Random.Int(0, 59))) // Date et heure de création passée
                .RuleFor(t => t.Duree, f => f.Random.UInt(60, 300)) // Durée aléatoire entre 60 et 600 secondes
                .RuleFor(t => t.DateSortie, f => f.Date.Past()) // Date de sortie passée
                .RuleFor(t => t.UrlJaquette, f => f.Image.PicsumUrl()) // URL de jaquette aléatoire
                .RuleFor(t => t.UrlEcoute, f => "https://www.youtube.com/embed/" + f.Random.AlphaNumeric(11)) // URL YouTube aléatoire
                .RuleFor(t => t.NbLectures, f => f.Random.UInt(0, 100)) // Nombre de lectures aléatoire
                .RuleFor(t => t.NbLikes, f => f.Random.UInt(0, 100)) // Nombre de likes aléatoire
                .RuleFor(t => t.Album, f => f.Lorem.Word()) // Album aléatoire
                .RuleFor(t => t.Commentaires, f => new List<Commentaire>()); // Liste de commentaires vide

            // Configuration du générateur pour les commentaires
            this.commentaireFake = new Faker<Commentaire>()
                .RuleFor(c => c.IdCommentaire, f => f.IndexFaker + 1) // ID unique
                .RuleFor(c => c.Contenu, f => f.Lorem.Paragraphs(1, 3)) // Contenu aléatoire
                .RuleFor(c => c.Auteur, f => f.Name.FirstName()) // Auteur aléatoire
                .RuleFor(c => c.DateCreation, f => f.Date.Past()) // Date de création passée
                .RuleFor(c => c.Titre, f => new Titre
                {
                    Libelle = f.Lorem.Word(), // Nom du titre généré aléatoirement
                    IdTitre = f.Random.Int(1, 100), // ID du titre aléatoire
                });

            // Configuration du générateur pour les styles
            this.styleFake = new Faker<Style>()
                .RuleFor(s => s.IdStyle, f => f.IndexFaker + 1) // ID unique
                .RuleFor(s => s.Libelle, f => f.Music.Genre()); // Genre musical aléatoire
        }

        /// <summary>
        /// Méthode pour générer un titre.
        /// </summary>
        /// <returns> Titre généré. </returns>
        public Titre GenerateTitre()
        {
            var titre = this.titreIndividuelFake.Generate(); // Générer un titre
            titre.IdArtiste = titre.Artiste.IdArtiste; // Assigner l'ID de l'artiste
            titre.Artiste.Titres.Add(titre); // Ajouter le titre à la liste de l'artiste

            // Générer 2-3 commentaires pour le titre
            var nbCommentaires = new Random().Next(2, 4); // Nombre aléatoire de commentaires
            var nbStyles = new Random().Next(1, 4);
            var commentaires = this.commentaireFake.Generate(nbCommentaires); // Générer les commentaires
            var styles = this.styleFake.Generate(nbStyles);
            foreach (var commentaire in commentaires)
            {
                commentaire.Titre = titre; // Assigner le titre au commentaire
                commentaire.IdTitre = titre.IdTitre; // Assigner l'ID du titre au commentaire
                titre.Commentaires.Add(commentaire); // Ajouter le commentaire au titre
            }

            foreach (var style in styles)
            {
                titre.Styles.Add(style);
            }

            return titre; // Retourner le titre généré
        }

        /// <summary>
        /// Méthode pour générer un artiste.
        /// </summary>
        /// <param name="count">Est le nombre de titres que l'on souhaite avoir dans la liste.</param>
        /// <returns> Une liste de titres.</returns>
        public List<Titre> GenerateTitres(int count)
        {
            var list = new List<Titre>();
            for (int i = 0; i < count; i++)
            {
                list.Add(this.GenerateTitre());
            }

            return list;
        }

        /// <summary>
        /// Méthode pour générer un artiste.
        /// </summary>
        /// <returns> Un artiste. </returns>
        public Artiste GenerateArtiste()
        {
            var artiste = this.artisteFake.Generate(); // Générer un artiste

            // Générer et assigner des titres à l'artiste
            var titres = this.titreFake.Generate(3); // Générer 3 titres
            foreach (var titre in titres)
            {
                titre.Artiste = artiste; // Assigner l'artiste au titre
                titre.IdArtiste = artiste.IdArtiste; // Assigner l'ID de l'artiste au titre
                artiste.Titres.Add(titre); // Ajouter le titre à la liste de l'artiste
            }

            return artiste; // Retourner l'artiste généré
        }

        /// <summary>
        /// Méthode pour générer une liste d'artistes.
        /// </summary>
        /// <param name="count"> Nombre d'artistes à générer. </param>
        /// <returns> Liste d'artistes générés. </returns>
        public List<Artiste> GenerateArtistes(int count)
        {
            var artistes = this.artisteFake.Generate(count); // Générer une liste d'artistes
            foreach (var artiste in artistes)
            {
                var titres = this.titreFake.Generate(3); // Générer 3 titres pour chaque artiste
                foreach (var titre in titres)
                {
                    titre.Artiste = artiste; // Assigner l'artiste au titre
                    titre.IdArtiste = artiste.IdArtiste; // Assigner l'ID de l'artiste au titre
                    artiste.Titres.Add(titre); // Ajouter le titre à la liste de l'artiste
                }
            }

            return artistes; // Retourner la liste d'artistes générés
        }

        /// <summary>
        /// Méthode pour générer un style.
        /// </summary>
        /// <returns> Un style généré. </returns>
        public Style GenerateStyle()
        {
            return this.styleFake.Generate(); // Retourner un style généré
        }

        /// <summary>
        /// Méthode pour générer une liste de styles.
        /// </summary>
        /// <param name="count"> Nombre de style à générer. </param>
        /// <returns> Liste des styles générés. </returns>
        public List<Style> GenerateStyles(int count)
        {
            return this.styleFake.Generate(count); // Retourner une liste de styles générés
        }

        /// <summary>
        /// Méthode pour générer un commentaire fake.
        /// </summary>
        /// <returns>Un faux commentaire.</returns>
        public Commentaire GenerateCommentaire()
        {
            // Générer et retourner un commentaire fake
            return this.commentaireFake.Generate();
        }

        /// <summary>
        /// Retourne une liste de faut commentaire.
        /// </summary>
        /// <param name="count">Est le nombre de commentaire qu'on souhaite avoir dans la liste.</param>
        /// <returns>Une liste de faux commentaire.</returns>
        public List<Commentaire> GenerateCommentaires(int count)
        {
            List<Commentaire> list = [];
            for (int i = 0; i < count; i++)
            {
                list.Add(this.GenerateCommentaire());
            }

            return list;
        }
    }
}