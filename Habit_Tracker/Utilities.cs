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
            string habitName = "";
            string inputNameMessage = "Input habit name (No spaces or special characters. Must be no more than 25 characters).\nOr, enter 0 to return to main menu:";
            string duplicateFoundMessage = "";

            while (true)
            {
                habitName = UserInput.GetNameInput($"{duplicateFoundMessage}{inputNameMessage}");

                if (habitName == "0") break;

                if (Database.CheckForDuplicates(habitName))
                {
                    duplicateFoundMessage = "Habit already exists. Please use a unique name.\n\n";
                    continue;
                }

                Database.CreateTable(habitName);

                Console.Clear();

                Console.WriteLine($"Habit Created: {habitName}\n\n.");

                string inputError = "";

                // TODO -- Write a reusable method for loop below
                while (true)
                {
                    string addNextHabit = UserInput.GetUserInput($"{inputError}Would you like to create another habit? (Y/N).", false);

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
            string inputNameMessage = "Input habit name (No spaces or special characters. Must be no more than 25 characters).\nOr, enter 0 to return to main menu:";
            string noHabitFoundMessage = "";

            while (true)
            {
                // TODO -- list all habit tables in the database to assist user in making seleciton

                habitName = UserInput.GetNameInput($"{noHabitFoundMessage}{inputNameMessage}");
                if (habitName == "0") break;

                if (!Database.CheckForDuplicates(habitName))
                {
                    noHabitFoundMessage = "Habit not found. Please enter an existing habit name.\n\n";
                    continue;
                }

                string date = UserInput.GetDateInput("Input habit date (Use mm-dd-yyyy format).\nOr, enter 0 to return to main menu:");
                if (date == "0") break;

                string quantity = UserInput.GetQuantityInput("Input habit quantity (Only whole numbers accepted).\nOr, enter 0 to return to main menu:");
                if (quantity == "0") break;

                Database.Insert(habitName, date, quantity);

                Console.Clear();

                string inputError = "";

                // TODO -- Write a reusable method for loop below
                while (true)
                {
                    string insertNextHabit = UserInput.GetUserInput($"{inputError}Would you like to insert another habit entry? (Y/N).", false);

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
    }
}