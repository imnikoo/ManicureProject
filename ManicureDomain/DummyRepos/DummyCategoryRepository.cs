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
    public class DummyCategoryRepository : DummyExtendedRepository<Category>, ICategoryRepository
    {
        public DummyCategoryRepository()
        {
            Collection = Storage.Categories;
        }

        public ICollection<Category> GetByTitle(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                return Collection.Where(x => x.Title == title).ToList();
            }
            throw new Exception();
        }
    }
}
