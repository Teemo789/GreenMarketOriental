## Endpoints API

**Cart**
- POST /api/Cart/add: Ajoute un article au panier.
- GET /api/Cart/{userId}: Récupère le contenu du panier pour un utilisateur donné.
- PUT /api/Cart/remove: Supprime un article du panier.
- GET /api/Cart/total/{userId}: Calcule le montant total du panier pour un utilisateur donné.

**Order**
- POST /api/Order: Passe une nouvelle commande.
- GET /api/Order/{userId}: Récupère les commandes d'un utilisateur.
- GET /api/Order/history/{userId}: Récupère l'historique des commandes d'un utilisateur.
- GET /api/Order/{userId}/{orderId}: Récupère les détails d'une commande spécifique pour un utilisateur donné.

**Payment**
- POST /api/Payment: Effectue un nouveau paiement.
- GET /api/Payment/{id}: Récupère les détails d'un paiement spécifique.
- POST /api/Payment/pay/{orderId}: Effectue le paiement pour une commande spécifique.

**Product**
- POST /api/Product: Ajoute un nouveau produit.
- GET /api/Product: Récupère la liste de tous les produits.
- PUT /api/Product/{id}: Met à jour un produit existant.
- GET /api/Product/{id}: Récupère les détails d'un produit spécifique par son ID.
- POST /api/Product/{productId}/categories/{categoryId}: Ajoute un produit à une catégorie spécifique.
- GET /api/Product/category/{category}: Récupère les produits d'une catégorie spécifique.
- GET /api/Product/price: Récupère les produits par plage de prix.
- GET /api/Product/search: Recherche des produits par mot-clé.
- POST /api/Product/filter: Filtre les produits selon des critères spécifiques.

**Promotion**
- POST /api/Promotion/{promotionId}/products/{productId}: Ajoute un produit à une promotion spécifique.

**Wishlist**
- POST /api/Wishlist: Ajoute un produit à la liste de souhaits.
- GET /api/Wishlist/{userId}: Récupère la liste de souhaits pour un utilisateur donné.
