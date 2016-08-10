using Data.EntityFramework.Infrastructure;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Services
{
    public class OrderService
    {
        IUnitOfWork uow;
        public OrderService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Tuple<IEnumerable<OrderDTO>, int> Get(int page, int perPage, string filterText)
        {
            List<Expression<Func<Order, bool>>> predicates = new List<Expression<Func<Order, bool>>>();

            if (!String.IsNullOrEmpty(filterText))
            {
                predicates.Add(x =>
                x.Client.FirstName.StartsWith(filterText)
                || x.Client.LastName.StartsWith(filterText)
                || x.Items.Any(orderItem => orderItem.Item.Title.StartsWith(filterText))
                || x.AdditionalInformation.Contains(filterText));
            }

            var orders = uow.OrderRepository.Get(predicates);
            var total = orders.Count();

            return new Tuple<IEnumerable<OrderDTO>, int>(
                orders.Skip(page * perPage).Take(perPage).Select(order => DTOService.ToDTO<Order, OrderDTO>(order)),
                total);
        }

        public OrderDTO Get(int id)
        {
            var entity = uow.OrderRepository.GetByID(id);
            return DTOService.ToDTO<Order, OrderDTO>(entity);
        }

        public OrderDTO Add(OrderDTO OrderToAdd)
        {
            Order newOrder = new Order();
            newOrder.Update(OrderToAdd, uow);
            uow.OrderRepository.Insert(newOrder);
            uow.Commit();
            return DTOService.ToDTO<Order, OrderDTO>(newOrder);
        }

        public OrderDTO Update(OrderDTO entity)
        {
            Order _Order = uow.OrderRepository.GetByID(entity.Id);
            _Order.Update(entity, uow);
            uow.OrderRepository.Update(_Order);
            uow.Commit();
            return DTOService.ToDTO<Order, OrderDTO>(_Order);
        }

        public bool Delete(int id)
        {
            bool deleteResult;
            var entityToDelete = uow.OrderRepository.GetByID(id);
            if (entityToDelete == null)
            {
                deleteResult = false;
            }
            else {
                uow.OrderRepository.Delete(id);
                uow.Commit();
                deleteResult = true;
            }
            return deleteResult;
        }
    }
}
