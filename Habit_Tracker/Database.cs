using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;

namespace Habit_Tracker
{
    internal class Database
    {
        private static string connectionString = "Data Source=C:/Users/melso/source/repos/Habit_Tracker/Habit_Tracker/Database/app.db;Version=3";
        //public static void CreateDB()
        //{
        //}

        public static void CreateTable(string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @$"
                            CREATE TABLE IF NOT EXISTS '{name}' (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Date TEXT,
                                Quantity INTEGER
                                )";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void Insert(string name, string date, string quantity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"INSERT INTO {name}(Date, Quantity) VALUES('{date}', '{quantity}')";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static string[] GetTableNames()
        {

            string names = "";
            string[] namesList;

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // TODO (optional) -- Format output below in the console -- for instance, into columns with 5-10 rows, etc.
                            names += $"{reader.GetString(0)},";
                        }
                        namesList = names.Split(',');
                        Array.Sort(namesList);
                        return (namesList);
                    }
                }
            }
        }

        public static void GetHabitRecords(string habit)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT * FROM {habit}";

                List<HabitRecord> tableData = new();

                SQLiteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(
                        new HabitRecord
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "MM-dd-yyyy", new CultureInfo("en-US")),
                            Quantity = reader.GetInt32(2),
                        });
                    }

                    connection.Close();

                    foreach (var item in tableData)
                    {
                        Console.WriteLine($"{item.Id} -- {item.Date.ToString("MMM-dd-yyyy")} -- Quantity: {item.Quantity}");
                    }
                }
                else
                {
                    Console.WriteLine("No entries found");
                }
            }
        }


        public static bool CheckForDuplicates(string habit)
        {
            string[] habitNames = GetTableNames();

            foreach (string habitName in habitNames)
            {
                if (habit == habitName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

