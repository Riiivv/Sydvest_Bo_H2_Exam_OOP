using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Sydvest_Bo_H2_Exam_OOP.models
{
    public class Lejlighedskompleks
    {
        public int KompleksID { get; set; }
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public int InspektørID { get; set; }

        private static string connectionString = Sql_String.connectionString;

        // --- CRUD metoder ---
        public static string OpretLejlighedskompleks(string navn, string adresse, int inspektørID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Lejlighedskompleks (Navn, Adresse, InspektørID) VALUES (@Navn, @Adresse, @InspektørID)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Navn", navn);
                    cmd.Parameters.AddWithValue("@Adresse", adresse);
                    cmd.Parameters.AddWithValue("@InspektørID", inspektørID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Lejlighedskompleks oprettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved oprettelse af lejlighedskompleks: " + ex.Message;
                    }
                }
            }
        }


        public static string ListLejlighedskomplekser()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Lejlighedskompleks";
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
                                result.AppendLine($"KompleksID: {reader["KompleksID"]}, Navn: {reader["Navn"]}, Adresse: {reader["Adresse"]}, InspektorID: {reader["InspektørID"]}");
                            }
                            return result.Length > 0 ? result.ToString() : "Ingen lejlighedskomplekser fundet.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved hentning af lejlighedskomplekser: " + ex.Message;
                    }
                }
            }
        }

        public static string OpdaterLejlighedskompleks(int kompleksID, string nyAdresse)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Lejlighedskompleks SET Adresse = @Adresse WHERE KompleksID = @KompleksID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Adresse", nyAdresse);
                    cmd.Parameters.AddWithValue("@KompleksID", kompleksID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Lejlighedskompleks opdateret med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved opdatering af lejlighedskompleks: " + ex.Message;
                    }
                }
            }
        }

        public static string SletLejlighedskompleks(int kompleksID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Lejlighedskompleks WHERE KompleksID = @KompleksID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@KompleksID", kompleksID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Lejlighedskompleks slettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved sletning af lejlighedskompleks: " + ex.Message;
                    }
                }
            }
        }

        public static string ListLejlighederForKompleks(int kompleksID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Lejlighed WHERE KompleksID = @KompleksID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@KompleksID", kompleksID);
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
                            return result.Length > 0 ? result.ToString() : "Ingen lejligheder fundet i dette kompleks.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved hentning af lejligheder for kompleks: " + ex.Message;
                    }
                }
            }
        }

        // --- Menu håndtering ---
        public static void StartKompleksMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            bool running = true;
            while (running)
            {
                string menuOptions = "1. Opret Lejlighedskompleks\n2. Liste Lejlighedskomplekser\n3. Opdater Lejlighedskompleks\n4. Slet Lejlighedskompleks\n5. Tilbage\n";
                string userChoice = getUserInput(menuOptions);

                switch (userChoice)
                {
                    case "1":
                        OpretKompleksMenu(getUserInput, displayMessage);
                        break;
                    case "2":
                        ListKomplekserMenu(displayMessage);
                        break;
                    case "3":
                        OpdaterKompleksMenu(getUserInput, displayMessage);
                        break;
                    case "4":
                        SletKompleksMenu(getUserInput, displayMessage);
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
        private static void OpretKompleksMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            string navn = getUserInput("Indtast navn på lejlighedskompleks: ");
            string adresse = getUserInput("Indtast adresse: ");
            int inspektorID = int.Parse(getUserInput("Indtast InspektørID: "));

            string result = OpretLejlighedskompleks(navn, adresse, inspektorID);
            displayMessage(result);
        }

        private static void ListKomplekserMenu(Action<string> displayMessage)
        {
            string result = ListLejlighedskomplekser();
            displayMessage(result);
        }

        private static void OpdaterKompleksMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int kompleksID = int.Parse(getUserInput("Indtast LejlighedskompleksID: "));
            string nyAdresse = getUserInput("Indtast ny adresse: ");

            string result = OpdaterLejlighedskompleks(kompleksID, nyAdresse);
            displayMessage(result);
        }

        private static void SletKompleksMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int kompleksID = int.Parse(getUserInput("Indtast LejlighedskompleksID: "));

            string result = SletLejlighedskompleks(kompleksID);
            displayMessage(result);
        }
    }
}
