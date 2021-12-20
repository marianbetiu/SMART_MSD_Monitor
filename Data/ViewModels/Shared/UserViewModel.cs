using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Data.ViewModels.Shared
{
    public class UserViewModel : _BaseEntityViewModel
    {
        #region Enums

        public enum RoleOptions
        {
            [Description("Viewer")]
            VIEWER = 0,

            [Description("Project Manager")]
            PM = 1,

            [Description("Sponsor")]
            SPONSOR = 2,

            [Description("Administrator")]
            ADMINISTRATOR = 3
        }

        #endregion

        #region Public Fields

        public string NameFull { get; set; }

        public string NameLogin { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsReceivingEmails { get; set; }

        public bool IsEnabled { get; set; }
        
        public int Role { get; set; }

        public RoleOptions RoleOption
        {
            get { return (RoleOptions)Role; }
            set { Role = (int)value; }
        }

        public string RoleOptionDescription
        {
            get { return MultiPack.EnumLib.GetDescriptionFromEnum(RoleOption); }
        }

        public int ProjectId { get; set; }

        public List<RoleViewModel> RolesNames { get; set; }

        #endregion

        #region Constructor

        public UserViewModel()
        {
            RoleOption = RoleOptions.VIEWER;
            EmailAddress = string.Empty;
            IsReceivingEmails = true;
            IsEnabled = true;
            RolesNames = new List<RoleViewModel>();
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
