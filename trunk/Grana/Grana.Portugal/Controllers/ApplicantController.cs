using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grana.Model;
using Grana.Model.ViewModel.Applicant;
using Grana.Service.Portugal;

namespace Grana.Portugal.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly ApplicantService _applicantService;

        public ApplicantController(ApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ApplicantSearch model = new ApplicantSearch();
            model.Applicants = new List<Applicant>();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ApplicantSearch model)
        {
            model.Applicants= _applicantService.SearchApplicant(model);

            return View(model);
        }

        public ActionResult ApplicantDetails(int applicantId)
        {
            ApplicantDetailsModel model = _applicantService.GetApplicantDetails(applicantId);

            return View(model);
        }
    }
}
