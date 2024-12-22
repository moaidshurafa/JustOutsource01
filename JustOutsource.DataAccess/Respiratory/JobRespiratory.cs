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
    public class JobRespiratory : Respiratory<Job>, IJobRepository
    {
        private ApplicationDbContext _db;
        public JobRespiratory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Job obj)
        {
            var objFromDb = _db.Jobs.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {

                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.RequiredSkills = obj.RequiredSkills;
                objFromDb.Category = obj.Category;
                objFromDb.Budget = obj.Budget;
                objFromDb.EmailToContact = obj.EmailToContact;
                objFromDb.PhoneNumber = obj.PhoneNumber;
                if (obj.AdditionalFile != null)
                {
                    objFromDb.AdditionalFile = obj.AdditionalFile;
                }

            }
        }
    }
}
