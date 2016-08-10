using ManicureDomain.DTOs;
using ManicureDomain.Entities;

namespace Services.Extensions
{
    public static class OrderItemExtensions
    {
        public static void Update(this OrderItem destination, OrderItemDTO source)
        {
            destination.ItemId = source.ItemId;
            destination.Quantity = source.Quantity;
        }
    }
}
