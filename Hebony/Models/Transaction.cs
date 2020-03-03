using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public enum TransactionType
    {
        Debit,
        Credit
    }

    public class Transaction
    {
        public int ID { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string AccountName { get; set; }
        public string SubCategory { get; set; }    
        public MainCategory MainCategory { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}