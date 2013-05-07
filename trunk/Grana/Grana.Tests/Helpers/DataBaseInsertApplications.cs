using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Grana.Model;
using Grana.Service;
using Grana.Tests.Helpers.Factories;

namespace Grana.Tests.Helpers
{
    public class DataBaseInsertApplications
    {
        public static Application InsertApplication(MoqContainer container , Application application)
        {
            using (ILifetimeScope scope = container.StartNewLifetime())
            {
                GranaDataDataContext granaDataContext = ServiceModule.ResolveReference<GranaDataDataContext>(scope);

                granaDataContext.Applications.InsertOnSubmit(application);
                granaDataContext.SubmitChanges();

                return application;
            }
        }

        public static void InsertContact(MoqContainer mockContainer, Contact mobile)
        {
            using (ILifetimeScope scope = mockContainer.StartNewLifetime())
            {
                GranaDataDataContext granaDataContext = ServiceModule.ResolveReference<GranaDataDataContext>(scope);

                granaDataContext.Contacts.InsertOnSubmit(mobile);
                granaDataContext.SubmitChanges();

            }
        }
    }
}
