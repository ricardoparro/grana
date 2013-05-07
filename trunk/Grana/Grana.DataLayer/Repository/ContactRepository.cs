using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.EnumModel;

namespace Grana.DataLayer.Repository
{
    public class ContactRepository : BaseRepository, IContactRepository 
    {
        public void UpdateContacts(List<Contact> contacts, int applicantId, int applicationId)
        {
            List<Contact> applicantOriginalContacts = GetApplicantContacts(applicantId, applicationId);

            Contact originalHomePhone = applicantOriginalContacts.Where(a => a.ContactTypeId == (int)ContactsOfType.HomePhone).FirstOrDefault();

            Contact homePhone = contacts.Where(a => a.ContactTypeId == (int)ContactsOfType.HomePhone).FirstOrDefault();

            if (originalHomePhone != null && homePhone != null)
                originalHomePhone.ContactDetail = homePhone.ContactDetail;

            Contact originalEmail = applicantOriginalContacts.Where(a => a.ContactTypeId == (int)ContactsOfType.EmailAddress).FirstOrDefault();

            Contact email = contacts.Where(a => a.ContactTypeId == (int)ContactsOfType.EmailAddress).FirstOrDefault();
            
            if (originalEmail != null && email != null)
                originalEmail.ContactDetail = email.ContactDetail;

            Contact originalMobile = applicantOriginalContacts.Where(a => a.ContactTypeId == (int)ContactsOfType.MobilePhone).FirstOrDefault();

            Contact mobile = contacts.Where(a => a.ContactTypeId == (int)ContactsOfType.MobilePhone).FirstOrDefault();

            if (originalMobile != null && mobile != null)
                originalMobile.ContactDetail = mobile.ContactDetail;

            context.SubmitChanges();
        }

        public List<Contact> GetApplicantContacts(int applicantId, int applicationId)
        {
            List<Contact> contacts = (from contact in context.Contacts
                             where contact.ApplicantId == applicantId && contact.ApplicationId == applicationId
                             select contact).ToList();

            return contacts;
        }
    }
}
