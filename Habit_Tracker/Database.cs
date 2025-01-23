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
        //private string _validatedString;
        //public string ValidatedString
        //{
        //    get { return _validatedString; }
        //    set
        //    {
        //        if (string.IsNullOrWhiteSpace(value))
        //        {
        //            throw new ArgumentException("Input cannot be empty or contain whitespace.");
        //        }

        //        if (value.Length >= 25)
        //        {
        //            throw new ArgumentException("Input must be less than 25 characters long.");
        //        }

        //        if (value.Contains(" "))
        //        {
        //            throw new ArgumentException("Input cannot contain spaces.");
        //        }

        //        if (!Regex.IsMatch(value, @"^[a-zA-Z]+$"))
        //        {
        //            throw new ArgumentException("Input must contain only alphabetic characters (a-z, A-Z).");
        //        }

        //        _validatedString = value; ;
        //    }
        //}

        public static void CreateTable(string Name, string Date, string Quantity)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = @$"
                            CREATE TABLE IF NOT EXISTS [{Name}] (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                [{Date}] TEXT,
                                [{Quantity}] INTEGER
                                )";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}

