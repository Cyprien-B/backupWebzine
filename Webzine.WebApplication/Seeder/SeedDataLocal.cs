// <copyright file="SeedDataLocal.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

namespace Webzine.WebApplication.Seeder
{
    using Webzine.Entity.Fixtures;
    using Webzine.EntityContext;

    /// <summary>
    /// Seeder local.
    /// </summary>
    public static class SeedDataLocal
    {
        /// <summary>
        /// Classe statique pour seeder une base de données.
        /// </summary>
        /// <param name="services">Provider de service de type <see cref="IServiceProvider"/>.</param>
        public static void Initialize(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<WebzineDbContext>();

                // Récupérer les données générées par les factories
                var artistes = Factory.Artistes;
                var styles = Factory.Styles;
                var titres = Factory.Titres;
                var commentaires = Factory.Commentaires;

                // Ajouter les données à la base de données
                context.Artistes.AddRange(artistes);
                context.Styles.AddRange(styles);
                context.Titres.AddRange(titres);
                context.Commentaires.AddRange(commentaires);

                // Sauvegarder les modifications
                context.SaveChanges();
            }
        }
    }
}