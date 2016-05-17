using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataa.EntityFramework.Mapping
{
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        public OrderConfiguration()
        {
            HasMany(x => x.Items).WithRequired(x=>x.Order).HasForeignKey(x => x.OrderId);
            HasRequired(x => x.Client).WithMany(x => x.Orders).HasForeignKey(x => x.ClientId);
        }
    }
}
