using Hebony.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hebony.Logic
{
    public class TellerPostingLogic
    {
        public static bool CreditCustomerAccount(CustomerAccount account, double amount)
        {
            try
            {
                if (account.AccountType == AccountType.Loan)
                {
                    account.Balance -= amount;       //Loan accounts are assets to the bank
                }
                else
                {
                    account.Balance += amount;       //Savings and current accounts are liabilities to the bank
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DebitCustomerAccount(CustomerAccount account, double amount)
        {
            try
            {
                if (account.AccountType == AccountType.Loan)
                {
                    account.Balance += amount;   //because its an asset to the bank.
                }
                else
                {
                    account.Balance -= amount;   //its a liability, hence a debit reduces it.
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string PostTeller(CustomerAccount account, GLAccount till, Double amt, PostingType pType, Configuration config)
        {
            string output = "";
            switch (pType)
            {
                case PostingType.Deposit:
                    CreditCustomerAccount(account, amt);
                    GLPostingLogic.DebitGL(till, amt);
                    output = "success";
                    break;

                case PostingType.Withdrawal:

                    if (account.AccountType == AccountType.Savings)
                    {
                        if (account.Balance >= config.SavingsMinimumBalance + amt)
                        {
                            if (till.Balance >= amt)
                            {
                                GLPostingLogic.CreditGL(till, amt);
                                DebitCustomerAccount(account, amt);
                                output = "success";
                                account.SavingsWithdrawalCount++;
                            }
                            else
                            {
                                output = "Insufficient fund in the Till account";
                            }
                        }
                        else
                        {
                            output = "insufficient Balance in Customer's account, cannot withdraw!";
                        }

                    }
                    else if (account.AccountType == AccountType.Current)
                    {
                        if (account.Balance >= config.CurrentMinimumBalance + amt)
                        {
                            if (till.Balance >= amt)
                            {
                                GLPostingLogic.CreditGL(till, amt);
                                DebitCustomerAccount(account, amt);
                                output = "success";

                                Double x = (amt) / 1000;
                                Double charge = (int)x * config.CurrentCOT;
                                account.DailyCOTAccrued += charge;
                            }
                            else
                            {
                                output = "Insufficient fund in the Till account";
                            }
                        }
                        else
                        {
                            output = "insufficient Balance in Customer's account, cannot withdraw!";
                        }

                    }
                    else //for loan
                    {
                        output = "Please select a valid account";
                    }
                    break;
                //break;
                default:
                    break;
            }//end switch
            return output;
        }//
    }
}