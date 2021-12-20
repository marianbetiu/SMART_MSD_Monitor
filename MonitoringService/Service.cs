using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace MSD.MonitoringService
{
    public partial class Service : ServiceBase
    {
        public BackgroundWorker worker = new BackgroundWorker();
        //Timer run interval in seconds

        public Service()
        {
            InitializeComponent();

            //Set service name
            ServiceName = "MSD.BE.MonitoringService";
            //Set EventLog source
            this.EventLog.Source = ServiceName;

            //Services.DataServices.AlertsDataService.MonitorVMTLinesStatus();
            worker.DoWork += Worker_DoWork;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();
            OnStart(null);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                Services.DataServices.AlertsDataService.MonitorVMTLinesStatus();
                if (Services.DataServices.AlertsDataService.EscalatedIncidentsList.Count > 0)
                {
                    Services.DataServices.AlertsDataService.SendAll(Services.DataServices.AlertsDataService.EscalatedIncidentsList);
                }
                Thread.Sleep(30000);
            }
        }

        protected override void OnStart(string[] args)
        {
            //Create new source in EventLog if not exists
            ((ISupportInitialize)(this.EventLog)).BeginInit();
            try
            {
                if (!EventLog.SourceExists(this.EventLog.Source))
                {
                    EventLog.CreateEventSource(this.EventLog.Source, this.EventLog.Log);
                }
            ((ISupportInitialize)(this.EventLog)).EndInit();
            }
            catch (Exception ex)
            {
                MultiPack.EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, ex.ToString());
            }

            //Timer = new System.Threading.Timer(TimerTick, null, TimerRunInterval * 1000, 0);
            //Timer = new System.Threading.Timer(TimerTick);
            
        }

        protected override void OnStop()
        {
            //Timer.Dispose();
            worker.CancelAsync();
            worker.Dispose();
        }

        private void TimerTick(object state)
        {
            Services.DataServices.AlertsDataService.MonitorVMTLinesStatus();
        }
    }
}
