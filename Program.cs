using System.Text.Json;

class Program
{
    static void PrintColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    public static List<FinancialGoal> goals = [];

    public static List<Expense> expenses = [];

    static void showMenu()
    {
        PrintColor("\n╔══════════════════════════════════════╗", ConsoleColor.Green);
        PrintColor("║       💸 FINANCE FLOW PLANNER        ║", ConsoleColor.Green);
        PrintColor("╚══════════════════════════════════════╝", ConsoleColor.Green);
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
                Console.Clear();
                PrintColor("❌ Error: Invalid deadline format!", ConsoleColor.Red);
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
            PrintColor("✅ Goal was added!", ConsoleColor.Green);
        }
        else
        {
            Console.Clear();
            PrintColor("❌ Error: The sum must be number!", ConsoleColor.Red);
        }
    }

    static void ShowGoals()
    {
        Console.Clear();
        PrintColor("\n╔══════════════════════════════════════╗", ConsoleColor.DarkBlue);
        PrintColor("║            📋 GOALS LIST             ║", ConsoleColor.DarkBlue);
        PrintColor("╚══════════════════════════════════════╝", ConsoleColor.DarkBlue);

        if (goals.Count == 0)
        {
            Console.Clear();
            PrintColor("⚠️ Goals list is empty!", ConsoleColor.Yellow);
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
                        Console.Clear();
                        PrintColor("❌ Error: The goal doesn't exist!", ConsoleColor.Red);
                    }
                    else
                    {
                        Console.Write("Enter sum of money: ");
                        string? sumInput = Console.ReadLine();

                        if (decimal.TryParse(sumInput, out decimal sum) && sum > 0)
                        {
                            goals[goalNum - 1] = goals[goalNum - 1].AddMoney(sum);
                            Console.Clear();
                            PrintColor($"✅ {sum:C} was added!", ConsoleColor.Green);
                            return;
                        }
                        else
                        {
                            Console.Clear();
                            PrintColor("❌ Error: The sum must be a positive number!", ConsoleColor.Red);
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    PrintColor("❌ Error: Enter a number!", ConsoleColor.Red);
                }
            }
            else
            {
                Console.Clear();
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
                Console.Clear();
                PrintColor("❌ Error: Invalid date format!", ConsoleColor.Red);
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
            PrintColor("✅ Expense was added!", ConsoleColor.Green);
        }
        else
        {
            Console.Clear();
            PrintColor("❌ Error: Invalid category or amount input!", ConsoleColor.Red);
        }
    }

    static void ShowExpenses()
    {
        decimal TotalExpensesSum = 0;

        Console.Clear();
        PrintColor("\n╔══════════════════════════════════════╗", ConsoleColor.DarkBlue);
        PrintColor("║           📋 EXPENSES LIST           ║", ConsoleColor.DarkBlue);
        PrintColor("╚══════════════════════════════════════╝", ConsoleColor.DarkBlue);

        if (expenses.Count == 0)
        {
            Console.Clear();
            PrintColor("⚠️ Expenses list is empty!", ConsoleColor.Yellow);
        }
        else
        {
            for (int i = 0; i < expenses.Count; i++)
            {
                Console.WriteLine($"┌─[{i + 1}]─ {expenses[i].Description}");
                Console.WriteLine($"│   Category: {expenses[i].Category}");
                Console.WriteLine($"│   Amount: {expenses[i].Amount:C}");
                Console.WriteLine($"│   Date: {expenses[i].DateDisplay}");
                Console.WriteLine($"└─────────────────────────────────────");

                TotalExpensesSum += expenses[i].Amount;
            }

            Console.Write("Total spent: ");
            PrintColor($"{TotalExpensesSum:C}", ConsoleColor.DarkGreen);
        }
    }

    static void ShowAnalytics()
    {
        Console.Clear();
        PrintColor("\n╔══════════════════════════════════════╗", ConsoleColor.Magenta);
        PrintColor("║             📊 ANALYTICS             ║", ConsoleColor.Magenta);
        PrintColor("╚══════════════════════════════════════╝", ConsoleColor.Magenta);

        if (expenses.Count == 0)
        {
            Console.Clear();
            PrintColor("⚠️ No expenses to analyze yet!", ConsoleColor.Yellow);
            return;
        }

        ExpenseCategory topCtg = ExpenseCategory.Other;
        decimal maxAmount = 0;
        decimal minExpense = decimal.MaxValue;
        decimal maxExpense = 0;
        decimal allExpensesTotal = 0;
        decimal averageExpenseSum;
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
                }

                if (expense.Amount > maxExpense)
                {
                    maxExpense = expense.Amount;
                }

                if (expense.Amount < minExpense)
                {
                    minExpense = expense.Amount;
                }
            }

            if (categoryTotal > maxAmount)
            {
                maxAmount = categoryTotal;
                topCtg = category;
            }


            if (categoryTotal > 0)
            {
                decimal percentageCtg = categoryTotal / allExpensesTotal;
                Console.WriteLine($"{category}: {categoryTotal:C} ({percentageCtg:P2})");
            }
        }

        averageExpenseSum = allExpensesTotal / expenses.Count;

        Console.WriteLine($"\n🧾 Transactions: {expenses.Count}");
        Console.WriteLine($"⚖️ Average: {averageExpenseSum:C}");
        Console.WriteLine($"🔺 Max spent: {maxExpense:C}");
        Console.WriteLine($"🔻 Min spent: {minExpense:C}");
        Console.Write("\n🏆 Top category: ");
        PrintColor($"{topCtg}", ConsoleColor.DarkYellow);
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
                        Console.Clear();
                        PrintColor("⚠️ Wrong menu index!", ConsoleColor.Yellow);
                        break;
                }
            }
            else
            {
                Console.Clear();
                PrintColor("❌ Error: Write a number!", ConsoleColor.Red);
            }
        }
    }
}