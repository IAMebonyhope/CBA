using Hebony.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hebony.Logic
{

    public class EODLogic
    {
        ApplicationDbContext context;
        Configuration config;
        int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public EODLogic(ApplicationDbContext _context, Configuration _config)
        {
            context = _context;
            config = _config;
        }

        public bool CloseAllBranches()
        {
            List<Branch> branches = context.Branches.Where(b => b.IsOpen == true).ToList();
            branches.ForEach(b => { b.IsOpen = false; });
            context.SaveChanges();
            return true;
        }

        public bool IsConfigurationSet()
        {
            if (config.CurrentCOTIncomeGLAccount == null || 
                config.CurrentInterestExpenseGLAccount == null || 
                config.CurrentInterestPayableGLAccount == null || 
                config.SavingsInterestExpenseGLAccount == null || 
                config.SavingsInterestPayableGLAccount == null ||
                config.LoanInterestExpenseGLAccount == null ||
                config.LoanInterestIncomeGLAccount == null ||
                config.LoanInterestReceivableGLAccount == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public void ComputeSavingsInterestAccrued()
        {
            int presentDay = config.FinancialDate.Day;     
            int presentMonth = config.FinancialDate.Month;    
            int daysRemaining = 0;  

            var accounts = context.CustomerAccounts.Where(a => a.AccountType == AccountType.Savings).ToList();

            foreach (var account in accounts)
            {
                daysRemaining = daysInMonth[presentMonth - 1] - presentDay + 1;
                double interestRemainingForTheMonth = account.Balance * config.SavingsCreditInterestRate * daysRemaining / daysInMonth[presentMonth - 1]; //using I = PRT, where R is the rate per month.
                                                                                                                                                                            //calculate today's interest and add it to the account's dailyInterestAccrued
                double todaysInterest = interestRemainingForTheMonth / daysRemaining;
                account.DailyInterestAccrued += todaysInterest;     //increments till month end. Disbursed if withdrawal limit is not exceeded

                GLPostingLogic.DebitGL(config.SavingsInterestExpenseGLAccount, todaysInterest);
                GLPostingLogic.CreditGL(config.SavingsInterestPayableGLAccount, todaysInterest);

                context.Entry(account).State = EntityState.Modified;
                context.Entry(config.SavingsInterestExpenseGLAccount).State = EntityState.Modified;
                context.Entry(config.SavingsInterestPayableGLAccount).State = EntityState.Modified;
                context.SaveChanges();

                TransactionLogic.CreateTransaction(context, config.SavingsInterestExpenseGLAccount, todaysInterest, TransactionType.Debit);
                TransactionLogic.CreateTransaction(context, config.SavingsInterestPayableGLAccount, todaysInterest, TransactionType.Credit);
            }

                //monthly savings interest payment
            if (config.FinancialDate.Day == daysInMonth[presentMonth - 1])     // checks for month end to calculate the interest and pay it into the appropriate account.
            {
                bool customerIsCredited = false;
                foreach (var account in accounts)
                {
                    GLPostingLogic.DebitGL(config.SavingsInterestPayableGLAccount, account.DailyInterestAccrued);

                    //if the Withdrawal limit is not exceeded
                    if (!(account.SavingsWithdrawalCount > 3))    //assuming the withdrawal limit is 3
                    {
                        //Credit the customer ammount
                        TellerPostingLogic.CreditCustomerAccount(account, account.DailyInterestAccrued);
                        customerIsCredited = true;
                    }
                    else
                    {
                        GLPostingLogic.CreditGL(config.SavingsInterestExpenseGLAccount, account.DailyInterestAccrued);
                    }
                    account.SavingsWithdrawalCount = 0;  //re-initialize to zero for the next month
                    account.DailyInterestAccrued = 0;

                    context.Entry(account).State = EntityState.Modified;
                    context.Entry(config.SavingsInterestExpenseGLAccount).State = EntityState.Modified;
                    context.Entry(config.SavingsInterestPayableGLAccount).State = EntityState.Modified;

                    context.SaveChanges();

                    TransactionLogic.CreateTransaction(context, config.SavingsInterestPayableGLAccount, account.DailyInterestAccrued, TransactionType.Debit);
                    if (customerIsCredited)
                    {
                        TransactionLogic.CreateTransaction(context, account, account.DailyInterestAccrued, TransactionType.Credit);
                    }
                    TransactionLogic.CreateTransaction(context, config.SavingsInterestExpenseGLAccount, account.DailyInterestAccrued, TransactionType.Credit);
                }
            }
        }

        public void ComputeCurrentInterestAccrued()
        {
            //ideally current account don't get any interest but due to the context given, it is assumed they are given interest.
            int presentDay = config.FinancialDate.Day;   //1 to totalDays in d month
            int daysRemaining = 0;

            int presentMonth = config.FinancialDate.Month;     //1 to 12
 
            var accounts = context.CustomerAccounts.Where(a => a.AccountType == AccountType.Current).ToList();

            //foreach (var account in accounts)
            //{

            //    TellerPostingLogic.DebitCustomerAccount(account, account.DailyCOTAccrued);
            //    busLogic.CreditGl(config.CurrentCotIncomeGl, currentAccount.dailyCOTAccrued);

            //    currentAccount.dailyCOTAccrued = 0;    //goes back to zero after daily deduction

            //    db.Entry(config.CurrentCotIncomeGl).State = EntityState.Modified;

            //    frLogic.CreateTransaction(currentAccount, currentAccount.dailyCOTAccrued, TransactionType.Debit);
            //    frLogic.CreateTransaction(config.CurrentCotIncomeGl, currentAccount.dailyCOTAccrued, TransactionType.Credit);

            //    #endregion


            //    //get the no of days remaining in this month
            //    daysRemaining = daysInMonth[presentMonth - 1] - presentDay + 1;     //+1 because we havent computed for today
            //    decimal interestRemainingForTheMonth = currentAccount.AccountBalance * (decimal)config.CurrentCreditInterestRate * daysRemaining / daysInMonth[presentMonth - 1];      //using I = PRT, where R is the rate per month
            //    //calculate today's interest and add it to the account's dailyInterestAccrued
            //    decimal todaysInterest = interestRemainingForTheMonth / daysRemaining;
            //    currentAccount.dailyInterestAccrued += todaysInterest;     //increments till month end. Disbursed if withdrawal limit is not exceeded

            //    busLogic.DebitGl(config.CurrentInterestExpenseGl, todaysInterest);
            //    busLogic.CreditGl(config.CurrentInterestPayableGl, todaysInterest);

            //    db.Entry(currentAccount).State = EntityState.Modified;
            //    db.Entry(config.CurrentInterestExpenseGl).State = EntityState.Modified;
            //    db.Entry(config.CurrentInterestPayableGl).State = EntityState.Modified;

            //    db.SaveChanges();

            //    frLogic.CreateTransaction(config.CurrentInterestExpenseGl, todaysInterest, TransactionType.Debit);
            //    frLogic.CreateTransaction(config.CurrentInterestPayableGl, todaysInterest, TransactionType.Credit);
            //}//end foreach

            ////monthly current interest payment
            //if (today.Day == daysInMonth[presentMonth - 1])     //MONTH END?
            //{
            //    bool customerIsCredited = false;
            //    foreach (var account in allCurrents)
            //    {
            //        busLogic.DebitGl(config.CurrentInterestPayableGl, account.dailyInterestAccrued);

            //        //if the Withdrawal limit is not exceeded
            //        if (!(account.SavingsWithdrawalCount > 3))    //assuming the withdrawal limit is 3 and not more
            //        {
            //            //Credit the customer ammount
            //            busLogic.CreditCustomerAccount(account, account.dailyInterestAccrued);
            //            customerIsCredited = true;
            //        }
            //        else
            //        {
            //            busLogic.CreditGl(config.CurrentInterestExpenseGl, account.dailyInterestAccrued);
            //        }
            //        account.SavingsWithdrawalCount = 0;  //re-initialize to zero for the next month
            //        account.dailyInterestAccrued = 0;

            //        db.Entry(account).State = EntityState.Modified;
            //        db.Entry(config.CurrentInterestExpenseGl).State = EntityState.Modified;
            //        db.Entry(config.CurrentInterestPayableGl).State = EntityState.Modified;

            //        db.SaveChanges();

            //        frLogic.CreateTransaction(config.CurrentInterestPayableGl, account.dailyInterestAccrued, TransactionType.Debit);
            //        if (customerIsCredited)
            //        {
            //            frLogic.CreateTransaction(account, account.dailyInterestAccrued, TransactionType.Credit);
            //        }
            //        frLogic.CreateTransaction(config.CurrentInterestExpenseGl, account.dailyInterestAccrued, TransactionType.Credit);
            //    }
            //}//end if


        }

       

    }
}