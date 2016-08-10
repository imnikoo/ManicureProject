using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.DTOs
{
    public class PurchaseDTO : EntityDTO
    {
        public double PricePerPiece { get; set; }
        public int Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ApproximateArrivalDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public bool IsArrived { get; set; }
        public string TrackNumber { get; set; }

        public int PurchasePlaceId { get; set; }

        public int ItemId { get; set; }
    }
}
