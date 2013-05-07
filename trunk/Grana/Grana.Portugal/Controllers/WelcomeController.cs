using System.Web.Mvc;

namespace Grana.Portugal.Controllers
{
    public class WelcomeController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }

}