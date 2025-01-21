using System;
using System.Data.SQLite;

namespace Habit_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        static void CreateDB()
        {
            string dbPath = "C:/Users/melso/source/repos/Habit_Tracker/Habit_Tracker/Database/app.db";
            string connectionString = $"Data Source={dbPath};Version=3";
            //string connectionString = $"Data Source={dbPath}";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS drink_water (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER
                        )";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }


    }
}