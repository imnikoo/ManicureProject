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
    public class DummyClientRepository : DummyExtendedRepository<Client>, IClientRepository
    {
        public DummyClientRepository()
        {
            Collection = Storage.Clients;
        }

        public ICollection<Client> FindBy(string name, City city)
        {
            var result = Collection.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(x => x.Name == name);
            }
            if (city != null)
            {
                result = result.Where(x => x.City == city);
            }
            if (!Equals(result, Collection.AsQueryable()))
            {
                return result.ToList();
            }
            return null;
        }
    }
}
