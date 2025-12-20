using System.Security;

public record Expense(
    string Description,
    ExpenseCategory Category,
    decimal Amount,
    DateTime? Date = null
)
{
    public DateTime EffectiveDate => Date ?? DateTime.Now;
    
    public string DateDisplay => EffectiveDate.ToString("d");
}