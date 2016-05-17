using ManicureDomain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class Order : Entity
    {
        public Order()
        {

        }

        public double Sum { get; set; }
        public double AlreadyPaid { get; set; }
        public double ToPay { get; set; }
        public double Discount { get; set; }
        public OrderState State { get; set; }
        public string AdditionalInformation { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

    }
}
