using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public static List<string> GetTableNames()
        {

            //string names = "";
            List<string> namesList = new List<string>();

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
                            namesList.Add(reader.GetString(0));
                        }
                        namesList.Sort();
                        return (namesList);
                    }
                }
            }
        }

        public static string GetHabitRecords(string habitName)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT * FROM {habitName}";

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

                    string stringData = "";

                    stringData += $"------Entries for habit \"{habitName}\"------\n";
                    stringData += "---------------------------------------------------\n";

                    foreach (var item in tableData)
                    {
                        stringData += $"{item.Id} -- {item.Date.ToString("MMM-dd-yyyy")} -- Quantity: {item.Quantity}\n";
                    }

                    stringData += "---------------------------------------------------";

                    return stringData;
                }
                else
                {
                    return "No entries found";
                }
            }
        }

        public static bool CheckRecord(string habitName, string habitEntryId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM {habitName} WHERE Id = {habitEntryId})";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                connection.Close();

                if (checkQuery == 0)
                {
                    return false;
                }

                return true;
            }
        }

        public static void Update(string habitName, string id, string date, string quantity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"UPDATE {habitName} SET date = '{date}', quantity = {quantity} WHERE Id = {id}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static int DeleteRecord(string habitName, string habitEntryId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"DELETE from '{habitName}' WHERE Id = '{habitEntryId}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                connection.Close();

                return rowCount;
            }
        }

        public static void DeleteTable(string habitName)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"DROP TABLE '{habitName}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                connection.Close();

            }
        }

        public static bool CheckForDuplicates(string habit)
        {
            List<string> habitNames = GetTableNames();

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

