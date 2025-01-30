Sydvest Bo Applikation
Beskrivelse
Sydvest Bo-applikationen er en konsolbaseret applikation skrevet i C#. Den er udviklet som en administrationsløsning til ejendomsselskabet Sydvest Bo. Applikationen giver brugerne mulighed for at:

Administrere ejere, lejligheder og inspektører.
Administrere reservationer og sæsonkategorier.
Gemme og hente data via en SQL Server-database.
Navigere i et brugervenligt menubaseret system.
Funktioner
1. Administrationsfunktioner
Ejere:
Opret, liste, opdater og slet ejere af ejendomme.
Lejlighedskomplekser:
Håndtering af lejlighedskomplekser med tilknyttede inspektører.
Inspektører:
Opret, liste, opdater og slet inspektører.
Lejligheder:
Håndtering af lejligheder i forskellige komplekser, inklusive pris og sæsonkategorier.
Områder:
Administrér geografiske områder for ejendommene.
Reservationer:
Opret reservationer for lejligheder med kontrol af dato og sæsonpriser.
Sæsonkategorier:
Opret, opdater og administrér sæsonkategorier med forskellige priser.
Teknisk Arkitektur
1. Klasser
Ejer: Håndterer CRUD-operationer for ejere af ejendomme.
Lejlighedskompleks: Administrerer komplekser og deres inspektører.
Inspektør: Håndterer information om inspektører.
Lejlighed: Administrerer lejligheder, herunder opdatering af sæsonpriser.
Reservation: Håndterer oprettelse, opdatering og sletning af reservationer.
SæsonKategori: Administrerer sæsonkategorier med priser og gyldighedsperioder.
MenuHandler: Leverer interaktive menuer til navigation i applikationen.
2. Databasehåndtering
Data gemmes og administreres i en SQL Server-database. Nøgletabeller inkluderer:

Ejer
Lejlighed
Lejlighedskompleks
Inspektør
Område
SæsonKategori
Reservation
Installationsguide
1. Krav
Visual Studio 2022 eller nyere.
Microsoft SQL Server Management Studio (SSMS).
.NET 6.0 eller nyere.
2. Opsætning
Klon eller download projektet til din lokale maskine.
Åbn projektet i Visual Studio.
Databaseinstallation:
Importer SQL-dumpfilen sydvest_bo_dump.sql via SSMS.
Tilpas connection string i Sql_String-klassen:
csharp
Kopiér
Rediger
public static string connectionString = "Data Source=DIN_SERVER;Initial Catalog=Sydvest_Bo;Integrated Security=True;";
Kør projektet via Visual Studio.
Brugervejledning
Hovedmenu
Ved start præsenteres brugeren for følgende menu:

markdown
Kopiér
Rediger
1. Administrér Ejere
2. Administrér Lejlighedskomplekser
3. Administrér Inspektører
4. Administrér Lejligheder
5. Administrér Områder
6. Administrér Reservationer
7. Administrér Sommerhuse
8. Administrér Sæsonkategorier
9. Afslut
Indtast et menupunkt og følg instruktionerne i terminalen.

Reservationseksempel
For at oprette en reservation:

Vælg "6. Administrér Reservationer".
Vælg "1. Opret Reservation".
Indtast følgende:
LejlighedID.
Startdato (format: YYYY-MM-DD).
Slutdato (format: YYYY-MM-DD).
Systemet tjekker, om lejligheden er ledig og opretter reservationen med den korrekte sæsonpris.
Eksempeldata
SæsonKategori
Eksempeldata for sæsonkategorier:

sql
Kopiér
Rediger
INSERT INTO SæsonKategori (KategoriNavn, UgeStart, UgeSlut, Pris) VALUES
('Forår', 1, 15, 5000),
('Sommer', 16, 32, 8000),
('Efterår', 33, 45, 5500),
('Vinter', 46, 52, 4000);
Ejere
Eksempeldata for ejere:

sql
Kopiér
Rediger
INSERT INTO Ejer (Navn, Adresse, Telefonnummer) VALUES
('Anders Jensen', 'Parkvej 10, 9000 Aalborg', '12345678'),
('Maria Hansen', 'Hovedgade 5, 8000 Aarhus', '87654321');
Udvidelsesmuligheder
Tilføj funktioner til administration af flere ejendomstyper.
Implementer søgning baseret på flere kriterier, f.eks. prisintervaller og geografisk placering.
Integrér GUI for en mere visuel oplevelse.
Sikkerhed: Tilføj adgangskontrol og loginsystem.
