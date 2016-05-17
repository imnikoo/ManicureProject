using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Abstract
{
    public interface IRepository<T>
    {
        void Add(T entity);
        T Get(int id);
        void Update(T entity);
        void Remove(T entity);
        void Remove(int id);

        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        void Commit();
    }
}
