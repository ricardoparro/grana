using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.ViewModel.Applicant;

namespace Grana.Service.Portugal
{
    public class ApplicantService : BaseService
    {
        private readonly IApplicantRepository _applicantRepository;

        public ApplicantService(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public List<Applicant> SearchApplicant(ApplicantSearch searchObj)
        {
            List<Applicant> applicants = new List<Applicant>();

            applicants = _applicantRepository.SearchApplicant(searchObj);

            return applicants;
        }

        public ApplicantDetailsModel GetApplicantDetails(int applicantId)
        {
           ApplicantDetailsModel model = _applicantRepository.GetApplicantDetails(applicantId);

            return model;

        }
    }
}
