// <copyright file="Program.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore;
using Webzine.EntityContext.Dbcontext;
using Webzine.Repository.Contracts;
using Webzine.Repository.Local;

/// <summary>
/// Contient le point d'entr�e principal de l'application.
/// </summary>
public static class Program
{
    /// <summary>
    /// Obtient ou définit le builder de l'application.
    /// </summary>
    public static WebApplicationBuilder? Builder { get; set; } = null;

    /// <summary>
    /// Obtient ou définit l'application compil�e par le builder.
    /// </summary>
    public static WebApplication? App { get; set; } = null;

    /// <summary>
    /// Point d'entr�e principal de l'application.
    /// </summary>
    /// <param name="args">Les arguments de ligne de commande pass�s au programme.</param>
    public static void Main(string[] args)
    {
        Builder = WebApplication.CreateBuilder(args);

        AddDependenciesInjections();

        // Gestion de la connection a SQLite
        Builder.Services.AddDbContext<SQLiteContext>(options =>
            options.UseSqlite(
                "Data Source=Webzine.db",
                b => b.MigrationsAssembly("Webzine.WebApplication")));

        Builder.Services.AddControllers();
        Builder.Services.AddControllersWithViews();

        App = Builder.Build();

        ConfigureMiddleware();

        ConfigureCustomRoutes();

        App.Run();
    }

    private static void AddDependenciesInjections()
    {
        Builder!.Services.AddScoped<IStyleRepository, LocalStyleRepository>();
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
        App.UseStaticFiles();
        App.UseRouting();

        // Gestion des erreurs 404
        App.UseStatusCodePages(context =>
        {
            if (context.HttpContext.Response.StatusCode == 404)
            {
                context.HttpContext.Response.Redirect("/Home/NotFound404");
            }

            return Task.CompletedTask;
        });
    }
}