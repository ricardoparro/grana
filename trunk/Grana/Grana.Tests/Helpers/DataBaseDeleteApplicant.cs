using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Grana.Model;
using Grana.Service;

namespace Grana.Tests.Helpers
{
    public class DataBaseDeleteApplicant
    {
        public static void DeleteApplicantsAndDetails(int[] applicantIds, GranaDataDataContext context)
        {

            //Delete Applications for these applicants
            List<Application> applications =
                context.Applications.Where(a => applicantIds.Contains(a.ApplicantId)).ToList();
            int[] applicationIds = applications.Select(a => a.ApplicationId).ToArray();

            //Delete Applications
            DataBaseDeleteApplications.DataBaseDeleteApplicationsAndDetails(applicationIds, context);

            //Delete Employments
            List<Employment> employments = context.Employments.Where(e => applicantIds.Contains(e.ApplicantId)).ToList();
            context.Employments.DeleteAllOnSubmit(employments);

            //Delete BankCards
            List<BankCard> bankCards = context.BankCards.Where(c => applicantIds.Contains(c.ApplicantId)).ToList();
            context.BankCards.DeleteAllOnSubmit(bankCards);

            //Delete BankAccount
            List<BankAccount> bankAccounts = context.BankAccounts.Where(a => applicantIds.Contains(a.ApplicantId)).ToList();
            context.BankAccounts.DeleteAllOnSubmit(bankAccounts);

            //Delete Addresses

            List<Address> addresses = context.Addresses.Where(a => applicantIds.Contains(a.ApplicantId)).ToList();
            context.Addresses.DeleteAllOnSubmit(addresses);

            //Delete Applicants
            List<Applicant> applicants = context.Applicants.Where(a => applicantIds.Contains(a.ApplicantId)).ToList();
            context.Applicants.DeleteAllOnSubmit(applicants);

            context.SubmitChanges();


        }

        public static void DeleteExistingApplicants(List<Applicant> applicants, GranaDataDataContext context)
        {
            int[] applicantIds = applicants.Select(a => a.ApplicantId).ToArray();

            DeleteApplicantsAndDetails(applicantIds, context);
        }

        public static void DeleteApplicants(int[] applicantIds, MoqContainer mockContainer)
        {
            using (ILifetimeScope scope = mockContainer.StartNewLifetime())
            {
                GranaDataDataContext context = ServiceModule.ResolveReference<GranaDataDataContext>(scope);
                DeleteApplicantsAndDetails(applicantIds, context);
            }
        }
    }


}

