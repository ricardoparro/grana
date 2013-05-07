using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.EnumModel;
using Grana.Model.ViewModel.Application;
using Grana.Model.ViewModel.ApplicationDetails;

namespace Grana.DataLayer.Repository
{
    public class ApplicationRepository : BaseRepository, IApplicationRepository 
    {
        public ApplicationDetails GetApplicationDetailsById(int applicationId)
        {
            ApplicationDetails app = (from application in context.Applications
                               join applicant in context.Applicants on application.ApplicantId equals applicant.ApplicantId
                               join employment in context.Employments on applicant.ApplicantId equals employment.ApplicantId into employmentGroup
                               from emp in employmentGroup.DefaultIfEmpty()
                               join bankAccount in context.BankAccounts on applicant.ApplicantId equals bankAccount.ApplicantId into bankAccountGroup
                               from bAcc in bankAccountGroup.DefaultIfEmpty()
                               join bankCard in context.BankCards on applicant.ApplicantId equals bankCard.ApplicantId into bankCardGroup
                               from bankC in bankCardGroup.DefaultIfEmpty()
                               join address in context.Addresses on applicant.ApplicantId equals address.ApplicantId into addressGroup
                               from addr in addressGroup.DefaultIfEmpty()

            
            
            
            
            
                               where application.ApplicationId == applicationId 
                               select new ApplicationDetails
                                          {
                                              Applicant = applicant,
                                              Employment = emp,
                                              HomePhone = application.Contacts.Where(a => a.ContactTypeId==(int)ContactsOfType.HomePhone).FirstOrDefault() ,
                                              MobilePhone = application.Contacts.Where(a => a.ContactTypeId == (int)ContactsOfType.MobilePhone).FirstOrDefault(),
                                              EmailAddress = application.Contacts.Where(a => a.ContactTypeId == (int)ContactsOfType.EmailAddress).FirstOrDefault(),
                                              BankAccount = bAcc,
                                              BankCard = bankC,
                                              Application = application,
                                              Address = addr,
                                              ApprovedReasons = context.Reasons.Where(a => a.Status == ApplicationStatuses.Approved.ToString()).ToList(),
                                              DeclinedReasons = context.Reasons.Where(a => a.Status == ApplicationStatuses.Declined.ToString()).ToList(),
                                              PendingReasons = context.Reasons.Where(a => a.Status == ApplicationStatuses.Undecided.ToString()).ToList()
                                              
                                          }).FirstOrDefault();

            return app;
        }

        public void UpdateApplication(Application application)
        {
            Application originalApp = GetApplication(application.ApplicationId);

            originalApp.Amount = application.Amount;
            originalApp.InterestRate = application.InterestRate;
            originalApp.PaybackDate = application.PaybackDate;
            
            context.SubmitChanges();
        }

        public List<ApplicationForList> GetApplicationsByStatus(ApplicationStatuses applicationStatus)
        {
            List<ApplicationForList> newApplications = (from application in context.Applications
                                    join applicant in context.Applicants on application.ApplicantId equals applicant.ApplicantId
                                    where application.AppStatus ==  applicationStatus.ToString()
                                    select new ApplicationForList
                                               {
                                                   ApplicationDate = application.ApplicationDate,
                                                   ApplicationId = application.ApplicationId,
                                                   FirstName = applicant.FirstName,
                                                   MiddleName = applicant.MiddleNames,
                                                   LastName =  applicant.LastName,
                                                   Email = application.Contacts.Where(a => a.ContactTypeId == (int)ContactsOfType.EmailAddress).FirstOrDefault().ContactDetail,
                                                   LoanAmount = application.Amount,
                                                   LoanPaybackDate = application.PaybackDate
                                               }).OrderByDescending(a => a.ApplicationDate).ToList();

            return newApplications;
        }

        public void UpdateApplicationStatus(ApplicationStatuses applicationStatus, int applicationId)
        {
            Application application = GetApplication(applicationId);
            application.AppStatus =  applicationStatus.ToString();

            context.SubmitChanges();
        }

        public List<UndecidedApplication> GetUndecidedApplications(ApplicationStatuses applicationStatus)
        {
            List<UndecidedApplication> undecidedApplications = (from application in context.Applications
                                                    join applicant in context.Applicants on application.ApplicantId equals applicant.ApplicantId
                                                    where application.AppStatus == applicationStatus.ToString()
                                                    select new UndecidedApplication
                                                    {
                                                        ApplicationDate = application.ApplicationDate,
                                                        ApplicationId = application.ApplicationId,
                                                        FirstName = applicant.FirstName,
                                                        MiddleName = applicant.MiddleNames,
                                                        LastName = applicant.LastName,
                                                        Email = application.Contacts.Where(a => a.ContactTypeId == (int)ContactsOfType.EmailAddress).FirstOrDefault().ContactDetail,
                                                        LoanAmount = application.Amount,
                                                        LoanPaybackDate = application.PaybackDate
                                                    }).OrderByDescending(a => a.ApplicationDate).ToList();

            return undecidedApplications;
        }

        public Application GetApplication(int applicationId)
        {
            Application application = (from app in context.Applications
                                          where app.ApplicationId == applicationId
                                          select app).FirstOrDefault();

            return application;
        }
    }
}
