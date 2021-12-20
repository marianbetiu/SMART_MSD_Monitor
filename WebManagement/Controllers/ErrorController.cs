using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace WebManagement.Controllers
{
    public class ErrorController : _BaseController
    {
        public ErrorController()
        {
            UserMustAuthenticate = false;

            DisplayName = "Error";
        }

        public ActionResult Index()
        {
            return View("Index", new WebManagement.ViewModels.Shared.ErrorViewModel { Title = "Generic Error", Description = "No details provided" });
        }

        public ActionResult HTTP404()
        {
            //Response.StatusCode = (int)HttpStatusCode.NotFound;
            //// If the url is relative ('NotFound' route) then replace with Requested path
            //model.RequestedUrl = Request.Url.OriginalString;
            //// Dont get the user stuck in a 'retry loop' by
            //// allowing the Referrer to be the same as the Request
            //model.ReferrerUrl = Request.UrlReferrer != null &&
            //    Request.UrlReferrer.OriginalString != model.RequestedUrl ?
            //    Request.UrlReferrer.OriginalString : null;

            //// TODO: insert ILogger here

            var error = RouteData.Values["error"] as HttpException;
            var description = error != null ? error.Message : string.Empty;

            return View("Index", new WebManagement.ViewModels.Shared.ErrorViewModel { Title = "HTTP 404 Error", Description = description});
        }

        public ActionResult HTTP500()
        {
            var error = RouteData.Values["error"] as HttpException;
            var description = error != null ? error.Message : string.Empty;

            return View("Index", new WebManagement.ViewModels.Shared.ErrorViewModel { Title = "HTTP 500 Error", Description = description });
        }

        public ActionResult AccessDenied()
        {
            return View("Index", new WebManagement.ViewModels.Shared.ErrorViewModel { Title = "ACCESS DENIED", Description = "User " + HttpContext.User.Identity.Name + " doesn't have the rights to access this page"});
        }
    }
}
