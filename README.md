Sydvest Bo OOP Eksamensprojekt
Dette repository indeholder koden og databasefilen til eksamensprojektet i OOP for Sydvest Bo.

Indhold
Projektbeskrivelse
Krav til projektet
Installation og opsætning
Databaseinformation
Brug af applikationen
Teknisk dokumentation
Projektbeskrivelse
Projektet er en ejendomsadministrationsløsning for Sydvest Bo. Applikationen gør det muligt at administrere:

Ejere
Lejligheder og lejlighedskomplekser
Inspektører
Områder
Sæsonkategorier
Reservationer af boliger
Krav til projektet
Projektet er udarbejdet med følgende krav i tankerne:

Objektorienteret programmering i C#
CRUD-funktionalitet for alle hovedkomponenter
Databaseforbindelse via Microsoft SQL Server
Installation og opsætning
1. Krav:
Visual Studio 2022 eller nyere
Microsoft SQL Server (SSMS eller en anden klient)
.NET 6.0 eller nyere
2. Kloning af repository
Hvis du bruger Git:

bash
Kopiér
Rediger
git clone https://github.com/DIT-BRUGERNAVN/DIT-REPOSITORY.git
Alternativt kan du downloade projektet som en ZIP-fil fra GitHub.

3. Databaseopsætning
Åbn SQL Server Management Studio (SSMS).
Kør sydvest_bo_dump.sql fra mappen /database/ for at oprette og fylde databasen med data.
Sørg for, at connection string i projektet (Sql_String.connectionString) er korrekt opsat til din SQL Server.
csharp
Kopiér
Rediger
public static string connectionString = "Data Source=DIN_SERVER;Initial Catalog=Sydvest_Bo;Integrated Security=True;";
Databaseinformation
Database-tabeller i projektet inkluderer:

Ejer
Lejlighed
Lejlighedskompleks
Inspektør
Område
SæsonKategori
Reservation
Brug af applikationen
Applikationen starter i en menu, hvor du kan vælge forskellige operationer:

Administrér ejere
Administrér lejlighedskomplekser
Administrér inspektører
Administrér lejligheder
Administrér områder
Administrér reservationer
Administrér sommerhuse
Administrér sæsonkategorier
Afslut applikationen
Vælg et menupunkt og følg de efterfølgende instruktioner i terminalen.

Teknisk dokumentation
CRUD-metoder
Projektet følger CRUD-principperne for hver komponent:

Create: Oprettelse af en ny post (f.eks. opret ny reservation)
Read: Læsning/liste af eksisterende poster (f.eks. liste inspektører)
Update: Opdatering af eksisterende poster (f.eks. opdater sæsonkategori)
Delete: Sletning af poster (f.eks. slet område)
Klasser
Nøgleklasser i projektet inkluderer:

Ejer
Lejlighed
Lejlighedskompleks
Inspektør
Område
SæsonKategori
Reservation
MenuHandler
