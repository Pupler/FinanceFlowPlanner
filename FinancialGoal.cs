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
}