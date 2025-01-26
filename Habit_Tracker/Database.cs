using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Habit_Tracker
{
    internal class Database
    {
        private static string connectionString = "Data Source=C:/Users/melso/source/repos/Habit_Tracker/Habit_Tracker/Database/app.db;Version=3";
        //public static void CreateDB()
        //{
        //}

        public static void CreateTable(string name, string date, string quantity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @$"
                            CREATE TABLE IF NOT EXISTS '{name}' (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                '{date}' TEXT,
                                '{quantity}' INTEGER
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

    }
}

