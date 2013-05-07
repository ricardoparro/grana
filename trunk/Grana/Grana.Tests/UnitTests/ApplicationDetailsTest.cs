using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.EnumModel;
using Grana.Model.ViewModel.ApplicationDetails;
using Grana.Service.Portugal;
using Grana.Service.Validation;
using Moq;
using NUnit.Framework;

namespace Grana.Tests.UnitTests
{
    [TestFixture]
    public class ApplicationDetailsTest : UnitTestBase<ApplicationDetailsService>
    {
        private  Mock<IApplicationRepository> _applicationRepository;
        private Mock<IApplicantRepository> _applicantRepository;
        private Mock<IAddressRepository> _addressRepository;
        private Mock<IContactRepository> _contactRepository;
        private Mock<IBankAccountRepository> _bankAccountRepository;
        private Mock<IBankCardRepository> _bankCardRepository;
        private Mock<IBankAgencyRepository> _bankAgencyRepository;
        private Mock<IEmploymentRepository> _employmentRepository;

        [SetUp]
        public void Setup()
        {
            _applicationRepository = new Mock<IApplicationRepository>();
            _applicationRepository.Setup(a => a.UpdateApplication(It.IsAny<Application>()));

            InjectMock(_applicationRepository);

            _applicantRepository = new Mock<IApplicantRepository>();
            _applicantRepository.Setup(a => a.UpdateApplicant(It.IsAny<Applicant>()));
            InjectMock(_applicantRepository);

            _addressRepository = new Mock<IAddressRepository>();
            _addressRepository.Setup(a => a.UpdateAddress(It.IsAny<Address>()));
            InjectMock(_addressRepository);

            _contactRepository = new Mock<IContactRepository>();
            _contactRepository.Setup(a => a.UpdateContacts(It.IsAny<List<Contact>>(), It.IsAny<int>(),It.IsAny<int>()));
            InjectMock(_contactRepository);

            _bankAccountRepository = new Mock<IBankAccountRepository>();
            _bankAccountRepository.Setup(a => a.UpdateBankAccount(It.IsAny<BankAccount>()));
            InjectMock(_bankAccountRepository);

            _bankCardRepository = new Mock<IBankCardRepository>();
            _bankCardRepository.Setup(a => a.UpdateBankCard(It.IsAny<BankCard>()));
            InjectMock(_bankCardRepository);

            _employmentRepository = new Mock<IEmploymentRepository>();
            _employmentRepository.Setup(a => a.UpdateEmployment(It.IsAny<Employment>()));
            InjectMock(_employmentRepository);

            List<BankAgency> bankAgencies = new List<BankAgency>();
            BankAgency agency = new BankAgency();
            agency.AgencyCode = 2;
            agency.AgencyName = "dsf";
            agency.BankAgencyId = 1;
            bankAgencies.Add(agency);
            _bankAgencyRepository = new Mock<IBankAgencyRepository>();
            _bankAgencyRepository.Setup(a => a.GetAgenciesByCode(It.IsAny<float>())).Returns(bankAgencies);
            InjectMock(_bankAgencyRepository);

        }

        [Test]
        public void ApplicationDetailsShouldBeUpdatedCorrectly()
        {
            var applicationDetails = GetApplicationDetails();

            ApplicationDetails updateApplicationDetails = ClassToTest.UpdateApplicationDetails(applicationDetails);


            Assert.IsTrue(updateApplicationDetails.UpdateSuccess);
        }

        private ApplicationDetails GetApplicationDetails()
        {
            ApplicationDetails applicationDetails = new ApplicationDetails();

            Applicant applicant = new Applicant();
            applicant.BirthDate = DateTime.Now.AddYears(-50);
            applicant.FirstName = "Alexandre";
            applicant.Gender = "Male";
            applicant.HomeStatus = true;
            applicant.IdentityNumber = "91.122.534-1";
            applicant.LastName = "Silva";
            applicant.MaritalStatus = "Married";
            applicant.MiddleNames = "Smith";
            applicant.NumberOfDependants = 2;
            applicant.OwnCar = false;
            applicant.SocialSecurityNumber = "000.000.003-53";

            applicationDetails.Applicant = applicant;

            Application application = new Application();
            application.Amount = 100;
            application.ApplicationDate = DateTime.Now.AddHours(-2);
            application.PaybackDate = DateTime.Now.AddDays(30);

            applicationDetails.Application = application;

            Employment employment = new Employment();
            employment.DirectDebit = true;
            employment.EmployersName = "Microsoft";
            employment.EmploymentStatus = "FullTime";
            employment.IncomeFrequency = "Monthly";
            employment.Industry = "Outsourcing";
            employment.MonthlyIncome = 1000;
            employment.PayDate = DateTime.Now.AddDays(15);
            employment.Position = "Manager";
            employment.TimeAtEmployer = 12;
            employment.WorkPhone = "(21) 3586-9101";

            applicationDetails.Employment = employment;

            Contact homephone = new Contact();
            homephone.ContactDetail = "(21) 3586-9122";
            homephone.ContactTypeId = (int) ContactsOfType.HomePhone;

            applicationDetails.HomePhone = homephone;

            Contact email = new Contact();
            email.ContactDetail = "alex.silva@gmail.com";
            email.ContactTypeId = (int) ContactsOfType.EmailAddress;

            applicationDetails.EmailAddress = email;

            Contact mobile = new Contact();
            mobile.ContactDetail = "(61) 9211-1229";
            mobile.ContactTypeId = (int) ContactsOfType.MobilePhone;

            applicationDetails.MobilePhone = mobile;

            Address address = new Address();
            address.Address1 = "Rua Voluntários da Pátria";
            address.City = "Rio de Janeiro";
            address.Complement = "dsf";
            address.Neighborhood = "Botafogo";
            address.Number = "12";
            address.Postcode = "22270-001";
            address.State = "RJ";

            applicationDetails.Address = address;

            BankAccount bankAccount = new BankAccount();
            bankAccount.AccountHolder = "Alexandre Silva";
            bankAccount.AccountNumber = "003655-9";
            bankAccount.BankAgencyCode = "002";
            bankAccount.BankName = "Bradesco";

            applicationDetails.BankAccount = bankAccount;

            BankCard bankCard = new BankCard();
            bankCard.CardHolder = "Alexandre Silva";
            bankCard.CardNumber = "6011 0000 0000 0004";
            bankCard.ExpiryDate = DateTime.Now.AddMonths(17);
            bankCard.SecurityCode = "234";
            bankCard.StartDate = DateTime.Now.AddYears(-3);

            applicationDetails.BankCard = bankCard;

            return applicationDetails;
        }


    }
}
