using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

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
                        Console.WriteLine("Tables in the database:");
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

