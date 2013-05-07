using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grana.DataLayer.Interfaces;
using Grana.Model;
using Grana.Model.ViewModel.Notes;

namespace Grana.DataLayer.Repository
{
    public class NoteRepository : BaseRepository, INoteRepository
    {
        public void AddNote(int applicationId, int applicantId, string noteText, Guid providerUserKey, DateTime dateTime)
        {
            Note note = new Note() { ApplicantId = applicantId, ApplicationId = applicationId, DateAdded = dateTime, Text = noteText, UserAdderId = providerUserKey };

            context.Notes.InsertOnSubmit(note);
            context.SubmitChanges();
        }

        public List<NotesRow> GetAllNotesByApplicationId(int applicationId)
        {
            List<NotesRow> notes = (from note in context.Notes
                                    join aspnetUser in context.aspnet_Users on note.UserAdderId equals aspnetUser.UserId
            
                          where note.ApplicationId == applicationId
                          select new NotesRow()
                                     {
                                         Text = note.Text,
                                         UserName = aspnetUser.LoweredUserName,
                                         DateAdded = note.DateAdded
                                     }).ToList();
            return notes;
        }
    }
}
