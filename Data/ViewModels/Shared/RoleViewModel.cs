using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Data.ViewModels.Shared
{
    public class RoleViewModel : Data.ViewModels.Shared._BaseEntityViewModel
    {
        #region Public Fields

        [Display(Name = "Role name")]
        public string RoleName { get; set; }

        public string RoleNameBootstrapColor
        {
            get
            {
                string result = "info";

                switch (RoleName)
                {
                    case "Administrator": result = "warning"; break;
                    case "Project Manager": result = "success"; break;
                }

                return result;
            }
        }
        #endregion

        #region Constructor

        public RoleViewModel()
        {
            RoleName = string.Empty;
        }

        #endregion

        #region ToString override

        public override string ToString()
        {
            return RoleName;
        }

        #endregion
    }
}
