using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Abstract
{
    public interface ICityRepository : IExtendedRepository<City>
    {
        ICollection<City> GetByTitle(string title);
    }
}
