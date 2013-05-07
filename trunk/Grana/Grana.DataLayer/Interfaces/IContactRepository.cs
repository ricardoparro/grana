using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;

namespace Grana.DataLayer.Interfaces
{
    public interface IContactRepository
    {
        void UpdateContacts(List<Contact> contacts, int applicantId, int applicationId);
        List<Contact> GetApplicantContacts(int applicantId, int applicationId);

    }
}
