using System;
using System.Data.SQLite;
using System.Security.Cryptography.X509Certificates;

namespace Habit_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {

            bool programIsRunning = true;

            Console.WriteLine("Welcome to your personal Habit Logger");
            MainMenu.DisplayMainMenu();


        }

    }
}

