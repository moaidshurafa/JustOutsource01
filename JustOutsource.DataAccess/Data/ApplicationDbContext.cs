using JustOutsource.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace JustOutsource.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

            
        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        //public DbSet<Job> Jobs { get; set; }
        //public DbSet<ApplicationUser> applicationUsers { get; set; }
        //public DbSet<Service> Services { get; set; }
        //public DbSet<Freelancer> Freelancers { get; set; }
        //public DbSet<Client> Clients { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Review> Reviews { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<Message> Messages { get; set; }

    }
}
