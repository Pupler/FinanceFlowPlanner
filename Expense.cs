using System.Text.Json.Serialization;

public record Expense
(
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("category")] ExpenseCategory Category,
    [property: JsonPropertyName("amount")] decimal Amount,
    [property: JsonPropertyName("date")] DateTime? Date = null
)
{
    public DateTime EffectiveDate => Date ?? DateTime.Now;
    
    public string DateDisplay => EffectiveDate.ToString("d");
}