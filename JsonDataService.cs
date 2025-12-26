using System.Data;
using System.Text.Json;

public static class JsonDataService
{
    private const string FileName = "finance_data.json";

    public static void SaveData(List<FinancialGoal> goals, List<Expense> expenses)
    {
        FinanceData data = new(
            Version: "1.0",
            LastSaved: null,
            Goals: goals,
            Expenses: expenses
        );

        string dataSave = JsonSerializer.Serialize(data);

        File.WriteAllText(FileName, dataSave);
    }

    public static (List<FinancialGoal>, List<Expense>) LoadData()
    {
        if (!File.Exists(FileName))
        {
            return ([], []);
        }
        else
        {
            string dataReadJson = File.ReadAllText(FileName);
            
            var dataRead = JsonSerializer.Deserialize<FinanceData>(dataReadJson);

            return (dataRead.Goals, dataRead.Expenses);
        }
    }
}