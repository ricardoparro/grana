using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Grana.Model;
using Grana.Model.EnumModel;
using Grana.Model.ViewModel.Application;
using Grana.Model.ViewModel.ApplicationDetails;
using Grana.Model.ViewModel.Notes;
using Grana.Service.Communication;
using Grana.Service.Mailers;
using Grana.Service.Portugal;
using Autofac;

namespace Grana.Portugal.Controllers
{
    [Authorize(Roles = "Underwriter, Administrator")]
    public class ApplicationController : BaseController
    {
        private readonly ApplicationDetailsService _applicationDetailsService;
        private readonly EmailService _emailService;
        private readonly NoteService _noteService;

        #region ViewPaths

        internal struct PARTIAL_PATHS
        {
            public const string NOTE_LINE_ITEM = "_NoteItem";
        }

        public struct VIEWDATA_HTML_KEYS
        {
            public const string Notes = "NotesHTML";
        }

        #endregion
        public ApplicationController(ApplicationDetailsService applicationDetailsService, EmailService emailService, NoteService noteService)
        {
            _applicationDetailsService = applicationDetailsService;
            _emailService = emailService;
            _noteService = noteService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            AppsForListModel model = _applicationDetailsService.GetApplications(ApplicationStatuses.New);
            return View(model);
        }

        [HttpGet]
        public ActionResult Undecided()
        {
            UndecidedAppsModel model = _applicationDetailsService.GetUndecidedApplications(ApplicationStatuses.Undecided);
            return View(model);
        }

        [HttpGet]
        public ActionResult Declined()
        {
            AppsForListModel model = _applicationDetailsService.GetApplications(ApplicationStatuses.Declined);
            return View(model);
        }

        [HttpGet]
        public ActionResult ApplicationDetails(int applicationId)
        {
            string baseDirectory = ConfigurationManager.AppSettings["DocumentBaseDirectory"];

            ApplicationDetails applicationDetails = _applicationDetailsService.GetApplicationDetails(applicationId, baseDirectory);

            return View(applicationDetails);
        }

        [HttpPost]
        public ActionResult ApplicationDetails(ApplicationDetails applicationDetails)
        {
            ApplicationDetails model = _applicationDetailsService.UpdateApplicationDetails(applicationDetails);

            return Json(new{Success = model.UpdateSuccess, ApplicationDetails = model});
        }
        [HttpPost]
        public ActionResult UpdateApplicationStatus(ApplicationStatuses applicationStatus, int applicationId, int reasonId)
        {

            Guid userId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            
            _applicationDetailsService.UpdateApplicationStatus(applicationStatus, applicationId, reasonId, userId, DateTime.Now);

            return JavaScript("window.top.location.href ='" + Url.Action("Index") + "';");
        }

        [HttpPost]
        public ActionResult SendSms(int applicationId, string message)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);

            string errorMessage = string.Empty;

           

            if (string.IsNullOrEmpty(message))
            {
                errorMessage = "Por favor preencha o campo Mensagem";
            }


            if (user != null && string.IsNullOrEmpty(errorMessage))
            {

               //sms Service

                return Json(new { Success = true });
            }


            return Json(new { Success = false, ErrorMessage = errorMessage });
        }
        [HttpPost]
        public ActionResult ContactCustomer(string subject, string message, string firstName)
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);

            string errorMessage = string.Empty;
            
            if(string.IsNullOrEmpty(subject))
            {
                errorMessage = "Por favor preencha o campo Assunto";
            }

            if(string.IsNullOrEmpty(message))
            {
                errorMessage = "Por favor preencha o campo Mensagem";
            }

            if(string.IsNullOrEmpty(firstName))
            {
                errorMessage = "Por favor preencha o nome";
            }


            if (user != null && string.IsNullOrEmpty(errorMessage))
            {

                _emailService.SendEmail(user.Email, "sdas@gmail.com", message, subject, firstName);

                return Json(new {Success = true});
            }

            return Json(new { Success = false, ErrorMessage = errorMessage});


        }
        #region Notes
        
        [HttpPost]
        public ActionResult AddNote(int applicationId, int applicantId, string noteText)
        {
            if(string.IsNullOrEmpty(noteText))
            {
                string errorMessage = "Por favor introduza o texto";
                return Json(new {Success = false, ErrorMessage = errorMessage});
            }

            Guid providerUserKey = (Guid) Membership.GetUser(User.Identity.Name).ProviderUserKey;

            NotesRow notesRow = _noteService.AddNote(applicationId, applicantId, noteText, providerUserKey, DateTime.Now, User.Identity.Name);

            if (notesRow != null)
            {
                string notesHtml = RenderPartialViewToHTMLForObj(notesRow, PARTIAL_PATHS.NOTE_LINE_ITEM);
                string formattedHtml = notesHtml.Replace("\n", "").Replace("\r", "");
                return Json(new { Success = true, Html = formattedHtml  });
            }
            else
            {
                return Json(new { Success = false });
            }

           
        }

        [HttpPost]
        public ActionResult ListNotes(int applicationId)
        {
            IEnumerable<NotesRow> notes = _noteService.GetNotes(applicationId);

            List<string> listNotes = new List<string>();

            foreach (NotesRow note in notes)
            {
                listNotes.Add(note.Text + " " + note.DateAdded + " " + note.UserName);
            }

            return Json(new { Success = true, Notes = listNotes });
        }
        #endregion


    }
}
