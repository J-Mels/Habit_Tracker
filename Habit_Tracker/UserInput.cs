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
            return input;
        }

        public static string GetNameInput(string message)
        {
            Console.Clear();
            string habitNameInput = GetUserInput(message);
            while (habitNameInput.Length > 25 || !Regex.IsMatch(habitNameInput, @$"^[a-zA-Z]+$") || habitNameInput.Contains(" "))
            {
                habitNameInput = GetUserInput($"Invalid entry. {message}\n");
            }
            return habitNameInput;
        }

        public static string GetDateInput(string message)
        {
            Console.Clear();
            string habitDateInput = GetUserInput(message);
            while (!DateTime.TryParseExact(habitDateInput, "MM-dd-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.Clear();
                habitDateInput = GetUserInput($"Invalid entry. {message}");
            }
            return habitDateInput;

        }
        public static string GetQuantityInput(string message)
        {
            Console.Clear();
            string habitQuantityInput = GetUserInput(message);
            while (true)
            {
                if (int.TryParse(habitQuantityInput, out _))
                {
                    return habitQuantityInput;
                }
                else
                {
                    habitQuantityInput = GetUserInput($"Invalid entry. {message}");
                }
            }
        }
    }
}








// DateTime.TryParseExact(dateInput, "dd-MM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
// Console.WriteLine("\n\nInvalid date. (Format: dd-mm-yyyy). Type 0 to return to main menu or try again:\n\n")