using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hebony.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Branch Branch { get; set; }

        public GLAccount GLAccount { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Hebony.Models.Branch> Branches { get; set; }

        public DbSet<Hebony.Models.GLCategory> GLCategories { get; set; }

        public DbSet<Hebony.Models.GLAccount> GLAccounts { get; set; }

        public DbSet<Hebony.Models.Configuration> Configurations { get; set; }

        public DbSet<Hebony.Models.CustomerAccount> CustomerAccounts { get; set; }

        public DbSet<Hebony.Models.Customer> Customers { get; set; }

        public DbSet<Hebony.Models.GLPosting> GLPostings { get; set; }

        public DbSet<Hebony.Models.TellerPosting> TellerPostings { get; set; }

        public DbSet<Hebony.Models.Transaction> Transactions { get; set; }
    }
}