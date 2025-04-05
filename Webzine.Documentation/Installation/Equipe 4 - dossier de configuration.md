# Dossier de configuration

Pour tester le code en local :

La commande suivante, exécutée dans le dossier Webzine.WebApplication, permet de
lancer le projet :

`dotnet run --urls "http://localhost:5000"`

Vous pourrez donc accéder au projet dans votre navigateur en local à l'adresse
suivante: http://localhost:5000

Pour une configuration avec des données mockées, il faut modifier l'appsettings.
![dossier appsetting](image.png)

Les options de configuration sont les suivantes :

Seeder : il existe deux options, "Local" et "Deezer", ou "Ignore" si vous ne
voulez pas seeder la base de données.

SGBD : soit SQLite, soit SQLServer.

Repository : Local ou Db, qui va dépendre de vos données mockées. Par défaut, il
prendra Db.

UsePathBase est utile en cas de déploiement de l'application et va permettre
d'ajouter un path de base au moment du déploiement.

NbTitresChroniquesParPaginations : spécifie le nombre de titres de chroniques à
afficher par page sur l'acceuil.

NbTitresPopulaires : spécifie le nombre de titres populaires à afficher.

NbTitresParPagination : spécifie le nombre de titres à afficher par page.

NbStylesParPagination : spécifie le nombre de styles à afficher par page.

NbArtistesParPagination : spécifie le nombre d'artistes à afficher par page.

NbCommentairesParPagination : spécifie le nombre de commentaires à afficher par
page.

Il est possible en mode Production d'implémenter un fichier exclusif appelé appsettings.Production.json.

Il suffit d'ajouter dans les variable d'environnement la variable ASPNETCORE_ENVIRONMENT avec la valeur "Production".