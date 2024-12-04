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
    public class OrderDeatilRespiratory : Respiratory<OrderDetail>, IOrderDetailRespiratory
    {
        private ApplicationDbContext _db;
        public OrderDeatilRespiratory(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
       

        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
