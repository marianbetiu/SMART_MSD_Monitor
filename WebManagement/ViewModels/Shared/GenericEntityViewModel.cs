using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebManagement.ViewModels.Shared
{
    public class GenericEntityViewModel : _BaseEntityViewModel
    {
        #region Public fields

        public string Name { get; set; }

        #endregion

        #region Constructor

        public GenericEntityViewModel()
        {
            Name = string.Empty;
        }

        #endregion

        #region ToString override

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
