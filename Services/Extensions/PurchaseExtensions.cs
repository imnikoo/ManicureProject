using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using System;

namespace Services.Extensions
{
    public static class PurchaseExtensions
    {
        public static void Update(this Purchase destination, PurchaseDTO source)
        {
            destination.PricePerPiece = source.PricePerPiece;
            destination.Amount = source.Amount;
            destination.OrderDate = source.OrderDate;
            destination.ApproximateArrivalDate = source.Id == 0 ? source.OrderDate.AddDays(30) : source.ApproximateArrivalDate;
            destination.IsArrived = source.IsArrived;
            destination.ArrivalDate = source.IsArrived && !destination.ArrivalDate.HasValue ? destination.ArrivalDate = DateTime.Now : destination.ArrivalDate;

            destination.PurchasePlaceId = source.PurchasePlaceId;
            destination.ItemId = source.ItemId;
        }
    }
}
