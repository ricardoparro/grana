using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grana.Model.ViewModel.Application
{
    public class ApplicationForList
    {

        public int ApplicationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal LoanAmount { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LoanPaybackDate { get; set; }
    }
}
