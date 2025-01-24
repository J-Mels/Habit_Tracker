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
            string? userInput;
            bool programIsRunning = true;
            bool invalidSelection = false;

            while (programIsRunning)
            {
                Console.Clear();
                Console.WriteLine("\n\n-------- MAIN MENU --------");
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1) Create new habit");
                Console.WriteLine("2) Update habits");
                Console.WriteLine("3) View habits");
                Console.WriteLine("4) Delete habits");
                Console.WriteLine("0) Exit");

                if (invalidSelection)
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        CreateHabit();
                        break;
                    case "2":
                        //DisplayUpdateMenu();
                        break;
                    case "3":
                        //DisplayViewMenu();
                        break;
                    case "4":
                        //DisplayDeletionMenu();
                        break;
                    case "0":
                        programIsRunning = false;
                        break;
                    default:
                        invalidSelection = true;
                        break;
                }
            }
        }

        public static void CreateHabit()
        {
            bool returnToMainMenu = false;
            string habitName = "";

            while (!returnToMainMenu)
            {

                habitName = UserInput.GetNameInput("Input habit name (No spaces or special characters. Must be no more than 25 characters).\nOr, enter 0 to return to main menu:");
                if (habitName == "0") returnToMainMenu = true;
                string habitDate = UserInput.GetDateInput("Input habit date (Use mm-dd-yyyy format).\nOr, enter 0 to return to main menu:");
                if (habitDate == "0") returnToMainMenu = true;
                string habitQuantity = UserInput.GetQuantityInput("Input habit quantity (Only whole numbers accepted).\nOr, enter 0 to return to main menu:");
                if (habitQuantity == "0") returnToMainMenu = true;
                Database.CreateTable(habitName, habitDate, habitQuantity);

                string addNextHabit = UserInput.GetUserInput($"\nHabit Created: {habitName}.\n\nWould you like to create another habit? (Y/N).");
                while (addNextHabit != "Y" && addNextHabit != "y" && addNextHabit != "N" && addNextHabit != "n")
                {
                    if (addNextHabit == "Y" || addNextHabit == "y")
                    {
                        continue;
                    }
                    else if (addNextHabit == "N" || addNextHabit == "n")
                    {
                        returnToMainMenu = true;
                        break;
                    }
                    else
                    {
                        addNextHabit = UserInput.GetUserInput("Invalid selection. Enter Y to create another habit or N to return to main menu.");
                    }

                }

            }

            DisplayMainMenu();
        }
    }
}
