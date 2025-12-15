public record FinancialGoal
(
    string Name,
    decimal TargetAmount,
    decimal CurrentAmount = 0
)
{
    public decimal RemainingAmount => TargetAmount - CurrentAmount;
}