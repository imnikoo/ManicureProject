using System.Collections.Generic;

namespace ManicureDomain.DTOs
{
    public class ClientDTO : EntityDTO
    {
        public ClientDTO()
        {
            Orders = new List<OrderDTO>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public string Source { get; set; }
        public string AdditionalInformation { get; set; }

        public int CityId { get; set; }
        public virtual CityDTO City { get; set; }

        public virtual IEnumerable<OrderDTO> Orders { get; set; }
    }
}
