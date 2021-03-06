﻿using ManicureDomain.Entities.Enums;
using System.Collections.Generic;

namespace ManicureDomain.Entities
{
    public class Order : Entity
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public double Sum { get; set; }
        public double AlreadyPaid { get; set; }
        public double ToPay { get; set; }
        public string Discount { get; set; }
        public int MailNumber { get; set; }
        public OrderState State { get; set; }
        public string AdditionalInformation { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public string Reciever { get; set; }
        public string PhoneNumber { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
