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
            Console.WriteLine(message);
            string? input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Clear();

                if (message.Contains("Invalid"))
                {
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine($"Invalid entry.\n\n{message}");
                }
                input = Console.ReadLine();
            }
            return input;
        }

        public static string GetNameInput(string message)
        {
            string habitNameInput = GetUserInput(message);

            while (habitNameInput != "0" && (habitNameInput.Length > 25 || !Regex.IsMatch(habitNameInput, @$"^[a-zA-Z0-9]+$") || habitNameInput.Contains(" ")))
            {
                Console.Clear();
                habitNameInput = GetUserInput($"Invalid entry.\n\n{message}");
            }

            return habitNameInput;
        }

        public static string GetDateInput(string message)
        {

            string habitDateInput = GetUserInput(message);
            while (habitDateInput != "0" && !DateTime.TryParseExact(habitDateInput, "MM-dd-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.Clear();
                habitDateInput = GetUserInput($"Invalid entry.\n\n{message}");
            }
            return habitDateInput;

        }

        public static string GetQuantityInput(string message)
        {
            string habitQuantityInput = GetUserInput(message);
            while (habitQuantityInput != "0" && !int.TryParse(habitQuantityInput, out _))
            {
                Console.Clear();
                habitQuantityInput = GetUserInput($"Invalid entry.\n\n{message}");
            }
            return habitQuantityInput;
        }

        public static bool AddAnother(string message)
        {
            string inputError = "";

            while (true)
            {
                string insertNextHabit = UserInput.GetUserInput($"{inputError}{message}");

                if (insertNextHabit.Equals("N", StringComparison.OrdinalIgnoreCase))
                    return false;
                else if (insertNextHabit.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    return true;
                else
                {
                    inputError = "Invalid selection. Only Y/y or N/n accepted.\n\n";
                    Console.Clear();
                }
            }
        }
    }
}
