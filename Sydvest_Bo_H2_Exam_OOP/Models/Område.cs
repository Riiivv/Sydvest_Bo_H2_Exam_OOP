using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Sydvest_Bo_H2_Exam_OOP.models
{
    public class Område
    {
        public int OmrådeID { get; set; }
        public string OmrådeNavn { get; set; }

        private static string connectionString = Sql_String.connectionString;

        // --- CRUD metoder ---
        public static string OpretOmråde(string områdeNavn)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Område (OmrådeNavn) VALUES (@OmrådeNavn)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@OmrådeNavn", områdeNavn);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Område oprettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved oprettelse af område: " + ex.Message;
                    }
                }
            }
        }

        public static string ListOmråder()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Område";
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
                                result.AppendLine($"OmrådeID: {reader["OmrådeID"]}, OmrådeNavn: {reader["OmrådeNavn"]}");
                            }
                            return result.Length > 0 ? result.ToString() : "Ingen områder fundet.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved hentning af områder: " + ex.Message;
                    }
                }
            }
        }

        public static string OpdaterOmråde(int områdeID, string nytNavn)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Område SET OmrådeNavn = @OmrådeNavn WHERE OmrådeID = @OmrådeID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@OmrådeNavn", nytNavn);
                    cmd.Parameters.AddWithValue("@OmrådeID", områdeID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Område opdateret med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved opdatering af område: " + ex.Message;
                    }
                }
            }
        }

        public static string SletOmråde(int områdeID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Område WHERE OmrådeID = @OmrådeID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@OmrådeID", områdeID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Område slettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved sletning af område: " + ex.Message;
                    }
                }
            }
        }

        // --- Menu håndtering ---
        public static void StartOmrådeMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            bool running = true;
            while (running)
            {
                string menuOptions = "1. Opret Område\n2. Liste Områder\n3. Opdater Område\n4. Slet Område\n5. Tilbage\n";
                string userChoice = getUserInput(menuOptions);

                switch (userChoice)
                {
                    case "1":
                        OpretOmrådeMenu(getUserInput, displayMessage);
                        break;
                    case "2":
                        ListOmråderMenu(displayMessage);
                        break;
                    case "3":
                        OpdaterOmrådeMenu(getUserInput, displayMessage);
                        break;
                    case "4":
                        SletOmrådeMenu(getUserInput, displayMessage);
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

        private static void OpretOmrådeMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            string områdeNavn = getUserInput("Indtast navn på område: ");
            string result = OpretOmråde(områdeNavn);
            displayMessage(result);
        }

        private static void ListOmråderMenu(Action<string> displayMessage)
        {
            string result = ListOmråder();
            displayMessage(result);
        }

        private static void OpdaterOmrådeMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int områdeID = int.Parse(getUserInput("Indtast OmrådeID: "));
            string nytNavn = getUserInput("Indtast nyt navn på område: ");

            string result = OpdaterOmråde(områdeID, nytNavn);
            displayMessage(result);
        }

        private static void SletOmrådeMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int områdeID = int.Parse(getUserInput("Indtast OmrådeID: "));

            string result = SletOmråde(områdeID);
            displayMessage(result);
        }
    }
}