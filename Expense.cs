using System.Security;

public record Expense(
    string Description,
    string Category,
    decimal Amount,
    DateTime Date = default
)
{
    public DateTime EffectiveDate 
    {
        get 
        {
            if (Date == default)
                return DateTime.Now;
            else
                return Date;
        }
    }
}