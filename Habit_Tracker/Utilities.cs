using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habit_Tracker
{
    class Utilities
    {
        public static void DisplayMainMenu()
        {
            string? userInput;
            bool programIsRunning = true;

            while (programIsRunning)
            {
                Console.WriteLine("-------- MAIN MENU --------");
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1) Create new habit");
                Console.WriteLine("2) Insert habit entry");
                Console.WriteLine("3) Update habit entry");
                Console.WriteLine("4) View habit");
                Console.WriteLine("5) Delete habit");
                Console.WriteLine("0) Exit");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        CreateHabit();
                        Console.Clear();
                        break;
                    case "2":
                        InsertHabit();
                        Console.Clear();
                        break;
                    case "3":
                        Console.WriteLine(String.Join(',', Database.GetTableNames()));
                        //Console.Clear();
                        break;
                    case "4":
                        Console.Clear();
                        break;
                    case "5":
                        Console.Clear();
                        break;
                    case "0":
                        programIsRunning = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid selection. Please try again.\n");
                        break;
                }
            }
        }

        private static void CreateHabit()
        {

            while (true)
            {

                string habitName = UserInput.GetNameInput("Input habit name (No spaces or special characters. Must be no more than 25 characters).\nOr, enter 0 to return to main menu:");
                if (habitName == "0") break;
                if (Database.CheckForDuplicates(habitName))
                {
                    Console.WriteLine($"${habitName} already exists. Please choose a different name");
                }

                // TODO -- Check if habitName table already exists in the database.

                Database.CreateTable(habitName);

                Console.Clear();

                Console.WriteLine($"\nHabit Created: {habitName}.");

                string inputError = "";

                while (true)
                {
                    string addNextHabit = UserInput.GetUserInput($"{inputError}\n\nWould you like to create another habit? (Y/N).", false);

                    if (addNextHabit.Equals("N", StringComparison.OrdinalIgnoreCase))
                        return;
                    else if (addNextHabit.Equals("Y", StringComparison.OrdinalIgnoreCase))
                        break;
                    else
                    {
                        inputError = "Invalid selection. Only Y/y or N/n accepted.";
                        Console.Clear();
                    }
                }
            }

        }

        private static void InsertHabit()
        {
            while (true)
            {
                // TODO -- list all habit tables in the database to assist user in making seleciton

                string habitName = UserInput.GetNameInput("Input habit name (No spaces or special characters. Must be no more than 25 characters).\nOr, enter 0 to return to main menu:");
                if (habitName == "0") break;

                // TODO -- Check if habitName exists in the database.

                string date = UserInput.GetDateInput("Input habit date (Use mm-dd-yyyy format).\nOr, enter 0 to return to main menu:");
                if (habitName == "0") break;

                string quantity = UserInput.GetQuantityInput("Input habit quantity (Only whole numbers accepted).\nOr, enter 0 to return to main menu:");
                if (habitName == "0") break;

                Database.Insert(habitName, date, quantity);

                Console.Clear();

                string inputError = "";

                while (true)
                {
                    string insertNextHabit = UserInput.GetUserInput($"{inputError}\n\nWould you like to insert another habit entry? (Y/N).", false);

                    if (insertNextHabit.Equals("N", StringComparison.OrdinalIgnoreCase))
                        return;
                    else if (insertNextHabit.Equals("Y", StringComparison.OrdinalIgnoreCase))
                        break;
                    else
                    {
                        inputError = "Invalid selection. Only Y/y or N/n accepted.";
                        Console.Clear();
                    }
                }

            }


        }
    }
}