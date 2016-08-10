namespace ManicureDomain.DTOs
{
    public class OrderItemDTO : EntityDTO
    {
        public int Quantity { get; set; }
        public int ItemId { get; set; }
    }
}
