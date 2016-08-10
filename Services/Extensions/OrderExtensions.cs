using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using Services.Services;
using System.Linq;

namespace Services.Extensions
{
    public static class OrderExtensions
    {
        public static void Update(this Order destination, OrderDTO source, IUnitOfWork uow)
        {
            destination.AdditionalInformation = source.AdditionalInformation;
            destination.AlreadyPaid = source.AlreadyPaid;
            destination.CityId = source.CityId;
            destination.ClientId = source.ClientId;
            destination.Discount = source.Discount;
            destination.MailNumber = source.MailNumber;
            destination.State = source.State;

            source.Items.Where(x => x.Id == 0).ToList().ForEach(newOrderItem =>
            {
                var toDomain = DTOService.ToEntity<OrderItemDTO, OrderItem>(newOrderItem);
                destination.Items.Add(toDomain);
            });
            source.Items.Where(x => x.Id != 0).ToList().ForEach(updatedOrderItem =>
            {
                var domainOrder = destination.Items.FirstOrDefault(x => x.Id == updatedOrderItem.Id);
                if (domainOrder == null)
                {
                    throw new System.Exception();
                }
                if (domainOrder.IsDeleted == true)
                {
                    destination.Items.ToList().Remove(domainOrder);
                }
                else
                {
                    domainOrder.Update(updatedOrderItem);
                }
            });
        }
    }
}
