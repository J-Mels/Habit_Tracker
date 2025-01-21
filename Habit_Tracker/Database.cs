using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habit_Tracker
{
    internal class Database
    {
        public static void CreateDB()
        {
            string dbPath = "C:/Users/melso/source/repos/Habit_Tracker/Habit_Tracker/Database/app.db";
            string connectionString = $"Data Source={dbPath};Version=3";

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
