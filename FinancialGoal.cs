using System.Text.Json.Serialization;

public record FinancialGoal
(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("targetAmount")] decimal TargetAmount,
    [property: JsonPropertyName("currentAmount")] decimal CurrentAmount = 0,
    [property: JsonPropertyName("deadline")] DateTime? Deadline = null
)
{
    public FinancialGoal AddMoney(decimal amount) => this with { CurrentAmount = CurrentAmount + amount };
    
    public decimal RemainingAmount => TargetAmount - CurrentAmount;
    
    public string DeadlineDisplay => Deadline.HasValue ? Deadline.Value.ToString("d") : "None";
}