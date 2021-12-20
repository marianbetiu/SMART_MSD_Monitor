using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebManagement.Controllers
{
    public class UsersController : _BaseController
    {
        public UsersController()
        {
            DisplayName = "Users";
        }

        [HttpGet]
        public ActionResult Index()
        {
            if ((User.Identity.Name == "CW01\\uidv2802") || UserIsAuthenticated)
            {
                var viewModel = new ViewModels.Management.Users.UsersIndexViewModel();

                viewModel.Items = Models.UsersModel.QueryAllIndex();

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult AddNew()
        {
            if ((User.Identity.Name == "CW01\\uidv2802") || UserIsAuthenticated)
            {
                var viewModel = new ViewModels.Management.Users.UserEditViewModel();

                return PartialView("Edit", viewModel);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNew(ViewModels.Management.Users.UserEditViewModel viewModel)
        {
            if ((User.Identity.Name == "CW01\\uidv2802") || UserIsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (Models.UsersModel.Update(viewModel))
                    {
                        SetMessage(MessageTypes.SuccessAddNew, string.Empty);

                        //Data.Models.Shared.LogEventsModel.AddNew(UserAuthenticated.Id, AppInfo, false, "Added new user: " + viewModel.NameFull + ", id: " + viewModel.Id.ToString());
                    }
                    else
                    {
                        SetMessage(MessageTypes.ErrorDatabaseConnection, string.Empty);
                    }
                }
                else
                {
                    SetMessage(MessageTypes.ErrorInvalidFields, string.Empty);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Edit(int id)
        {
            if ((User.Identity.Name == "CW01\\uidv2802") || UserIsAuthenticated)
            {
                var viewModel = Models.UsersModel.QuerySingleEdit(id);

                if (viewModel != null)
                {
                    return PartialView("Edit", viewModel);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Management.Users.UserEditViewModel viewModel)
        {
            if ((User.Identity.Name == "CW01\\uidv2802") || UserIsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (Models.UsersModel.Update(viewModel))
                    {
                        SetMessage(MessageTypes.SuccessEdit, string.Empty);

                        //Data.Models.Shared.LogEventsModel.AddNew(UserAuthenticated.Id, AppInfo, false, "Modified user: " + viewModel.NameFull + ", id: " + viewModel.Id.ToString());
                    }
                    else
                    {
                        SetMessage(MessageTypes.ErrorDatabaseConnection, string.Empty);
                    }
                }
                else
                {
                    SetMessage(MessageTypes.ErrorInvalidFields, string.Empty);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if ((User.Identity.Name == "CW01\\uidv2802") || UserIsAuthenticated)
            {
                var viewModel = Models.UsersModel.QuerySingleEdit(id);

                if (viewModel != null)
                {
                    if (Models.UsersModel.Delete(id))
                    {
                        SetMessage(MessageTypes.SuccessDelete, string.Empty);

                        //Data.Models.Shared.LogEventsModel.AddNew(UserAuthenticated.Id, AppInfo, false, "Deleted user: " + viewModel.NameFull + ", id: " + viewModel.Id.ToString());
                    }
                    else
                    {
                        SetMessage(MessageTypes.ErrorDeleteDependingEntities, string.Empty);
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}
