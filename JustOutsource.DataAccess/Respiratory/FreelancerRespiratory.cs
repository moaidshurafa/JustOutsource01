﻿using JustOutsource.DataAccess.Data;
using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustOutsource.DataAccess.Respiratory
{
    public class FreelancerRespiratory : Respiratory<Freelancer>, IFreelancerRespiratory
    {
        private ApplicationDbContext _db;
        public FreelancerRespiratory(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Freelancer obj)
        {
            var objFromDb = _db.Freelancers.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {

                objFromDb.Name = obj.Name;
                objFromDb.Skills = obj.Skills;
                objFromDb.ProfileDescription = obj.ProfileDescription;
                objFromDb.Category = obj.Category;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.YearsOfExperience = obj.YearsOfExperience;
                objFromDb.DateOfBirth = obj.DateOfBirth;
                objFromDb.LinkedInUrl = obj.LinkedInUrl;
                objFromDb.GitHubUrl = obj.GitHubUrl;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
                if(obj.CV != null)
                {
                    objFromDb.CV = obj.CV;
                }
            }
        }
    }
}
