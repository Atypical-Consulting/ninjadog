# Ninjadog

The .NET REST API Source Generator

## The long road to MVP
- [x] Solution qui compile
- [ ] BMC
- [ ] SWOT
- [ ] Plan financier
- [ ] Tarification
- [ ] Branding
  - [x] Nom
  - [ ] Logo
  - [ ] TagLine
  - [ ] Fichiers de presentations (Word, PowerPoint...)
- [ ] Avantages de la solution
- [ ] Audience cible
- [ ] Ecrire une documentation
- [ ] Site web Shopify avec vente de produit dematerialise
- [ ] Une demo client
- [ ] Une demo pour les institutions financieres (Noshak...)

## How to start
- Creation d'un projet .NET6 Web API vide
- Installez le package NuGet Ninjadog
- Creez vos entites en suivant le pattern DDD
- Ajoutez un attribut au dessus de vos entites
- Compilez :-)

## The Goal

Permettre a chaque developpeur .NET de garder son temps pour ce qui est vraiment important: **le business et non pas le code plomberie**.

## Fonctionalites

- Genere les endpoints REST en se basant sur une approche DDD (Domain Driven Design).
  - Create
  - Read All
  - Read One
  - Update
  - Delete
- Genere les clients d'API en C# et TypeScript.
  - Client C# accessible via le endpoint: `/cs-client`
  - Client TypeScript accessible via le endpoint: `/ts-client`

## Avantages
- BEAUCOUP de temps gagne
- MOINS de code a maintenir
- MOINS d'erreur
- FINI le "code plomberie"
- REDUIT la duree de la mise en production
- Genere egalement les clients d'API en C# et TypeScript
