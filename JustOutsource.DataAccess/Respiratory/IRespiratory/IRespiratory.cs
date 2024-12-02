using JustOutsource.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace JustOutsource.DataAccess.Respiratory.IRespiratory
{
    public interface IRespiratory<T> where T : class
    {
        // T category
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter= null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>>? filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);



    }
}
