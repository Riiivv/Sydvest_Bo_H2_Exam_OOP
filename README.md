# Sydvest Bo Applikation

## Beskrivelse
Sydvest Bo-applikationen er en konsolbaseret applikation skrevet i C#. Den bruges til at administrere ejendomsselskabets data og giver funktionalitet til at håndtere ejere, lejligheder, inspektører, reservationer, områder, sommerhuse og sæsonkategorier.

Applikationen er bygget med fokus på overskuelig menu-navigation og tilbyder CRUD-funktionalitet (Create, Read, Update, Delete) for hver dataenhed.

---

## Funktioner

### 1. Administrationsfunktioner

- **Ejere**: Opret, liste, opdater og slet ejere.
- **Lejlighedskomplekser**: Administrer lejlighedskomplekser med tilknyttede inspektører.
- **Inspektører**: Håndter inspektører og deres oplysninger.
- **Lejligheder**: Opret og opdater lejligheder, inklusiv prissætning og sæsondata.
- **Områder**: Administrer geografiske områder.
- **Reservationer**: Opret og administrer reservationer med automatisk dato- og sæsontjek.
- **Sæsonkategorier**: Definer sæsonpriser og gyldighedsperioder.

---

## Teknisk Arkitektur

### 1. Klasser og deres ansvar

- **Ejer**: Håndterer data vedrørende ejere.
- **Lejlighedskompleks**: Håndterer lejlighedskomplekser og deres inspektører.
- **Inspektør**: Håndterer inspektørdata.
- **Lejlighed**: Styrer lejligheder og tilknyttede sæsondata.
- **Reservation**: Styrer reservationer og kontrollerer for overlap.
- **Område**: Håndterer geografiske inddelinger.
- **SæsonKategori**: Definerer sæsonkategorier med priser.
- **MenuHandler**: Styrer menu-navigationen og kalder de relevante klasser.

### 2. Databaseopsætning

Projektet anvender en SQL Server-database. Tabellenes struktur er som følger:

- **Ejer** (ID, Navn, Adresse, Telefonnummer)
- **Lejlighed** (ID, Adresse, AntalVærelser, Pris, KompleksID, SæsonID)
- **Lejlighedskompleks** (ID, Navn, Adresse, InspektørID)
- **Inspektør** (ID, Navn, Email, Telefon)
- **Reservation** (ID, LejlighedID, StartDato, SlutDato, Pris)
- **Område** (ID, OmrådeNavn)
- **SæsonKategori** (ID, KategoriNavn, UgeStart, UgeSlut, Pris)

---

## Installationsguide

### 1. Krav

- Visual Studio 2022 eller nyere
- SQL Server Management Studio (SSMS)
- .NET SDK 6.0 eller nyere

### 2. Opsætning

1. Klon eller download projektet til din lokale maskine.
2. Importer SQL-dumpfilen `sydvest_bo_dump.sql` i SSMS for at oprette databasen.
3. Tilpas `connectionString` i klassen `Sql_String` for at matche din lokale databasekonfiguration.
4. Åbn projektet i Visual Studio og kør applikationen.

---

## Brugervejledning

Ved start præsenteres en hovedmenu med følgende muligheder:

1. **Administrér Ejere**
2. **Administrér Lejlighedskomplekser**
3. **Administrér Inspektører**
4. **Administrér Lejligheder**
5. **Administrér Områder**
6. **Administrér Reservationer**
7. **Administrér Sommerhuse**
8. **Administrér Sæsonkategorier**
9. **Afslut programmet**

### Eksempel: Opret en reservation

1. Vælg "6. Administrér Reservationer" fra hovedmenuen.
2. Vælg "1. Opret Reservation".
3. Indtast de efterspurgte oplysninger:
   - LejlighedID
   - Startdato (format: YYYY-MM-DD)
   - Slutdato (format: YYYY-MM-DD)
4. Systemet kontrollerer for overlap og beregner prisen baseret på den tilknyttede sæsonkategori.

---

## Eksempeldata

### SæsonKategori

```sql
INSERT INTO SæsonKategori (KategoriNavn, UgeStart, UgeSlut, Pris) VALUES
('Forår', 1, 15, 5000),
('Sommer', 16, 32, 8000),
('Efterår', 33, 45, 5500),
('Vinter', 46, 52, 4000);
```

### Område

```sql
INSERT INTO Område (OmrådeNavn) VALUES
('Nordjylland'),
('Midtjylland'),
('Sydjylland'),
('Sjælland'),
('Bornholm');
```

---

## Udvidelsesmuligheder

- Tilføj rapportgenerering for hver enhed (ejere, reservationer osv.).
- Implementer avanceret søgning og filtrering i menuen.
- Skift fra konsolbaseret applikation til en GUI ved hjælp af Windows Forms eller WPF.

---

## Kontakt

For spørgsmål eller fejlrapportering, kontakt udvikleren via projektets GitHub-side.

