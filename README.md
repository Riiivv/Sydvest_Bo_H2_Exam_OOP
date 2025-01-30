Sydvest Bo Applikation
Beskrivelse
Sydvest Bo-applikationen er en konsolbaseret applikation skrevet i C#. Den bruges til at administrere ejendomsselskabets data og giver funktionalitet til at håndtere ejere, lejligheder, inspektører, reservationer, områder, sommerhuse og sæsonkategorier.

Funktioner
Administrér ejere: Opret, liste, opdater og slet ejere.
Lejlighedskomplekser: Opret, opdater og håndter komplekser med inspektører.
Inspektører: Administrér inspektører og deres oplysninger.
Lejligheder: Opret og opdater lejligheder, inklusive pris- og sæsondata.
Områder: Administrér geografiske områder.
Reservationer: Opret reservationer med dato- og sæsontjek.
Sæsonkategorier: Opret og opdater sæsonpriser og gyldighedsperioder.
Teknisk Arkitektur
Klasser og ansvarsområder:

Ejer: Håndterer ejerdata.
Lejlighedskompleks: Administrerer komplekser og inspektører.
Inspektør: Administrerer inspektørdata.
Lejlighed: Styrer lejligheder og deres sæsonpriser.
Reservation: Styrer reservationer og datotjek.
MenuHandler: Håndterer applikationens menustruktur.
Data håndteres via en SQL Server-database. Tabeller inkluderer:

Ejer, Lejlighed, Lejlighedskompleks, Inspektør, Område, Reservation, SæsonKategori.
Installationsguide
Krav:

Visual Studio 2022 eller nyere
SQL Server Management Studio (SSMS)
.NET SDK 6.0 eller nyere
Opsætning:

Klon eller download projektet.
Importer SQL-dumpfilen sydvest_bo_dump.sql i SSMS.
Tilpas connectionString i klassen Sql_String.
Brugervejledning
Start applikationen, og vælg fra hovedmenuen:

Administrér ejere
Administrér lejlighedskomplekser
Administrér inspektører
Administrér lejligheder
Administrér områder
Administrér reservationer
Administrér sommerhuse
Administrér sæsonkategorier
Afslut programmet
Eksempel: Opret en reservation
Vælg "6. Administrér Reservationer"
Vælg "1. Opret Reservation"
Indtast lejlighedID, startdato og slutdato
Systemet opretter reservationen, hvis ingen overlap findes, og beregner prisen baseret på sæsonkategori.
Eksempeldata
SæsonKategori
INSERT INTO SæsonKategori (KategoriNavn, UgeStart, UgeSlut, Pris) VALUES
('Forår', 1, 15, 5000),
('Sommer', 16, 32, 8000),
('Efterår', 33, 45, 5500),
('Vinter', 46, 52, 4000);
