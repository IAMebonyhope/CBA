using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public class Configuration
    {
        public int Id { get; set; }

        [Display(Name = "Business Status")]
        public bool IsBusinessOpen { get; set; } = true;

        [Display(Name = "Financial Date")]
        public DateTime FinancialDate { get; set; }

        [Display(Name = "Savings Credit Interest Rate")]
        [Range(0, 100)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format for interest rate")]
        public Double SavingsCreditInterestRate { get; set; }

        [Display(Name = "Savings Minimum Balance")]
        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format for minimum balance")]
        public Double SavingsMinimumBalance { get; set; }

        [Display(Name = "Savings Interest Expense GLAccount")]
        public GLAccount SavingsInterestExpenseGLAccount { get; set; }

        [Display(Name = "Savings Interest Payable GLAccount")]
        public GLAccount SavingsInterestPayableGLAccount { get; set; }


        [Display(Name = "Current Credit Interest Rate")]
        [Range(0, 100)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format for interest rate")]
        public Double CurrentCreditInterestRate { get; set; }

        [Display(Name = "Current Minimum Balance")]
        [Range(0, (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format for minimum balance")]
        public Double CurrentMinimumBalance { get; set; }

        [Display(Name = "Current Interest Expense GLAccount")]
        public GLAccount CurrentInterestExpenseGLAccount { get; set; }

        [Display(Name = "Current Interest Payable GLAccount")]
        public GLAccount CurrentInterestPayableGLAccount { get; set; }


        [DataType(DataType.Currency)]
        [Display(Name = "Current COT")]
        [Range(0.00, 1000.00)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format")]
        public Double CurrentCOT { get; set; }

        [Display(Name = "Current COT Income GLAccount")]
        public GLAccount CurrentCOTIncomeGLAccount { get; set; }



        [Display(Name = "Loan Debit Interest Rate")]
        [Range(0, 100)]
        [RegularExpression(@"^[.0-9]+$", ErrorMessage = "Invalid format")]
        public double LoanDebitInterestRate { get; set; }

        [Display(Name = "Select Interest Income GLAccount")]
        public GLAccount LoanInterestIncomeGLAccount { get; set; }

        [Display(Name = "Select Interest Expense GLAccount")]
        public GLAccount LoanInterestExpenseGLAccount { get; set; }        //Expense: from where the loan is disbursed

        [Display(Name = "Select Interest Receivable GLAccount")]
        public GLAccount LoanInterestReceivableGLAccount { get; set; }
    }
}