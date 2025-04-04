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
        private static readonly HttpClient httpClient = new HttpClient();

        // Listes temporaires pour stocker les entités
        public static List<Artiste> Artistes { get; private set; } = new List<Artiste>();
        public static List<Style> Styles { get; private set; } = new List<Style>();
        public static List<Titre> Titres { get; private set; } = new List<Titre>();
        public static List<Commentaire> Commentaires { get; private set; } = new List<Commentaire>(); // Vide

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
            using (var scope = services.CreateScope())
            {
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
        }

        /// <summary>
        /// Récupère les données depuis l'API Deezer et remplit les listes d'entités.
        /// </summary>
        /// <returns>Une tâche asynchrone.</returns>
        public static async Task FetchDataFromDeezerAsync()
        {
            const int total = 2000; // Nombre total de titres à récupérer
            const int batchSize = 100; // Nombre de titres par lot

            var tracks = await GetTracksAsync(total, batchSize);

            // Extraction des artistes uniques
            Artistes = tracks
                .Select(t => new Artiste { Nom = t.Artist.Name })
                .GroupBy(a => a.Nom)
                .Select(g => g.First())
                .ToList();

            // Extraction des styles uniques
            Styles = tracks
                .SelectMany(t => t.Genres ?? new List<DeezerGenre>())
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
                    Styles = t.Genres?.Select(g => new Style { Libelle = g.Name }).ToList() ?? new List<Style>(),
                    DateCreation = DateTime.UtcNow,
                    DateSortie = DateTime.UtcNow // Pas disponible dans l'API Deezer
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

            Styles = Styles
                .GroupBy(s => s.Libelle.Trim(), StringComparer.OrdinalIgnoreCase) // Insensible à la casse et aux espaces
                .Select(g => g.First())
                .ToList();

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
            string url = $"https://api.deezer.com/chart/0/tracks?limit={limit}";

            while (allTracks.Count < total)
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Erreur lors de la récupération des titres Deezer : {response.StatusCode}");
                }

                string json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DeezerResponse>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (result?.Data == null || result.Data.Count == 0)
                {
                    break;
                }

                allTracks.AddRange(result.Data);

                // Vérifier si nous avons atteint la fin des résultats ou le total souhaité
                if (result.Data.Count < limit || allTracks.Count >= total)
                {
                    break;
                }

                // Mise à jour de l'URL pour paginer
                url = $"https://api.deezer.com/chart/0/tracks?limit={limit}&index={allTracks.Count}";

                await Task.Delay(500); // Pause pour éviter d'être bloqué par l'API
            }

            return allTracks.Take(total).ToList();
        }

        // Modèles JSON pour la désérialisation
        private class DeezerResponse { public List<DeezerTrack>? Data { get; set; } }

        private class DeezerTrack
        {
            public string Title { get; set; }
            public DeezerArtist Artist { get; set; }
            public DeezerAlbum Album { get; set; }
            public List<DeezerGenre>? Genres { get; set; }
        }

        private class DeezerArtist { public string Name { get; set; } }

        private class DeezerAlbum
        {
            public string Title { get; set; }
            public string Cover { get; set; }
        }

        private class DeezerGenre { public string Name { get; set; } }
    }
}
