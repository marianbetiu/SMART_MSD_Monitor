using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebManagement.Controllers
{
    public class HomeController : _BaseController
    {
        public HomeController()
        {
            UserMustAuthenticate = true;
            DisplayName = "Actual Status";

            //var password = MultiPack.EncryptionLib.Decrypt("J9v57Egw/fndcarr/tUjUB9QiL0JvQ==");
        }

        public ActionResult Index()
        {
            var viewModel = new Data.ViewModels.HomeIndexViewModel();
            //viewModel.Items = await viewModel.GetItems();
            return View(viewModel);
        }
        
        public async Task<ActionResult> Refresh(int qty, int remTime, string msl)
        {
            var viewmodel = new Data.ViewModels.HomeIndexViewModel();
            viewmodel.Items = await viewmodel.GetItems(qty, remTime, msl);
            return PartialView("_ResultInfo", viewmodel);
        }
    }
}