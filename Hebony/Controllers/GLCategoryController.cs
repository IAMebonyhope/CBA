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
    public class GLCategoryController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: GLCategory
        public ActionResult Index()
        {
            return View(context.GLCategories.ToList());
        }

        // GET: GLCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLCategory gLCategory = context.GLCategories.Find(id);
            if (gLCategory == null)
            {
                return HttpNotFound();
            }
            return View(gLCategory);
        }

        // GET: GLCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GLCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MainCategory,Description,RowVersion")] GLCategory gLCategory)
        {
            if (ModelState.IsValid)
            {
                context.GLCategories.Add(gLCategory);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gLCategory);
        }

        // GET: GLCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLCategory gLCategory = context.GLCategories.Find(id);
            if (gLCategory == null)
            {
                return HttpNotFound();
            }
            return View(gLCategory);
        }

        // POST: GLCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MainCategory,Description,RowVersion")] GLCategory gLCategory)
        {
            if (ModelState.IsValid)
            {
                context.Entry(gLCategory).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gLCategory);
        }

        // GET: GLCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLCategory gLCategory = context.GLCategories.Find(id);
            if (gLCategory == null)
            {
                return HttpNotFound();
            }
            return View(gLCategory);
        }

        // POST: GLCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GLCategory gLCategory = context.GLCategories.Find(id);
            context.GLCategories.Remove(gLCategory);
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
