using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Grana.Model;
using Grana.Service;

namespace Grana.Tests.Helpers
{
    public class DataBaseInsertApplicant
    {
        public static Applicant InsertApplicant(MoqContainer mockContainer, Applicant applicant)
        {
            using (ILifetimeScope scope = mockContainer.StartNewLifetime())
            {
                GranaDataDataContext granaAppContext = ServiceModule.ResolveReference<GranaDataDataContext>(scope);
                granaAppContext.Applicants.InsertOnSubmit(applicant);
                granaAppContext.SubmitChanges();

                return applicant;
            }
        }
    }
}
