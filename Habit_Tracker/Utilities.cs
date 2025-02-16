using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                Console.WriteLine("4) View habit entries");
                Console.WriteLine("5) Delete habit entries");
                Console.WriteLine("6) Delete habit");
                Console.WriteLine("0) Exit");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        ViewHabits();
                        Console.Clear();
                        break;
                    case "2":
                        CreateHabit();
                        Console.Clear();
                        break;
                    case "3":
                        InsertHabit();
                        Console.Clear();
                        break;
                    case "4":
                        UpdateHabit();
                        Console.Clear();
                        break;
                    case "5":
                        DeleteHabitRecords();
                        Console.Clear();
                        break;
                    case "6":
                        DeleteHabitTable();
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

                if (UserInput.AddAnother("Would you like to create another habit? (Y/N)"))
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        private static void InsertHabit()
        {
            string habitName;
            string selectHabitMessage = "Select a habit from the database.\nOr, enter 0 to return to main menu:";
            string date;
            string quantity;

            while (true)
            {

                List<string> tableNames = Database.GetTableNames();

                if (tableNames.Count == 0)
                {
                    Console.WriteLine("No habits in the database. Press any key to return to main menu ...");
                    return;
                }

                habitName = UserInput.GetHabitNameByIndex(tableNames, selectHabitMessage);

                if (habitName == "0")
                    break;

                Console.Clear();

                date = UserInput.GetDateInput("Input habit date (Use mm-dd-yyyy format).\nOr, enter 0 to return to main menu:");
                if (date == "0") break;

                Console.Clear();
                quantity = UserInput.GetNumberInput("Input habit quantity (Only whole numbers accepted).\nOr, enter 0 to return to main menu:");
                if (quantity == "0") break;

                Database.Insert(habitName, date, quantity);

                Console.Clear();

                Console.WriteLine($"Habit entry inserted into {habitName}\n");

                if (UserInput.AddAnother($"Would you like to insert another habit entry? (Y/N)"))
                {
                    continue;
                }
                else
                {
                    break;
                }

            }

        }

        private static void UpdateHabit()
        {
            string habitName;
            string updateHabitMessage = "Select a habit from the database to update an entry.\nOr, enter 0 to return to main menu:";
            string habitRecords;
            string idSelection;


            while (true)
            {
                List<string> tableNames = Database.GetTableNames();

                if (tableNames.Count == 0)
                {
                    Console.WriteLine("No habits in the database. Press any key to return to main menu ...");
                    return;
                }

                habitName = UserInput.GetHabitNameByIndex(tableNames, updateHabitMessage);
                if (habitName == "0")
                    break;

                Console.Clear();

                habitRecords = Database.GetHabitRecords(habitName);

                if (habitRecords == "No entries found")
                {
                    Console.WriteLine($"No entries found for {habitName}");
                    Console.WriteLine("Press any key to continue ...");
                    Console.ReadKey();
                    continue;
                }

                while (true)
                {
                    Console.Clear();

                    idSelection = UserInput.GetNumberInput($"{habitRecords}\n\nEnter the ID of the record you would like to update, or enter 0 to return to main menu.");

                    if (idSelection == "0")
                        return;

                    if (Database.CheckRecord(habitName, idSelection))
                    {
                        Console.Clear();
                        string date = UserInput.GetDateInput("Input habit date (Use mm-dd-yyyy format).\nOr, enter 0 to return to main menu:");
                        if (date == "0") return;
                        Console.Clear();

                        string quantity = UserInput.GetNumberInput("Input habit quantity (Only whole numbers accepted).\nOr, enter 0 to return to main menu:");
                        if (quantity == "0") return;
                        Console.Clear();

                        Database.Update(habitName, idSelection, date, quantity);

                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nRecord with Id {idSelection} doesn't exist.");
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                        continue;
                    }


                }

                Console.WriteLine($"Entry #{idSelection} updated from {habitName}");

                if (UserInput.AddAnother($"\nWould you like to update another habit record? (Y/N)"))
                    continue;
                else { break; }
            }
        }

        private static void ViewHabits()
        {
            string habitName;
            string viewHabitMessage = "Select a habit from the database to view entries.\nOr, enter 0 to return to main menu:";
            List<string> tableNames = Database.GetTableNames();

            if (tableNames.Count == 0)
            {
                Console.WriteLine("No habits in the database. Press any key to return to main menu ...");
                return;
            }

            while (true)
            {

                habitName = UserInput.GetHabitNameByIndex(tableNames, viewHabitMessage);
                if (habitName == "0")
                    break;

                Console.Clear();

                Console.WriteLine(Database.GetHabitRecords(habitName));

                if (UserInput.AddAnother($"Would you like to view more habit records? (Y/N)"))
                    continue;
                else { break; }
            }
        }

        private static void DeleteHabitRecords()
        {
            string habitName;
            string idSelection;
            string habitRecords;
            int rowsDeleted;
            string deleteHabitMessage = "Select a habit from the database to delete entries.\nOr, enter 0 to return to main menu:";

            while (true)
            {
                Console.Clear();

                List<string> tableNames = Database.GetTableNames();

                if (tableNames.Count == 0)
                {
                    Console.WriteLine("No habits in the database. Press any key to return to main menu ...");
                    return;
                }

                habitName = UserInput.GetHabitNameByIndex(tableNames, deleteHabitMessage);

                if (habitName == "0")
                    break;

                Console.Clear();

                habitRecords = Database.GetHabitRecords(habitName);

                if (habitRecords == "No entries found")
                {
                    Console.WriteLine($"No entries found for {habitName}");
                    Console.WriteLine("Press any key to continue ...");
                    Console.ReadKey();
                    continue;
                }

                while (true)
                {
                    Console.Clear();

                    idSelection = UserInput.GetNumberInput($"{habitRecords}\n\nEnter the ID of the record you would like to delete, or enter 0 to return to main menu.");

                    if (idSelection == "0")
                        return;


                    rowsDeleted = Database.DeleteRecord(habitName, idSelection);

                    if (rowsDeleted == 0)
                    {
                        Console.WriteLine("\nThe record ID entered does not exist in the table.");
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                        continue;
                    }

                    break;

                }

                Console.WriteLine($"\nEntry #{idSelection} deleted from {habitName}");

                if (UserInput.AddAnother($"\nWould you like to delete another habit record? (Y/N)"))
                    continue;
                else { break; }
            }

        }

        private static void DeleteHabitTable()
        {
            string habitName;
            string deleteHabitMessage = "Select a habit from the database to delete.\nOr, enter 0 to return to main menu:";


            while (true)
            {
                Console.Clear();

                List<string> tableNames = Database.GetTableNames();

                if (tableNames.Count == 0)
                {
                    Console.WriteLine("No habits in the database. Press any key to return to main menu ...");
                    return;
                }

                habitName = UserInput.GetHabitNameByIndex(tableNames, deleteHabitMessage);

                if (habitName == "0")
                    break;


                Console.Clear();

                string confirmationPrompt = $"Are you sure you want to delete habit {habitName}?";
                string warning = "\n\n***WARNING*** Once deleted, the habit table and all records cannot be retrieved.";
                string confirmation = "\n\nTo confirm your choice, enter the name of the habit below, or 0 to cancel the operation.";

                string confirmSelection = UserInput.GetUserInput($"{confirmationPrompt}{warning}{confirmation}");

                if (confirmSelection == "0")
                {
                    continue;
                }
                else if (confirmSelection == habitName)
                {
                    Database.DeleteTable(habitName);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Habit name entered does not match. Please try again.");
                    Console.WriteLine("\nPress any key to continue ...");
                    Console.ReadKey();
                    continue;
                }

                Console.Clear();

                Console.WriteLine($"{habitName} deleted.");

                if (UserInput.AddAnother($"\nWould you like to delete another habit? (Y/N)"))
                    continue;
                else { break; }
            }
        }
    }
}