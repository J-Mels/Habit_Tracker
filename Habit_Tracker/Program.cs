﻿using System;
using System.Data.SQLite;
using System.Security.Cryptography.X509Certificates;

namespace Habit_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to your personal Habit Logger");

            Utilities.DisplayMainMenu();

            // End Program
            Console.WriteLine("Application shutting down...");
            Console.WriteLine();
        }

    }
}

