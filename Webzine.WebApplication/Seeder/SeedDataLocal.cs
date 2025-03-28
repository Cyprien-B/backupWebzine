using Webzine.Entity.Fixtures;
using Webzine.EntityContext.Dbcontext;

public static class SeedDataLocal
{
    public static void Initialize(IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<SQLiteContext>();

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