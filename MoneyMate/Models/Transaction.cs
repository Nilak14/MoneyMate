
using System.Text.Json.Serialization;

namespace MoneyMate.Models
{
    public class TransactionModel
    {
        public string title { get; set; }
        public string transactionTagId { get; set; }
        public string transactionId { get; set; }
        public float amount { get; set; }
        public string note { get; set; }
        public DateTime transactionDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType transactionType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public  DebtStatus? debtStatus { get; set; } 
        public string debtSource { get; set; }
        public DateTime? debtDueDate { get; set; }

    }
}
