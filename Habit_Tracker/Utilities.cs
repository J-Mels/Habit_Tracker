using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habit_Tracker
{
    class MainMenu
    {
        public static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\n-------- MAIN MENU --------");
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1) Create new habit");
            Console.WriteLine("2) Update habits");
            Console.WriteLine("3) View habits");
            Console.WriteLine("4) Delete habits");
            Console.WriteLine("5) Exit");
        }
    }
}
