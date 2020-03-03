using Hebony.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Hebony.Startup))]
namespace Hebony
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //createRolesandUsers();
            //createAdmin();
            //createConfig();
        }

        public void createConfig()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            Configuration config = new Configuration();
            config.FinancialDate = DateTime.Now;
            config.SavingsCreditInterestRate = 0.2;
            config.SavingsMinimumBalance = 1000;
            config.SavingsInterestExpenseGLAccount = context.GLAccounts.Find(1002);
            config.CurrentCOTIncomeGLAccount = context.GLAccounts.Find(1004);
            config.SavingsInterestPayableGLAccount = context.GLAccounts.Find(1005);
            config.CurrentInterestExpenseGLAccount = context.GLAccounts.Find(1003);
            config.CurrentInterestPayableGLAccount = context.GLAccounts.Find(1006);
            config.CurrentCreditInterestRate = 10;
            config.CurrentMinimumBalance = 10000;
            config.CurrentCOT = 100;

            context.Configurations.Add(config);
            context.SaveChanges();
        }

        private void createAdmin()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser();
            user.UserName = "admin3";
            user.Email = "admin3@gmail.com";
        
            string userPWD = "password";

            var chkUser = UserManager.Create(user, userPWD);

            //Add default User to Role Admin    
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Admin");

            }
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "shanu";
                user.Email = "syedshanumcain@gmail.com";

                string userPWD = "A@Z200711";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Manager role     
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

            }

            // creating Creating Employee role     
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }

            // creating Creating Employee role
            if (!roleManager.RoleExists("Teller"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Teller";
                roleManager.Create(role);

            }
        }
    }
}
