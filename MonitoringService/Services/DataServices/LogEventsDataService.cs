using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FileMonitor.MessagingService.Services.DataServices
{
    public class LogEventsDataService
    {
        public static void AddNew(string appDescription, bool isError, string message)
        {
            try
            {
                using (var model = Data.Models.GetNewModelFileMonitor())
                {
                    var entity = new Data.LogEvent
                    {
                        DateCreated = DateTime.Now,
                        IsError = isError,
                        Message = message,
                        Source = appDescription,
                        UserId = null
                    };

                    model.LogEvents.Add(entity);

                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MultiPack.EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
            }
        }
    }
}

