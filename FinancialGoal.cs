using System.Diagnostics.Tracing;

public record FinancialGoal
(
    string Name,
    decimal TargetAmount,
    decimal CurrentAmount = 0,
    DateTime? Deadline = null
)
{
    public FinancialGoal AddMoney(decimal amount) => this with { CurrentAmount = CurrentAmount + amount };
    
    public decimal RemainingAmount => TargetAmount - CurrentAmount;
    
    public string DeadlineDisplay => Deadline.HasValue ? Deadline.Value.ToString("d") : "None";
}