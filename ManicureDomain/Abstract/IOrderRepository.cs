using ManicureDomain.Entities;
using ManicureDomain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManicureDomain.Abstract
{
    public interface IOrderRepository : IExtendedRepository<Order>
    {
        ICollection<Order> GetByState(OrderState state);
        ICollection<Order> GetByClient(Client client);
        ICollection<Order> GetByItem(Item item);

        /*void AddOrder(Order order, Item item, int quantity);
        void RemoveOrder(Order order, Product product);
        void EditOrder(Order order, Product product, int quantity);*/
    }
}
