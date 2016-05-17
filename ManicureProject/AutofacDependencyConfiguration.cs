using Autofac;
using Autofac.Integration.WebApi;
using Data.EntityFramework;
using Data.EntityFramework.Infrastructure;
using Data.EntityFramework.Repositories;
using ManicureDomain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
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

           /* builder.RegisterType<EFCandidateRepository>()
                .As<IRepository<Candidate>>()
                .InstancePerRequest();

            builder.RegisterType<EFVacancyStageInfoRepository>()
                .As<IRepository<VacancyStageInfo>>()
                .InstancePerRequest();

            builder.RegisterType<EFVacancyRepository>()
                .As<IRepository<Vacancy>>()
                .InstancePerRequest();*/

            builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerRequest();

            builder.RegisterType<DataRepositoryFactory>()
            .As<IDataRepositoryFactory>()
            .InstancePerRequest();

            builder.RegisterGeneric(typeof(EFEntityRepository<>))
            .As(typeof(IRepository<>))
            .InstancePerRequest();

            Container = builder.Build();
            return Container;
        }
    }
}