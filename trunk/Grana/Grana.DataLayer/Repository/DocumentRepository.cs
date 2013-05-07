using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.ViewModel.Documents;

namespace Grana.DataLayer.Repository
{
    public class DocumentRepository : BaseRepository, IDocumentRepository 
    {
        public void AddDocumentEntry(string directory, string filename, int applicationId, int applicantId, DateTime dateAdded, Guid userId)
        {
            Document document = new Document();

            document.ApplicantId = applicantId;
            document.ApplicationId = applicationId;
            document.DateAdded = dateAdded;
            document.DocumentName = filename;
            document.DocumentPath = directory;
            document.UserAdderId = userId;

            context.Documents.InsertOnSubmit(document);
            context.SubmitChanges();
        }


        public List<DocumentRow> GetDocumentsByApplicationId(int applicationId, string baseDirectory)
        {
            List<DocumentRow> documents = (from document in context.Documents
                                           join aspnetUser in context.aspnet_Users on document.UserAdderId equals aspnetUser.UserId
                              where document.ApplicationId == applicationId
                              select new DocumentRow
                                         {
                                             FileName = document.DocumentName,
                                             Path = "documents/" + document.ApplicantId + "/" + applicationId +"/" +  document.DocumentName ,
                                             TimeAdded = document.DateAdded,
                                             Username = aspnetUser.LoweredUserName
                                         }).ToList();

            return documents;
        }
    }
}
