using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore2022.DataAccess.Repository.IRepository
{
    public interface IRepository <T> where T :class
    {
        // T- Category
        IEnumerable<T> GetAll (Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T Entity);
        void Remove(T Entity);
        void RemoveRange(IEnumerable <T> Entity);


    }
}

