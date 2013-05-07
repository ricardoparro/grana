using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grana.Model.ViewModel.Application
{
    public class ApplicationDetails
    {
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public string LastName { get; set; }
        public Applicant Applicant { get; set; }
        public Employment Employment { get; set; }
        public Contact Contact { get; set; }

        public BankAccount BankAccount { get; set; }

        public BankCard BankCard { get; set; }
    }
}
