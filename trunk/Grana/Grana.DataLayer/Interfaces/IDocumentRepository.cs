using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;
using Grana.Model.ViewModel.Documents;

namespace Grana.DataLayer.Interfaces
{
    public interface IDocumentRepository
    {
        void AddDocumentEntry(string directory, string filename, int applicationId, int applicantId, DateTime dateAdded, Guid userId);
        List<DocumentRow> GetDocumentsByApplicationId(int applicationId, string baseDirectory);
    }
}
