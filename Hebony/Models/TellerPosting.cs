using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public enum PostingType
    {
        Deposit,
        Withdrawal
    }

    public class TellerPosting
    {
        public int Id { get; set; }

        public CustomerAccount CustomerAccount { get; set; }

        public GLAccount TillAccount { get; set; }

        public Double DebitAmount { get; set; }

        public Double CreditAmount { get; set; }

        public string Narration { get; set; }

        public DateTime TransactionDate { get; set; }

        public PostingType PostingType { get; set; }
    }
}