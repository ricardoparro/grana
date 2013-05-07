using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;

namespace Grana.Tests.Helpers
{
    public class DataBaseDeleteApplications
    {

        public static void DataBaseDeleteApplicationsAndDetails(int[] applicationIds, GranaDataDataContext context)
        {
            List<Application> applications = context.Applications.Where(a => applicationIds.Contains(a.ApplicationId)).ToList();

            //Delete Reasons
            List<ReasonLog> reasons = context.ReasonLogs.Where(r => applicationIds.Contains(r.ApplicationId)).ToList();
            context.ReasonLogs.DeleteAllOnSubmit(reasons);

            //Delete Notes
            List<Note> notes = context.Notes.Where(n => applicationIds.Contains(n.ApplicationId)).ToList();
            context.Notes.DeleteAllOnSubmit(notes);

            //Delete Documents
            List<Document> documents = context.Documents.Where(d => applicationIds.Contains(d.ApplicationId)).ToList();
            context.Documents.DeleteAllOnSubmit(documents);

            //Delete Contacts
            List<Contact> contacts = context.Contacts.Where(c => applicationIds.Contains(c.ApplicationId)).ToList();
            context.Contacts.DeleteAllOnSubmit(contacts);


            //Delete applications
            context.Applications.DeleteAllOnSubmit(applications);
            
            context.SubmitChanges();
        }
    }
}
