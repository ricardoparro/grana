using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.EnumModel;
using Grana.Model.ViewModel.Application;
using Grana.Model.ViewModel.ApplicationDetails;
using Grana.Service.Validation;

namespace Grana.Service.Portugal
{
    public class ApplicationDetailsService : BaseService
    {
       
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBankAgencyRepository _bankAgencyRepository;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IEmploymentRepository _employmentRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IBankCardRepository _bankCardRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IReasonRepository _reasonRepository;
        private readonly IActionLogRepository _actionLogRepository;


        public ApplicationDetailsService(IApplicationRepository applicationRepository, IBankAgencyRepository bankAgencyRepository, IApplicantRepository applicantRepository,
            IEmploymentRepository employmentRepository, IAddressRepository addressRepository, IContactRepository contactRepository,
            IBankAccountRepository bankAccountRepository, IBankCardRepository bankCardRepository, INoteRepository noteRepository, IDocumentRepository documentRepository,
            IReasonRepository reasonRepository, IActionLogRepository actionLogRepository)
        {
            _applicationRepository = applicationRepository;
            _bankAgencyRepository = bankAgencyRepository;
            _applicantRepository = applicantRepository;
            _employmentRepository = employmentRepository;
            _addressRepository = addressRepository;
            _contactRepository = contactRepository;
            _bankAccountRepository = bankAccountRepository;
            _bankCardRepository = bankCardRepository;
            _noteRepository = noteRepository;
            _documentRepository = documentRepository;
            _reasonRepository = reasonRepository;
            _actionLogRepository = actionLogRepository;
        }

        public ApplicationDetails GetApplicationDetails(int applicationId, string baseDirectory)
        {
            //get application details by Id
            ApplicationDetails application = _applicationRepository.GetApplicationDetailsById(applicationId);

            application.Notes = _noteRepository.GetAllNotesByApplicationId(applicationId);

            application.Documents = _documentRepository.GetDocumentsByApplicationId(applicationId,baseDirectory);

            return application;
        }

        public ApplicationDetails UpdateApplicationDetails(ApplicationDetails model)
        {
            ApplicationDetailsValidator applicationDetailsValidator = new ApplicationDetailsValidator(_bankAgencyRepository);
            
            //validate the form data
            ValidationResult validationResult = applicationDetailsValidator.Validate(model);

            model.UpdateSuccess = true;
            //if it's invalid prepare the error list to be handled on frontend side
            if(!validationResult.IsValid)
            {
                if(validationResult.Errors != null && validationResult.Errors.Count > 0)
                {
                    model.NumberOfErrors = validationResult.Errors.Count;
                    model.UpdateSuccess = false;
                    model.Errors = new List<ValidationFailure>();
                    List<string> sections = new List<string>();
                    
                    for (int index = 0; index < validationResult.Errors.Count; index++)
                    {
                        ValidationFailure failure = validationResult.Errors[index];
                        //we need to know which sections have any errors so on the frontend we can mark them as red
                        GetSectionName(failure.PropertyName, sections);

                        model.Errors.Add(failure);
                    }
                    model.SectionsWithErrors = sections;
                }

                return model;

            }

            //Updates section

            //Update Applicant
            _applicantRepository.UpdateApplicant(model.Applicant);


            //Update Application

            _applicationRepository.UpdateApplication(model.Application);

            //Update Employment

            _employmentRepository.UpdateEmployment(model.Employment);


            //Update Address

            _addressRepository.UpdateAddress(model.Address);

            //Update Contacts

            List<Contact> contacts = new List<Contact>();
            
            contacts.Add(model.HomePhone);
            contacts.Add(model.EmailAddress);
            contacts.Add(model.MobilePhone);

            _contactRepository.UpdateContacts(contacts, model.Applicant.ApplicantId, model.Application.ApplicationId);

            //Update Bank Account

            _bankAccountRepository.UpdateBankAccount(model.BankAccount);

            //Update Bank Card

            _bankCardRepository.UpdateBankCard(model.BankCard);


            return model;

        }

        private void GetSectionName(string propertyName, List<string> sectionsAlreadySet)
        {
            string[] strings = propertyName.Split('.');


            if(strings[0] == "Applicant")
            {


                bool anyNameSection = sectionsAlreadySet.Any(a => a == FormSections.Name);
                bool anyPersonalInfo = sectionsAlreadySet.Any(a => a == FormSections.PersonalInfo);
                switch (strings[1])
                {
                    case "FirstName":
                    case "LastName":
                    case "MiddleNames":
                        if (!anyNameSection)
                            sectionsAlreadySet.Add(FormSections.Name);
                        break;

                    default:
                        if (!anyPersonalInfo)
                            sectionsAlreadySet.Add(FormSections.PersonalInfo);
                        break;
                }
            }

            bool anyLoanInfoSection = sectionsAlreadySet.Any(a => a == FormSections.LoanInfo);

            if (strings[0] == "Application" && !anyLoanInfoSection)
            {
                sectionsAlreadySet.Add(FormSections.LoanInfo);
            }

            bool anyEmployment = sectionsAlreadySet.Any(a => a == FormSections.Employment);

            if (strings[0] == "Employment" && !anyEmployment)
            {
                sectionsAlreadySet.Add(FormSections.Employment);
            }

            bool anyAddress = sectionsAlreadySet.Any(a => a == FormSections.Address);

            if (strings[0] == "Address" && !anyAddress)
            {
                sectionsAlreadySet.Add(FormSections.Address);
            }

            bool anyBankAccount = sectionsAlreadySet.Any(a => a == FormSections.BankAccount);

            if (strings[0] == "BankAccount" && !anyBankAccount)
            {
                sectionsAlreadySet.Add(FormSections.BankAccount);
            }

            bool anyBankCard = sectionsAlreadySet.Any(a => a == FormSections.BankCard);

            if (strings[0] == "BankCard" && !anyBankCard)
            {
                sectionsAlreadySet.Add(FormSections.BankCard);
            }

            bool anyContact = sectionsAlreadySet.Any(a => a == FormSections.Contacts);


            if ((strings[0] == "HomePhone" || strings[0] == "MobilePhone" || strings[0] == "EmailAddress") && !anyContact)
            {
                sectionsAlreadySet.Add(FormSections.Contacts);

            }

        }

        public AppsForListModel GetApplications(ApplicationStatuses applicationStatus)
        {
            AppsForListModel model = new AppsForListModel();
           
            model.ApplicationsRows =  _applicationRepository.GetApplicationsByStatus(applicationStatus);
            model.ApplicationsCount = model.ApplicationsRows.Count;
           
            return model;
        }

        public void UpdateApplicationStatus(ApplicationStatuses status, int applicationId, int reasonId, Guid userId, DateTime dateAdded )
        {
            _applicationRepository.UpdateApplicationStatus(status, applicationId);
            _reasonRepository.AddReasonLog(status, applicationId, reasonId, userId, dateAdded);
            _actionLogRepository.LogAction(status.ToString(), applicationId, userId, dateAdded);

        }

        public UndecidedAppsModel GetUndecidedApplications(ApplicationStatuses applicationStatus)
        {
            UndecidedAppsModel model = new UndecidedAppsModel();

            model.UndecidedApplicationsRows = _applicationRepository.GetUndecidedApplications(applicationStatus);
            model.ApplicationsCount = model.UndecidedApplicationsRows.Count;

            return model;
        }
    }
}
