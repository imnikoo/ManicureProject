using System;

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
        public string TrackNumber { get; set; }

        public int PurchasePlaceId { get; set; }
        public virtual PurchasePlace PurchasePlace { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
