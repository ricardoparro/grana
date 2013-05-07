using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.EnumModel;
using Grana.Model.ViewModel.Applicant;

namespace Grana.DataLayer.Repository
{
    public class ApplicantRepository : BaseRepository, IApplicantRepository
    {
        public void UpdateApplicant(Applicant applicant)
        {
            Applicant originalApplicant = GetApplicant(applicant.ApplicantId);

            originalApplicant.BirthDate = applicant.BirthDate;
            originalApplicant.FirstName = applicant.FirstName;
            originalApplicant.LastName = applicant.LastName;
            originalApplicant.MiddleNames = applicant.MiddleNames;
            originalApplicant.Gender = applicant.Gender;
            originalApplicant.HomeStatus = applicant.HomeStatus;
            originalApplicant.IdentityNumber = applicant.IdentityNumber;
            originalApplicant.MaritalStatus = applicant.MaritalStatus;
            originalApplicant.NumberOfDependants = applicant.NumberOfDependants;
            originalApplicant.OwnCar = applicant.OwnCar;
            originalApplicant.SocialSecurityNumber = applicant.SocialSecurityNumber;
            originalApplicant.CarLicense = applicant.CarLicense;
            context.SubmitChanges();

        }

        public Applicant GetApplicant(int applicantId)
        {
            Applicant app = (from applicant in context.Applicants
                                        where applicant.ApplicantId == applicantId
                                        select applicant).FirstOrDefault();

            return app;
        }

        public List<Applicant> SearchApplicant(ApplicantSearch request)
        {
            IQueryable<Applicant> applicantQuery = context.Applicants;

            //Trim down which Applicant Loan Request's we're looking for)
            if (!string.IsNullOrEmpty(request.Firstname))
                applicantQuery = applicantQuery.Where(a => a.FirstName.StartsWith(request.Firstname));

            if (!string.IsNullOrEmpty(request.Lastname))
                applicantQuery = applicantQuery.Where(a => a.LastName.StartsWith(request.Lastname));

            if (!string.IsNullOrEmpty(request.Email))
                applicantQuery = applicantQuery.Where(a => a.Contacts.Any(c => c.ContactDetail.StartsWith(request.Email) && c.ContactTypeId == (int)ContactsOfType.EmailAddress));

            if (!string.IsNullOrEmpty(request.Mobile))
                applicantQuery = applicantQuery.Where(a => a.Contacts.Any(c => c.ContactDetail.StartsWith(request.Mobile) && c.ContactTypeId == (int)ContactsOfType.MobilePhone));

            if (!string.IsNullOrEmpty(request.Postcode))
                applicantQuery = applicantQuery.Where(app => app.Addresses.Any(ha => ha.Postcode.StartsWith(request.Postcode)));

            //Card number search special cause not displaying it on the page
            if (!string.IsNullOrEmpty(request.CardNumber))
            {
                applicantQuery = applicantQuery.Where(a => a.BankCards.Any(b => b.CardNumber.StartsWith(request.CardNumber)));
            }


            List<Applicant> applicants = (from app in applicantQuery
                                          join applicant in context.Applicants on app.ApplicantId equals
                                              applicant.ApplicantId
                                          select applicant).ToList();
                                            
                                                              
            return applicants;

        }

        public ApplicantDetailsModel GetApplicantDetails(int applicantId)
        {
            ApplicantDetailsModel applicantDetailsModel = (from applicant in context.Applicants
                                                           where applicant.ApplicantId == applicantId
                                                           select new ApplicantDetailsModel()
                                                                      {
                                                                          Applications = context.Applications.Where(a => a.ApplicantId == applicantId).ToList()
                                                                      }).FirstOrDefault();

            return applicantDetailsModel;
        }
    }
}
