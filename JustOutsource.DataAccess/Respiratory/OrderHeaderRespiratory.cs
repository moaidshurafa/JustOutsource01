using JustOutsource.DataAccess.Data;
using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JustOutsource.DataAccess.Respiratory
{
    public class OrderHeaderRespiratory : Respiratory<OrderHeader>, IOrderHeaderRespiratory
    {
        private ApplicationDbContext _db;
        public OrderHeaderRespiratory(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
       

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }
    }
}
