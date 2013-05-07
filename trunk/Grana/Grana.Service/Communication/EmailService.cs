using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Grana.Service.Mailers;
using Mvc.Mailer;

namespace Grana.Service.Communication
{
    public class EmailService
    {
        public EmailService()
        {
            
        }

        public MailMessage SendEmail(string from, string to , string message, string subject, string firstName)
        {
            var mailer = new UserMailer();
            MailMessage msg = mailer.ContactCustomer(subject, from, message, to, firstName);
            msg.Send();

            return msg;
        }
    }
}
