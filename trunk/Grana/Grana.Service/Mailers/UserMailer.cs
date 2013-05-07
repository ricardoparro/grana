using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;
using System.Net.Mail;

namespace Grana.Service.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer     
	{
		public UserMailer(): base()
		{
			MasterName="_Layout";
		}

		
		public virtual MailMessage ContactCustomer()
		{
			var mailMessage = new MailMessage{Subject = "ContactCustomer"};
			
			PopulateBody(mailMessage, viewName: "ContactCustomer");

			return mailMessage;
		}

        public virtual MailMessage ContactCustomer(string subject, string from, string message, string email, string firstName)
        {
            var mailMessage = new MailMessage { Subject = subject};

            ViewBag.message = message;
            ViewBag.firstname = firstName;
            mailMessage.To.Add(email);
            PopulateBody(mailMessage, viewName: "ContactCustomer");

            return mailMessage;
        }

		
	}
}