using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Autofac;
using Autofac.Integration.Mvc;
using FluentValidation.Mvc;
using Grana.DataLayer;
using Grana.Model.ViewModel.ApplicationDetails;
using Grana.Service;
using Grana.Service.Validation;
using Grana.Service.Validation.Interfaces;

namespace Grana.Portugal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication 
    {
       

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            //create IoC-container
            var builder = new ContainerBuilder();

            //Register all controllers of the current Assembly
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            ServiceModule.AddInjectionOfControlDependencies(builder, true);

            //set Autofac as default Dependency Resolver for application
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
            //and leave below as it was before

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            

        }
    }
}