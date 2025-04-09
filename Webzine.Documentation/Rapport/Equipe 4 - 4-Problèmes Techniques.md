## Problèmes Techniques

### Sprint 1

L'uniformisation du code lors du sprint 1 a été un véritable défi. Chaque fois
que nous codions de notre côté, nous nous rendions compte que nous n'avions pas
défini de conventions claires, ce qui rendait nécessaire l'harmonisation de nos
codes respectifs. Nous avons dû nous mettre d'accord sur les conventions à
respecter, et les revues de code ainsi que les échanges que nous avons eus nous
ont beaucoup aidés dans cette démarche.

Nous avons également rencontré des problèmes avec Bootstrap, une technologie que
nous ne maîtrisions pas encore. Cela a été une source de frustration, car dans
certains cas, l'utilisation de CSS pur nous aurait permis de résoudre certains
problèmes plus rapidement. Cependant, nous en avons discuté et avons choisi de
prioriser les demandes client les plus importantes pour la livraison du
sprint 1.

Un autre défi a été la densité du cahier des charges, qui a été une source de
stress constante. Nous avons passé énormément de temps à échanger sur tous les
détails à respecter, ce qui a parfois ralenti notre progression.

### Sprint 2

Un des problèmes que nous avons rencontrés a été l'intégration des titres dans
la base de données. En effet, notre factory de fausses données était trop
complexe. Nous avions voulu bien séparer toutes les factories, mais cela a
entraîné une complexité dans la sélection de l'ordre de génération des fausses
données. De plus, nous avions créé par erreur une seconde liste de titres liés à
l'artiste, et donc leur administration était impossible. Nous avons résolu le
problème en simplifiant la factory et en modifiant le code déjà créé pour que
tous les titres soient liés à une seule source de données.

### Sprint 3

Nous avons eu un débat entre Cyprien et Diego sur la mise en place d'un service
pour le dashboard, car Diego argumentait que cela est moins efficace que
d'utiliser directement les repositories. Cependant, étant donné que l'objectif
était de mettre en place un service pour répondre à une demande client, cette
solution a été retenue.

Mathéo a eu beaucoup de problèmes avec Deezer, qui ne correspondait pas à ce
dont nous avions besoin. Nous n'avons jamais réussi à aller chercher plus de 300
titres. Il était impossible d'accéder au style d'un titre autrement qu'en
faisant un appel à l'album lié au titre, et pour 10 000 titres, cela aurait fait
trop d'appels.

Gitea a été un vrai calvaire. Le fait de devoir le mettre en place par les ops
nous a fait perdre du temps sur le sprint 1. Pour le sprint 2, nous avons eu des
problèmes avec la connexion à Gitea, suite à une manipulation des ops. Ce
problème a perduré pour Esteban pendant une semaine. De plus, un des commits de
Diego a été attribué à Esteban sans aucune raison. Le stockage du code sur
GitHub ou Azure nous aurait évité au moins une journée de travail.
