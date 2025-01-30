using System;
using Microsoft.Data.SqlClient;
using System.Text;
using Sydvest_Bo_H2_Exam_OOP.models;

namespace Sydvest_Bo_H2_Exam_OOP
{
    public class Lejlighed
    {
        public int LejlighedID { get; set; }
        public string Adresse { get; set; }
        public int AntalVærelser { get; set; }
        public decimal PrisPrMåned { get; set; }
        public int KompleksID { get; set; }
        public int InspektorID { get; set; }
        public int SæsonID { get; set; }

        private static string connectionString = Sql_String.connectionString;

        // --- CRUD metoder ---

        public static string OpretLejlighed(string adresse, int kompleksID, int antalVærelser, decimal prisPrMåned)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Lejlighed (Adresse, KompleksID, AntalVærelser, PrisPrMåned) VALUES (@Adresse, @KompleksID, @AntalVærelser, @PrisPrMåned)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Adresse", adresse);
                    cmd.Parameters.AddWithValue("@KompleksID", kompleksID);
                    cmd.Parameters.AddWithValue("@AntalVærelser", antalVærelser);
                    cmd.Parameters.AddWithValue("@PrisPrMåned", prisPrMåned);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Lejlighed oprettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved oprettelse af lejlighed: " + ex.Message;
                    }
                }
            }
        }

        public static string ListLejligheder()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Lejlighed";
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
                                result.AppendLine($"LejlighedID: {reader["LejlighedID"]}, Adresse: {reader["Adresse"]}, Antal Værelser: {reader["AntalVærelser"]}, Pris pr. måned: {reader["PrisPrMåned"]}");
                            }
                            return result.Length > 0 ? result.ToString() : "Ingen lejligheder fundet.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved hentning af lejligheder: " + ex.Message;
                    }
                }
            }
        }

        public static string OpdaterLejlighed(int lejlighedID, decimal nyPrisPrMåned)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Lejlighed SET PrisPrMåned = @PrisPrMåned WHERE LejlighedID = @LejlighedID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@PrisPrMåned", nyPrisPrMåned);
                    cmd.Parameters.AddWithValue("@LejlighedID", lejlighedID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Lejlighed opdateret med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved opdatering af lejlighed: " + ex.Message;
                    }
                }
            }
        }

        public static string SletLejlighed(int lejlighedID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Lejlighed WHERE LejlighedID = @LejlighedID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@LejlighedID", lejlighedID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Lejlighed slettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved sletning af lejlighed: " + ex.Message;
                    }
                }
            }
        }

        // --- Menu håndtering ---
        public static void StartLejlighedMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            bool running = true;
            while (running)
            {
                string menuOptions = "1. Opret Lejlighed\n2. Liste Lejligheder\n3. Opdater Lejlighed\n4. Slet Lejlighed\n5. Tilbage\n";
                string userChoice = getUserInput(menuOptions);

                switch (userChoice)
                {
                    case "1":
                        OpretLejlighedMenu(getUserInput, displayMessage);
                        break;
                    case "2":
                        ListLejlighederMenu(displayMessage);
                        break;
                    case "3":
                        OpdaterLejlighedMenu(getUserInput, displayMessage);
                        break;
                    case "4":
                        SletLejlighedMenu(getUserInput, displayMessage);
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

        private static void OpretLejlighedMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            string adresse = getUserInput("Indtast adresse: ");
            int kompleksID = int.Parse(getUserInput("Indtast LejlighedskompleksID: "));
            int antalVærelser = int.Parse(getUserInput("Indtast antal værelser: "));
            decimal prisPrMåned = decimal.Parse(getUserInput("Indtast pris pr. måned: "));

            string result = OpretLejlighed(adresse, kompleksID, antalVærelser, prisPrMåned);
            displayMessage(result);
        }

        private static void ListLejlighederMenu(Action<string> displayMessage)
        {
            string result = ListLejligheder();
            displayMessage(result);
        }

        private static void OpdaterLejlighedMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int lejlighedID = int.Parse(getUserInput("Indtast LejlighedID: "));
            decimal nyPrisPrMåned = decimal.Parse(getUserInput("Indtast ny pris pr. måned: "));

            string result = OpdaterLejlighed(lejlighedID, nyPrisPrMåned);
            displayMessage(result);
        }

        private static void SletLejlighedMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int lejlighedID = int.Parse(getUserInput("Indtast LejlighedID: "));

            string result = SletLejlighed(lejlighedID);
            displayMessage(result);
        }
    }
}
