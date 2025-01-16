using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMate.Models;
namespace MoneyMate.Helpers
{
    public class GetAllAmount
    {
        
        public static decimal GetTotalIncome()
        {
            var transactions = TransactionHelper.GetAllTransactions();
            return transactions
                .Where(t => t.transactionType == TransactionType.Income) 
                .Sum(t => t.amount);
        }

        public static decimal GetTotalExpenses()
        {
            var transactions = TransactionHelper.GetAllTransactions();
            return transactions
                .Where(t => t.transactionType == TransactionType.Expense)
                .Sum(t => t.amount);
        }

       
        public static decimal GetTotalDebt()
        {
            var transactions = TransactionHelper.GetAllTransactions();
            return transactions
                .Where(t => t.transactionType == TransactionType.Debt)
                .Sum(t => t.amount);
        }

        public static decimal GetTotalPendingDebt()
        {
            var transactions = TransactionHelper.GetAllTransactions();
            return transactions
                .Where(t => t.transactionType == TransactionType.Debt && t.debtStatus != DebtStatus.Cleared)
                .Sum(t => t.amount);
        }

        public static decimal GetTotalClearedDebt()
        {
            var transactions = TransactionHelper.GetAllTransactions();
            return transactions
                .Where(t => t.transactionType == TransactionType.Debt && t.debtStatus != DebtStatus.Pending)
                .Sum(t => t.amount);
        }
        public static decimal GetTotalBalance()
        {
            var totalIncome = GetTotalIncome();
            var totalExpenses = GetTotalExpenses();
            var totalPendingDebt = GetTotalPendingDebt();

            return totalIncome + totalPendingDebt - totalExpenses;
        }
    }
}
