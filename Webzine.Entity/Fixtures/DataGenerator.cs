// <copyright file="DataGenerator.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.Entity.Fixtures
{
    using Bogus; // G�n�rateur de donn�es fictives

    /// <summary>
    /// Classe pour g�n�rer des donn�es fictives.
    /// </summary>
    public class DataGenerator
    {
        // D�claration des g�n�rateurs de donn�es fictives
        private readonly Faker<Artiste> artisteFake;
        private readonly Faker<Titre> titreFake;
        private readonly Faker<Titre> titreIndividuelFake;
        private readonly Faker<Style> styleFake;
        private readonly Faker<Commentaire> commentaireFake;

        /// <summary>
        /// Initialise une nouvelle instance de classe. <see cref="DataGenerator"/>.
        /// </summary>
        public DataGenerator()
        {
            // Initialisation du g�n�rateur al�atoire
            Randomizer.Seed = new Random(123);

            // Configuration du g�n�rateur pour les artistes
            this.artisteFake = new Faker<Artiste>()
                .RuleFor(a => a.IdArtiste, f => f.IndexFaker + 1) // ID unique
                .RuleFor(a => a.Nom, f => f.Name.FullName()) // Nom complet
                .RuleFor(a => a.Biographie, f => f.Lorem.Sentences()) // Biographie al�atoire
                .RuleFor(a => a.Titres, f => new List<Titre>()); // Liste de titres vide

            // Configuration du g�n�rateur pour les titres
            this.titreFake = new Faker<Titre>()
                .RuleFor(t => t.IdTitre, f => f.IndexFaker + 1) // ID unique
                .RuleFor(t => t.Artiste, f => f.PickRandom(this.artisteFake)) // Artiste al�atoire
                .RuleFor(t => t.Libelle, f => f.Lorem.Sentence(1, 3)) // Libell� al�atoire
                .RuleFor(t => t.Chronique, f => f.Lorem.Sentences()) // Chronique al�atoire
                .RuleFor(t => t.DateCreation, f => f.Date.Recent()) // Date de cr�ation pass�e
                .RuleFor(t => t.Duree, f => f.Random.UInt(60, 300)) // Dur�e al�atoire entre 60 et 600 secondes
                .RuleFor(t => t.DateSortie, f => f.Date.Past()) // Date de sortie pass�e
                .RuleFor(t => t.UrlJaquette, f => f.Image.PicsumUrl()) // URL de jaquette al�atoire
                .RuleFor(t => t.UrlEcoute, f => f.Internet.Url()) // URL d'�coute al�atoire
                .RuleFor(t => t.NbLectures, f => f.Random.UInt(0, 100)) // Nombre de lectures al�atoire
                .RuleFor(t => t.NbLikes, f => f.Random.UInt(0, 100)) // Nombre de likes al�atoire
                .RuleFor(t => t.Album, f => f.Lorem.Word()) // Album al�atoire
                .RuleFor(t => t.Commentaires, f => new List<Commentaire>()); // Liste de commentaires vide

            // Configuration du g�n�rateur pour les titres individuels
            this.titreIndividuelFake = new Faker<Titre>()
                .RuleFor(t => t.IdTitre, f => f.IndexFaker + 1) // ID unique
                .RuleFor(t => t.Artiste, f => f.PickRandom(this.artisteFake)) // Artiste al�atoire
                .RuleFor(t => t.Libelle, f => f.Lorem.Word()) // Libell� al�atoire
                .RuleFor(t => t.Chronique, f => f.Lorem.Sentences()) // Chronique al�atoire
                .RuleFor(t => t.DateCreation, f => f.Date.Past()) // Date de cr�ation pass�e
                .RuleFor(t => t.Duree, f => f.Random.UInt(60, 300)) // Dur�e al�atoire entre 60 et 600 secondes
                .RuleFor(t => t.DateSortie, f => f.Date.Past()) // Date de sortie pass�e
                .RuleFor(t => t.UrlJaquette, f => f.Image.PicsumUrl()) // URL de jaquette al�atoire
                .RuleFor(t => t.UrlEcoute, f => f.Internet.Url()) // URL d'�coute al�atoire
                .RuleFor(t => t.NbLectures, f => f.Random.UInt(0, 100)) // Nombre de lectures al�atoire
                .RuleFor(t => t.NbLikes, f => f.Random.UInt(0, 100)) // Nombre de likes al�atoire
                .RuleFor(t => t.Album, f => f.Lorem.Word()) // Album al�atoire
                .RuleFor(t => t.Commentaires, f => new List<Commentaire>()); // Liste de commentaires vide

            // Configuration du g�n�rateur pour les commentaires
            this.commentaireFake = new Faker<Commentaire>()
                .RuleFor(c => c.IdCommentaire, f => f.IndexFaker + 1) // ID unique
                .RuleFor(c => c.Contenu, f => f.Lorem.Paragraphs(1, 3)) // Contenu al�atoire
                .RuleFor(c => c.Auteur, f => f.Name.FirstName()) // Auteur al�atoire
                .RuleFor(c => c.DateCreation, f => f.Date.Past()); // Date de cr�ation pass�e

            // Configuration du g�n�rateur pour les styles
            this.styleFake = new Faker<Style>()
                .RuleFor(s => s.IdStyle, f => f.IndexFaker + 1) // ID unique
                .RuleFor(s => s.Libelle, f => f.Music.Genre()); // Genre musical al�atoire
        }

        /// <summary>
        /// M�thode pour g�n�rer un titre.
        /// </summary>
        /// <returns> Titre g�n�r�. </returns>
        public Titre GenerateTitre()
        {
            var titre = this.titreIndividuelFake.Generate(); // G�n�rer un titre
            titre.IdArtiste = titre.Artiste.IdArtiste; // Assigner l'ID de l'artiste
            titre.Artiste.Titres.Add(titre); // Ajouter le titre � la liste de l'artiste

            // G�n�rer 2-3 commentaires pour le titre
            var nbCommentaires = new Random().Next(2, 4); // Nombre al�atoire de commentaires
            var commentaires = this.commentaireFake.Generate(nbCommentaires); // G�n�rer les commentaires
            foreach (var commentaire in commentaires)
            {
                commentaire.Titre = titre; // Assigner le titre au commentaire
                commentaire.IdTitre = titre.IdTitre; // Assigner l'ID du titre au commentaire
                titre.Commentaires.Add(commentaire); // Ajouter le commentaire au titre
            }

            return titre; // Retourner le titre g�n�r�
        }

        /// <summary>
        /// M�thode pour g�n�rer un artiste.
        /// </summary>
        /// <returns> Un artiste. </returns>
        public Artiste GenerateArtiste()
        {
            var artiste = this.artisteFake.Generate(); // G�n�rer un artiste

            // G�n�rer et assigner des titres � l'artiste
            var titres = this.titreFake.Generate(3); // G�n�rer 3 titres
            foreach (var titre in titres)
            {
                titre.Artiste = artiste; // Assigner l'artiste au titre
                titre.IdArtiste = artiste.IdArtiste; // Assigner l'ID de l'artiste au titre
                artiste.Titres.Add(titre); // Ajouter le titre � la liste de l'artiste
            }

            return artiste; // Retourner l'artiste g�n�r�
        }

        /// <summary>
        /// M�thode pour g�n�rer une liste d'artistes.
        /// </summary>
        /// <param name="count"> Nombre d'artistes � g�n�rer. </param>
        /// <returns> Liste d'artistes g�n�r�s. </returns>
        public List<Artiste> GenerateArtistes(int count)
        {
            var artistes = this.artisteFake.Generate(count); // G�n�rer une liste d'artistes
            foreach (var artiste in artistes)
            {
                var titres = this.titreFake.Generate(3); // G�n�rer 3 titres pour chaque artiste
                foreach (var titre in titres)
                {
                    titre.Artiste = artiste; // Assigner l'artiste au titre
                    titre.IdArtiste = artiste.IdArtiste; // Assigner l'ID de l'artiste au titre
                    artiste.Titres.Add(titre); // Ajouter le titre � la liste de l'artiste
                }
            }

            return artistes; // Retourner la liste d'artistes g�n�r�s
        }

        /// <summary>
        /// M�thode pour g�n�rer un style.
        /// </summary>
        /// <returns> Un style g�n�r�. </returns>
        public Style GenerateStyle()
        {
            return this.styleFake.Generate(); // Retourner un style g�n�r�
        }

        /// <summary>
        /// M�thode pour g�n�rer une liste de styles.
        /// </summary>
        /// <param name="count"> Nombre de style � g�n�rer. </param>
        /// <returns> Liste des styles g�n�r�s. </returns>
        public List<Style> GenerateStyles(int count)
        {
            return this.styleFake.Generate(count); // Retourner une liste de styles g�n�r�s
        }
    }
}