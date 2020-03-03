using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hebony.Models;

namespace Hebony.Controllers
{
    public class ConfigurationController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: CustomerAccountType
        public ActionResult Index()
        {
            return View(context.Configurations.ToList());
        }

        // GET: CustomerAccountType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuration customerAccountType = context.Configurations.Find(id);
            if (customerAccountType == null)
            {
                return HttpNotFound();
            }
            return View(customerAccountType);
        }

        // GET: CustomerAccountType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerAccountType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreditInterestRate,DebitInterestRate,COT,MinBalance,RowVersion")] Configuration customerAccountType)
        {
            if (ModelState.IsValid)
            {
                context.Configurations.Add(customerAccountType);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerAccountType);
        }

        // GET: CustomerAccountType/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewData["GLAccounts"] = context.GLAccounts.ToList();
            if (id == null)
            {
                return View();
            }
            Configuration customerAccountType = context.Configurations.Find(id);
            if (customerAccountType == null)
            {
                return View();
            }
            return View(customerAccountType);
        }

        // POST: CustomerAccountType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Configuration config)
        {
            ViewData["GLAccounts"] = context.GLAccounts.ToList();
            if (config == null)
            {

                config = new Configuration();
            }

            if (String.IsNullOrEmpty(config.Id.ToString()))
            {
                config = new Configuration();
            }
                       
            if (ModelState.IsValid)
            {
                context.Entry(config).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(config);
        }

        // GET: CustomerAccountType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configuration customerAccountType = context.Configurations.Find(id);
            if (customerAccountType == null)
            {
                return HttpNotFound();
            }
            return View(customerAccountType);
        }

        // POST: CustomerAccountType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Configuration customerAccountType = context.Configurations.Find(id);
            context.Configurations.Remove(customerAccountType);
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
