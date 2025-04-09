// <copyright file="RateLimiterMiddleware.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Middlewares
{
    using System.Collections.Concurrent;

    /// <summary>
    /// Middleware permettant de limiter le nombre de requêtes effectuées par une même adresse IP
    /// dans une fenêtre temporelle donnée.
    /// </summary>
    /// <remarks>
    /// Initialise une nouvelle instance de la classe <see cref="RateLimiterMiddleware"/>.
    /// </remarks>
    /// <param name="next">Le middleware suivant dans le pipeline.</param>
    /// <param name="requestLimit">Le nombre maximum de requêtes autorisées.</param>
    /// <param name="timeWindow">La durée de la fenêtre temporelle.</param>
    public class RateLimiterMiddleware(RequestDelegate next, int requestLimit, TimeSpan timeWindow)
    {
        /// <summary>
        /// Dictionnaire concurrent contenant les informations de requêtes par adresse IP.
        /// </summary>
        private static readonly ConcurrentDictionary<string, RequestInfo> Requests = new();

        /// <summary>
        /// Délégué représentant le middleware suivant dans le pipeline.
        /// </summary>
        private readonly RequestDelegate next = next;

        /// <summary>
        /// Nombre maximum de requêtes autorisées par fenêtre temporelle.
        /// </summary>
        private readonly int requestLimit = requestLimit;

        /// <summary>
        /// Durée de la fenêtre temporelle pour limiter les requêtes.
        /// </summary>
        private readonly TimeSpan timeWindow = timeWindow;

        /// <summary>
        /// Méthode principale du middleware qui est appelée pour chaque requête HTTP.
        /// </summary>
        /// <param name="context">Le contexte HTTP actuel.</param>
        /// <returns>Une tâche représentant l'exécution du middleware.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(ipAddress))
            {
                // Si l'adresse IP ne peut pas être déterminée, autorisez la requête
                await this.next(context);
                return;
            }

            var now = DateTime.UtcNow;

            // Récupérer ou initialiser les informations de requêtes pour cette IP
            var requestInfo = Requests.GetOrAdd(ipAddress, new RequestInfo
            {
                Timestamp = now,
                RequestCount = 0,
            });

            lock (requestInfo)
            {
                // Réinitialiser la fenêtre temporelle si elle est expirée
                if (now - requestInfo.Timestamp > this.timeWindow)
                {
                    requestInfo.Timestamp = now;
                    requestInfo.RequestCount = 0;
                }

                // Incrémenter le compteur de requêtes
                requestInfo.RequestCount++;
            }

            // Vérifier si la limite est dépassée
            if (requestInfo.RequestCount > this.requestLimit)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests; // HTTP 429
                context.Response.ContentType = "text/plain; charset=utf-8"; // Spécifie le type de contenu et l'encodage (Oui parce que sinon, si jamais c'est affiché au client, on a des symbole bizarres).

                await context.Response.WriteAsync($"Vous avez effectué trop de requêtes. Limite de {this.requestLimit} requêtes par {this.timeWindow.TotalSeconds} secondes.");
                return;
            }

            // Passer au middleware suivant si la limite n'est pas atteinte
            await this.next(context);
        }

        private class RequestInfo
        {
            public DateTime Timestamp { get; set; } = DateTime.UtcNow;

            public int RequestCount { get; set; } = 0;
        }
    }
}