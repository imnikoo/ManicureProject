using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class ExtendedEntity : Entity
    {
        public DateTime EditDate { get; set; }
        public string AdditionalInformation { get; set; }
    }
}
