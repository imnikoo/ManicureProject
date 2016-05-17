using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class OrderItem : Entity
    {
        public int Quantity { get; set; }
        public double Price { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
