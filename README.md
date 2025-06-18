# LaboBackEnd

Ce projet constitue le **backend ASP.NET Core** d'une application e-commerce développée en architecture 3 couches :  
**DAL (Data Access Layer) → BLL (Business Logic Layer) → API (Web Layer)**

---

## 🏗️ Architecture

- **DAL (`LaboBack.DAL`)**  
  Accès à la base de données via ADO.NET (`Connection`, `Command`)  
  Repositories typés (Utilisateur, Produit, Commande)

- **BLL (`LaboBack.BLL`)**  
  Contient la logique métier et les interfaces de services (IService)  
  Utilise des mappers entre modèles DAL et BLL

- **API (`LaboBack.API`)**  
  Contrôleurs RESTful sécurisés  
  Génération de JWT pour l'authentification  
  Modèles API (DTO) exposés au frontend

---

## 🔐 Authentification

- JWT signé avec secret (`TokenManager`)
- Stockage sécurisé des mots de passe via `BCrypt`
- Rôles supportés : `user` et `admin`
- Middleware `Authorize` pour sécuriser les endpoints

---

## 📦 Endpoints REST principaux

### 🔑 Authentification (`/api/Auth`)
- `POST /login` : Connexion utilisateur
- `POST /register` : Création d’un compte utilisateur

### 👤 Utilisateur (`/api/User`)
- `GET /{id}` : Profil utilisateur
- `PUT /{id}` : Mise à jour du profil
- `PUT /mdp/{id}` : Mise à jour du mot de passe

### 🛍️ Produits (`/api/Produit`)
- `GET /` : Tous les produits
- `GET /{id}` : Produit par id
- `POST /` : Ajouter un produit (admin)
- `PUT /{id}` : Modifier un produit
- `DELETE /{id}` : Supprimer un produit

### 📦 Commandes (`/api/Commande`)
- `POST /create` : Créer une commande
- `GET /utilisateur/{id}` : Commandes d’un utilisateur
- `GET /` : Toutes les commandes (admin)
- `GET /{id}` : Détails d’une commande
- `PUT /statut/{id}` : Mise à jour du statut

---

## 🧪 Test et lancement

### Lancer en local

Depuis le projet `LaboBack.API` :

```bash
dotnet run
```

> Le projet utilise par défaut l’environnement `Development` et écoute sur `https://localhost:7248`

---

## 🗃️ Base de données

Contenu SQL dans `LaboBack.ADO/Tables/` :

- `Utilisateur.sql`
- `Produit.sql`
- `Commande.sql`
- `MM_Commande_Produit.sql`

> Chaque entité est reliée par des clefs primaires/étrangères cohérentes.

---

## ✅ Bonnes pratiques appliquées

- Architecture en couches respectée
- Utilisation de DTO pour découpler les modèles internes
- Validation des modèles côté API
- Séparation des rôles et sécurisation par `Authorize`
- Mappers centralisés pour les transformations DAL ↔ BLL ↔ API
