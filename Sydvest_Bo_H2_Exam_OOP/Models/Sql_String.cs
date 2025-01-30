using System;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;

namespace Sydvest_Bo_H2_Exam_OOP.models
{
    public class Sql_String
    {
        public static string connectionString = "Data Source=localhost;Initial Catalog=Sydvest_Bo;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;Connect Timeout=60;";

        // --- Forbindelse ---
        public string Connect()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    return "Connected with success";
                }
                catch (Exception ex)
                {
                    return "Fejl ved oprettelse af forbindelse: " + ex.Message;
                }
            }
        }

        public string TestConnection()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    return "Databaseforbindelse er OK!";
                }
                catch (Exception ex)
                {
                    return "Databaseforbindelsesfejl: " + ex.Message;
                }
            }
        }

        // --- CRUD metoder for Ejer ---

        public static string OpretEjer(string navn, string adresse, string telefonnummer)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Ejer (Navn, Adresse, Telefonnummer) VALUES (@Navn, @Adresse, @Telefonnummer)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Navn", navn);
                    cmd.Parameters.AddWithValue("@Adresse", adresse);
                    cmd.Parameters.AddWithValue("@Telefonnummer", telefonnummer);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Ejer oprettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved oprettelse af ejer: " + ex.Message;
                    }
                }
            }
        }

        public static string ListEjere()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Ejer";
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
                                result.AppendLine($"EjerID: {reader["EjerID"]}, Navn: {reader["Navn"]}, Adresse: {reader["Adresse"]}, Telefonnummer: {reader["Telefonnummer"]}");
                            }
                            return result.Length > 0 ? result.ToString() : "Ingen ejere fundet.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved hentning af ejere: " + ex.Message;
                    }
                }
            }
        }

        public static string OpdaterEjer(int ejerId, string nyAdresse)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Ejer SET Adresse = @Adresse WHERE EjerID = @EjerID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Adresse", nyAdresse);
                    cmd.Parameters.AddWithValue("@EjerID", ejerId);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Ejer opdateret med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved opdatering af ejer: " + ex.Message;
                    }
                }
            }
        }

        public static string SletEjer(int ejerId)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Ejer WHERE EjerID = @EjerID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@EjerID", ejerId);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Ejer slettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved sletning af ejer: " + ex.Message;
                    }
                }
            }
        }

        // --- Menu håndtering for Ejer ---

        public static void StartEjerMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            bool running = true;
            while (running)
            {
                string menuOptions = "1. Opret Ejer\n2. Liste Ejere\n3. Opdater Ejer\n4. Slet Ejer\n5. Tilbage\n";
                string userChoice = getUserInput(menuOptions);

                switch (userChoice)
                {
                    case "1":
                        OpretEjerMenu(getUserInput, displayMessage);
                        break;
                    case "2":
                        ListEjereMenu(displayMessage);
                        break;
                    case "3":
                        OpdaterEjerMenu(getUserInput, displayMessage);
                        break;
                    case "4":
                        SletEjerMenu(getUserInput, displayMessage);
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

        private static void OpretEjerMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            string navn = getUserInput("Indtast navn: ");
            string adresse = getUserInput("Indtast adresse: ");
            string telefonnummer = getUserInput("Indtast telefonnummer: ");

            string result = OpretEjer(navn, adresse, telefonnummer);
            displayMessage(result);
        }

        private static void ListEjereMenu(Action<string> displayMessage)
        {
            string result = ListEjere();
            displayMessage(result);
        }

        private static void OpdaterEjerMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int ejerId = int.Parse(getUserInput("Indtast EjerID: "));
            string nyAdresse = getUserInput("Indtast ny adresse: ");

            string result = OpdaterEjer(ejerId, nyAdresse);
            displayMessage(result);
        }

        private static void SletEjerMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int ejerId = int.Parse(getUserInput("Indtast EjerID: "));

            string result = SletEjer(ejerId);
            displayMessage(result);
        }
    }
}
