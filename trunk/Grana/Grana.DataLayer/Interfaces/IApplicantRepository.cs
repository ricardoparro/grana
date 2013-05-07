using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;
using Grana.Model.ViewModel.Applicant;

namespace Grana.DataLayer.Interfaces
{
    public interface IApplicantRepository
    {
        void UpdateApplicant(Applicant applicant);
        Applicant GetApplicant(int applicantId);
        List<Applicant> SearchApplicant(ApplicantSearch request);
        ApplicantDetailsModel GetApplicantDetails(int applicantId);
    }
}
