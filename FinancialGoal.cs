public record FinancialGoal
(
    string Name,
    decimal TargetAmount,
    decimal CurrentAmount = 0
)
{
    public FinancialGoal AddMoney(decimal amount) => this with { CurrentAmount = CurrentAmount + amount };
    public decimal RemainingAmount => TargetAmount - CurrentAmount;
}