using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hebony.Models;
using Hebony.Logic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hebony.Controllers
{
    public class TellerPostingController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private Configuration config;

        public TellerPostingController()
        {
            config = context.Configurations.First();
        }

        public ActionResult Index()
        {
            return View(context.TellerPostings.Include(c => c.CustomerAccount).Include(t => t.TillAccount).ToList());
        }

        // GET: TellerPosting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TellerPosting tellerPosting = context.TellerPostings.Find(id);
            if (tellerPosting == null)
            {
                return HttpNotFound();
            }
            return View(tellerPosting);
        }

        // GET: TellerPosting/Create
        public ActionResult Create()
        {
            if (config.IsBusinessOpen == false)
            {
                return View("BusinessClosed");
            }
            ViewData["CustomerAccounts"] = context.CustomerAccounts.ToList();
            return View();
        }

        // POST: TellerPosting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TellerPostingViewModel model)
        {
            if (config.IsBusinessOpen == false)
            {
                return View("BusinessClosed");
            }

            if (ModelState.IsValid)
            {

                TellerPosting tellerPost = new TellerPosting();
                tellerPost.CreditAmount = model.CreditAmount;
                tellerPost.DebitAmount = model.DebitAmount;
                tellerPost.CustomerAccount = context.CustomerAccounts.Find(model.CustomerAccountID);
                tellerPost.Narration = model.Narration;
                tellerPost.TransactionDate = DateTime.Now;
                tellerPost.PostingType = (PostingType)model.PostingType;

                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);
                tellerPost.TillAccount = currentUser.GLAccount;

                string result = TellerPostingLogic.PostTeller(tellerPost.CustomerAccount, tellerPost.TillAccount, tellerPost.CreditAmount, tellerPost.PostingType, config);

                if (result == "success")
                {
                    context.TellerPostings.Add(tellerPost);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Message = result;
                return View(model);
            }

            return View(model);
        }

        
        // GET: TellerPosting/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TellerPosting tellerPosting = context.TellerPostings.Find(id);
            if (tellerPosting == null)
            {
                return HttpNotFound();
            }
            return View(tellerPosting);
        }

        // POST: TellerPosting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TellerPosting tellerPosting = context.TellerPostings.Find(id);
            context.TellerPostings.Remove(tellerPosting);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
