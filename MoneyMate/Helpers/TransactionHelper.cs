using MoneyMate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyMate.Helpers
{
    public class TransactionHelper
    {
        
        private static string transactionFilePath = Path.Combine(FileSystem.AppDataDirectory, "transactions.json");
        
       public static  List<TransactionModel> GetAllTransactions()
        {
            if (File.Exists(transactionFilePath))
            {
                var json = File.ReadAllText(transactionFilePath);
                return JsonSerializer.Deserialize<List<TransactionModel>>(json) ?? new List<TransactionModel>();
            }
            else
            {
                return new List<TransactionModel>();
            }
        }

        private static async Task SaveTransactions(List<TransactionModel> transactions)
        {
            var json = JsonSerializer.Serialize(transactions);
            await File.WriteAllTextAsync(transactionFilePath, json);
        }

        public static async Task AddTransaction(TransactionModel transaction)
        {
            var transactions = GetAllTransactions();
            transactions.Add(transaction);
            await SaveTransactions(transactions);
        }


    }
}
