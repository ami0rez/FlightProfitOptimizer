# Optimisateur de Profit de Vol ( FlightProfitOptimizer )

![FlightProfitOptimizer Icon](https://i.imgur.com/SxoDySnm.png)


## Table des matières
1. [Aperçu](#overview)
2. [Énoncé du problème](#problem-statement)
3. [Approche algorithmique](#algorithmic-approach)
	 - [Programmation dynamique et problème du sac à dos](#dynamic-programming-and-the-knapsack-problem)
4. [Structure de la solution](#solution-structure)
	 - [Structure du projet FlightProfitOptimizer](#flightprofitoptimizer-project-structure)
	 - [Structure du projet FlightProfitOptimizer.Tests](#flightprofitoptimizertests-project-structure)
5. [Utilisation de Xunit et Bogus](#utilizing-xunit-and-bogus)
6. [Conclusion](#conclusion)

---

## Aperçu
L'Optimisateur de Profit de Vol est une solution complète conçue pour maximiser les revenus des compagnies aériennes à partir des attributions de sièges. Cet outil répond au problème complexe de l'allocation des sièges où l'objectif est d'optimiser le revenu total tout en respectant diverses contraintes telles que les règles de placement familial et les besoins des passagers.

## Énoncé du problème
Dans l'industrie aérienne concurrentielle, maximiser le profit par vol est crucial. Le défi consiste à attribuer des sièges de manière à maximiser les revenus tout en garantissant la satisfaction des passagers en respectant les dispositions familiales et d'autres préférences individuelles.

## Approche algorithmique
Pour relever ce défi, l'Optimisateur de Profit de Vol utilise la programmation dynamique pour résoudre une variation du problème du sac à dos. Cette approche computationnelle est essentielle en raison des complexités et des contraintes de l'attribution des sièges de vol, nécessitant une méthode efficace pour trouver la combinaison la plus rentable d'attributions de sièges.

### Programmation dynamique et problème du sac à dos
La programmation dynamique est une méthode pour résoudre des problèmes complexes en les décomposant en sous-problèmes plus simples. Elle est particulièrement bien adaptée aux problèmes d'optimisation comme le problème du sac à dos, où l'objectif est de maximiser ou de minimiser une valeur particulière (dans ce cas, les revenus) tout en satisfaisant à des contraintes spécifiques (la capacité des sièges de l'avion).

Dans le contexte de l'Optimisateur de Profit de Vol, la programmation dynamique est utilisée pour trouver le sous-ensemble optimal d'attributions de sièges qui mène au revenu maximum sans dépasser la capacité disponible de sièges.

## Structure de la solution
La solution est structurée en deux projets principaux :
- `FlightProfitOptimizer` : Le projet principal contenant toutes les fonctionnalités nécessaires pour générer des attributions de sièges optimales.
- `FlightProfitOptimizer.Tests` : Le projet de test qui assure la fiabilité et la performance des fonctionnalités principales grâce à des tests unitaires complets.`` 

`FlightProfitOptimizer`
├── `src/FlightProfitOptimizer`
├── `tests/FlightProfitOptimizer.Tests`
├── `.dockerignore` # Docker context exclusion patterns 
├── `.gitignore` # Git repository exclusion patterns 
├── `Dockerfile` # Docker configuration for containerization 
└── `README.md` # Documentation for the project

 ### Structure du projet FlightProfitOptimizer
- `Models` : Définit les modèles de données utilisés dans tout le projet comme `Passenger`, `Family` et `Assignment`.
- `Services` : Contient des services comme `FlightOptimizer` et `PassengerGenerator` qui encapsulent la logique métier.
- `Utils` : Comprend des classes utilitaires comme `Generators` qui fournissent un soutien pour diverses opérations au sein des services.
- `Common` : Contient des constantes et des exceptions qui normalisent la gestion des erreurs et les messages.`` 

`FlightProfitOptimizer`/ 
├── `Common/` 
│ ├── `Constants/` # Application-wide constant values 
│ ├── `Exceptions/` # Custom exception classes for business logic 
│ └── `Extensions/` # Extension methods for built-in types 
├── `Models/` # Domain models representing entities like passengers and families 
├── `Services/` # Services that contain the core business logic of the application 
├── `Utils/` # Utilities and helper functions 
├── `Program.cs` # Entry point of the console application 
└──`appsettings.json` # Configuration settings 

 ### Structure du projet FlightProfitOptimizer.Tests
- `UnitTests` : Contient des tests unitaires pour les modèles et les services, garantissant que chaque composant se comporte comme prévu.
- `TestUtilities` : Fournit des utilitaires tels que `Factories` et `Mocks` pour soutenir les tests unitaires en générant des données de test.
- `TestData` : Comprend des fichiers JSON et des modèles pour représenter des scénarios de test et des ensembles de données.

`FlightProfitOptimizer.Tests/ `
├── `IntegrationTests/` # Integration tests that cover interactions between components 
├── 	`TestData/` # Data used for testing purposes 
│ ├── `Models/` # Test models 
│ └── `passengerData.json` # JSON file with mock passenger data 
├── `TestUtilities/` # Utilities to aid in writing tests 
│ ├── `Factories/` # Factories for creating test objects 
│ └──  `Mocks/` # Mocked objects and data for isolated tests 
└── `UnitTests/` # Tests for individual units of code 
├── `Models/` # Tests for model validation and behavior 
├── `Services/` # Tests for service logic and integration with models 
└── `Utils/` # Tests for utility classes and methods

 ### Utilisation de Xunit et Bogus
- `Xunit` : Un cadre de test choisi pour ses puissantes capacités de test qui facilitent le développement dirigé par les tests (TDD) et sa prise en charge des tests paramétrés.
- `Bogus` : Une bibliothèque utilisée pour générer de fausses données essentielles pour créer des scénarios de test réalistes et garantir que l'Optimisateur de Profit de Vol peut gérer une large gamme d'entrées.

## Conclusion
L'Optimisateur de Profit de Vol représente une solution robuste à un problème d'optimisation complexe dans l'industrie aérienne. En exploitant la programmation dynamique et une structure de projet stratégique, il fournit un outil efficace pour maximiser les revenus des vols tout en satisfaisant à des contraintes de sièges critiques.