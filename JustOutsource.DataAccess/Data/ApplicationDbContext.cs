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
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        
        

    }
}
