using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebManagement.ViewModels.Management.Users
{
    public class UserEditViewModel : Data.ViewModels.Shared._BaseEntityViewModel
    {
        #region Public Fields

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "{0} is required")]
        public string NameFull { get; set; }

        [Display(Name = "Login name")]
        [Required(ErrorMessage = "{0} is required")]
        public string NameLogin { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
       
        #endregion

        #region Constructor

        public UserEditViewModel()
        {
            NameFull = string.Empty;
        }
        #endregion
    }
}