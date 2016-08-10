using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using Services.Services;
using System.Linq;

namespace Services.Extensions
{
    public static class ItemExtensions
    {
        public static void Update(this Item destination, ItemDTO source, IUnitOfWork uow)
        {
            destination.Title = source.Title;
            destination.Stock = source.Stock;
            destination.OriginalPrice = source.OriginalPrice;
            destination.MarginalPrice = source.MarginalPrice;
            destination.AdditionalInformation = source.AdditionalInformation;
            destination.CategoryId = source.CategoryId;

            source.Purchases.Where(x => x.Id == 0).ToList().ForEach(newPurchase =>
                {
                    var toDomain = DTOService.ToEntity<PurchaseDTO, Purchase>(newPurchase);
                    toDomain.Update(newPurchase);
                    destination.Purchases.Add(toDomain);
                });

            source.Purchases.Where(x => x.Id != 0).ToList().ForEach(updPurchase =>
                {
                    var domainPurchase = destination.Purchases.FirstOrDefault(x => x.Id == updPurchase.Id);
                    if (domainPurchase == null)
                    {
                        throw new System.Exception();
                    }
                    if (domainPurchase.IsDeleted == true)
                    {
                        uow.PurchaseRepository.Delete(domainPurchase.Id);
                    }
                    else
                    {
                        domainPurchase.Update(updPurchase);
                    }
                });
        }
    }
}
