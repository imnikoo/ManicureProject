using ManicureDomain.Entities.Enums;
using System.Collections.Generic;

namespace ManicureDomain.DTOs
{
    public class OrderDTO : EntityDTO
    {
        public double Sum { get; set; }
        public double AlreadyPaid { get; set; }
        public double ToPay { get; set; }
        public string Discount { get; set; }
        public int MailNumber { get; set; }
        public OrderState State { get; set; }
        public string AdditionalInformation { get; set; }

        public int CityId { get; set; }
        public int ClientId { get; set; }

        public string Reciever { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
}
