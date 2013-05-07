using System.Collections.Generic;
using FluentValidation.Attributes;
using FluentValidation.Results;
using Grana.Model;
using Grana.Model.ViewModel.Documents;
using Grana.Model.ViewModel.Notes;

namespace Grana.Model.ViewModel.ApplicationDetails
{
    public class ApplicationDetails
    {
        public Model.Applicant Applicant { get; set; }
        public Employment Employment { get; set; }
        public Contact HomePhone { get; set; }
        public Contact EmailAddress { get; set; }
        public Contact MobilePhone { get; set; }
        public BankAccount BankAccount { get; set; }
        public BankCard BankCard { get; set; }
        public Grana.Model.Application Application { get; set; }
        public Address Address { get; set; }
        public List<ValidationFailure> Errors { get; set; }

        public bool UpdateSuccess { get; set; }

        public int NumberOfErrors { get; set; }

        public List<string> SectionsWithErrors { get; set; }
        public List<NotesRow> Notes { get; set; }
        public List<DocumentRow> Documents { get; set; }

        public List<Reason> ApprovedReasons { get; set; }

        public List<Reason> DeclinedReasons { get; set; }

        public List<Reason> PendingReasons { get; set; }
    }
}
