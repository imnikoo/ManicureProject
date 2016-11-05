using System.Collections.Generic;

namespace ManicureDomain.DTOs
{
    public class ItemDTO : EntityDTO
    {
        public string Title { get; set; }
        public int Stock { get; set; }
        public double OriginalPrice { get; set; }
        public double MarginalPrice { get; set; }
        public string AdditionalInformation { get; set; }
        public int OrdersOfItem { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<PurchaseDTO> Purchases { get; set; }

    }
}
