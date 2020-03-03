using Hebony.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hebony.Logic
{
    public class TransactionLogic
    {

        public static void CreateTransaction(ApplicationDbContext context, GLAccount account, double amount, TransactionType txnType)
        {
            Transaction transaction = new Transaction();
            transaction.Amount = amount;
            transaction.Date = DateTime.Now;
            transaction.AccountName = account.Name;
            transaction.SubCategory = account.GLCategory.Name;
            transaction.MainCategory = account.GLCategory.MainCategory;
            transaction.TransactionType = txnType;

            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

        public static void CreateTransaction(ApplicationDbContext context, CustomerAccount account, double amount, TransactionType txnType)
        {

            Transaction transaction = new Transaction();
            transaction.Amount = amount;
            transaction.Date = DateTime.Now;
            transaction.AccountName = account.Name;
            transaction.SubCategory = "Customer Account";
            transaction.MainCategory = MainCategory.Liability;
            transaction.TransactionType = txnType;

            context.Transactions.Add(transaction);
            context.SaveChanges();

        }
    }
}