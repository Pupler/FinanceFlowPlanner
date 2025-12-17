using System;
using System.Runtime.InteropServices;

class Program
{
    public static List<FinancialGoal> goals = [];
    public static List<Expense> expenses = [];
    static void AddFinancialGoal()
    {
        Console.Write("Enter goal description: ");
        string? goalName = Console.ReadLine();

        Console.Write("Enter target sum: ");
        string? amountInput = Console.ReadLine();
        
        Console.Write("Deadline (YYYY-MM-DD) or Enter for none: ");
        string? deadlineInput = Console.ReadLine();

        if (DateTime.TryParse(deadlineInput, out DateTime deadline))
        {
            if (decimal.TryParse(amountInput, out decimal amount))
            {
                FinancialGoal goal = new(
                    Name: string.IsNullOrEmpty(goalName) ? "Unnamed" : goalName,
                    TargetAmount: amount,
                    Deadline: deadline
                );

                goals.Add(goal);
                Console.Clear();
                Console.WriteLine("Goal was added!");
            }
            else
            {
                Console.WriteLine("Error: The sum must be number!");
            }
        }
    }
    static void ShowGoals()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║            📋 GOALS LIST             ║");
        Console.WriteLine("╚══════════════════════════════════════╝");

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
                Console.WriteLine($"│   Deadline: {goals[i].Deadline}");
                Console.WriteLine($"└─────────────────────────────────────");
            }
            Console.Write("\nAdd money to some goal? (y/n): ");
            string? add_money_input = Console.ReadLine();

            if (char.TryParse(add_money_input, out char add_money) && (add_money == 'y' || add_money == 'Y'))
            {
                Console.Write("Enter the goal number: ");
                string? goalNumInput = Console.ReadLine();

                if (int.TryParse(goalNumInput, out int goalNum))
                {
                    if (goalNum < 1 || goalNum > goals.Count)
                    {
                        Console.WriteLine("Error: The goal doesn't exist!");
                    }
                    else
                    {
                        Console.Write("Enter sum of money: ");
                        string? sumInput = Console.ReadLine();

                        if (decimal.TryParse(sumInput, out decimal sum) && sum > 0)
                        {
                            goals[goalNum - 1] = goals[goalNum - 1].AddMoney(sum);
                            Console.WriteLine($"{sum:C} was added!");
                        }
                        else
                        {
                            Console.WriteLine("Error: The sum must be a positive number!");
                        }   
                    }
                }
                else
                {
                    Console.WriteLine("Error: Enter a number!");
                }
            }
            else
            {
                return;
            }
        }
    }
    static void AddExpense()
    {
        Console.Write("Enter expense description: ");
        string? expenseDesc = Console.ReadLine();

        Console.Write("Enter category: ");
        string? expenseCtg = Console.ReadLine();

        Console.Write("Enter amount: ");
        string? amountInput = Console.ReadLine();

        if (decimal.TryParse(amountInput, out decimal amount) && amount > 0)
        {
            Expense expense = new(
                Description: string.IsNullOrEmpty(expenseDesc) ? "Unnamed" : expenseDesc,
                Category: string.IsNullOrEmpty(expenseCtg) ? "Other" : expenseCtg,
                Amount: amount
            );

            expenses.Add(expense);
            Console.Clear();
            Console.WriteLine("Expense was added!");
        }
        else
        {
            Console.WriteLine("Error: Amount must be a positive number!");
        }
    }
    static void ShowExpenses()
    {
        Console.WriteLine("ShowExpenses");
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
            Console.WriteLine("4. Show expenses");
            Console.WriteLine("5. Show analytics");
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
                        ShowExpenses();
                        break;
                    case 5:
                        ShowAnalytics();
                        break;
                    default:
                        Console.WriteLine("Error: Wrong menu index!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Error: Write a number!");
            }
        }
    }
}