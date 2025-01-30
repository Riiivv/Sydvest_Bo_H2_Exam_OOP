using System;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Sydvest_Bo_H2_Exam_OOP.models
{
    public class SæsonKategori
    {
        public int SæsonID { get; set; }
        public string KategoriNavn { get; set; }
        public int UgeStart { get; set; }
        public int UgeSlut { get; set; }
        public decimal Pris { get; set; }

        private static string connectionString = Sql_String.connectionString;

        // --- CRUD metoder ---
        public static string OpretSæsonKategori(string kategoriNavn, int ugeStart, int ugeSlut, decimal pris)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO SæsonKategori (KategoriNavn, UgeStart, UgeSlut, Pris) VALUES (@KategoriNavn, @UgeStart, @UgeSlut, @Pris)";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@KategoriNavn", kategoriNavn);
                    cmd.Parameters.AddWithValue("@UgeStart", ugeStart);
                    cmd.Parameters.AddWithValue("@UgeSlut", ugeSlut);
                    cmd.Parameters.AddWithValue("@Pris", pris);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Sæsonkategori oprettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved oprettelse af sæsonkategori: " + ex.Message;
                    }
                }
            }
        }

        public static string ListSæsonKategorier()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM SæsonKategori";
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
                                result.AppendLine($"SæsonID: {reader["SæsonID"]}, KategoriNavn: {reader["KategoriNavn"]}, UgeStart: {reader["UgeStart"]}, UgeSlut: {reader["UgeSlut"]}, Pris: {reader["Pris"]}");
                            }
                            return result.Length > 0 ? result.ToString() : "Ingen sæsonkategorier fundet.";
                        }
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved hentning af sæsonkategorier: " + ex.Message;
                    }
                }
            }
        }

        public static string OpdaterSæsonKategori(int sæsonID, string nytNavn, int nyUgeStart, int nyUgeSlut, decimal nyPris)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "UPDATE SæsonKategori SET KategoriNavn = @KategoriNavn, UgeStart = @UgeStart, UgeSlut = @UgeSlut, Pris = @Pris WHERE SæsonID = @SæsonID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@KategoriNavn", nytNavn);
                    cmd.Parameters.AddWithValue("@UgeStart", nyUgeStart);
                    cmd.Parameters.AddWithValue("@UgeSlut", nyUgeSlut);
                    cmd.Parameters.AddWithValue("@Pris", nyPris);
                    cmd.Parameters.AddWithValue("@SæsonID", sæsonID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Sæsonkategori opdateret med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved opdatering af sæsonkategori: " + ex.Message;
                    }
                }
            }
        }

        public static string SletSæsonKategori(int sæsonID)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM SæsonKategori WHERE SæsonID = @SæsonID";
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@SæsonID", sæsonID);

                    try
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return "Sæsonkategori slettet med succes.";
                    }
                    catch (Exception ex)
                    {
                        return "Fejl ved sletning af sæsonkategori: " + ex.Message;
                    }
                }
            }
        }

        // --- Menu håndtering ---
        public static void StartSæsonKategoriMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            bool running = true;
            while (running)
            {
                string menuOptions = "1. Opret Sæsonkategori\n2. Liste Sæsonkategorier\n3. Opdater Sæsonkategori\n4. Slet Sæsonkategori\n5. Tilbage\n";
                string userChoice = getUserInput(menuOptions);

                switch (userChoice)
                {
                    case "1":
                        OpretSæsonKategoriMenu(getUserInput, displayMessage);
                        break;
                    case "2":
                        ListSæsonKategorierMenu(displayMessage);
                        break;
                    case "3":
                        OpdaterSæsonKategoriMenu(getUserInput, displayMessage);
                        break;
                    case "4":
                        SletSæsonKategoriMenu(getUserInput, displayMessage);
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

        private static void OpretSæsonKategoriMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            string kategoriNavn = getUserInput("Indtast kategori navn: ");
            int ugeStart = int.Parse(getUserInput("Indtast startuge: "));
            int ugeSlut = int.Parse(getUserInput("Indtast slutuge: "));
            decimal pris = decimal.Parse(getUserInput("Indtast pris: "));

            string result = OpretSæsonKategori(kategoriNavn, ugeStart, ugeSlut, pris);
            displayMessage(result);
        }

        private static void ListSæsonKategorierMenu(Action<string> displayMessage)
        {
            string result = ListSæsonKategorier();
            displayMessage(result);
        }

        private static void OpdaterSæsonKategoriMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int sæsonID = int.Parse(getUserInput("Indtast SæsonID: "));
            string nytNavn = getUserInput("Indtast nyt navn: ");
            int nyUgeStart = int.Parse(getUserInput("Indtast ny startuge: "));
            int nyUgeSlut = int.Parse(getUserInput("Indtast ny slutuge: "));
            decimal nyPris = decimal.Parse(getUserInput("Indtast ny pris: "));

            string result = OpdaterSæsonKategori(sæsonID, nytNavn, nyUgeStart, nyUgeSlut, nyPris);
            displayMessage(result);
        }

        private static void SletSæsonKategoriMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            int sæsonID = int.Parse(getUserInput("Indtast SæsonID: "));

            string result = SletSæsonKategori(sæsonID);
            displayMessage(result);
        }
    }
}
