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
            string habitNameInput = GetUserInput(message);
            while (habitNameInput != "0" && habitNameInput.Length > 25 || !Regex.IsMatch(habitNameInput, @$"^[a-zA-Z]+$") || habitNameInput.Contains(" "))

            while (habitNameInput.Length > 25 || !Regex.IsMatch(habitNameInput, @$"^[a-zA-Z]+$") || habitNameInput.Contains(" "))
            {
                habitNameInput = GetUserInput($"Invalid entry. {message}\n");
            }
            return habitNameInput;
        }

        public static string GetDateInput(string message)
        {

            string habitDateInput = GetUserInput(message);
            while (habitDateInput != "0" && !DateTime.TryParseExact(habitDateInput, "MM-dd-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                habitDateInput = GetUserInput($"Invalid entry. {message}\n");
            }
            return habitDateInput;

        }

        public static string GetQuantityInput(string message)
        {
            string habitQuantityInput = GetUserInput(message);
            while (habitQuantityInput != "0" && !int.TryParse(habitQuantityInput, out _))
            {
                habitQuantityInput = GetUserInput($"Invalid entry. {message}\n");
            }
            return habitQuantityInput;
        }
    }
}
