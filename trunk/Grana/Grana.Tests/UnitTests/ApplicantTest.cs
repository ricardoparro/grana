using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.EnumModel;
using Grana.Model.ViewModel.Applicant;
using Grana.Service.Portugal;
using Moq;
using NUnit.Framework;

namespace Grana.Tests.UnitTests
{
    [TestFixture]
    public class ApplicantTest : UnitTestBase<ApplicantService>
    {

        private Mock<IApplicantRepository> _applicantRepository;

        [Test]
        public void SearchApplicantByFirstNameTest()
        {
            
            List<Applicant> applicants = new List<Applicant>();
            BankCard cardApplicant1 = new BankCard{CardHolder = "John Malkovich", CardNumber = "876877868", ApplicantId = 1};
            BankAccount bankAccount = new BankAccount{AccountHolder = "John Malkovich", AccountNumber = "787678767", ApplicantId = 1, BankAgencyCode = "001"};
            Contact email = new Contact{ApplicantId = 1, ContactDetail = "john.malk@gmail.com", ContactTypeId = (int)ContactsOfType.EmailAddress};
            Contact mobile = new Contact{ApplicantId = 1, ContactDetail = "079999999", ContactTypeId = (int)ContactsOfType.MobilePhone};
            Applicant applicant1 = new Applicant {FirstName = "John", LastName = "Malkovich", ApplicantId = 1};
            applicant1.BankCards = new EntitySet<BankCard>();
            applicant1.BankAccounts = new EntitySet<BankAccount>();
            applicant1.Contacts = new EntitySet<Contact>();
            applicant1.BankCards.Add(cardApplicant1);
            applicant1.BankAccounts.Add(bankAccount);
            applicant1.Contacts.Add(mobile);
            applicant1.Contacts.Add(email);
            applicants.Add(applicant1);

            ApplicantSearch searchObj = new ApplicantSearch();

            _applicantRepository = new Mock<IApplicantRepository>();
            InjectMock(_applicantRepository);

            _applicantRepository.Setup(a => a.SearchApplicant(searchObj)).Returns(applicants);

            searchObj.Applicants = ClassToTest.SearchApplicant(searchObj);

           _applicantRepository.Verify(a => a.SearchApplicant(searchObj));

            Assert.AreEqual(1, searchObj.Applicants.Count);
            Assert.AreEqual(applicant1.FirstName, searchObj.Applicants.First().FirstName);

            Assert.AreEqual(applicant1.LastName, searchObj.Applicants.First().LastName);
            Assert.AreEqual(applicant1.BankCards.First().CardNumber, searchObj.Applicants.First().BankCards.First().CardNumber);

        }

    }
}
