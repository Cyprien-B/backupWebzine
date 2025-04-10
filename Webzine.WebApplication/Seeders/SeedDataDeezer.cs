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
    /// Seeder pour récupérer et valider des données depuis l'API Deezer.
    /// </summary>
    public static class SeedDataDeezer
    {
        private static readonly HttpClient HttpClient = new();

        /// <summary>
        /// Obtient la liste des artistes.
        /// Cette liste contient les artistes récupérés ou générés pour le peuplement de la base de données.
        /// </summary>
        public static List<Artiste> Artistes { get; private set; } = [];

        /// <summary>
        /// Obtient la liste des styles.
        /// Cette liste contient les styles musicaux associés aux titres.
        /// </summary>
        public static List<Style> Styles { get; private set; } = [];

        /// <summary>
        /// Obtient la liste des titres.
        /// Cette liste contient les titres récupérés ou générés pour le peuplement de la base de données.
        /// </summary>
        public static List<Titre> Titres { get; private set; } = [];

        /// <summary>
        /// Obtient la liste des commentaires.
        /// Cette liste est vide par défaut et peut être utilisée pour gérer les commentaires associés aux titres.
        /// </summary>
        public static List<Commentaire> Commentaires { get; private set; } = [];

        /// <summary>
        /// Initialise la base de données avec les données validées.
        /// </summary>
        /// <param name="services">Fournisseur de services.</param>
        /// <returns>Une tâche asynchrone.</returns>
        public static async Task Initialize(IServiceProvider services)
        {
            // Étape 1 : Récupérer les données depuis l'API Deezer
            await FetchDataFromDeezerAsync();

            // Étape 2 : Valider les données récupérées
            ValidateData();

            // Étape 3 : Insérer les données validées dans la base de données
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebzineDbContext>();

            if (!context.Titres.Any())
            {
                Console.WriteLine("Insertion des données validées dans la base de données...");
                SeedDatabase(context);
                Console.WriteLine("Base de données peuplée avec succès !");
            }
            else
            {
                Console.WriteLine("La base de données est déjà peuplée.");
            }
        }

        /// <summary>
        /// Récupère les données depuis l'API Deezer et remplit les listes d'entités.
        /// </summary>
        /// <returns>Une tâche asynchrone.</returns>
        public static async Task FetchDataFromDeezerAsync()
        {
            const int total = 3000; // Nombre total de titres à récupérer
            const int batchSize = 300; // Nombre de titres par lot

            var tracks = await GetTracksAsync(total, batchSize);

            // Extraction des artistes uniques
            Artistes = tracks
                .Select(t => new Artiste { Nom = t.Artist.Name })
                .GroupBy(a => a.Nom)
                .Select(g => g.First())
                .ToList();

            // Extraction des styles uniques
            Styles = tracks
                .SelectMany(t => t.Genres ?? [])
                .Select(g => new Style { Libelle = g.Name })
                .GroupBy(s => s.Libelle)
                .Select(g => g.First())
                .ToList();

            // Création des titres
            Titres = tracks.Select(t =>
            {
                var titre = new Titre
                {
                    Libelle = t.Title,
                    Album = t.Album.Title,
                    UrlJaquette = t.Album.Cover,
                    IdArtiste = 0, // Lien avec l'artiste sera vérifié plus tard
                    Styles = t.Genres?.Select(g => new Style { Libelle = g.Name }).ToList() ?? [],
                    DateCreation = DateTime.UtcNow,
                    DateSortie = DateTime.UtcNow, // Pas disponible dans l'API Deezer
                };

                return titre;
            }).ToList();
        }

        /// <summary>
        /// Valide les données dans les listes et supprime les doublons.
        /// </summary>
        public static void ValidateData()
        {
            // Supprimer les doublons dans chaque liste
            Artistes = Artistes
                .GroupBy(a => a.Nom.Trim(), StringComparer.OrdinalIgnoreCase) // Insensible à la casse et aux espaces
                .Select(g => g.First())
                .ToList();

            Styles =
            [
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
                new Style { IdStyle = 20, Libelle = "Hard Rock" },
                new Style { IdStyle = 21, Libelle = "Metal" },
                new Style { IdStyle = 22, Libelle = "Punk" },
                new Style { IdStyle = 23, Libelle = "Grunge" },
                new Style { IdStyle = 24, Libelle = "K-Pop" },
                new Style { IdStyle = 25, Libelle = "J-Pop" },
                new Style { IdStyle = 26, Libelle = "World Music" },
                new Style { IdStyle = 27, Libelle = "Latin Music" },
                new Style { IdStyle = 28, Libelle = "Afrobeat" },
                new Style { IdStyle = 29, Libelle = "Trap" },
                new Style { IdStyle = 30, Libelle = "Dubstep" },
                new Style { IdStyle = 31, Libelle = "Indie Rock" },
                new Style { IdStyle = 32, Libelle = "Lo-fi Hip-Hop" },
            ];

            var validTitres = new List<Titre>();

            foreach (var titre in Titres)
            {
                // Vider la liste des styles pour ce titre
                titre.Styles.Clear();

                // Vérifier si l'IdArtiste existe dans la liste des artistes
                var artiste = Artistes.FirstOrDefault(a => a.IdArtiste == titre.IdArtiste);

                if (artiste != null)
                {
                    // Associer l'objet Artiste au titre
                    titre.Artiste = artiste;
                    validTitres.Add(titre); // Ajouter le titre à la liste des titres valides
                    continue;
                }

                // Si l'IdArtiste n'existe pas, chercher par le nom de l'artiste
                artiste = Artistes.FirstOrDefault(a => a.Nom.Equals(titre.Artiste?.Nom, StringComparison.OrdinalIgnoreCase));

                if (artiste != null)
                {
                    // Associer l'objet Artiste au titre et mettre à jour l'IdArtiste
                    titre.IdArtiste = artiste.IdArtiste;
                    titre.Artiste = artiste;
                    validTitres.Add(titre); // Ajouter le titre à la liste des titres valides
                    continue;
                }

                // Si aucun artiste valide n'est trouvé, supprimer le titre (ne pas l'ajouter aux titres valides)
            }

            // Remplacer la liste des titres par les titres valides uniquement
            Titres = validTitres;

            Titres = Titres
                .GroupBy(t => new { t.Libelle, t.IdArtiste }) // Grouper par Libelle et IdArtiste pour vérifier unicité
                .Select(g => g.First()) // Conserver uniquement le premier titre pour chaque groupe unique
                .ToList();

            Titres.ForEach(t =>
            {
                if (t.Duree <= 0)
                {
                    t.Duree = 1;
                }

                if (t.Chronique.Length < 10)
                {
                    t.Chronique = "NOUS N'AVONS PAS DE CHRONIQUE POUR CET ARTISTE.";
                }
            });
        }

        /// <summary>
        /// Insère les données validées dans la base de données.
        /// </summary>
        private static void SeedDatabase(WebzineDbContext context)
        {
            context.Artistes.AddRange(Artistes);
            context.Styles.AddRange(Styles);
            context.Titres.AddRange(Titres);
            context.Commentaires.AddRange(Commentaires); // Vide pour le moment

            context.SaveChanges();
        }

        /// <summary>
        /// Récupère les titres depuis l'API Deezer par lots.
        /// </summary>
        private static async Task<List<DeezerTrack>> GetTracksAsync(int total, int limit)
        {
            var allTracks = new List<DeezerTrack>();
            int index = 0; // Indice de départ pour la pagination
            string url = $"https://api.deezer.com/chart/0/tracks?limit={limit}";

            while (allTracks.Count < total)
            {
                HttpResponseMessage response = await HttpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Erreur lors de la récupération des titres Deezer : {response.StatusCode}");
                }

                string json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DeezerResponse>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });

                if (result?.Data == null || result.Data.Count == 0)
                {
                    break; // Arrêter si aucune donnée n'est retournée
                }

                allTracks.AddRange(result.Data);

                // Vérifier si nous avons atteint la fin des résultats ou le total souhaité
                if (result.Data.Count < limit || allTracks.Count >= total)
                {
                    break;
                }

                // Mettre à jour l'URL pour paginer
                index += limit; // Incrémenter l'index par la limite
                url = $"https://api.deezer.com/chart/0/tracks?limit={limit}&index={index}";

                await Task.Delay(500); // Pause pour éviter d'être bloqué par l'API
            }

            return allTracks.Take(total).ToList();
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
