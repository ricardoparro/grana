using System;
using System.Collections.Generic;
using Grana.DataLayer.Interfaces;
using Grana.Model.ViewModel.Notes;

namespace Grana.Service.Portugal
{
    public class NoteService : BaseService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public NotesRow AddNote(int applicationId, int applicantId, string noteText, Guid providerUserKey, DateTime dateTime, string userName)
        {
            _noteRepository.AddNote(applicationId, applicantId, noteText, providerUserKey, dateTime);

            NotesRow notesRow = new NotesRow {DateAdded = dateTime, Text = noteText, UserName = userName};

            return notesRow;
        }

        public List<NotesRow> GetNotes(int applicationId)
        {
            List<NotesRow> allNotesByApplicationId = _noteRepository.GetAllNotesByApplicationId(applicationId);
            
            return allNotesByApplicationId;
        }
    }
}
