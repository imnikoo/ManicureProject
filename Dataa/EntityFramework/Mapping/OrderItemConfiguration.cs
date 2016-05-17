using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataa.EntityFramework.Mapping
{
    public class OrderItemConfiguration : EntityConfiguration<OrderItem>
    {
        public OrderItemConfiguration()
        {
            HasRequired(x => x.Order).WithMany().HasForeignKey(x => x.OrderId);
            HasRequired(x => x.Item).WithMany().HasForeignKey(x => x.ItemId);
        }
    }
}
