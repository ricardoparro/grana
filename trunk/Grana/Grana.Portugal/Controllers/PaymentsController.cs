using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grana.Model.EnumModel;
using Grana.Model.ViewModel.Application;
using Grana.Service.Portugal;

namespace Grana.Portugal.Controllers
{
    [Authorize(Roles = "Underwriter, Administrator")]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDetailsService _applicationDetailsService;

        public PaymentsController(ApplicationDetailsService applicationDetailsService)
        {
            _applicationDetailsService = applicationDetailsService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PaymentsToBeProcessed()   
        {
             AppsForListModel model = _applicationDetailsService.GetApplications(ApplicationStatuses.Approved);

            return View(model);
        }

    }
}
