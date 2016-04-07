using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ManicureDomain.DummyRepos
{
    public class DummyRepository<T> : IRepository<T> where T : Entity, new()
    {
        protected static IList<T> Collection;

        public void Create(T entity)
        {
            if (!(Collection.Any(x => x == entity)))
            {
                entity.Id = Collection.Last().Id + 1;
                Collection.Add(entity);
            }
            else {
                throw new Exception();
            }
        }

        public T Read(int id)
        {
            return Collection.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return Collection.AsQueryable<T>();
        }

        public IQueryable<T> GetByIsActive(bool isActive)
        {
            return Collection.Where(x => x.IsActive == isActive).AsQueryable<T>();
        }

        public void Update(T entity)
        {
            Collection[Collection.IndexOf(Collection.First(x => x.Id == entity.Id))] = entity;
        }

        public void Delete(T entity)
        {
            if (Collection.Any(x => x == entity))
            {
                Collection.First(x => x == entity).IsActive = false;
            }
            else
                throw new Exception();
        }

        public void Delete(int id)
        {
            if (Collection.Any(x => x.Id == id))
            {
                Collection.First(x => x.Id == id).IsActive = false;
            }
            else
                throw new Exception();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Collection.AsQueryable().Where(predicate);
        }
    }
}
