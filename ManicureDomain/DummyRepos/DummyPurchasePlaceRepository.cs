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
    public class DummyPurchasePlaceRepository : DummyRepository<PurchasePlace>, IPurchasePlaceRepository
    {
        public DummyPurchasePlaceRepository()
        {
            Collection = Storage.PurchasePlaces;
        }

        public ICollection<PurchasePlace> GetByTitle(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                return Collection.Where(x => x.Title == title).ToList();
            }
            throw new Exception();
        }
    }
}
