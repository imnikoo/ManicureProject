using ManicureDomain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class Order : ExtendedEntity
    {
        public Order()
        {
           // ToPay = Sum;
        }
        public double Sum {
            get { return Items.Sum(x => x.Price * x.Quantity); }
        }
        public double AlreadyPaid { get; set; }
        public double ToPay { get; set; }
        public double Discount { get; set; }
        public OrderState State { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
        public virtual Client Client { get; set; }
    }
}
