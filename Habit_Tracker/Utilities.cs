﻿using System;
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

                habitName = UserInput.GetNameInput("Input habit name (No spaces or special characters. Must be no more than 25 characters). Or, enter 0 to return to main menu:");
                if (habitName == "0") returnToMainMenu = true;
                string habitDate = UserInput.GetDateInput("Input habit date (Use mm-dd-yyyy format). Or, enter 0 to return to main menu:");
                if (habitDate == "0") returnToMainMenu = true;
                string habitQuantity = UserInput.GetQuantityInput("Input habit quantity (Only whole numbers accepted). Or, enter 0 to return to main menu:");
                if (habitQuantity == "0") returnToMainMenu = true;
                Database.CreateTable(habitName, habitDate, habitQuantity);


            }

            Console.WriteLine($"\nHabit Created: {habitName}. Press any key to continue.");
            Console.ReadKey();
            DisplayMainMenu();
        }
        //public static void DisplayUpdateMenu()
        //{
        //    string? userInput;
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine("\n\n-------- UPDATE MENU -------");
        //        Console.WriteLine("\nPlease select an option:");
        //        Console.WriteLine("1) Update Habit");
        //        Console.WriteLine("0) Back to Main Menu");

        //        userInput = Console.ReadLine();

        //        switch (userInput)
        //        {
        //            case "1":
        //                // TODO: Create update functionality
        //                break;
        //            case "0":
        //                break;
        //            default:
        //                Console.WriteLine("Invalid selection. Please try again or enter 0 to return to update menu.");
        //                break;
        //        }

        //    } while (userInput != "0");
        //    DisplayMainMenu();
        //}
        //public static void DisplayViewMenu() { }
        //public static void DisplayDeletionMenu() { }
    }
}
