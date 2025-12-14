using System;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n╔══════════════════════════════════════╗\n║       💸 FINANCE FLOW PLANNER        ║\n╚══════════════════════════════════════╝\n");
            Console.WriteLine("MAIN MENU:");
            Console.WriteLine("1. Add financial goal");
            Console.WriteLine("2. View goals");
            Console.WriteLine("3. Add expense");
            Console.WriteLine("4. Show analytics");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose option: ");
            string? input = Console.ReadLine();
            Console.WriteLine($"You chose: {input}");
        }
    }
}