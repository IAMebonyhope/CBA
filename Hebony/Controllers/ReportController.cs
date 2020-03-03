using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hebony.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ProfitAndLoss()
        {
            return View();
        }

        public ActionResult BalanceSheet()
        {
            return View();
        }

        public ActionResult TrialBalance()
        {
            return View();
        }
    }
}