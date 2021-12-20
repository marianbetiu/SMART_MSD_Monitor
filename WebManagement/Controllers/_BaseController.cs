using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebManagement.Controllers
{
    public class _BaseController : Controller
    {
        #region DisplayName

        public string DisplayName { get; set; }

        #endregion

        public enum MessageTypes
        {
            SuccessGeneric,
            SuccessAddNew,
            SuccessEdit,
            SuccessDelete,
            ErrorGeneric,
            ErrorDeleteDependingEntities,
            ErrorInvalidFields,
            ErrorDatabaseConnection,
            ErrorUnknown,
            AllRoutinesDone,
            NotAllRoutinesDone
        }

        public ViewModels.Management.Users.UserIndexViewModel UserAuthenticated { get; set; }
        public bool UserMustAuthenticate { get; set; }
        public bool UserIsAuthenticated { get { return (UserAuthenticated != null); } }

        public System.Diagnostics.FileVersionInfo AppInfo;

        public _BaseController()
        {
            UserMustAuthenticate = true; // to put on true
        }

        protected void SetMessage(ViewModels.Shared.MessageViewModel.Levels level, string content)
        {
            var message = DisplayName + " - " + content;

            TempData["Message"] = new ViewModels.Shared.MessageViewModel(level, message);

            //Log every movement
            //Services.DataServices.Shared.LogEventsDataService.AddNew(this, (level == ViewModels.Shared.MessageViewModel.Levels.ERROR), message);
        }

        protected void UnSetMessage()
        {
            TempData["Message"] = null;
        }

        protected void SetMessage(MessageTypes type, string entityName)
        {
            entityName = (!string.IsNullOrEmpty(entityName) ? entityName : "item");

            if (type == MessageTypes.SuccessGeneric)
            {
                SetMessage(ViewModels.Shared.MessageViewModel.Levels.SUCCESS, "Action completed.");
            }

            if (type == MessageTypes.SuccessAddNew)
            {
                SetMessage(ViewModels.Shared.MessageViewModel.Levels.SUCCESS, "Added new " + entityName + ".");
            }

            if (type == MessageTypes.SuccessEdit)
            {
                SetMessage(ViewModels.Shared.MessageViewModel.Levels.SUCCESS, "Selected " + entityName + " was updated.");
            }

            if (type == MessageTypes.SuccessDelete)
            {
                SetMessage(ViewModels.Shared.MessageViewModel.Levels.SUCCESS, "Selected " + entityName + " was deleted.");
            }

            if (type == MessageTypes.ErrorGeneric)
            {
                SetMessage(ViewModels.Shared.MessageViewModel.Levels.ERROR, "Action not completed.");
            }

            if (type == MessageTypes.ErrorDatabaseConnection)
            {
                SetMessage(ViewModels.Shared.MessageViewModel.Levels.ERROR, "Action not completed due to database connection issues. Try again.");
            }

            if (type == MessageTypes.ErrorInvalidFields)
            {
                var errors = ModelState.Values.SelectMany(o => o.Errors).Select(o => o.ErrorMessage);

                SetMessage(ViewModels.Shared.MessageViewModel.Levels.ERROR, "Invalid fields for " + entityName + ": " + string.Join(", ", errors) + ".");
            }

            if (type == MessageTypes.ErrorDeleteDependingEntities)
            {
                SetMessage(ViewModels.Shared.MessageViewModel.Levels.ERROR, "Could not delete selected " + entityName + " because other items are depending on it. Delete those first.");
            }

            if (type == MessageTypes.AllRoutinesDone)
            {
                SetMessage(ViewModels.Shared.MessageViewModel.Levels.SUCCESS, "Congratulations! All routines are done!");
            }

            if (type == MessageTypes.NotAllRoutinesDone)
            {
                SetMessage(ViewModels.Shared.MessageViewModel.Levels.WARNING, "Not all routines are done. Please try to do your best to close all of them!");
            }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            //Get program name and version from Assembly file
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            AppInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

            UserAuthenticated = Models.UsersModel.Authenticate(System.Web.HttpContext.Current.User.Identity.Name);
            if (UserAuthenticated == null)
                UserMustAuthenticate = false;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserIsAuthenticated)
            {
                ViewBag.UserName = UserAuthenticated.NameFull;
            }
            else
            {
                if (UserMustAuthenticate)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "AccessDenied", controller = "Error" }));
                }
            }

            ViewBag.UserIsAuthenticated = UserIsAuthenticated;
            ViewBag.AppName = AppInfo.ProductName;
            ViewBag.AppDescription = AppInfo.Comments;
            ViewBag.AppVersion = AppInfo.ProductVersion;

            ViewBag.DisplayName = DisplayName;
            //ViewBag.UserRol = UserAuthenticated.RoleName;

            base.OnActionExecuting(filterContext);
        }
    }
}