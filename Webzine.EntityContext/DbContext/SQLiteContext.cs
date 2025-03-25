// <copyright file="SQLiteContext.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>
namespace Webzine.EntityContext.Dbcontext
{
    using Microsoft.EntityFrameworkCore;
    using Webzine.Entity;

    /// <summary>
    /// Contexte de la base de données SQLite pour l'application Webzine.
    /// </summary>
    public class SQLiteContext : DbContext
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="SQLiteContext"/>.
        /// </summary>
        /// <param name="options">Options de configuration pour le contexte.</param>
        public SQLiteContext(DbContextOptions<SQLiteContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Obtient ou définit la collection d'artistes.
        /// </summary>
        public DbSet<Artiste> Artistes { get; set; }

        /// <summary>
        /// Obtient ou définit la collection de titres.
        /// </summary>
        public DbSet<Titre> Titres { get; set; }

        /// <summary>
        /// Obtient ou définit la collection de styles.
        /// </summary>
        public DbSet<Style> Styles { get; set; }

        /// <summary>
        /// Obtient ou définit la collection de commentaires.
        /// </summary>
        public DbSet<Commentaire> Commentaires { get; set; }
    }
}
