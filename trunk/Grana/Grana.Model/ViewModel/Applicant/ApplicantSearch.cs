using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grana.Model.ViewModel.Applicant
{
    public class ApplicantSearch
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CardNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Homephone { get; set; }

        public List<Model.Applicant> Applicants { get; set; }

        public string Postcode { get; set; }
    }
}
