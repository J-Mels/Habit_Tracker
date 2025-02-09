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
            string? userInput;
            bool programIsRunning = true;

            while (programIsRunning)
            {
                Console.WriteLine("-------- MAIN MENU --------");
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1) Create new habit");
                Console.WriteLine("2) Insert habit entry");
                Console.WriteLine("3) Update habit entry");
                Console.WriteLine("4) View habits");
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
                        Database.GetHabitRecords("a");
                        Console.WriteLine("\nPress any key to continue ...");
                        Console.ReadKey();
                        Console.Clear();
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
            string habitName = "";
            string inputNameMessage = "Input habit name (No spaces or special characters. Must be no more than 25 characters).\nOr, enter 0 to return to main menu:";
            string tableNames = string.Join("\n", Database.GetTableNames());
            string existingHabits = $"\n\nHabits already in the Database:\n\n{tableNames}\n";


            while (true)
            {


                Console.Clear();

                habitName = UserInput.GetNameInput($"{inputNameMessage}{existingHabits}");

                if (habitName == "0") break;

                if (Database.CheckForDuplicates(habitName))
                {
                    Console.Clear();
                    Console.WriteLine("Habit already exists. Please use a unique name.\n");
                    Console.WriteLine("(Press any key to continue ...)");
                    Console.ReadKey();
                    continue;
                }

                Database.CreateTable(habitName);

                Console.Clear();

                Console.WriteLine($"Habit Created: {habitName}\n");

                string inputError = "";

                // TODO -- Write a reusable method for loop below
                while (true)
                {
                    string addNextHabit = UserInput.GetUserInput($"{inputError}Would you like to create another habit? (Y/N).");

                    if (addNextHabit.Equals("N", StringComparison.OrdinalIgnoreCase))
                        return;
                    else if (addNextHabit.Equals("Y", StringComparison.OrdinalIgnoreCase))
                        break;
                    else
                    {
                        inputError = "Invalid selection. Only Y/y or N/n accepted.\n\n";
                        Console.Clear();
                    }
                }
            }
        }

        private static void InsertHabit()
        {
            string habitName = "";
            string selectHabitMessage = "Select a habit from the database.\nOr, enter 0 to return to main menu:";
            List<string> tableNames = Database.GetTableNames();
            string tableNamesNumbered = "";
            string habitSelection;
            int habitIndex;

            for (int i = 0; i < tableNames.Count; i++)
            {
                tableNamesNumbered += $"{i + 1}) {tableNames[i]}\n";
            }

            while (true)
            {

                Console.Clear();

                habitSelection = UserInput.GetUserInput($"{selectHabitMessage}\n\n{tableNamesNumbered}"); /////////////////PROBLEM: CONSOLE MIGHT GET CLEARED

                if (habitSelection == "0") break;

                if (int.TryParse(habitSelection, out habitIndex) && habitIndex <= tableNames.Count)
                {
                    habitName = tableNames[habitIndex - 1];
                }
                else
                {
                    Console.WriteLine($"\nInvalid selection. Please select a whole number withinrange (1 - {tableNames.Count}) matching a habit in the above list.");
                    Console.WriteLine("\nPress any key to continue and try again.");
                    Console.ReadKey();
                    continue;
                }

                Console.Clear();
                string date = UserInput.GetDateInput("Input habit date (Use mm-dd-yyyy format).\nOr, enter 0 to return to main menu:");
                if (date == "0") break;

                Console.Clear();
                string quantity = UserInput.GetQuantityInput("Input habit quantity (Only whole numbers accepted).\nOr, enter 0 to return to main menu:");
                if (quantity == "0") break;

                Database.Insert(habitName, date, quantity);

                Console.Clear();

                string inputError = "";

                Console.WriteLine($"Habit entry inserted into {habitName}\n");


                // TODO -- Write a reusable method for loop below
                while (true)
                {
                    string insertNextHabit = UserInput.GetUserInput($"{inputError}Would you like to insert another habit entry? (Y/N).");

                    if (insertNextHabit.Equals("N", StringComparison.OrdinalIgnoreCase))
                        return;
                    else if (insertNextHabit.Equals("Y", StringComparison.OrdinalIgnoreCase))
                        break;
                    else
                    {
                        inputError = "Invalid selection. Only Y/y or N/n accepted.\n\n";
                        Console.Clear();
                    }
                }

            }


        }

        private static void ViewHabits()
        {

        }
    }
}