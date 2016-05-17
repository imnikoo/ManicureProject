using ManicureDomain.Abstract;
using ManicureDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace Data.Extensions
{
    public static class RequestMessageExtensions
    {
        private static TService GetService<TService>(this HttpRequestMessage request)
        {
            IDependencyScope dependencyScope = request.GetDependencyScope();
            TService service = (TService)dependencyScope.GetService(typeof(TService));
            return service;
        }

        public static IRepository<T> GetDataRepository<T>(this HttpRequestMessage request)
            where T : Entity, new()
        {
            return request.GetService<IRepository<T>>();
        }

    }
}
