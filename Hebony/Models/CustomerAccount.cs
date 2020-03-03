using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public enum AccountType
    {
        Savings,
        Current,
        Loan
    }

    public class CustomerAccount
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string AccNo { get; set; }

        public bool IsActive { get; set; } = true;

        public Branch Branch { get; set; }

        public Customer Customer { get; set; }

        public AccountType AccountType { get; set; }

        public Double Balance { get; set; }

        public Double DailyInterestAccrued { get; set; }

        public Double DailyCOTAccrued { get; set; }

        public int SavingsWithdrawalCount { get; set; }
    }
}