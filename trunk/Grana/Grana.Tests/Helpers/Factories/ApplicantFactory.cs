using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;
using Grana.Model.EnumModel;

namespace Grana.Tests.Helpers.Factories
{
    public class ApplicantFactory
    {
        public static Applicant GetApplicant()
        {
            Applicant applicant = new Applicant();
            applicant.FirstName = "John";
            applicant.LastName = "Malkovich";
            applicant.BirthDate = DateTime.Now.AddYears(-32);
            applicant.Gender = "Male";
            applicant.HomeStatus = true;
            applicant.MaritalStatus = "Married";
            applicant.NumberOfDependants = 0;
            applicant.SocialSecurityNumber = "1234567891011121314";
            applicant.IdentityNumber = "10987654321";
            applicant.OwnCar = true;
            applicant.UserId = Guid.Parse("1594E441-6275-4015-A217-FD7FF9BDDF90");


            //BankCard cardApplicant1 = new BankCard { CardHolder = "John Malkovich", CardNumber = "876877868", ApplicantId = 1, DateAdded = DateTime.Now, UserAdderId = Guid.Parse("1594E441-6275-4015-A217-FD7FF9BDDF90") };
            //BankAccount bankAccount = new BankAccount { AccountHolder = "John Malkovich", AccountNumber = "787678767", ApplicantId = 1, BankAgencyCode = "001", DateAdded = DateTime.Now, UserAdderId = Guid.Parse("1594E441-6275-4015-A217-FD7FF9BDDF90") };
            //Contact email = new Contact { ApplicantId = 1, ContactDetail = "john.malk@gmail.com", ContactTypeId = (int)ContactsOfType.EmailAddress, DateAdded = DateTime.Now, UserAdderId = Guid.Parse("1594E441-6275-4015-A217-FD7FF9BDDF90") };
            //Contact mobile = new Contact { ApplicantId = 1, ContactDetail = "079999999", ContactTypeId = (int)ContactsOfType.MobilePhone, DateAdded = DateTime.Now, UserAdderId = Guid.Parse("1594E441-6275-4015-A217-FD7FF9BDDF90") };

            //applicant.BankCards.Add(cardApplicant1);
            //applicant.BankAccounts.Add(bankAccount);
            //applicant.Contacts.Add(email);
            //applicant.Contacts.Add(mobile);

            return applicant;

        }

        public static Address GetHomeAddress(Applicant applicant, string postcode)
        {
            Address address = new Address
            {
                Address1 = "Line 1",
                Applicant = applicant,
                Neighborhood = "My Neighborhood",
                DateAdded = DateTime.Now,
                UserAdderId = Guid.Parse("D867862C-462B-4262-A3A1-0A7B8DB2D5D1"),
                State = "Rio",
                Postcode = postcode,
                City = "My City",
                Number = "7"
                

            };

            return address;

        }

        public static Contact GetMobile(int applicationId, int applicantId, string mobile)
        {
            Contact contact = new Contact()
                                  {
                                      ContactDetail =mobile,
                                      ContactTypeId = (int)ContactsOfType.MobilePhone,
                                      DateAdded = DateTime.Now,
                                      UserAdderId = Guid.Parse("D867862C-462B-4262-A3A1-0A7B8DB2D5D1"),
                                      ApplicantId = applicantId,
                                      ApplicationId = applicationId
                                      
                                  };

            return contact;
        }
    }
}
