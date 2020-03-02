using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hebony.Controllers
{
    public class Helper
    {
        public static string GenerateUserPassword()
        {
            //todo
            //write own generate password function
            int length = 10;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                password.Append(letter);
            }

            return password.ToString();
        }

        public static string GenerateGLAccNo(int gLCategoryID)
        {
            int length = 10;

            StringBuilder password = new StringBuilder();
            password.Append(gLCategoryID.ToString());
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(10 * flt));
                password.Append(shift.ToString());
            }

            return password.ToString();
        }

        public static string GenerateCustomerAccNo(int accTypeID, int customerID)
        {
            int length = 10;

            StringBuilder password = new StringBuilder();
            password.Append(accTypeID.ToString());
            password.Append(customerID.ToString());
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(10 * flt));
                password.Append(shift.ToString());
            }

            return password.ToString();
        }
    }
}