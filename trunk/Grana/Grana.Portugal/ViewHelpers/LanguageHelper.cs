using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grana.Portugal.ViewHelpers
{
    public class LanguageHelper
    {
        
        public static string ConvertStatusToPortuguese(string status)
        {
            string translatedStatus = string.Empty;

            switch (status)
            {
                case "New":
                    translatedStatus = "Nova Entrada";
                    break;
                case "Approved":
                    translatedStatus = "Aprovado";
                    break;
                case "Declined" :
                    translatedStatus = "Declined";
                    break;
                case "Undecided":
                    translatedStatus =  "Pendente";
                    break;
                default: translatedStatus = "";
                    break;
                    
            }

            return translatedStatus;
        }
    }
}
