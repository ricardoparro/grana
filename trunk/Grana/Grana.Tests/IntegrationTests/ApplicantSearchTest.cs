using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;
using Grana.Model.EnumModel;
using Grana.Model.ViewModel.Applicant;
using Grana.Service.Portugal;
using Grana.Tests.Helpers;
using Grana.Tests.Helpers.Factories;
using NUnit.Framework;

namespace Grana.Tests.IntegrationTests
{
    public class ApplicantSearchTest: BaseTestWithTestClass<ApplicantService>
    {
        private List<Applicant> _lisApplicantsToDelete;

        [TestFixtureSetUp]
        public void Setup()
        {
        }
        [SetUp]
        public void CalledBeforeEachTest()
        {
            _lisApplicantsToDelete = new List<Applicant>();
        }

        [TearDown]
        public void TearDown()
        {
            int[] applicantIds = _lisApplicantsToDelete.Select(a => a.ApplicantId).ToArray();
            DataBaseDeleteApplicant.DeleteApplicants(applicantIds, MockContainer);
        }

        [Test]
        public void SearchForPostcode()
        {
             //get Applicant
            Applicant applicant = ApplicantFactory.GetApplicant();

          
            applicant.Addresses.Clear();         
            ApplicantFactory.GetHomeAddress(applicant, "22270-009");
            Applicant inserApplicant = DataBaseInsertApplicant.InsertApplicant(MockContainer, applicant);
       
            _lisApplicantsToDelete.Add(inserApplicant);

            ApplicantSearch applicantSearch = new ApplicantSearch();
            applicantSearch.Postcode = "22270-009";
            List<Applicant> searchApplicant;

            using (BeginScope())
            {
                searchApplicant = ClassToTest.SearchApplicant(applicantSearch);


                Applicant app = searchApplicant.FirstOrDefault();

                Assert.IsNotNull(app);
                Assert.AreEqual(1, searchApplicant.Count);
                Assert.AreEqual(inserApplicant.ApplicantId, app.ApplicantId);
                Assert.AreEqual(inserApplicant.Addresses.First().Postcode, app.Addresses.First().Postcode);
            }
        }

        [Test]
        public void SearchForFirstName()
        {
            Applicant applicant = ApplicantFactory.GetApplicant();

            applicant.FirstName = "John";

            Applicant applicantFirstName = DataBaseInsertApplicant.InsertApplicant(MockContainer, applicant);

            _lisApplicantsToDelete.Add(applicantFirstName);

            ApplicantSearch applicantSearch = new ApplicantSearch();
            applicantSearch.Firstname = "John";

            List<Applicant> searchApplicants = new List<Applicant>();

            using (BeginScope())
            {
                searchApplicants = ClassToTest.SearchApplicant(applicantSearch);

                Applicant app = searchApplicants.FirstOrDefault();

                Assert.IsNotNull(app);
                Assert.AreEqual(1, searchApplicants.Count);
                Assert.AreEqual(applicantFirstName.ApplicantId, app.ApplicantId);
                Assert.AreEqual(applicantFirstName.FirstName, app.FirstName);
            }
        }

        [Test]
        public void SearchForLastName()
        {
            Applicant applicant = ApplicantFactory.GetApplicant();

            applicant.LastName = "Lynch";

            Applicant applicantLastName = DataBaseInsertApplicant.InsertApplicant(MockContainer, applicant);

            _lisApplicantsToDelete.Add(applicantLastName);

            ApplicantSearch applicantSearch = new ApplicantSearch();
            applicantSearch.Lastname = "Lynch";

            List<Applicant> searchApplicants = new List<Applicant>();

            using (BeginScope())
            {
                searchApplicants = ClassToTest.SearchApplicant(applicantSearch);

                Applicant app = searchApplicants.FirstOrDefault();

                Assert.IsNotNull(app);
                Assert.AreEqual(1, searchApplicants.Count);
                Assert.AreEqual(applicantLastName.ApplicantId, app.ApplicantId);
                Assert.AreEqual(applicantLastName.LastName, app.LastName);
            }
        }
        [Test]
        public void SearchForMobile()
        {
            Applicant applicant = ApplicantFactory.GetApplicant();

            applicant.Contacts.Clear();

            Applicant applicantMobile = DataBaseInsertApplicant.InsertApplicant(MockContainer, applicant);

            _lisApplicantsToDelete.Add(applicantMobile);

            Application application = ApplicationFactory.GetApplication(ApplicationStatuses.New.ToString(), DateTime.Now, applicantMobile.ApplicantId);

            DataBaseInsertApplications.InsertApplication(MockContainer,application);

            Contact mobile = ApplicantFactory.GetMobile(application.ApplicationId, applicant.ApplicantId, "(61) 9999-1229");

            DataBaseInsertApplications.InsertContact(MockContainer, mobile);

            ApplicantSearch applicantSearch = new ApplicantSearch();
            applicantSearch.Mobile = "(61) 9999-1229";

            List<Applicant> searchApplicants = new List<Applicant>();

            using (BeginScope())
            {
                searchApplicants = ClassToTest.SearchApplicant(applicantSearch);

                Applicant app = searchApplicants.FirstOrDefault();

                Assert.IsNotNull(app);
                Assert.AreEqual(1, searchApplicants.Count);
                Assert.AreEqual(applicantMobile.ApplicantId, app.ApplicantId);
                Assert.AreEqual(mobile.ContactDetail, app.Contacts.First().ContactDetail);
            }
        }
    }
}
