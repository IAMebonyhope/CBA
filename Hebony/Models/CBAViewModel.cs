using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hebony.Models
{
    public class GLCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Main Category")]
        public int MainCategoryID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }

    public class GLAccountViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Account Number")]
        public string AccNo { get; set; }

        [Required]
        [Display(Name = "Balance")]
        public Double Balance { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public int BranchID { get; set; }

        [Required]
        [Display(Name = "GL Category")]
        public int GLCategoryID { get; set; }
    }

    public class CustomerAccountTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Credit Interest Rate")]
        public Double CreditInterestRate { get; set; }

        [Display(Name = "Debit Interest Rate")]
        public Double DebitInterestRate { get; set; }

        [Display(Name = "Cost on Transaction")]
        public Double COT { get; set; }
        
        [Display(Name = "Minimum Balance")]
        public Double MinBalance { get; set; }

        [Display(Name = "Interest Expense GL Account")]
        public GLAccount IntestestExpenseGLAccount { get; set; }

        [Display(Name = "COTIncomeGLAccount")]
        public GLAccount COTIncomeGLAccount { get; set; }

        [Display(Name = "InterestIncomeGLAccount")]
        public GLAccount InterestIncomeGLAccount { get; set; }
    }

    public class CustomerAccountViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Account No")]
        public string AccNo { get; set; }

        [Required]
        [Display(Name = "Branch")]
        public int BranchID { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerID { get; set; }

        [Display(Name = "Account Type")]
        public int AccountType { get; set; }

        public Double Balance { get; set; }

        [Display(Name = "Linked Account")]
        public int LinkedAccountID { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Gender { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }

    public class GLPostingViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account To Be Debited")]
        public int DebitAccountID { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Debit Amount must be between 0 and a maximum reasonable value")]
        [Display(Name = "Debit Amount")]
        public Double DebitAmount { get; set; }

        [Required]
        [Display(Name = "Account To Be Credited")]
        public int CreditAccountID { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Compare("DebitAmount", ErrorMessage = "Credit Amount must be equal to Debit Amount")]
        [Display(Name = "Credit Amount")]
        public Double CreditAmount { get; set; }

        public string Narration { get; set; }

    }

    public class TellerPostingViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer Account")]
        public int CustomerAccountID { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Debit Amount must be between 0 and a maximum reasonable value")]
        [Display(Name = "Debit Amount")]
        public Double DebitAmount { get; set; }

        [Required]
        [Display(Name = "Till Account")]
        public int TillAccountID { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Compare("DebitAmount", ErrorMessage = "Credit Amount must be equal to Debit Amount")]
        [Display(Name = "Credit Amount")]
        public Double CreditAmount { get; set; }

        public string Narration { get; set; }

        [Display(Name = "Post Type")]
        public int PostingType { get; set; }

    }
}