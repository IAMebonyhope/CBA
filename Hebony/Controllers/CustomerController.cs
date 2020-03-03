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
    public class CustomerController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Customer
        public ActionResult Index()
        {
            return View(context.Customers.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Email = model.Email;
                customer.PhoneNumber = model.PhoneNumber;
                customer.Gender = (Gender)model.Gender;

                context.Customers.Add(customer);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerViewModel model = new CustomerViewModel();
            model.FirstName = customer.FirstName;
            model.LastName = customer.LastName;
            model.Email = customer.Email;
            model.PhoneNumber = customer.PhoneNumber;
            model.Gender = (int)customer.Gender;
            return View(model);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = context.Customers.Find(model.Id);
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Email = model.Email;
                customer.PhoneNumber = model.PhoneNumber;
                customer.Gender = (Gender)model.Gender;

                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
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
