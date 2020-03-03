using Hebony.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hebony.Logic
{
    public class GLPostingLogic
    {
        public static bool CreditGL(GLAccount account, double amount)
        {
            try
            {
                switch ((MainCategory)account.GLCategory.MainCategory)
                {
                    case MainCategory.Asset:
                    case MainCategory.Expense:
                        if(amount > account.Balance)
                        {
                            return false;
                        }
                        account.Balance -= amount;
                        break;
                    case MainCategory.Capital:
                    case MainCategory.Liability:
                    case MainCategory.Income:
                        account.Balance += amount;
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool DebitGL(GLAccount account, double amount)
        {
            try
            {
                switch (account.GLCategory.MainCategory)
                {
                    case MainCategory.Asset:
                    case MainCategory.Expense:
                        account.Balance += amount;
                        break;
                    case MainCategory.Capital:
                    case MainCategory.Liability:
                    case MainCategory.Income:
                        if (amount > account.Balance)
                        {
                            return false;
                        }
                        account.Balance -= amount;
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}