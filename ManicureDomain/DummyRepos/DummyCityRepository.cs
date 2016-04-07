using ManicureDomain.Abstract;
using ManicureDomain.Data;
using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.DummyRepos
{
    public class DummyCityRepository : DummyExtendedRepository<City>, ICityRepository
    {
        public DummyCityRepository()
        {
            Collection = Storage.Cities;
        }

        public ICollection<City> GetByTitle(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                return Collection.Where(x => x.Title == title).ToList();
            }
            throw new Exception();
        }
    }
}
