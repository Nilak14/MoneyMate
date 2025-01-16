using AntDesign;
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
        public static async Task DeleteTransaction(string transactionId)
        {
            var transactions = GetAllTransactions();

            
            var transactionToRemove = transactions.FirstOrDefault(t => t.transactionId == transactionId);
            if (transactionToRemove != null)
            {
                transactions.Remove(transactionToRemove);
                await SaveTransactions(transactions);
            }
        }
        public static async Task<string> ClearDebt(string transactionId)
        {
            var transaction = GetAllTransactions();
            var debtTransaction = transaction.FirstOrDefault(t => t.transactionId == transactionId && t.transactionType == TransactionType.Debt);
            if (debtTransaction ==  null)
            {
                return "Debt Not Found";
            }
            var totalBalance = GetAllAmount.GetTotalBalance();

            if(totalBalance < debtTransaction.amount)
            {
                return "You balance is not sufficient  to clear the debt";
            }
            debtTransaction.debtStatus = DebtStatus.Cleared;

            await TransactionHelper.SaveTransactions(transaction);
            return $"Debt cleared successfully. Your new balance is {GetAllAmount.GetTotalBalance()}.";
        }

    }
}
