using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using System.Linq;

namespace Services.Extensions
{
    public static class ClientExtensions
    {
        public static void Update(this Client destination,
            ClientDTO source,
            IUnitOfWork uow)
        {
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.PhoneNumber = source.PhoneNumber;

            destination.Source = source.Source;
            destination.AdditionalInformation = source.AdditionalInformation;

            destination.CityId = source.City.Id;

            source.Orders.Where(x => x.Id == 0).ToList().ForEach(newOrder =>
                {
                    var toDomain = new Order();
                    toDomain.Update(newOrder, uow);
                    destination.Orders.Add(toDomain);
                });
            source.Orders.Where(x => x.Id != 0).ToList().ForEach(updatedOrder =>
                {
                    var domainOrder = destination.Orders.FirstOrDefault(x => x.Id == updatedOrder.Id);
                    if (domainOrder == null)
                    {
                        throw new System.Exception();
                    }
                    if (domainOrder.IsDeleted == true)
                    {
                        uow.OrderRepository.Delete(domainOrder.Id);
                    }
                    else
                    {
                        domainOrder.Update(updatedOrder, uow);
                    }
                });
        }
    }
}
