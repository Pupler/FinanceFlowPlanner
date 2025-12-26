using System.Text.Json;

public static class JsonDataService
{
    private const string FileName = "data.json";

    public static void SaveData(List<FinancialGoal> goals, List<Expense> expenses)
    {
        FinanceData data = new(
            Version: "1.0",
            LastSaved: null,
            Goals: goals,
            Expenses: expenses
        );

        string dataString = JsonSerializer.Serialize(data);

        File.WriteAllText(FileName, dataString);
    }

    public static void LoadData()
    {
        
    }
}