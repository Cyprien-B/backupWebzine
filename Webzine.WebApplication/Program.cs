// <copyright file="Program.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Webzine.EntityContext.Dbcontext;
using Webzine.Repository.Contracts;
using Webzine.Repository.Db;
using Webzine.Repository.Local;
using Webzine.WebApplication.Seeder;

/// <summary>
/// Contient le point d'entrée principal de l'application.
/// </summary>
public static class Program
{
    /// <summary>
    /// Obtient ou définit le builder de l'application.
    /// </summary>
    public static WebApplicationBuilder? Builder { get; set; } = null;

    /// <summary>
    /// Obtient ou définit l'application compilée par le builder.
    /// </summary>
    public static WebApplication? App { get; set; } = null;

    /// <summary>
    /// Obtient ou définit les log NLog.
    /// </summary>
    public static Logger? Logger { get; set; } = null;

    /// <summary>
    /// Point d'entrée principal de l'application.
    /// </summary>
    /// <param name="args">Les arguments de ligne de commande passés au programme.</param>
    public static void Main(string[] args)
    {
        Logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

        Builder = WebApplication.CreateBuilder(args);

        CheckConfigurations();

        ConfigureNLog();

        AddDependenciesInjections();

        Builder.Services.AddControllersWithViews(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        });

        ConfigureConnexionSGBD();

        Builder.Services.AddControllers();
        Builder.Services.AddControllersWithViews();

        App = Builder.Build();

        SeedDataBase();

        ConfigureMiddleware();

        ConfigureCustomRoutes();

        App.Run();
    }

    private static void AddDependenciesInjections()
    {
        if (Builder!.Configuration.GetValue<string>("App:Repository") == "Local")
        {
            Builder!.Services.AddScoped<IStyleRepository, LocalStyleRepository>();
            Builder!.Services.AddScoped<ITitreRepository, LocalTitreRepository>();
            Builder!.Services.AddScoped<IArtisteRepository, LocalArtisteRepository>();
            Builder!.Services.AddScoped<ICommentaireRepository, LocalCommentaireRepository>();
        }
        else
        {
            Builder!.Services.AddScoped<IStyleRepository, DbStyleRepository>();
            Builder!.Services.AddScoped<ITitreRepository, DbTitreRepository>();
            Builder!.Services.AddScoped<IArtisteRepository, DbArtisteRepository>();
            Builder!.Services.AddScoped<ICommentaireRepository, DbCommentaireRepository>();
        }
    }

    private static void CheckConfigurations()
    {
        // Vérification et configuration de SGBD
        string sgbd = Builder!.Configuration["App:SGBD"] ?? string.Empty;
        if (!new[] { "Local", "SQLite" }.Contains(sgbd, StringComparer.OrdinalIgnoreCase))
        {
            Builder.Configuration["App:SGBD"] = "SQLite";
        }

        Logger!.Info($"Utilisation du SGBD {Builder!.Configuration.GetValue<string>("App:SGBD") ?? string.Empty}");

        // Vérification et configuration de Seeder
        string seeder = Builder.Configuration["App:Seeder"] ?? string.Empty;
        if (string.IsNullOrEmpty(seeder) || (!seeder.Equals("Local", StringComparison.OrdinalIgnoreCase) && !seeder.Equals("Ignore", StringComparison.OrdinalIgnoreCase)))
        {
            Builder.Configuration["App:Seeder"] = "Local";
        }

        Logger!.Info($"Utilisation du Seeder {Builder!.Configuration.GetValue<string>("App:Seeder") ?? string.Empty}");

        // Vérification et configuration de Repository
        string rep = Builder.Configuration["App:Repository"] ?? string.Empty;
        if (!new[] { "Local", "Db" }.Contains(rep, StringComparer.OrdinalIgnoreCase))
        {
            Builder.Configuration["App:Repository"] = "Db";
        }

        Logger!.Info($"Utilisation du Repository {Builder!.Configuration.GetValue<string>("App:Repository") ?? string.Empty}");

        // Vérification et configuration de Repository
        string usepathbase = Builder.Configuration["App:UsePathBase"] ?? string.Empty;
        if (string.IsNullOrEmpty(usepathbase))
        {
            Logger!.Info("Pas de PathBase");
        }
        else
        {
            Logger!.Info($"Utilisation du PathBase {Builder!.Configuration.GetValue<string>("App:UsePathBase") ?? string.Empty}");
        }

        // Vérification et configuration de la chaîne de connexion
        string connexion = Builder.Configuration["App:DbConnexion"] ?? string.Empty;
        if (string.IsNullOrEmpty(connexion))
        {
            Builder.Configuration["App:DbConnexion"] = "Data Source=Webzine.db";
            Logger!.Info("Connexion par défaut");
        }
        else
        {
            Logger!.Info($"Connexion à {Builder!.Configuration.GetValue<string>("App:DbConnexion") ?? string.Empty}");
        }

        // Vérification et configuration de Repository
        if (!int.TryParse(Builder.Configuration["App:Pages:Home:NbTitresChroniquesParPaginations"], out int homeTitresChroniquesParPagination) || homeTitresChroniquesParPagination < 1)
        {
            homeTitresChroniquesParPagination = 3;
            Builder.Configuration["App:Pages:Home:NbTitresChroniquesParPaginations"] = homeTitresChroniquesParPagination.ToString();
        }

        // Vérification et configuration de Repository
        if (!int.TryParse(Builder.Configuration["App:Pages:Home:NbTitresPopulaires"], out int homeTitresPopulairesParPagination) || homeTitresPopulairesParPagination < 1)
        {
            homeTitresPopulairesParPagination = 3;
            Builder.Configuration["App:Pages:Home:NbTitresPopulaires"] = homeTitresPopulairesParPagination.ToString();
        }
    }

    private static void ConfigureConnexionSGBD()
    {
        Builder!.Services.AddDbContext<SQLiteContext>(options =>
            options.UseSqlite(
                Builder!.Configuration.GetValue<string>("App:DbConnexion") ?? string.Empty,
                b => b.MigrationsAssembly("Webzine.WebApplication")));
    }

    private static void ConfigureCustomRoutes()
    {
        App!.MapControllerRoute(
        name: "page",
        pattern: "page/{page:int}",
        defaults: new { controller = "Home", action = "Index" });

        App!.MapControllerRoute(
        name: "titre-id",
        pattern: "titre/{id:int}",
        defaults: new { controller = "Titre", action = "Index" });

        App!.MapControllerRoute(
        name: "titre-style",
        pattern: "titres/style/{nomStyle}",
        defaults: new { controller = "Titre", action = "Style" });

        App!.MapControllerRoute(
        name: "artiste",
        pattern: "artiste/{nomArtiste}",
        defaults: new { controller = "Artiste", action = "Index" });

        App!.MapControllerRoute(
        name: "titres-administration",
        pattern: "administration/artistes",
        defaults: new { area = "Administration", controller = "Artiste", action = "Index" });

        App!.MapControllerRoute(
        name: "titres-administration",
        pattern: "administration/commentaires",
        defaults: new { area = "Administration", controller = "Commentaire", action = "Index" });

        App!.MapControllerRoute(
        name: "titres-administration",
        pattern: "administration/styles",
        defaults: new { area = "Administration", controller = "Style", action = "Index" });

        App!.MapControllerRoute(
        name: "titres-administration",
        pattern: "administration/titres",
        defaults: new { area = "Administration", controller = "Titre", action = "Index" });

        App!.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        App!.MapDefaultControllerRoute();
    }

    private static void ConfigureMiddleware()
    {
        string usepathbase = Builder!.Configuration.GetValue<string>("App:UsePathBase") ?? string.Empty;
        if (usepathbase != string.Empty)
        {
            App!.UsePathBase(usepathbase);
        }

        App!.UseStaticFiles();
        App!.UseRouting();

        App!.UseExceptionHandler(usepathbase + "/Home/Error");

        // Gestion des erreurs 404
        App!.UseStatusCodePages(context =>
        {
            if (context.HttpContext.Response.StatusCode == 404)
            {
                context.HttpContext.Response.Redirect(usepathbase + "/Home/NotFound404");
            }

            return Task.CompletedTask;
        });
    }

    private static void ConfigureNLog()
    {
        Builder!.Logging.ClearProviders();
        Builder!.Host.UseNLog();
    }

    private static void SeedDataBase()
    {
        // Vider et recréer la base de données
        using (var scope = App!.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<SQLiteContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (Builder!.Configuration.GetValue<string>("App:Seeder") != "Ignore")
            {
                SeedDataLocal.Initialize(scope.ServiceProvider);
            }
        }
    }
}