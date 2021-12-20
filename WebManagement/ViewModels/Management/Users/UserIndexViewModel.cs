using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebManagement.ViewModels.Management.Users
{
    public class UserIndexViewModel : Data.ViewModels.Shared._BaseEntityViewModel
    {
        #region Public Fields

        [Display(Name = "Full name")]
        public string NameFull { get; set; }

        [Display(Name = "Login name")]
        public string NameLogin { get; set; }

        #endregion

        #region Constructor

        public UserIndexViewModel()
        {
            NameFull = string.Empty;
        }

        #endregion

        #region ToString override

        public override string ToString()
        {
            return NameFull;
        }

        #endregion
    }
}
