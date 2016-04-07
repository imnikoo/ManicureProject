using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Abstract
{
    public interface IExtendedRepository<T> : IRepository<T>
    {
        ICollection<T> GetByDate(DateTime date);
    }
}
