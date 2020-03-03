using Hebony.Logic;
using Hebony.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hebony.Controllers
{
    public class EODController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private Configuration config;
        private EODLogic eodLogic;

        public EODController()
        {
            config = context.Configurations.First();
            eodLogic = new EODLogic(context, config);

        }
        
        public ActionResult CloseBusiness()
        {
            if (config.IsBusinessOpen == false)
            {
                ViewBag.Message = "Business Is Already Closed";
                return View();
            }
            if (!eodLogic.CloseAllBranches())
            {
                ViewBag.Message = "Cant Close All Branches";
                return View();
            }
            config.IsBusinessOpen = false;
            context.SaveChanges();
            ViewBag.Message = "Business Closed";
            return View();
        }

        public ActionResult OpenBusiness()
        {
            if(config.IsBusinessOpen == true)
            {
                ViewBag.Message = "Business Is Already Opened";
            }
            config.IsBusinessOpen = true;
            context.SaveChanges();
            ViewBag.Message = "Business Opened";

            return View();
        }

        public ActionResult RunEOD()
        {
            if (eodLogic.IsConfigurationSet())
            {
                config.IsBusinessOpen = false;

                eodLogic.ComputeSavingsInterestAccrued(); // to calculate interest on saving account.
                eodLogic.ComputeCurrentInterestAccrued(); // to calculate interest on current account and the commision on turnover.
                
                //var config = db.AccountConfiguration.First();
                config.FinancialDate = config.FinancialDate.AddDays(1);        //increments the financial date at the EOD

                context.Entry(config).State = EntityState.Modified;
                context.SaveChanges();
                //Ensures all or none of the 5 steps above executes and gets saved                     
                ViewBag.Message = "End of Day Executed Successfully!";
            }

            ViewBag.Message = "Error executing EOD process!";

            return View();
        }


    }
}