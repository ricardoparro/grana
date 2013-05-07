using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.Model;
using Grana.Model.ViewModel.Notes;

namespace Grana.DataLayer.Interfaces
{
    public interface INoteRepository
    {
        void AddNote(int applicationId, int applicantId, string noteText, Guid providerUserKey, DateTime dateTime);
        List<NotesRow> GetAllNotesByApplicationId(int applicationId);
    }
}
