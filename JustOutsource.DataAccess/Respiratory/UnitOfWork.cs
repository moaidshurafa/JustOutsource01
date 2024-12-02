using JustOutsource.DataAccess.Data;
using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustOutsource.DataAccess.Respiratory
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IShoppingCartRepository ShoppingCart { get; private set; }

        public ICategoryRespiratory Category { get; private set; }
        public IFreelancerRespiratory Freelancer { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            Category = new CategoryRespiratory(_db);
            Freelancer = new FreelancerRespiratory(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
