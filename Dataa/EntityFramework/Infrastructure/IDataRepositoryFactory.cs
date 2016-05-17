using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Data.EntityFramework.Infrastructure
{
    public interface IDataRepositoryFactory
    {
        IRepository<T> GetDataRepository<T>(HttpRequestMessage request) where T : Entity, new();
    }
}
