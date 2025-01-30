using System;
using Sydvest_Bo_H2_Exam_OOP.models;

namespace Sydvest_Bo_H2_Exam_OOP
{
    public class MenuHandler
    {
        private bool _running = true;

        public void StartMenu(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            while (_running)
            {
                string menuOptions = GetMainMenuOptions();
                string userChoice = getUserInput(menuOptions);
                HandleMainMenuChoice(userChoice, getUserInput, displayMessage);
            }
        }

        private string GetMainMenuOptions()
        {
            return "1. Administrér Ejere\n" +
                   "2. Administrér Lejlighedskomplekser\n" +
                   "3. Administrér Inspektører\n" +
                   "4. Administrér Lejligheder\n" +
                   "5. Administrér Områder\n" +
                   "6. Administrér Reservationer\n" +
                   "7. Administrér Sommerhuse\n" +
                   "8. Administrér Sæsonkategorier\n" +
                   "9. Afslut\n";
        }

        private void HandleMainMenuChoice(string choice, Func<string, string> getUserInput, Action<string> displayMessage)
        {
            switch (choice)
            {
                case "1":
                    HandleEjere(getUserInput, displayMessage);
                    break;
                case "2":
                    HandleLejlighedskomplekser(getUserInput, displayMessage);
                    break;
                case "3":
                    HandleInspektører(getUserInput, displayMessage);
                    break;
                case "4":
                    HandleLejligheder(getUserInput, displayMessage);
                    break;
                case "5":
                    HandleOmråder(getUserInput, displayMessage);
                    break;
                case "6":
                    HandleReservationer(getUserInput, displayMessage);
                    break;
                case "7":
                    HandleSommerhuse(getUserInput, displayMessage);
                    break;
                case "8":
                    HandleSæsonKategorier(getUserInput, displayMessage);
                    break;
                case "9":
                    _running = false;
                    break;
                default:
                    displayMessage("Ugyldigt valg. Prøv igen.");
                    break;
            }
        }

        private void HandleEjere(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            Sql_String.StartEjerMenu(getUserInput, displayMessage);
        }

        private void HandleLejlighedskomplekser(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            Lejlighedskompleks.StartKompleksMenu(getUserInput, displayMessage);
        }

        private void HandleInspektører(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            Inspektør.StartInspektørMenu(getUserInput, displayMessage);
        }

        private void HandleLejligheder(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            Lejlighed.StartLejlighedMenu(getUserInput, displayMessage);
        }

        private void HandleOmråder(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            Område.StartOmrådeMenu(getUserInput, displayMessage);
        }

        private void HandleReservationer(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            Reservation.StartReservationMenu(getUserInput, displayMessage);
        }

        private void HandleSommerhuse(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            Sommerhus.StartSommerhusMenu(getUserInput, displayMessage);
        }

        private void HandleSæsonKategorier(Func<string, string> getUserInput, Action<string> displayMessage)
        {
            SæsonKategori.StartSæsonKategoriMenu(getUserInput, displayMessage);
        }
    }
}