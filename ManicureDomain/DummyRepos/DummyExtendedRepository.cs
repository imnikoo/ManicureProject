using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.DummyRepos
{
    public class DummyExtendedRepository<T> : DummyRepository<T>, IExtendedRepository<T> where T : ExtendedEntity, new()
    {
        public ICollection<T> GetByDate(DateTime date)
        {
            if (Collection.Any(x => x.EditDate.ToShortDateString() == date.ToShortDateString()))
            {
                return Collection.Where(x => x.EditDate.ToShortDateString() == date.ToShortDateString()).ToList();
            }
            throw new Exception();
        }
    }
}
