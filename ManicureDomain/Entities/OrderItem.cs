namespace ManicureDomain.Entities
{
    public class OrderItem : Entity
    {
        public int Quantity { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
