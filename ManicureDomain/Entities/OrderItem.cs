using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class OrderItem : Entity
    {
        public virtual Item Item { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
