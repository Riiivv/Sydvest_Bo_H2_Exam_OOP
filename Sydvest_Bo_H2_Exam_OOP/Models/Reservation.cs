using System;
using Microsoft.Data.SqlClient;
using System.Text;
using Sydvest_Bo_H2_Exam_OOP.models;

namespace Sydvest_Bo_H2_Exam_OOP
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int LejlighedID { get; set; }
        public DateTime StartDato { get; set; }
        public DateTime SlutDato { get; set; }
        public decimal Pris { get; set; }

        private static string connectionString = Sql_String.connectionString;

        // --- CRUD metoder ---
        // --- CRUD metoder ---
        public static string OpretReservation(int lejlighedID, DateTime startDato, DateTime slutDato)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlTransaction transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        Console.WriteLine("Starter tjek for eksisterende reservationer...");

                        // Tjek for eksisterende reservation
                        string checkQuery = @"SELECT COUNT(*) 
                                      FROM Reservation 
                                      WHERE LejlighedID = @LejlighedID 
                                      AND NOT (SlutDato <= @StartDato OR StartDato >= @SlutDato)";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, cnn, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@LejlighedID", lejlighedID);
                            checkCmd.Parameters.AddWithValue("@StartDato", startDato);
                            checkCmd.Parameters.AddWithValue("@SlutDato", slutDato);

                            int count = (int)checkCmd.ExecuteScalar();
                            if (count > 0)
                            {
                                transaction.Rollback();
                                return "Fejl: Der findes allerede en reservation i denne periode for denne lejlighed.";
                            }
                        }

                        Console.WriteLine("Tjek gennemført, ingen eksisterende reservationer fundet.");

                        // Hent pris fra sæsonkategori, og håndter hvis ingen sæsonkategori findes
                        string getPriceQuery = @"SELECT Pris 
                                         FROM SæsonKategori 
                                         WHERE SæsonID = (SELECT SæsonID FROM Lejlighed WHERE LejlighedID = @LejlighedID)";
                        decimal? pris = null;

                        using (SqlCommand priceCmd = new SqlCommand(getPriceQuery, cnn, transaction))
                        {
                            priceCmd.Parameters.AddWithValue("@LejlighedID", lejlighedID);
                            object priceResult = priceCmd.ExecuteScalar();

                            if (priceResult != null)
                            {
                                pris = Convert.ToDecimal(priceResult);
                            }
                        }

                        if (pris == null)
                        {
                            transaction.Rollback();
                            return "Fejl: Ingen sæsonkategori tilknyttet denne lejlighed. Opret eller tildel en sæsonkategori først.";
                        }

                        // Opret reservation med den fundne pris
                        string insertQuery = @"INSERT INTO Reservation (LejlighedID, StartDato, SlutDato, Pris) 
                                       VALUES (@LejlighedID, @StartDato, @SlutDato, @Pris)";
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, cnn, transaction))
                        {
                            insertCmd.Parameters.AddWithValue("@LejlighedID", lejlighedID);
                            insertCmd.Parameters.AddWithValue("@StartDato", startDato);
                            insertCmd.Parameters.AddWithValue("@SlutDato", slutDato);
                            insertCmd.Parameters.AddWithValue("@Pris", pris);

                            insertCmd.ExecuteNonQuery();
                            transaction.Commit();
                            Console.WriteLine("Reservation oprettet.");
                            return "Reservation oprettet med korrekt sæsonpris.";
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return "Fejl ved oprettelse af reservation: " + ex.Message;
                    }
                }
            }
        }
        public static string ListReservationer()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT R.ReservationID, R.StartDato, R.SlutDato, R.Pris, L.Adresse FROM Reservation R INNER JOIN Lejlighed L ON R.LejlighedID = L.LejlighedID ORDER BY R.StartDato ASC";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    try
                    {
                        cnn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            StringBuilder result = new StringBuilder();
                            while (reader.Read())
                            {
                                result.AppendLine($"ReservationID: {reader["ReservationID"]}, Start: {reader["StartDato"]}, Slut: {reader["SlutDato"]}, Pris: {reader["Pris"]}, Lejlighed: {reader["Adresse"]}");
                            }
                            return result.Length > 0 ? result.ToString() : "Ingen reservationer fundet.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved hentning af reservationer: " + ex.Message;
                    }
                }
            }
        }

        public static string OpdaterReservation(int reservationID, decimal nyPris)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Reservation SET Pris = @Pris WHERE ReservationID = @ReservationID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Pris", nyPris);
                    cmd.Parameters.AddWithValue("@ReservationID", reservationID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Reservation opdateret med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved opdatering af reservation: " + ex.Message;
                    }
                }
            }
        }

        public static string SletReservation(int reservationID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Reservation WHERE ReservationID = @ReservationID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@ReservationID", reservationID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Reservation slettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved sletning af reservation: " + ex.Message;
                    }
                }
            }
        }

        // --- Menu håndtering ---
        public static void StartReservationMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            bool running = true;
            while (running)
            {
                string menuOptions = "1. Opret Reservation\n2. Liste Reservationer\n3. Opdater Reservation\n4. Slet Reservation\n5. Tilbage\n";
                string userChoice = getUserInput(menuOptions);

                switch (userChoice)
                {
                    case "1":
                        OpretReservationMenu(getUserInput, displayMessage);
                        break;
                    case "2":
                        ListReservationerMenu(displayMessage);
                        break;
                    case "3":
                        OpdaterReservationMenu(getUserInput, displayMessage);
                        break;
                    case "4":
                        SletReservationMenu(getUserInput, displayMessage);
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        displayMessage("Ugyldigt valg. Prøv igen.");
                        break;
                }
            }
        }

        private static void OpretReservationMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {

            try
            {
                int lejlighedID = int.Parse(getUserInput("Indtast LejlighedID: "));

                // Håndtering af datoindlæsning med validering
                DateTime startDato, slutDato;

                while (!DateTime.TryParseExact(getUserInput("Indtast startdato (YYYY-MM-DD): "), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out startDato))
                {
                    displayMessage("Ugyldigt datoformat. Prøv igen (YYYY-MM-DD).");
                }

                while (!DateTime.TryParseExact(getUserInput("Indtast slutdato (YYYY-MM-DD): "), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out slutDato))
                {
                    displayMessage("Ugyldigt datoformat. Prøv igen (YYYY-MM-DD).");
                }

                // Kontrollér om slutdato er efter startdato
                if (slutDato <= startDato)
                {
                    displayMessage("Fejl: Slutdato skal være efter startdato.");
                    return;
                }

                string result = OpretReservation(lejlighedID, startDato, slutDato);
                displayMessage(result);
            }
            catch (FormatException)
            {
                displayMessage("Ugyldigt input. Sørg for at indtaste tal og datoer korrekt.");
            }
            catch (Exception ex)
            {
                displayMessage($"Der opstod en fejl: {ex.Message}");
            }
        }


        private static void ListReservationerMenu(Action<string> displayMessage)
        {
            string result = ListReservationer();
            displayMessage(result);
        }

        private static void OpdaterReservationMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int reservationID = int.Parse(getUserInput("Indtast ReservationID: "));
            decimal nyPris = decimal.Parse(getUserInput("Indtast ny pris: "));

            string result = OpdaterReservation(reservationID, nyPris);
            displayMessage(result);
        }

        private static void SletReservationMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int reservationID = int.Parse(getUserInput("Indtast ReservationID: "));

            string result = SletReservation(reservationID);
            displayMessage(result);
        }
    }
}
