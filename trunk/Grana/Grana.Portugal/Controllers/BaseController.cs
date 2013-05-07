using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grana.Portugal.Controllers
{
    public class BaseController : Controller
    {


        #region Render HTML Methods

        protected string RenderPartialViewToHTMLForObj(object model, string viewName)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            //To make it render a View instead of partial view just change the below line
            ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);

            return RenderPartialViewToHTMLForObj(viewResult, model);
        }

        protected string RenderPartialViewToHTMLForList<SubT>(List<SubT> list, string lineItemPartialPath)
        {
            ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, lineItemPartialPath);

            string html = string.Empty;

            foreach (SubT childObj in list)
            {
                html += RenderPartialViewToHTMLForObj(viewResult, childObj);
            }

            return html;
        }

        #region Helpers

        private string RenderPartialViewToHTMLForObj(ViewEngineResult viewResult, object model)
        {
            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString().Trim();
            }
        }

        #endregion

        #endregion

        #region Store HTML Methods

        protected void StoreHTMLInViewDataForObject<SubT>(SubT obj, string key, string lineItemPartialPath)
        {
            string html = RenderPartialViewToHTMLForObj(obj, lineItemPartialPath);
            ViewData[key] = html;
        }

        protected void StoreHTMLInViewDataForList<SubT>(List<SubT> list, string key, string lineItemPartialPath)
        {
            string html = RenderPartialViewToHTMLForList(list, lineItemPartialPath);
            ViewData[key] = html;
        }

        protected void StoreHTMLInViewDataForSubPropertyList<BaseT, SubT>(List<BaseT> list, Func<BaseT, List<SubT>> getChildProperty, Func<BaseT, string> getIdForList, string lineItemPartialPath)
        {
            ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, lineItemPartialPath);

            foreach (BaseT baseObj in list)
            {
                List<SubT> childList = getChildProperty(baseObj);
                string id = getIdForList(baseObj);

                string html = string.Empty;

                foreach (SubT childObj in childList)
                {
                    html += RenderPartialViewToHTMLForObj(viewResult, childObj);
                }

                ViewData[id] = html;
            }
        }

        #endregion


    }
}
