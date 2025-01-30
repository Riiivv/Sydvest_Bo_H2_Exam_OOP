using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Sydvest_Bo_H2_Exam_OOP.models
{
    public class Inspektør
    {
        public int InspektørID { get; set; }
        public string Navn { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        private static string connectionString = Sql_String.connectionString;

        // --- CRUD metoder ---
        public static string OpretInspektør(string navn, string email, string telefon)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Inspektør (Navn, Email, Telefon) VALUES (@Navn, @Email, @Telefon)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Navn", navn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Telefon", telefon);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Inspektør oprettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved oprettelse af inspektør: " + ex.Message;
                    }
                }
            }
        }

        public static string ListInspektører()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Inspektør";
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
                                result.AppendLine($"InspektørID: {reader["InspektørID"]}, Navn: {reader["Navn"]}, Email: {reader["Email"]}, Telefon: {reader["Telefon"]}");
                            }
                            return result.Length > 0 ? result.ToString() : "Ingen inspektører fundet.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved hentning af inspektører: " + ex.Message;
                    }
                }
            }
        }

        public static string OpdaterInspektør(int inspektørID, string nyEmail, string nyTelefon)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Inspektør SET Email = @Email, Telefon = @Telefon WHERE InspektørID = @InspektørID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@Email", nyEmail);
                    cmd.Parameters.AddWithValue("@Telefon", nyTelefon);
                    cmd.Parameters.AddWithValue("@InspektørID", inspektørID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Inspektør opdateret med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved opdatering af inspektør: " + ex.Message;
                    }
                }
            }
        }

        public static string SletInspektør(int inspektørID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Inspektør WHERE InspektørID = @InspektørID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@InspektørID", inspektørID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Inspektør slettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved sletning af inspektør: " + ex.Message;
                    }
                }
            }
        }

        // --- Menu håndtering ---
        public static void StartInspektørMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            bool running = true;
            while (running)
            {
                string menuOptions = "1. Opret Inspektør\n2. Liste Inspektører\n3. Opdater Inspektør\n4. Slet Inspektør\n5. Tilbage\n";
                string userChoice = getUserInput(menuOptions);

                switch (userChoice)
                {
                    case "1":
                        OpretInspektørMenu(getUserInput, displayMessage);
                        break;
                    case "2":
                        ListInspektørerMenu(displayMessage);
                        break;
                    case "3":
                        OpdaterInspektørMenu(getUserInput, displayMessage);
                        break;
                    case "4":
                        SletInspektørMenu(getUserInput, displayMessage);
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

        private static void OpretInspektørMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            string navn = getUserInput("Indtast navn: ");
            string email = getUserInput("Indtast email: ");
            string telefon = getUserInput("Indtast telefon: ");

            string result = OpretInspektør(navn, email, telefon);
            displayMessage(result);
        }

        private static void ListInspektørerMenu(Action<string> displayMessage)
        {
            string result = ListInspektører();
            displayMessage(result);
        }

        private static void OpdaterInspektørMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int inspektørID = int.Parse(getUserInput("Indtast InspektørID: "));
            string nyEmail = getUserInput("Indtast ny email: ");
            string nyTelefon = getUserInput("Indtast ny telefon: ");

            string result = OpdaterInspektør(inspektørID, nyEmail, nyTelefon);
            displayMessage(result);
        }

        private static void SletInspektørMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int inspektørID = int.Parse(getUserInput("Indtast InspektørID: "));

            string result = SletInspektør(inspektørID);
            displayMessage(result);
        }
    }
}
