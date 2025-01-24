using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Habit_Tracker
{
    internal class UserInput
    {
        public static string GetUserInput(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            string? input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Clear();
                Console.WriteLine($"Invalid entry. {message}\n");
                input = Console.ReadLine();
            }
            if (input == "0") { Utilities.DisplayMainMenu(); }
            return input;
        }

        public static string GetNameInput(string message)
        {

            string habitNameInput = GetUserInput(message);
            if (habitNameInput == "0") return habitNameInput;
            while (habitNameInput.Length > 25 || !Regex.IsMatch(habitNameInput, @$"^[a-zA-Z]+$") || habitNameInput.Contains(" "))
            {
                habitNameInput = GetUserInput($"Invalid entry. {message}\n");
                if (habitNameInput == "0") return habitNameInput;
            }
            return habitNameInput;
        }

        public static string GetDateInput(string message)
        {

            string habitDateInput = GetUserInput(message);
            if (habitDateInput == "0") return habitDateInput;
            while (!DateTime.TryParseExact(habitDateInput, "MM-dd-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                habitDateInput = GetUserInput($"Invalid entry. {message}\n");
                if (habitDateInput == "0") return habitDateInput;
            }
            return habitDateInput;

        }
        public static string GetQuantityInput(string message)
        {
            string habitQuantityInput = GetUserInput(message);
            if (habitQuantityInput == "0") return habitQuantityInput;
            while (!int.TryParse(habitQuantityInput, out _) && habitQuantityInput != "0")
            {
                habitQuantityInput = GetUserInput($"Invalid entry. {message}\n");
            }
            return habitQuantityInput;
        }
    }
}








// DateTime.TryParseExact(dateInput, "dd-MM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
// Console.WriteLine("\n\nInvalid date. (Format: dd-mm-yyyy). Type 0 to return to main menu or try again:\n\n")