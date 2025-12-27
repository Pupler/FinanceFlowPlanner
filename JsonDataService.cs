using System.Text.Json;

public static class JsonDataService
{
    private const string FileName = "finance_data.json";

    public static void SaveData(List<FinancialGoal> goals, List<Expense> expenses)
    {
        try
        {
            FinanceData data = new(
                Version: "1.0",
                LastSaved: null,
                Goals: goals,
                Expenses: expenses
            );

            string DataSave = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreReadOnlyProperties = true
            });

            File.WriteAllText(FileName, DataSave);   
        }
        catch (Exception ex)
        {
            Program.PrintColor($"⚠️ Error saving data: {ex.Message}", ConsoleColor.Red);
        }
    }

    public static (List<FinancialGoal>, List<Expense>) LoadData()
    {
        if (!File.Exists(FileName))
        {
            return ([], []);
        }
        
        try
        {
            string DataReadJson = File.ReadAllText(FileName);

            if (string.IsNullOrWhiteSpace(DataReadJson))
            {
                return ([], []);
            }

            var DataRead = JsonSerializer.Deserialize<FinanceData>(DataReadJson);

            return (DataRead?.Goals ?? [], DataRead?.Expenses ?? []);
        }
        catch (JsonException)
        {
            Program.PrintColor("⚠️ Corrupted save file. Starting fresh!", ConsoleColor.Yellow);

            return ([], []);
        }
        catch (Exception ex)
        {
            Program.PrintColor($"⚠️ Error loading data: {ex.Message}", ConsoleColor.Red);

            return ([], []);
        }
    }
}