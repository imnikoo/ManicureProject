using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int MailNumber { get; set; }
        public string Source { get; set; }
        public string AdditionalInformation { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
