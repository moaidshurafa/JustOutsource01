using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustOutsource.DataAccess.Respiratory.IRespiratory
{
    public interface IUnitOfWork
    {
        ICategoryRespiratory Category { get; }
        IFreelancerRespiratory Freelancer { get; }
        void Save();
    }
}
