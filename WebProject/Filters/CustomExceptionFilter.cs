using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebProject.Filters
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && (filterContext.Exception is InvalidOperationException 
                                                    || filterContext.Exception is NullReferenceException
                                                    || filterContext.Exception is ArgumentNullException
                                                    || filterContext.Exception.Message.Equals("UserId not found.")))
            {
                filterContext.Result = new RedirectResult("Error.cshtml");
                filterContext.ExceptionHandled = true;
            }
        }
    }
}