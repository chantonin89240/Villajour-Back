Fonctionnalité: Ajouter une annonce
  Afin de publier une nouvelle annonce
  En tant qu'utilisateur
  Je veux pouvoir ajouter une annonce

Scénario: Ajouter une annonce avec des informations valides
  Étant donné une annonce avec les informations suivantes
    | Titre       | Description          |
    | TestAnnonce | Ceci est une annonce |
  Quand l'utilisateur ajoute l'annonce
  Alors la réponse doit être "200 OK"
  Et l'annonce doit contenir "TestAnnonce" et "Ceci est une annonce"
