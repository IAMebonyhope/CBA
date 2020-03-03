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

namespace Hebony.Controllers
{
    public class GLPostingController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private Configuration config;

        public GLPostingController()
        {
            try
            {
                config = context.Configurations.First();
            }
            catch
            {
                return;
            }
            
        }

        // GET: GLPosting
        public ActionResult Index()
        {
            return View(context.GLPostings.Include(g => g.DebitAccount).Include(c => c.CreditAccount).ToList());
        }

        // GET: GLPosting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLPosting gLPosting = context.GLPostings.Find(id);
            if (gLPosting == null)
            {
                return HttpNotFound();
            }
            return View(gLPosting);
        }

        // GET: GLPosting/Create
        public ActionResult Create()
        {
            if (config.IsBusinessOpen == false)
            {
                return View("BusinessClosed");
            }
            ViewData["GLAccounts"] = context.GLAccounts.ToList();
            return View();
        }

        // POST: GLPosting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GLPostingViewModel model)
        {
            if (config.IsBusinessOpen == false)
            {
                return View("BusinessClosed");
            }
            ViewData["GLAccounts"] = context.GLAccounts.ToList();

            if (ModelState.IsValid)
            {
                GLPosting gLPost = new GLPosting();
                gLPost.CreditAccount = context.GLAccounts.Include(g => g.GLCategory).SingleOrDefault(x => x.Id == model.CreditAccountID);
                gLPost.DebitAccount = context.GLAccounts.Include(g => g.GLCategory).SingleOrDefault(x => x.Id == model.DebitAccountID);
                gLPost.CreditAmount = model.CreditAmount;
                gLPost.DebitAmount = model.DebitAmount;
                gLPost.TransactionDate = DateTime.Now;

                if((GLPostingLogic.CreditGL(gLPost.CreditAccount, gLPost.CreditAmount)) && (GLPostingLogic.DebitGL(gLPost.DebitAccount, gLPost.DebitAmount)))
                {
                    context.GLPostings.Add(gLPost);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Error Completing Transaction";
                return View(model);
            }

            return View(model);
        }

    }
}
