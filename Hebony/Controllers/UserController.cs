using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hebony.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Text;

namespace Hebony.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class UserController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            //todo
            //filter out what should be displayed by using registerviewmodel
            return View(context.Users.Include(s => s.Roles));
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            //todo
            //filter out what should be displayed by using registerviewmodel
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewData["Branches"] = context.Branches.ToList();
            ViewData["Roles"] = context.Roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Id }).ToList();
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel model)
        {
            //todo
            //send mail containing username and password to user on successful registeration
            //remember role should be an array
            try
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var user = new ApplicationUser();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;
                user.Branch = context.Branches.Find(model.BranchID);

                string userPWD = GeneratePassword();
                var chkUser = UserManager.Create(user, userPWD);
  
                if (chkUser.Succeeded)
                {
                    var role = context.Roles.Find(model.RoleID);
                    var result1 = UserManager.AddToRole(user.Id, role.Name);

                    TempData["UserCreated"] = "User Successfully Created";

                }
                else
                {
                    TempData["UserCreated"] = "Error Creating User";
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser();
            user = UserManager.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            RegisterViewModel model = new RegisterViewModel();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            //model.BranchID = user.Branch.ID;
            model.PhoneNumber = user.PhoneNumber;

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RegisterViewModel model)
        {
            //todo
            //validate email address with unique ish like laravel
            //pass id to model from edit view
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = UserManager.FindByEmail(model.Email);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            Branch branch = context.Branches.Find(model.BranchID);
            user.Branch = branch;

            var chkUser = UserManager.Update(user);

            if (chkUser.Succeeded)
            {
                TempData["UserUpdated"] = "User Successfully Updated";
                RedirectToAction("Details", user.Id);
            }
            return View(model);
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(context.Users.Find(id));
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = await UserManager.FindByIdAsync(id);

            var result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["UserDeleted"] = "User Successfully Deleted";
            }
            else
            {
                TempData["UserDeleted"] = "Error Deleting User";
            }

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

        private string GeneratePassword()
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

    }
}
