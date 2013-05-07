using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;

namespace Grana.Tests.Helpers.Factories
{
    public class ApplicationFactory
    {
        public static Application GetApplication(string applicationStatus, DateTime applicationDate, int applicantId)
        {
            Application application = new Application
                                          {
                                              ApplicationDate = applicationDate,
                                              Amount = 1000,
                                              AppStatus = applicationStatus,
                                              InterestRate = 0.5,
                                              PaybackDate = DateTime.Now.AddDays(22),
                                              ApplicantId = applicantId
                                              
                                          };

            return application;
        }
    }
}
