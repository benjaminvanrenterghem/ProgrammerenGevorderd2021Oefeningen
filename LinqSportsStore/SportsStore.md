# Oefeningen - LINQ 

## Introductie SportsStore
De domeinlaag bevat klassen voor SportStore. 
Het domein vormt het hart van een online shop die sportartikelen verkoopt. Klanten (`Customer/City`) kunnen in de online shop browsen doorheen de producten (`Product/Category`), en deze naar wens toevoegen aan, of verwijderen uit, een winkelkarretje (`Cart/CartLine`). Desgewenst kan men de producten die in het winkelkarretje liggen ook effectief bestellen (`Order/OrderLine`). 

## Alvorens te starten met de oefeningen
1. Clone deze repository
2. Bekijk de solution in visual studio en lees volgende instructies **grondig** door.

### De solution(.sln) bevat volgende componenten
#### Models
Deze folder bevat de domein laag. 

![datamodel.png](./datamodel.png "Domein")
#### Data
Deze folder bevat 1 klasse `DataSourceProvider`. De DataSourceProvider heeft drie publieke read-only properties. Ze leveren de IEnumerables aan waarop we met Linq zullen werken. 
Als je wil zien wat deze properties bevatten kan je de private `Seed` methode raadplegen in deze klasse maar je hoeft in feite geen verdere kennis te hebben van deze klasse om de oefeningen te kunnen maken.
```cs
public static IEnumerable<Category> Categories {get;set;}
public static IEnumerable<Customer> Customers {get;set;}
public static IEnumerable<Product> Products {get;set;}
```
#### ViewModels
Deze folder is een klasse `ProductViewModel` gedefinieerd. Deze klasse heeft drie properties: `Name, Price en PriceEuro`: de naam van een product, zijn prijs in $, en de prijs in Euro (gebruikte koers 1 $ = 0.9 Euro). Tijdens de oefeningen zal je straks instanties van deze klasse moeten maken.
#### Program.cs
In de `Main` methode van deze klasse vind je bovenaan de declaratie van drie IEnumerables die je zal gebruiken met LINQ:
```cs
IEnumerable<Category> categories = DataSourceProvider.Categories;
IEnumerable<Customer> customers = DataSourceProvider.Customers;
IEnumerable<Product> products = DataSourceProvider.Products;
```

---

## Opgave
In de `Main` methode van `Program.cs` vind je per region een opgave. Je moet telkens de LINQ expressie schrijven die het gevraagde resultaat oplevert. Er is telkens reeds een stukje code voorzien die zorgt voor de uitvoer van het resultaat naar de console.  
Enkele opmerkingen:
Je kan jouw uitvoer vergelijken met de voorbeeld uitvoer die hieronder is gegeven. 

> De voornamen en steden voor de klanten deels random werden gekozen. Voor queries waar deze properties gebruikt worden kan je dus een afwijkend resultaat bekomen. Evalueer daarom zelf steeds goed je resultaat. Je kan steeds gebruik maken van breakpoints om de inhoud van `categories`, `customers` en `products` te bekijken.

> Na elke vraag staat er een `Console.ReadLine()`, dus druk enter om de oplossing op de volgende vraag te bekijken. 

> De resultaten voor de klanten kunnen afwijken daar voornamen en steden van klanten random worden aangemaakt.

### Voorbeeld uitvoer
![output1.png](./output1.png "Voorbeeld Uitvoer")

![output2.png](./output2.png "Voorbeeld Uitvoer Vervolg")

### Extra
1. Pas de klasse `Cart` aan, maak gebruik van expression bodied function members 
2. Maak een extension methode `Shuffle` die de klasse `IEnumerable<T>` uitbreidt en de lijst van T door mekaar haalt.
