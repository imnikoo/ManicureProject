using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Abstract
{
    public interface IItemRepository : IExtendedRepository<Item>
    {
        ICollection<Item> GetBy(string name, Category category);
    }
}
