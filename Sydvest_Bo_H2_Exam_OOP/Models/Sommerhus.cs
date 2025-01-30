using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Sydvest_Bo_H2_Exam_OOP.models
{
    public class Sommerhus
    {
        public int HusID { get; set; }
        public string Adresse { get; set; }
        public int AntalSenge { get; set; }
        public string Standard { get; set; }
        public int EjerID { get; set; }

        private static string connectionString = Sql_String.connectionString;

        // --- CRUD metoder ---

        public static string OpretSommerhus(string adresse, int antalSenge, string standard, int ejerID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Sommerhus (Adresse, AntalSenge, Standard, EjerID) VALUES (@Adresse, @AntalSenge, @Standard, @EjerID)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Adresse", adresse);
                    cmd.Parameters.AddWithValue("@AntalSenge", antalSenge);
                    cmd.Parameters.AddWithValue("@Standard", standard);
                    cmd.Parameters.AddWithValue("@EjerID", ejerID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Sommerhus oprettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved oprettelse af sommerhus: " + ex.Message;
                    }
                }
            }
        }

        public static string ListSommerhuse()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Sommerhus";
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
                                result.AppendLine($"HusID: {reader["HusID"]}, Adresse: {reader["Adresse"]}, Antal Senge: {reader["AntalSenge"]}, Standard: {reader["Standard"]}, EjerID: {reader["EjerID"]}");
                            }
                            return result.Length > 0 ? result.ToString() : "Ingen sommerhuse fundet.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved hentning af sommerhuse: " + ex.Message;
                    }
                }
            }
        }

        public static string OpdaterSommerhus(int husID, string nyAdresse, int nyAntalSenge, string nyStandard)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Sommerhus SET Adresse = @Adresse, AntalSenge = @AntalSenge, Standard = @Standard WHERE HusID = @HusID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Adresse", nyAdresse);
                    cmd.Parameters.AddWithValue("@AntalSenge", nyAntalSenge);
                    cmd.Parameters.AddWithValue("@Standard", nyStandard);
                    cmd.Parameters.AddWithValue("@HusID", husID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Sommerhus opdateret med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved opdatering af sommerhus: " + ex.Message;
                    }
                }
            }
        }

        public static string SletSommerhus(int husID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Sommerhus WHERE HusID = @HusID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@HusID", husID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Sommerhus slettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved sletning af sommerhus: " + ex.Message;
                    }
                }
            }
        }

        // --- Menu håndtering ---
        public static void StartSommerhusMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            bool running = true;
            while (running)
            {
                string menuOptions = "1. Opret Sommerhus\n2. Liste Sommerhuse\n3. Opdater Sommerhus\n4. Slet Sommerhus\n5. Tilbage\n";
                string userChoice = getUserInput(menuOptions);

                switch (userChoice)
                {
                    case "1":
                        OpretSommerhusMenu(getUserInput, displayMessage);
                        break;
                    case "2":
                        ListSommerhuseMenu(displayMessage);
                        break;
                    case "3":
                        OpdaterSommerhusMenu(getUserInput, displayMessage);
                        break;
                    case "4":
                        SletSommerhusMenu(getUserInput, displayMessage);
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

        private static void OpretSommerhusMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            string adresse = getUserInput("Indtast adresse: ");
            int antalSenge = int.Parse(getUserInput("Indtast antal senge: "));
            string standard = getUserInput("Indtast standard (Lav, Mellem, Høj): ");
            int ejerId = int.Parse(getUserInput("Indtast EjerID: "));

            string result = OpretSommerhus(adresse, antalSenge, standard, ejerId);
            displayMessage(result);
        }

        private static void ListSommerhuseMenu(Action<string> displayMessage)
        {
            string result = ListSommerhuse();
            displayMessage(result);
        }

        private static void OpdaterSommerhusMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int husId = int.Parse(getUserInput("Indtast SommerhusID: "));
            string nyAdresse = getUserInput("Indtast ny adresse: ");
            int nyAntalSenge = int.Parse(getUserInput("Indtast nyt antal senge: "));
            string nyStandard = getUserInput("Indtast ny standard: ");

            string result = OpdaterSommerhus(husId, nyAdresse, nyAntalSenge, nyStandard);
            displayMessage(result);
        }

        private static void SletSommerhusMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int husId = int.Parse(getUserInput("Indtast SommerhusID: "));

            string result = SletSommerhus(husId);
            displayMessage(result);
        }
    }
}
