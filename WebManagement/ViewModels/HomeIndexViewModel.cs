using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;

namespace WebManagement.ViewModels
{
    public class HomeIndexViewModel : Shared._BaseEntityViewModel
    {
        public List<HomeItemViewModel> Items { get; set; }
        public List<string> Quantities { get { return new List<string>() {"2", "5", "10", "20", "50" }; } }
        public string selectedQty { get; set; }
        public List<string> RemTimes { get { return new List<string>() { "1200", "1000", "800", "600", "500", "400", "300", "250", "210", "180", "150", "120", "100", "80", "60", "45", "30" }; } }
        public string selectedRemTime { get; set; }
        public HomeIndexViewModel()
        {
            Items = new List<HomeItemViewModel>();
        }

        public async Task<List<HomeItemViewModel>> GetItems(int qty, int remTime)
        {
            return await Models.HomeModel.GetItems(qty, remTime);
        }

        
        //public async Task<string> GetLocation(string Id)
        //{
        //    return await Task.FromResult(Data.Franco.GetReelLocation(Id));
        //}

        //public async Task<double?> GetQuantity(string Id)
        //{
        //    return await Task.FromResult(Data.Franco.GetReelQty(Id));
        //}
    }
}
