﻿using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using ManicureDomain.Entities.Enums;
using Services.Services;
using System.Linq;

namespace Services.Extensions
{
    public static class OrderExtensions
    {
        public static void Update(this Order destination, OrderDTO source, IUnitOfWork uow)
        {
            destination.AdditionalInformation = source.AdditionalInformation;
            if(source.State == OrderState.Closed || destination.State != OrderState.Closed)
            {
                foreach (var orderedItem in source.Items)
                {
                    uow.ItemRepository.GetByID(orderedItem.ItemId).Stock -= orderedItem.Quantity;
                }
            }
            destination.State = source.State;
            destination.AlreadyPaid = source.AlreadyPaid;
            destination.CityId = source.CityId;
            destination.ClientId = source.ClientId;
            destination.Discount = source.Discount;
            destination.MailNumber = source.MailNumber;
            destination.State = source.State;
            destination.Sum = source.Sum;
            destination.ToPay = source.ToPay;
            destination.Reciever = source.Reciever;
            destination.PhoneNumber = source.PhoneNumber;


            source.Items.Where(x => x.Id == 0).ToList().ForEach(newOrderItem =>
            {
                var toDomain = DTOService.ToEntity<OrderItemDTO, OrderItem>(newOrderItem);
                toDomain.Item = null;
                destination.Items.Add(toDomain);
            });
            source.Items.Where(x => x.Id != 0).ToList().ForEach(updatedOrderItem =>
            {
                var domainOrder = destination.Items.FirstOrDefault(x => x.Id == updatedOrderItem.Id);
                if (domainOrder == null)
                {
                    throw new System.Exception();
                }
                if(updatedOrderItem.Removed)
                {
                    uow.OrderItemRepository.Delete(updatedOrderItem.Id);
                }
                else
                {
                    domainOrder.Update(updatedOrderItem);
                }
            });
        }
    }
}
