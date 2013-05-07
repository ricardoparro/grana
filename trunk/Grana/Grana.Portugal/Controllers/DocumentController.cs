using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Grana.Model;
using Grana.Model.ViewModel.Documents;
using Grana.Service.Documents;

namespace Grana.Portugal.Controllers
{
    public class DocumentController : BaseController
    {
        private readonly DocumentService _documentService;

        #region ViewPaths

        internal struct PARTIAL_PATHS
        {
            public const string DOCUMENT_LINE_ITEM = "_DocumentItem";
        }

        public struct VIEWDATA_HTML_KEYS
        {
            public const string Documents = "DocumentHTML";
        }

        #endregion

        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        public ActionResult StoreDocument(string documentName, int applicationId, int applicantId)
        {
            string error = string.Empty;
            string json = string.Empty;


            if (string.IsNullOrEmpty(documentName))
            {

                error = "Por favor introduza um nome para o ficheiro";
                json = string.Format(@"success: false, errorMsg:'{0}'", error);
            }

            else
            {
                HttpPostedFileBase file = Request.Files[0];

                string baseDirectory = ConfigurationManager.AppSettings["DocumentBaseDirectory"];

                string directory = Server.MapPath(baseDirectory + applicantId.ToString() + "/" + applicationId.ToString());

                Guid providerUserKey = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;

                DocumentRow document = _documentService.StoreDocument(file, documentName, applicationId, applicantId, directory, DateTime.Now, providerUserKey);

                string formattedHtml = null;

                if (document != null)
                {
                    string documentsHtml = RenderPartialViewToHTMLForObj(document, PARTIAL_PATHS.DOCUMENT_LINE_ITEM);
                    formattedHtml = documentsHtml.Replace("\n", "").Replace("\r", "");
                }

                if (string.IsNullOrEmpty(document.Error))
                    // redirect back to the index action to show the form once again
                    json= string.Format(@"success: true, html:'{0}'", formattedHtml); //return Json(new { Success = true, Html = formattedHtml });
                else
                {
                    json = string.Format(@"success: false, errorMsg:'{0}'", error);
                }
            }

            /* This must be like this as this is the expected format by a external file upload JS
                 * It doesn't like the Json result type */
            string stringToReturn = "{" + json + "}";

            return Content(stringToReturn);
        }
      
    }

    }

