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
                Console.WriteLine("4) View habit entries");
                Console.WriteLine("5) Delete habit entries");
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
                        ViewHabits();
                        Console.Clear();
                        break;
                    case "5":
                        DeleteHabitRecords();
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
            List<string> tableNames = Database.GetTableNames();
            string date;
            string quantity;

            while (true)
            {

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


                // TODO -- Write a reusable method for loop below
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

        private static void ViewHabits()
        {
            string habitName;
            string viewHabitMessage = "Select a habit from the database to view entries.\nOr, enter 0 to return to main menu:";
            List<string> tableNames = Database.GetTableNames();

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
    }
}