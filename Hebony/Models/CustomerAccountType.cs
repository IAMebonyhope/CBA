using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public class CustomerAccountType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Double CreditInterestRate { get; set; }

        public Double DebitInterestRate { get; set; }

        public Double COT { get; set; }

        public Double MinBalance { get; set; }

        public GLAccount IntestestExpenseGLAccount { get; set; }

        public GLAccount COTIncomeGLAccount { get; set; }

        public GLAccount InterestIncomeGLAccount { get; set; }

        public virtual ICollection<CustomerAccount> CustomerAccounts { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}