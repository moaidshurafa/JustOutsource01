using JustOutsource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustOutsource.DataAccess.Respiratory.IRespiratory
{
    public interface IFreelancerRespiratory : IRespiratory<Freelancer>
    {
        void Update(Freelancer obj);
    }
}
