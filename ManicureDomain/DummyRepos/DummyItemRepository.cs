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
    public class DummyItemRepository : DummyExtendedRepository<Item>, IItemRepository
    {
        public DummyItemRepository()
        {
            Collection = Storage.Items;
        }

        public ICollection<Item> GetBy(string name, Category category)
        {
            var result = Collection.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(x => x.Name == name);
            }
            if (category != null)
            {
                result = result.Where(x => x.Category == category);
            }
            return result.ToList();
        }
    }
}
