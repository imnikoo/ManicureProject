using System.Collections.Generic;

namespace ManicureDomain.Entities
{
    public class Client : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public string Source { get; set; }
        public string AdditionalInformation { get; set; }

        public int? CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
