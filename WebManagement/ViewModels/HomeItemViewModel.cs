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
    public class HomeItemViewModel : Shared._BaseEntityViewModel
    {
        [Display(Name = "IDENTIFICATOR")]
        public string Identifier { get; set; }

        [Display(Name = "MATERIAL")]
        public string Material { get; set; }

        [Display(Name = "MSL")]
        public string MSL_Level { get; set; }

        [Display(Name = "LOCATION")]
        public string Location { get; set; }

        [Display(Name = "QUANTITY")]
        public double? Quantity { get; set; }

        [Display(Name = "REMAINING TIME")]
        public int RemTime { get; set; }
        
        public HomeItemViewModel()
        {
        }

        public override string ToString()
        {
            return Identifier;
        }        
    }
}
