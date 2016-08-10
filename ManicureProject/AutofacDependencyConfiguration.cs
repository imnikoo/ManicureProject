using Autofac;
using Autofac.Integration.WebApi;
using Data.EntityFramework;
using Data.EntityFramework.Infrastructure;
using Data.EntityFramework.Repositories;
using ManicureDomain.Abstract;
using ManicureDomain.DTOs;
using ManicureDomain.Entities;
using Services.Services;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace ManicureProject
{
    public class AutofacDependencyConfiguration
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ManicureContext>()
            .As<DbContext>()
           .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerRequest();

            builder.RegisterType<CategoryService>()
               .As<BaseService<Category, CategoryDTO>>()
               .InstancePerRequest();

            builder.RegisterType<CityService>()
               .As<BaseService<City, CityDTO>>()
               .InstancePerRequest();

            builder.RegisterType<ClientService>()
               .As<ClientService>()
               .InstancePerRequest();

            builder.RegisterType<ItemService>()
               .As<ItemService>()
               .InstancePerRequest();

            builder.RegisterType<OrderService>()
               .As<OrderService>()
               .InstancePerRequest();

            builder.RegisterType<PurchasePlaceService>()
             .As<BaseService<PurchasePlace, PurchasePlaceDTO>>()
             .InstancePerRequest();

            builder.RegisterGeneric(typeof(Repository<>))
            .As(typeof(IRepository<>))
            .InstancePerRequest();

            Container = builder.Build();
            return Container;
        }
    }
}