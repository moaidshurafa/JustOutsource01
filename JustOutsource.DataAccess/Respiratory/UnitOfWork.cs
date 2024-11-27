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
        public ICategoryRespiratory Category { get; private set; }
        public IFreelancerRespiratory Freelancer { get; private set; }

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Category = new CategoryRespiratory(_db);
            Freelancer = new FreelancerRespiratory(_db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
