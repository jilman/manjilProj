using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace manjilProj.Areas.Areas.Controllers
{
    public class BaseController : Controller
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // base.OnActionExecuting(context);

            if (filterContext.HttpContext.Request.Headers != null && filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //AjaxRequest-true
                ViewBag.Layout = null;
            }
            else
            {
                //AjaxRequest-false
                ViewBag.Layout = "~/Views/Shared/_LayoutAdmin.cshtml";
            }
        }

    }
}