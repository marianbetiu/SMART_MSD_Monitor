using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using MSD.Data;
using System.Threading;

namespace MSD.MonitoringService.Models
{
    public class AlertModel : _BaseModel
    {
        #region Public Fields
        public List<string> EmailAddress { get; set; }
        public int IncidentId { get; set; }
        public int EscalationLevelId { get; set; }
        public string EscalationLevel { get; set; }
        public string AlertGroup { get; set; }
        public DateTime AlertDateTime { get; set; }
        public string Message { get; set; }
        public string LineName { get; set; }
        public string EquipmentName { get; set; }
        public string RootCause { get; set; }
        public int CategoryBDId { get; set; }
        public string CaterogyBDName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string AreaName { get; set; }
        public double BreakDownDuration { get; set; }
        public string StationaryType { get; set; }
        public int EscalatedIncidentId { get; set; }
        #endregion

        #region Constructor

        public AlertModel()
        {
        }

        #endregion

    }
}
