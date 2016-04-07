using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class Client : ExtendedEntity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public City City { get; set; }
        public int MailNumber { get; set; }
        public string Source { get; set; }
    }
}
