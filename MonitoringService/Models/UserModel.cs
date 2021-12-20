using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MSD.MonitoringService.Models
{
    public class UserModel : _BaseModel
    {
        #region Public Fields
        public struct StakeHolder
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string emailAddress { get; set; }
        }
        public struct KPI
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }
        public struct Project
        {
            public string Name { get; set; }
            public List<string> KPIs { get; set; }
        }
        public StakeHolder ProjectManager { get; set; }
        public StakeHolder Sponsor { get; set; }
        public List<Project> Projects { get; set; }
        #endregion

        #region Constructor

        public UserModel()
        {
            ProjectManager = new StakeHolder();
            Sponsor = new StakeHolder();
            Projects = new List<Project>();
        }

        #endregion
    }
}
