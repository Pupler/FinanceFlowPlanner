using System;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;

class Program
{
    public static List<FinancialGoal> goals = [];
    public static List<Expense> expenses = [];

    static void showMenu()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║       💸 FINANCE FLOW PLANNER        ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.WriteLine("\nMAIN MENU:");
        Console.WriteLine("1. Add financial goal");
        Console.WriteLine("2. View goals");
        Console.WriteLine("3. Add expense");
        Console.WriteLine("4. View expenses");
        Console.WriteLine("5. Show analytics");
        Console.WriteLine("0. Exit");
        Console.Write("\nChoose option: ");
    }
    static void AddFinancialGoal()
    {
        Console.Write("Enter goal description: ");
        string? goalName = Console.ReadLine();

        Console.Write("Enter target sum: ");
        string? amountInput = Console.ReadLine();

        Console.Write("Deadline (YYYY-MM-DD) or Enter for none: ");
        string? deadlineInput = Console.ReadLine();

        DateTime? deadline = null;

        if (!string.IsNullOrWhiteSpace(deadlineInput))
        {
            if (!DateTime.TryParse(deadlineInput, out DateTime parsedDeadline))
            {
                Console.WriteLine("Error: Invalid deadline format!");
                return;
            }

            deadline = parsedDeadline;
        }
        if (decimal.TryParse(amountInput, out decimal amount))
        {
            FinancialGoal goal = new(
                Name: string.IsNullOrEmpty(goalName) ? "Unnamed" : goalName,
                TargetAmount: amount,
                Deadline: deadline
            );

            goals.Add(goal);
            Console.Clear();
            Console.WriteLine("✅ Goal was added!");
        }
        else
        {
            Console.WriteLine("Error: The sum must be number!");
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
                Console.WriteLine($"┌─[{i + 1}]─ {goals[i].Name}");
                Console.WriteLine($"│   Target: {goals[i].TargetAmount:C}");
                Console.WriteLine($"│   Progress: {goals[i].CurrentAmount:C} / {goals[i].TargetAmount:C}");
                Console.WriteLine($"│   Remaining: {goals[i].RemainingAmount:C}");
                Console.WriteLine($"│   Deadline: {goals[i].DeadlineDisplay}");
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

        Console.WriteLine("Categories: 1.Food 2.Transport 3.Entertainment 4.Bills 5.Shopping 6.Health 7.Other");
        Console.Write("Choose category (1-7): ");
        string? categoryInput = Console.ReadLine();

        Console.Write("Enter amount: ");
        string? amountInput = Console.ReadLine();

        Console.Write("Enter date (YYYY-MM-DD) or Enter for today: ");
        string? dateInput = Console.ReadLine();

        DateTime? date = null;

        if (!string.IsNullOrWhiteSpace(dateInput))
        {
            if (!DateTime.TryParse(dateInput, out DateTime parsedDate))
            {
                Console.WriteLine("Error: Invalid date format!");
                return;
            }

            date = parsedDate;
        }
        if (int.TryParse(categoryInput, out int categoryNum) && (categoryNum >=1) && (categoryNum <=7) &&
        decimal.TryParse(amountInput, out decimal amount) && amount > 0)
        {
            Expense expense = new(
                Description: string.IsNullOrEmpty(expenseDesc) ? "Unnamed" : expenseDesc,
                Category: (ExpenseCategory)(categoryNum - 1),
                Amount: amount,
                Date: date
            );

            expenses.Add(expense);
            Console.Clear();
            Console.WriteLine("✅ Expense was added!");
        }
        else
        {
            Console.WriteLine("Error: Invalid category or amount input!");
        }
    }
    static void ShowExpenses()
    {
        decimal TotalExpensesSum = 0;

        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║           📋 EXPENSES LIST           ║");
        Console.WriteLine("╚══════════════════════════════════════╝");

        if (expenses.Count == 0)
        {
            Console.WriteLine("Expenses list is empty!");
        }
        else
        {
            for (int i = 0; i < expenses.Count; i++)
            {
                Console.WriteLine($"┌─[{i + 1}]─ Expense");
                Console.WriteLine($"│   Description: {expenses[i].Description}");
                Console.WriteLine($"│   Category: {expenses[i].Category}");
                Console.WriteLine($"│   Amount: {expenses[i].Amount:C}");
                Console.WriteLine($"│   Date: {expenses[i].DateDisplay}");
                Console.WriteLine($"└─────────────────────────────────────");

                TotalExpensesSum += expenses[i].Amount;
            }

            Console.WriteLine($"Total spent: {TotalExpensesSum:C}");
        }
    }
    static void ShowAnalytics()
    {
        // TEST
        //expenses.Add(new Expense("Burger", ExpenseCategory.Food, 12, DateTime.Now));
        //expenses.Add(new Expense("Bus", ExpenseCategory.Transport, 3, DateTime.Now));
        //expenses.Add(new Expense("Netflix", ExpenseCategory.Entertainment, 15, DateTime.Now));
        //expenses.Add(new Expense("Rent", ExpenseCategory.Bills, 500, DateTime.Now));
        //expenses.Add(new Expense("T-shirt", ExpenseCategory.Shopping, 25, DateTime.Now));
       // expenses.Add(new Expense("Pills", ExpenseCategory.Health, 8, DateTime.Now));
        //expenses.Add(new Expense("Gift", ExpenseCategory.Other, 30, DateTime.Now));

        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║             📊 ANALYTICS             ║");
        Console.WriteLine("╚══════════════════════════════════════╝");

        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses to analyze yet!");
            return;
        }

        ExpenseCategory topCtg = ExpenseCategory.Other;
        decimal maxAmount = 0;
        decimal allExpensesTotal = 0;
        foreach (var expense in expenses)
        {
            allExpensesTotal += expense.Amount;
        }

        foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
        {
            decimal categoryTotal = 0;

            foreach (var expense in expenses)
            {
                if (expense.Category == category)
                {
                    categoryTotal += expense.Amount;

                    if (categoryTotal > maxAmount)
                    {
                        maxAmount = categoryTotal;
                    }
                }
            }

            if (categoryTotal > 0)
            {
                decimal percentageCtg = categoryTotal / allExpensesTotal;
                Console.WriteLine($"{category}: {categoryTotal:C} ({percentageCtg:P2})");
            }

            if (categoryTotal == maxAmount)
            {
                topCtg = category;
            }
        }

        Console.WriteLine($"\n🏆 Top category: {topCtg}");
    }

    static void Main()
    {
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture =
        new System.Globalization.CultureInfo("de-DE");

        System.Globalization.CultureInfo.DefaultThreadCurrentUICulture =
        new System.Globalization.CultureInfo("de-DE");

        while (true)
        {
            showMenu();
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