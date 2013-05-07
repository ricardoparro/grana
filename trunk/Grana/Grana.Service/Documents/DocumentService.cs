using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.ViewModel.Documents;

namespace Grana.Service.Documents
{
    public class DocumentService: BaseService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }


        public DocumentRow StoreDocument(HttpPostedFileBase file, string documentName, int applicationId, int applicantId, string directory, DateTime dateAdded, Guid userId)
        {
            DocumentRow documentRow = new DocumentRow();

            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileExtension = Path.GetExtension(file.FileName);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);


                // store the file inside documents folder
                var path = Path.Combine(directory, documentName);

                bool exists = File.Exists(path);

                if(!exists)
                    file.SaveAs(path + fileExtension);
                else
                {
                    documentRow.Error = "Já existe um ficheiro com o mesmo nome " + documentName;
                    return documentRow;
                }

                documentRow.FileName = documentName + fileExtension;
                documentRow.Path =  "/documents/" + applicantId.ToString() + "/" + applicationId.ToString() + "/" + documentRow.FileName;
                documentRow.TimeAdded = dateAdded;
                
                //Record the document entry in the database
                _documentRepository.AddDocumentEntry(directory, documentName + fileExtension , applicationId, applicantId, dateAdded, userId);

            }
            return documentRow;
        }

       
    }
}
