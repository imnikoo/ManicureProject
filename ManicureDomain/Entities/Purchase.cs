using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Entities
{
    public class Purchase
    {
        public double PricePerPiece { get; set; }
        public PurchasePlace Place { get; set; }
        public int Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ApproximateArrivalDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public bool IsArrived { get; set; }
    }
}
