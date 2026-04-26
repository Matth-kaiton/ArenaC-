# ArenaC-

Projet .NET 10 : ArenaC-

Description

Ce projet est une application .NET (backend) développée en C# destinée é simuler un combat entre 2 personnage

Prérequis

- .NET 10 SDK
- Visual Studio 2026 (extensions C#)
- SQL Server 

Installation

1. Cloner le dépôt :
   git clone https://github.com/Matth-kaiton/ArenaC-.git
2. Se placer dans le dossier du projet :
   cd ArenaC-
   

Exécution

- Ouvrir la solution dans Visual Studio et démarrer le débogage (F5).

Architecture MVVM

- View : Interface utilisateur (UI) avec WPF
- viewModel : Logique de présentation et liaison de données
- Model : Représentation des données


Packages NuGet utilisés

- Bcrypt.Net-Next : Pour le hachage des mots de passe
- CommunityToolkit.Mvvm : Pour faciliter l'implémentation du pattern MVVM
- MaterialDesignThemes : Pour l'interface utilisateur
- Microsoft.EntityFrameworkCore : Pour l'accès à la base de données
- Microsoft.EntityFrameworkCore.SqlServer : Fournisseur SQL Server pour Entity Framework Core
- Microsoft.EntityFrameworkCore.Tools : Outils pour Entity Framework Core
- Microsoft.EntityFrameworkCore.Design : Outils de conception pour Entity Framework Core

!!!! Important  !!!!

je n'ai pas fait de la fenètre pour rentré l'url de la bdd,
normalement la connection "Server=localhost\\SQLEXPRESS;Database=ExerciceHero;Trusted_Connection=True;TrustServerCertificate=True;"
cela vas chercher le serveur BDD local du nom "SQLEXPRESS" qui est le nom de base donnée par défaut a la création du serveur,
si vous avez un autre nom de serveur, il faudra le changer dans le fichier "ExerciceHeroContext.cs" à la ligne 28,
ensuite il se connecter a la base de données "ExerciceHero" qui doit est la meme que celle du sujet.

mon script d'init BD ne fait que rentré les donnée des heros et des spells , il ne crée pas la base de données ni les tables.
