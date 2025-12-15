using System;
using System.Runtime.InteropServices;

class Program
{
    public static List<FinancialGoal> goals = [];
    static void AddFinancialGoal()
    {
        Console.Write("Enter goal description: ");
        string? goalDesc = Console.ReadLine();

        Console.Write("Enter target sum: ");
        string? amountInput = Console.ReadLine();

        if (decimal.TryParse(amountInput, out decimal amount))
        {
            FinancialGoal goal = new(
                Name: goalDesc ?? "Unnamed",
                TargetAmount: amount
            );

            goals.Add(goal);
        }
        else
        {
            Console.WriteLine("Error: The sum must be number!");
        }
    }
    static void ShowGoals()
    {
        Console.WriteLine("ShowGoals");
    }
    static void AddExpense()
    {
        Console.WriteLine("AddExpense");
    }
    static void ShowAnalytics()
    {
        Console.WriteLine("ShowAnalytics");
    }
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
            
            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Closing program...");
                        return;
                    case 1:
                        AddFinancialGoal();
                        break;
                    case 2:
                        ShowGoals();
                        break;
                    case 3:
                        AddExpense();
                        break;
                    case 4:
                        ShowAnalytics();
                        break;
                    default:
                        Console.WriteLine("Error: Wrong menu index!");
                        break;
                }
            } else
            {
                Console.WriteLine("Error: Write a number!");
            }
        }
    }
}