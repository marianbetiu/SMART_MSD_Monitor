using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace MSD.MonitoringService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //while (true)
            //{
            //    Services.DataServices.AlertsDataService.MonitorVMTLinesStatus();
            //    if (Services.DataServices.AlertsDataService.EscalatedIncidentsList.Count > 0)
            //    {
            //        Services.DataServices.AlertsDataService.SendAll(Services.DataServices.AlertsDataService.EscalatedIncidentsList);
            //    }
            //    System.Threading.Thread.Sleep(1234);
            //}

            //using (var incidentModel = Data.ModelAccess.GetNewEscalationDBModel())
            //{
            //    var targetGrpId = 4;
            //    int? divisionId = null;
            //    int? areaId = null;
            //    Data.EscalationEmailGroup emailGroup = incidentModel.EscalationEmailGroups.FirstOrDefault(r => ((r.TargetGroupId == targetGrpId) && (r.DivisionId == divisionId) && (r.AreaId == areaId)));
            //    var emailAddr = emailGroup.EmailGroup;
            //}

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
