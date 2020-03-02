using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hebony.Models;
using System.Text;

namespace Hebony.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class GLAccountController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: GLAccount
        public ActionResult Index()
        {
            var GlAccounts = context.GLAccounts.Include(b => b.Branch).Include(g => g.GLCategory).ToList();
            return View(GlAccounts);
        }

        // GET: GLAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLAccount gLAccount = context.GLAccounts.Find(id);
            if (gLAccount == null)
            {
                return HttpNotFound();
            }
            return View(gLAccount);
        }

        // GET: GLAccount/Create
        public ActionResult Create()
        {
            ViewData["Branches"] = context.Branches.ToList();
            ViewData["GLCategories"] = context.GLCategories.ToList();
            return View();
        }

        // POST: GLAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GLAccountViewModel model)
        {
            ViewData["Branches"] = context.Branches.ToList();
            ViewData["GLCategories"] = context.GLCategories.ToList();
            if (ModelState.IsValid)
            {
                GLAccount glAccount = new GLAccount();
                glAccount.AccNo = Helper.GenerateGLAccNo(model.GLCategoryID);
                glAccount.Name = model.Name;
                glAccount.Balance = model.Balance;
                glAccount.Branch = context.Branches.Find(model.BranchID);
                glAccount.GLCategory = context.GLCategories.Find(model.GLCategoryID);

                context.GLAccounts.Add(glAccount);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: GLAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewData["Branches"] = context.Branches.ToList();
            ViewData["GLCategories"] = context.GLCategories.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLAccount glAccount = context.GLAccounts.Find(id);
            if (glAccount == null)
            {
                return HttpNotFound();
            }

            GLAccountViewModel model = new GLAccountViewModel();
            model.AccNo = glAccount.AccNo;
            model.Name = glAccount.Name;
            model.Balance = glAccount.Balance;
            model.BranchID = glAccount.Branch.ID;
            model.GLCategoryID = glAccount.GLCategory.Id;

            return View(model);
        }

        // POST: GLAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GLAccountViewModel model)
        {
            ViewData["Branches"] = context.Branches.ToList();
            ViewData["GLCategories"] = context.GLCategories.ToList();

            if (ModelState.IsValid)
            {
                GLAccount glAccount = context.GLAccounts.Find(model.Id);
                glAccount.Name = model.Name;
                glAccount.Branch = context.Branches.Find(model.BranchID);

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: GLAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLAccount gLAccount = context.GLAccounts.Find(id);
            if (gLAccount == null)
            {
                return HttpNotFound();
            }
            return View(gLAccount);
        }

        // POST: GLAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GLAccount gLAccount = context.GLAccounts.Find(id);
            context.GLAccounts.Remove(gLAccount);
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
