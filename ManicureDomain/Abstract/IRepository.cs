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
        void Create(T entity);
        T Read(int id);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);

        IQueryable<T> GetAll();
        IQueryable<T> GetByIsActive(bool isActive);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
