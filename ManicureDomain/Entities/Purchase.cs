using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class Purchase : Entity
    {
        public double PricePerPiece { get; set; }
        public int Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ApproximateArrivalDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public bool IsArrived { get; set; }

        public int PlaceId { get; set; }
        public virtual PurchasePlace Place { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
