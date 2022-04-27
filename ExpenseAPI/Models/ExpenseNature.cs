using System.Text.Json.Serialization;

namespace ExpenseAPI.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ExpenseNature
    {
        Restaurant,
        Hotel,
        Misc
    }
}
