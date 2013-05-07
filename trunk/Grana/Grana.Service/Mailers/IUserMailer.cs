using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mvc.Mailer;
using System.Net.Mail;

namespace Grana.Service.Mailers
{ 
    public interface IUserMailer
    {
				
		MailMessage ContactCustomer();
		
		
	}
}