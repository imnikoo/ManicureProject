using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManicureDomain.Entities.Enums;
using ManicureDomain.Data;

namespace ManicureDomain.DummyRepos
{
    public class DummyOrderRepository : DummyExtendedRepository<Order>, IOrderRepository
    {
        public DummyOrderRepository()
        {
            Collection = Storage.Orders;
        }

        public ICollection<Order> GetByClient(Client client)
        {
            return Collection.Where(x => x.Client == client).ToList();
        }

        public ICollection<Order> GetByItem(Item item)
        {
            return Collection.Where(x => x.Items.AsQueryable().Any(y => y.Item == item)).ToList();
        }

        public ICollection<Order> GetByState(OrderState state)
        {
            return Collection.Where(x => x.State == state).ToList();
        }
    }
}
