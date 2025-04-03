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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Webzine.Entity;
    using Webzine.EntityContext;

    /// <summary>
    /// Seeder pour récupérer et insérer des données depuis l'API Deezer.
    /// </summary>
    public static class SeedDataDeezer
    {
        private static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Initialise la base de données avec les données de Deezer.
        /// </summary>
        /// <param name="services">Fournisseur de services.</param>
        /// <returns>Une tâche asynchrone.</returns>
        public static async Task Initialize(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<WebzineDbContext>();

                // Vérifier si la base contient déjà des titres
                if (context.Titres.Any())
                {
                    return;
                }

                Console.WriteLine(" Récupération des titres depuis Deezer...");
                await SeedDatabaseAsync(context);
                Console.WriteLine(" Base de données peuplée avec succès depuis Deezer !");
            }
        }

        /// <summary>
        /// Récupère et insère 10 000 titres depuis l'API Deezer.
        /// </summary>
        private static async Task SeedDatabaseAsync(WebzineDbContext context)
        {
            List<DeezerTrack> tracks = await GetTracksAsync();

            // Extraction des artistes et styles uniques
            var artistes = tracks.Select(t => new Artiste { Nom = t.Artist.Name })
            .DistinctBy(a => a.Nom)
            .ToList();

            var styles = tracks.SelectMany(t => t.Genres ?? new List<DeezerGenre>())
            .Select(g => new Style { Libelle = g.Name })
            .DistinctBy(s => s.Libelle)
            .ToList();

            // Insertion des artistes
            foreach (var artiste in artistes)
            {
                var existingArtist = context.Artistes.FirstOrDefault(a => a.Nom == artiste.Nom);
                if (existingArtist == null)
                {
                    context.Artistes.Add(artiste);
                }
                else
                {
                    existingArtist.Nom = artiste.Nom; // Mettre à jour les informations de l'artiste si nécessaire
                    context.Artistes.Update(existingArtist);
                }
            }

            // Insertion des styles
            foreach (var style in styles)
            {
                var existingStyle = context.Styles.FirstOrDefault(s => s.Libelle == style.Libelle);
                if (existingStyle == null)
                {
                    context.Styles.Add(style);
                }
                else
                {
                    existingStyle.Libelle = style.Libelle; // Mettre à jour les informations du style si nécessaire
                    context.Styles.Update(existingStyle);
                }
            }

            await context.SaveChangesAsync();



            // Recharger les artistes et styles depuis la base avec leurs ID
            var artistDict = context.Artistes.ToDictionary(a => a.Nom);
            var styleDict = context.Styles.ToDictionary(s => s.Libelle);

            // Création des titres
            var titres = tracks.Select(t => new Titre
            {
                Libelle = t.Title,
                Album = t.Album.Title,
                UrlJaquette = t.Album.Cover,
                IdArtiste = artistDict[t.Artist.Name].IdArtiste,
                Styles = t.Genres?.Where(g => styleDict.ContainsKey(g.Name))
            .Select(g => styleDict[g.Name])
            .ToList() ?? new List<Style>(),
                DateCreation = DateTime.UtcNow,
                DateSortie = DateTime.UtcNow, // Pas dispo dans Deezer, on met une date fictive
            }).ToList();

            // Insertion en base
            context.Titres.AddRange(titres);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Récupère les titres depuis l'API Deezer par lots de 100.
        /// </summary>
        private static async Task<List<DeezerTrack>> GetTracksAsync(int total = 10000, int limit = 300)
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

                if (result?.Data == null)
                {
                    break;
                }

                allTracks.AddRange(result.Data);

                // Vérifier si nous avons atteint la fin des résultats
                if (result.Data.Count < limit)
                {
                    break;
                }

                // Obtenir l'URL de la prochaine page
                url = $"https://api.deezer.com/chart/0/tracks?limit={limit}&index={allTracks.Count}";

                await Task.Delay(500); // Pause pour éviter d'être bloqué par l'API
            }

            return allTracks.Take(total).ToList();
        }

        // Modèles JSON pour la désérialisation
        private class DeezerResponse { public List<DeezerTrack>? Data { get; set; } }

        private class DeezerTrack
        {
            public long Id { get; set; }

            public string Title { get; set; }

            public DeezerArtist Artist { get; set; }

            public DeezerAlbum Album { get; set; }

            public List<DeezerGenre>? Genres { get; set; }
        }

        private class DeezerArtist
        {
            public string Name { get; set; }
        }

        private class DeezerAlbum
        {
            public string Title { get; set; }
            public string Cover { get; set; }
        }

        private class DeezerGenre
        {
            public string Name { get; set; }
        }
    }
}
