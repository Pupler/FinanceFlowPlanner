using System.Text.Json.Serialization;

public record FinanceData(
    [property: JsonPropertyName("version")] string Version = "1.0",
    [property: JsonPropertyName("lastSaved")] DateTime? LastSaved = null,
    [property: JsonPropertyName("goals")] List<FinancialGoal> Goals = null!,
    [property: JsonPropertyName("expenses")] List<Expense> Expenses = null!
);