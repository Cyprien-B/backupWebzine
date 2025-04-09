// <copyright file="WebzineDbContext.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>
namespace Webzine.EntityContext
{
    using Microsoft.EntityFrameworkCore;
    using Webzine.Entity;

    /// <summary>
    /// Contexte de la base de données SQLite pour l'application Webzine.
    /// </summary>
    /// <remarks>
    /// Initialise une nouvelle instance de la classe <see cref="WebzineDbContext"/>.
    /// </remarks>
    /// <param name="options">Options de configuration pour le contexte.</param>
    public class WebzineDbContext(DbContextOptions<WebzineDbContext> options) : DbContext(options)
    {
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

        /// <summary>
        /// Configure le modèle de la bdd.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artiste>(entity =>
            {
                entity.ToTable("Artiste");
                entity.HasKey(a => a.IdArtiste);
                entity.HasIndex(a => a.Nom).IsUnique();
                entity.Property(a => a.Biographie);
                entity.HasMany(a => a.Titres).WithOne(t => t.Artiste).HasForeignKey(t => t.IdArtiste);
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.ToTable("Style");
                entity.HasKey(s => s.IdStyle);
                entity.HasIndex(s => s.Libelle).IsUnique();
                entity.HasMany(s => s.Titres).WithMany(t => t.Styles);
            });

            modelBuilder.Entity<Titre>(entity =>
            {
                entity.ToTable("Titre");
                entity.HasKey(t => t.IdTitre);
                entity.HasIndex(t => new { t.Libelle, t.IdArtiste }).IsUnique();
                entity.Property(t => t.Duree);
                entity.Property(t => t.DateSortie);
                entity.Property(t => t.DateCreation);
                entity.Property(t => t.NbLectures);
                entity.Property(t => t.NbLikes);
                entity.Property(t => t.Album);
                entity.Property(t => t.Chronique);
                entity.Property(t => t.UrlJaquette);
                entity.Property(t => t.UrlEcoute);
                entity.HasMany(t => t.Styles).WithMany(s => s.Titres);
            });

            modelBuilder.Entity<Commentaire>(entity =>
            {
                entity.ToTable("Commentaire");
                entity.HasKey(c => c.IdCommentaire);
                entity.Property(c => c.Auteur);
                entity.Property(c => c.Contenu);
                entity.Property(c => c.DateCreation);

                entity.HasOne(c => c.Titre).WithMany(t => t.Commentaires).HasForeignKey(c => c.IdTitre);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
