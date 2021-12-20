using MSD.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MultiPack;
using System.Drawing;
using System.IO;
using System.Net.Mail;

namespace MSD.MonitoringService.Services.DataServices
{
    public class AlertsDataService
    {
        public static List<Models.AlertModel> EscalatedIncidentsList = new List<Models.AlertModel>();

        public static bool SendSingle(string toAddress, List<string> ccAddresses, string title, string content)
        {
            bool result = false;
            var toAddresses = new List<string>() { toAddress };
            var error = MultiPack.EmailLib.SendContinental(toAddresses, ccAddresses, title, content);

            if (string.IsNullOrEmpty(error))
            {
                result = true;
            }
            else
            {
                MultiPack.EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, System.Reflection.MethodBase.GetCurrentMethod().Name, error);
            }

            return result;
        }

        public static void SendAll(List<Models.AlertModel> toUsers)
        {
            try
            {
                using (var model = Data.ModelAccess.GetNewDBModel())
                {
                    foreach (var user in toUsers)
                    {
                        string body = string.Empty;
                        // Subject : FA ( PCBA / SMT ) Line Name (Renault3) [FF xx Segmentxxx (PSS OSIS) ] - BreakDown alert! 
                        var subject = string.Format("[{0}-{1}] {2} - BreakDown alert!",user.AreaName, user.DivisionName, user.LineName);
                        body = string.Format("<h4>This is an automatic message from Escalation.Process service. Please do not reply</h4>There is a Break down on the line: <b>{0}</b>", user.LineName);
                        body += string.Format("<p>Details: </p><br/>");
                        body += string.Format("<table border = \"1\"><tr><td><b>Target group</b></td><td>{0}</td></tr>", user.AlertGroup);
                        body += string.Format("<tr><td><b>Issue type</b></td><td>{0}</td></tr>", user.CaterogyBDName);
                        body += string.Format("<tr><td><b>Affected equipment</b></td><td>{0}</td></tr>", user.EquipmentName);
                        body += string.Format("<tr><td><b>Root cause</b></td><td>{0}</td></tr>", user.RootCause);
                        body += string.Format("<tr><td><b>Escalation level</b></td><td>{0}</td></tr>", user.EscalationLevel);
                        body += string.Format("<tr><td><b>BreakDown duration</b></td><td>{0} minutes</td></tr>", user.BreakDownDuration);
                        body += string.Format("<tr><td><b>Intervention Id</b></td><td><a href=\"http://tmas336a:8097/Home/InterventionHistory/{0}\" />{1}</td></tr></table>", user.EscalatedIncidentId, user.EscalatedIncidentId);
                        body += "<div>To access the Escalation Dashboard, please click <a href=\"http://tmas302x.tm.ro.conti.de:8080/MicroStrategy/servlet/mstrWeb?evt=3186&src=mstrWeb.3186&subscriptionID=E30F489311E9D62F172C0080EF65786A&Server=TMAS302X.TM.RO.CONTI.DE&Project=PRiME%20DWH&Port=0&share=1\"/>here</div>";
                        var pictureFileName = string.Format("{0}\\Image.jpg", BE.MonitoringService.Properties.Settings.Default.ImagePath);
                        
                        //var address = "marian.betiu@continental-corporation.com";
                        //SendSingle(address, subject, body, pictureFileName);

                        foreach (var singleAddress in user.EmailAddress)
                        {
                            //body += string.Format("<b>{0}</b><br/>", singleAddress);

                            try
                            {
                                SendSingle(singleAddress, subject, body, pictureFileName);
                            }
                            catch(Exception ex)
                            {
                                EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MultiPack.EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
            }
        }

        public AlertsDataService()
        { }
    }
}

