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
            //Database.CreateDB();

            Console.Clear();
            Console.WriteLine("\n\n-------- MAIN MENU --------");
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1) Create new habit");
            Console.WriteLine("2) Update habits");
            Console.WriteLine("3) View habits");
            Console.WriteLine("4) Delete habits");
            Console.WriteLine("0) Exit");

            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    CreateHabit();
                    break;
                case "2":
                    DisplayUpdateMenu();
                    break;
                case "3":
                    DisplayViewMenu();
                    break;
                case "4":
                    DisplayDeletionMenu();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }

        public static void CreateHabit()
        {
            string? userInput;
            do
            {
                Console.Clear();

                userInput = Console.ReadLine();

                string habitName = UserInput.GetNameInput("Input habit name (no spaces or special characters. Must be no more than 25 characters):");
                string habitDate = UserInput.GetDateInput("Input habit date (Use mm-dd-yyyy format):");
                string habitQuantity = UserInput.GetQuantityInput("Input habit quantity:");

                Database.CreateTable(habitName, habitDate, habitQuantity);

                Console.WriteLine($"\nHabit Created: {habitName}");

                //switch (userInput)
                //{
                //    case "1":
                //        Console.WriteLine("Input habit name (no spaces, numbers or special characters):");
                //        string? habitName = Console.ReadLine();
                //        Console.WriteLine("Input habit date:");
                //        string? habitDate = Console.ReadLine();
                //        Console.WriteLine("Input habit quantity:");
                //        string? habitQuantity = Console.ReadLine();

                //        Database.CreateTable(habitName, habitDate, habitQuantity);

                //        Console.WriteLine($"\nHabit Created: {habitName}");

                //        break;
                //    case "0":
                //        break;
                //    default:
                //        Console.WriteLine("Invalid selection. Please try again or enter 0 to return to creation menu.");
                //        break;
                //}

            } while (userInput != "0");
            DisplayMainMenu();
        }
        public static void DisplayUpdateMenu()
        {
            string? userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("\n\n-------- UPDATE MENU -------");
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1) Update Habit");
                Console.WriteLine("0) Back to Main Menu");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        // TODO: Create update functionality
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again or enter 0 to return to update menu.");
                        break;
                }

            } while (userInput != "0");
            DisplayMainMenu();
        }
        public static void DisplayViewMenu() { }
        public static void DisplayDeletionMenu() { }
    }
}
