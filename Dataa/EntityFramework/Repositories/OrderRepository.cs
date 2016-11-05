using Data.EntityFramework.Repositories;
using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dataa.EntityFramework.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {

        }
    }
}
