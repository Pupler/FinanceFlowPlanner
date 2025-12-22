using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExpenseCategory
{
    Food,
    Transport, 
    Entertainment,
    Bills,
    Shopping,
    Health,
    Other
}