using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Abstract
{
    public interface IPurchasePlaceRepository : IRepository<PurchasePlace>
    {
        ICollection<PurchasePlace> GetByTitle(string title);
    }
}
