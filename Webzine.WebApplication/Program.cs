// <copyright file="Program.cs" company="Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ">
// Copyright (c) Equipe 4 - BARRAND, BORDET, COPPIN, DANNEAU, ERNST, FICHET, GRANDVEAU, SADIKAJ. All rights reserved.
// </copyright>

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Ajoute les services nécessaires pour permettre l'utilisation des
// controllers avec des vues.
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "page",
    pattern: "page/{page:int}",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "titre-id",
    pattern: "titre/{id:int}",
    defaults: new { controller = "Titre", action = "Style" });

app.MapControllerRoute(
    name: "titre-style",
    pattern: "titres/style/{nomStyle}",
    defaults: new { controller = "Titre", action = "Style" });

app.MapControllerRoute(
    name: "titres-administration",
    pattern: "administration/artistes",
    defaults: new { area = "Administration", controller = "Artiste", action = "Index" });

app.MapControllerRoute(
    name: "titres-administration",
    pattern: "administration/commentaires",
    defaults: new { area = "Administration", controller = "Commentaire", action = "Index" });

app.MapControllerRoute(
    name: "titres-administration",
    pattern: "administration/styles",
    defaults: new { area = "Administration", controller = "Style", action = "Index" });

app.MapControllerRoute(
    name: "titres-administration",
    pattern: "administration/titres",
    defaults: new { area = "Administration", controller = "Titre", action = "Index" });

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

app.Run();