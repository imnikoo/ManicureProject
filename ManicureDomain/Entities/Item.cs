using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class Item : Entity
    {
        public Item()
        {
            Purchases = new System.Collections.Generic.List<Purchase>();
        }
        public string Name { get; set; }
        public int Amount { get; set; }
        public double OriginalPrice { get; set; }
        public double MarginalPrice { get; set; }
        public string AdditionalInformation { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
