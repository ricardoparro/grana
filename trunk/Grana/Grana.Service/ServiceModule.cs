using System.Configuration;
using System.Reflection;
using Autofac;
using FluentValidation;
using Grana.DataLayer;
using Grana.Model;
using Grana.Service.Validation;
using Grana.Service.Validation.Interfaces;
using Module = Autofac.Module;

namespace Grana.Service
{
    public class ServiceModule : Module
    {
        public static void AddInjectionOfControlDependencies(ContainerBuilder builder, bool isMvcProject)
        {
            RegisterServices(builder, isMvcProject);
            RegisterRepositories(builder, isMvcProject);
        }

        //Register services for Autofac to work
        private static void RegisterServices(ContainerBuilder builder, bool isMvcProject)
        {
            Assembly serviceAssembly = Assembly.GetAssembly(typeof(BaseService));

            var serviceDependency = builder.RegisterAssemblyTypes(serviceAssembly);

            if (isMvcProject)
                serviceDependency.InstancePerLifetimeScope();
            else
                serviceDependency.InstancePerDependency();

            //for the validators

            Assembly validatorAssembly = Assembly.GetAssembly(typeof(ValidationFactory<>));

            var validatorDependency = builder.RegisterAssemblyTypes(validatorAssembly);

            if (isMvcProject)
                validatorDependency.InstancePerLifetimeScope();
            else
                validatorDependency.InstancePerDependency();


        }
        public static TReturnType ResolveReference<TReturnType>(IComponentContext container)
        {
            return container.Resolve<TReturnType>();
        }

        //Register repositories for Autofac to work
        private static void RegisterRepositories(ContainerBuilder builder, bool isMvcProject)
        {
            var gnBuilder = builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof (BaseRepository))).AsImplementedInterfaces().
                    PropertiesAutowired();

            if (isMvcProject)
                gnBuilder.InstancePerLifetimeScope();
            else
                gnBuilder.InstancePerDependency();


            ConnectionStringSettings granaConnection = ConfigurationManager.ConnectionStrings["GranaConnectionString"];

            if (granaConnection != null)
            {
                string mrLconnectionStr = granaConnection.ConnectionString;
                var granacontext = builder.Register(a => new GranaDataDataContext(mrLconnectionStr));

                granacontext.InstancePerLifetimeScope();
            }
        }
    }
}
