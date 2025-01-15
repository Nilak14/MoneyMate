using System.Text.Json.Serialization;
namespace MoneyMate.Models
{
    public class UserModel
    {
        public string username { get; set; }
        public string password { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserCurrencyType? currencyType { get; set; }

    }
}
