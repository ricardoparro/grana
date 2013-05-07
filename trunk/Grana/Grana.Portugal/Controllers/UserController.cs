using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Grana.Model.ViewModel;

namespace Grana.Portugal.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles="Administrator")]
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult CreateUser(CreateUserData data)
        {
            MembershipCreateStatus status;
            Membership.CreateUser(data.UserID, data.Password, data.Email, data.Question, data.Answer, true, out status);

            if (status == MembershipCreateStatus.Success)
            {
                ViewBag.StatusMessage = "O utilizador foi criado com sucesso!";
            }
            else
            {
                ViewBag.StatusMessage = "Erro: " + status;
            }

            //keep form data

            ViewBag.UserID = data.UserID;
            ViewBag.Question = data.Question;
            ViewBag.Password = "";
            ViewBag.Answer = data.Answer;
            ViewBag.Email = data.Email;
            ViewBag.FirstName = data.FirstName;
            ViewBag.LastName = data.LastName;


            return View();
        }
       
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginData data)
        {
            //user forms authentication

            if (Membership.ValidateUser(data.UserID, data.Password))
            {
                bool flag = (data.RememberMe == "on" ? true : false);

                FormsAuthentication.SetAuthCookie(data.UserID, flag);
                //redirects to the return url specified
                string redirectTo = Request["ReturnUrl"];
                if (!string.IsNullOrEmpty(redirectTo))
                {
                    FormsAuthentication.RedirectFromLoginPage(data.UserID, true);
                    return View();                
                }
                return RedirectToAction("Index", "Welcome", null);
            }
            else
            {
                ViewBag.ErrorMessage = "A senha ou o username não estão corretos";
                return View();
            }
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }
}
