using System;
using System.Runtime.InteropServices;

class Program
{
    public static List<FinancialGoal> goals = [];
    static void AddFinancialGoal()
    {
        Console.Write("Enter goal description: ");
        string? goalName = Console.ReadLine();

        Console.Write("Enter target sum: ");
        string? amountInput = Console.ReadLine();

        if (decimal.TryParse(amountInput, out decimal amount))
        {
            FinancialGoal goal = new(
                Name: goalName ?? "Unnamed",
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
        if (goals.Count == 0)
        {
            Console.WriteLine("Goals list is empty!");
        }
        else
        {
            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"┌─[{i+1}]─ {goals[i].Name}");
                Console.WriteLine($"│   Target: {goals[i].TargetAmount:C}");
                Console.WriteLine($"│   Progress: {goals[i].CurrentAmount:C} / {goals[i].TargetAmount:C}");
                Console.WriteLine($"│   Remaining: {goals[i].RemainingAmount:C}");
                Console.WriteLine($"└─────────────────────────────────────");
            }
        }
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
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = 
        new System.Globalization.CultureInfo("de-DE");
    
        System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = 
        new System.Globalization.CultureInfo("de-DE");

        while (true)
        {
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║       💸 FINANCE FLOW PLANNER        ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("\nMAIN MENU:");
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