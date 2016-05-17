using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataa.EntityFramework.Mapping
{
    public class ClientConfiguration : EntityConfiguration<Client>
    {
        public ClientConfiguration()
        {
            HasOptional(x => x.City).WithMany();
            HasMany(x => x.Orders).WithRequired(x => x.Client).HasForeignKey(x => x.ClientId);
        }
    }
}
