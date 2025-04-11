// <copyright file="SeedDataDeezer.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Webzine.Entity;
    using Webzine.EntityContext;

    /// <summary>
    /// Classe pour peupler la base de données avec des données de Deezer.
    /// </summary>
    public static class SeedDataDeezer
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        /// <summary>
        /// Obtient la liste des artistes.
        /// </summary>
        public static List<Artiste> Artistes { get; private set; } = new List<Artiste>();

        /// <summary>
        /// Obtient la liste des styles.
        /// </summary>
        public static List<Style> Styles { get; private set; } = new List<Style>();

        /// <summary>
        /// Obtient la liste des titres.
        /// </summary>
        public static List<Titre> Titres { get; private set; } = new List<Titre>();

        /// <summary>
        /// Obtient la liste des commentaires.
        /// </summary>
        public static List<Commentaire> Commentaires { get; private set; } = new List<Commentaire>();

        /// <summary>
        /// Initialise la base de données avec les données de Deezer.
        /// </summary>
        /// <param name="services">Le fournisseur de services.</param>
        /// <returns>Une tâche représentant l'opération asynchrone.</returns>
        public static async Task Initialize(IServiceProvider services)
        {
            // 1. Récupération et préparation des données
            await FetchDataFromDeezerAsync().ConfigureAwait(false);
            ValidateData();

            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<WebzineDbContext>();

                // Si aucun titre n'existe, on persiste nos données.
                if (!context.Titres.Any())
                {
                    Console.WriteLine("Insertion des artistes et styles dans la base de données...");

                    // Enregistrer artistes et styles
                    context.Artistes.AddRange(Artistes);
                    context.Styles.AddRange(Styles);
                    context.SaveChanges();

                    // IMPORTANT :
                    // Après SaveChanges, les objets Artistes ont désormais leurs Ids attribués.
                    // On permet de récupérer la liste mise à jour depuis le contexte.
                    var artistesPersistes = context.Artistes.ToList();

                    // Réassigner l'artiste persistant à chaque titre en recherchant par nom.
                    foreach (var titre in Titres)
                    {
                        if (titre.Artiste != null)
                        {
                            // On recherche dans la liste persistée l'artiste dont le nom correspond.
                            var artistePersistant = artistesPersistes
                                .FirstOrDefault(a =>
                                    a.Nom.Equals(titre.Artiste.Nom, StringComparison.OrdinalIgnoreCase));
                            if (artistePersistant != null)
                            {
                                titre.Artiste = artistePersistant;
                                titre.IdArtiste = artistePersistant.IdArtiste;
                            }
                        }

                        // Pour les styles, on peut également réassigner
                        if (titre.Styles != null && titre.Styles.Any())
                        {
                            var stylesPersistes = context.Styles.ToList();
                            var stylesAssocies = new List<Style>();
                            foreach (var st in titre.Styles)
                            {
                                var stylePers = stylesPersistes.FirstOrDefault(s =>
                                    s.Libelle.Equals(st.Libelle, StringComparison.OrdinalIgnoreCase));
                                if (stylePers != null)
                                {
                                    stylesAssocies.Add(stylePers);
                                }
                            }

                            titre.Styles = stylesAssocies;
                        }
                    }

                    // Enregistrer les titres et commentaires
                    Console.WriteLine("Insertion des titres dans la base de données...");
                    context.Titres.AddRange(Titres);
                    context.Commentaires.AddRange(Commentaires);
                    context.SaveChanges();

                    Console.WriteLine("Base de données peuplée avec succès !");
                }
                else
                {
                    Console.WriteLine("La base de données est déjà peuplée.");
                }
            }
        }

        /// <summary>
        /// Récupère les données depuis l'API Deezer.
        /// </summary>
        /// <returns>Une tâche représentant l'opération asynchrone.</returns>
        public static async Task FetchDataFromDeezerAsync()
        {
            var tracks = await GetTracksAsync().ConfigureAwait(false);

            // Créer la liste d'artistes uniques selon le nom
            Artistes = tracks
                .Select(t => t.Artist.Name)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(nom => new Artiste { Nom = nom })
                .ToList();

            // Créer la liste de styles uniques à partir des genres
            Styles = tracks
                .SelectMany(t => t.Genres ?? new List<DeezerGenre>())
                .Select(g => g.Name)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(name => new Style { Libelle = name })
                .ToList();

            var random = new Random();

            // Création des titres : on associe initialement l'artiste de la liste Artistes (non persistée)
            Titres = tracks.Select(t =>
            {
                var artisteAssocie = Artistes
                    .FirstOrDefault(a => a.Nom.Equals(t.Artist.Name, StringComparison.OrdinalIgnoreCase));

                var styleAleatoire = Styles.Count > 0
    ? Styles[random.Next(Styles.Count)]
    : new Style { Libelle = "Inconnu" };

                var titre = new Titre
                {
                    Libelle = t.Title,
                    Album = t.Album.Title,
                    UrlJaquette = t.Album.Cover,
                    Artiste = artisteAssocie!,

                    // On n'affecte pas IdArtiste ici ; ce sera mis à jour après persistance.
                    Styles = new List<Style> { styleAleatoire },
                    DateCreation = DateTime.UtcNow,
                    DateSortie = DateTime.UtcNow,
                    Duree = 0,
                    Chronique = string.Empty,
                };

                return titre;
            }).ToList();
        }

        /// <summary>
        /// Valide les données récupérées.
        /// </summary>
        public static void ValidateData()
        {
            // Supprimer les doublons dans les artistes
            Artistes = Artistes
                .GroupBy(a => a.Nom.Trim(), StringComparer.OrdinalIgnoreCase)
                .Select(g => g.First())
                .ToList();

            // Pour test, ici on garde la liste prédéfinie des styles
            Styles = new List<Style>
            {
                new Style { IdStyle = 1, Libelle = "Pop" },
                new Style { IdStyle = 2, Libelle = "Rock" },
                new Style { IdStyle = 3, Libelle = "Jazz" },
                new Style { IdStyle = 4, Libelle = "Blues" },
                new Style { IdStyle = 5, Libelle = "Classique" },
                new Style { IdStyle = 6, Libelle = "Hip-Hop" },
                new Style { IdStyle = 7, Libelle = "Rap" },
                new Style { IdStyle = 8, Libelle = "R&B" },
                new Style { IdStyle = 9, Libelle = "Soul" },
                new Style { IdStyle = 10, Libelle = "Reggae" },
                new Style { IdStyle = 11, Libelle = "Ska" },
                new Style { IdStyle = 12, Libelle = "Funk" },
                new Style { IdStyle = 13, Libelle = "Country" },
                new Style { IdStyle = 14, Libelle = "Electro" },
                new Style { IdStyle = 15, Libelle = "House" },
                new Style { IdStyle = 16, Libelle = "Techno" },
                new Style { IdStyle = 17, Libelle = "Trance" },
                new Style { IdStyle = 18, Libelle = "Ambient" },
                new Style { IdStyle = 19, Libelle = "Chillout" },
            };

            // On filtre les titres sans artiste.
            Titres = Titres
                .Where(t => t.Artiste != null && !string.IsNullOrWhiteSpace(t.Artiste.Nom))
                .GroupBy(t => new { t.Libelle, NomArtiste = t.Artiste.Nom })
                .Select(g => g.First())
                .ToList();

            // Compléter les données manquantes dans les titres.
            foreach (var titre in Titres)
            {
                if (titre.Duree <= 0)
                {
                    titre.Duree = 1;
                }

                if (string.IsNullOrWhiteSpace(titre.Chronique) || titre.Chronique.Length < 10)
                {
                    titre.Chronique = "NOUS N'AVONS PAS DE CHRONIQUE POUR CET ARTISTE.";
                }
            }
        }

        private static async Task<List<DeezerTrack>> GetTracksAsync()
        {
            var urls = new List<string>
            {
                "https://api.deezer.com/search?q=%7Blove%7D&limit=300&index=300",
                "https://api.deezer.com/chart/0/tracks?limit=300&index=100",
                "https://api.deezer.com/search?q=%7Bintro%7D&limit=300&index=0",
                "https://api.deezer.com/search?q=%7Blove%7D&limit=300&index=0",
                "https://api.deezer.com/chart/0/tracks?limit=300",
                "https://api.deezer.com/search?q=rock&limit=300&index=0",
                "https://api.deezer.com/search?q=you&limit=300&index=100",
                "https://api.deezer.com/search?q=rain&limit=300&index=100",
                "https://api.deezer.com/search?q=cloud&limit=300&index=0",
                "https://api.deezer.com/search?q=rain&limit=300&index=200",
                "https://api.deezer.com/search?q=sun&limit=300&index=100",
                "https://api.deezer.com/search?q=burn&limit=300&index=0",
                "https://api.deezer.com/search?q=%7hello%7D&limit=300&index=0",
                "https://api.deezer.com/search?q=%7by%7D&limit=300&index=0",
                "https://api.deezer.com/search?q=%7despaire%7D&limit=300&index=0",
                "https://api.deezer.com/search?q=%7deso%7D&limit=300&index=0",
                "https://api.deezer.com/search?q=label%3A%22Universal%22&index=52",
                "https://api.deezer.com/search?q=label%3A%22Universal%22&index=27",
            };

            var allTracks = new List<DeezerTrack>();

            foreach (var url in urls)
            {
                HttpResponseMessage response = await HttpClient.GetAsync(url).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Erreur lors de la récupération des titres Deezer : {response.StatusCode} pour l'URL {url}");
                }

                string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonSerializer.Deserialize<DeezerResponse>(
                    json, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                if (result?.Data != null && result.Data.Count > 0)
                {
                    allTracks.AddRange(result.Data);
                }
            }

            return allTracks;
        }

        /// <summary>
        /// Représente la réponse de l'API Deezer contenant une liste de titres.
        /// </summary>
        private class DeezerResponse
        {
            /// <summary>
            /// Obtient ou définit la liste des titres retournés par l'API Deezer.
            /// </summary>
            public List<DeezerTrack>? Data { get; set; }
        }

        /// <summary>
        /// Représente un titre musical retourné par l'API Deezer.
        /// </summary>
        private class DeezerTrack
        {
            /// <summary>
            /// Obtient ou définit le titre du morceau.
            /// </summary>
            public string Title { get; set; } = string.Empty;

            /// <summary>
            /// Obtient ou définit l'artiste associé au morceau.
            /// </summary>
            public DeezerArtist Artist { get; set; } = new();

            /// <summary>
            /// Obtient ou définit l'album auquel appartient le morceau.
            /// </summary>
            public DeezerAlbum Album { get; set; } = new();

            /// <summary>
            /// Obtient ou définit la liste des genres associés au morceau.
            /// Peut être null si aucun genre n'est associé.
            /// </summary>
            public List<DeezerGenre>? Genres { get; set; }
        }

        /// <summary>
        /// Représente un artiste musical retourné par l'API Deezer.
        /// </summary>
        private class DeezerArtist
        {
            /// <summary>
            /// Obtient ou définit le nom de l'artiste.
            /// </summary>
            public string Name { get; set; } = string.Empty;
        }

        /// <summary>
        /// Représente un album musical retourné par l'API Deezer.
        /// </summary>
        private class DeezerAlbum
        {
            /// <summary>
            /// Obtient ou définit le titre de l'album.
            /// </summary>
            public string Title { get; set; } = string.Empty;

            /// <summary>
            /// Obtient ou définit l'URL de la jaquette de l'album.
            /// </summary>
            public string Cover { get; set; } = string.Empty;
        }

        /// <summary>
        /// Représente un genre musical retourné par l'API Deezer.
        /// </summary>
        private class DeezerGenre
        {
            /// <summary>
            /// Obtient ou définit le nom du genre musical.
            /// </summary>
            public string Name { get; set; } = string.Empty;
        }
    }
}
