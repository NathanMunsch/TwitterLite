# Cahier des charges projet TwitterLite

## Introduction

TwitterLite est une application similaire à Twitter qui permet aux utilisateurs d'envoyer des tweets, de liker des tweets et de consulter des tweets selon différents critères.

## Objectifs de l'application

L'objectif principal de TwitterLite est de fournir une plateforme conviviale et légère pour le partage de tweets. Les fonctionnalités principales incluent l'envoi de tweets, la possibilité de liker des tweets, la consultation de tweets avec des options de filtrage.

## Fonctionnalités

### Envoyer un tweet
- Les utilisateurs doivent pouvoir composer et envoyer des tweets.
- La longueur maximale d'un tweet sera de 450 caractères.

### Liker un tweet
- Les utilisateurs doivent pouvoir liker les tweets publiés par d'autres utilisateurs.

### Consulter des tweets
- Les utilisateurs doivent pouvoir consulter les tweets selon différents critères :
  - Filtrage par nombre de likes.
  - Filtrage des tweets les plus anciens ou les plus récents.

### S'inscrire
- Les utilisateurs doivent pouvoir créer un compte sur l'application en fournissant les informations nécessaires (nom, prénom, adresse e-mail, mot de passe, etc.).

### Se connecter
- Les utilisateurs inscrits doivent pouvoir se connecter à leur compte en utilisant leur nom d'utilisateur et leur mot de passe.

### Éditer les paramètres du compte
- Les utilisateurs doivent pouvoir modifier certains paramètres de leur compte, tels que le mot de passe et le nom d'utilisateur.

## Contraintes techniques
- Le backend de l'application sera développé en C# avec ASP.NET.
- Le frontend de l'application sera développé en Vue.js.
- La base de données sera gérée par Entity Framework (ORM) avec SQL Server en tant que base de données.
- L'application doit être compatible avec les principaux navigateurs web et les systèmes d'exploitation mobiles (iOS, Android).
- La sécurité des données sera prise en compte (MDP hashé, authentification par token JWT).

## Planning
- Début du développement de l'application : 02/01/2024
- Livraison de l'application finale : 03/06/2024
- Livraison du code source via un repository GitHub

## Editeurs
- Nathan Munsch
- Philipps Hugo
- Kaya Hasan
