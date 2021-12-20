using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileMonitor.MessagingService.Services.DataServices
{
    public class UsersDataService
    {
        public static Models.UserModel Load(Data.User entity)
        {
            Models.UserModel result = null;

            if (entity != null)
            {
                result = new Models.UserModel
                {
                    Id = entity.Id,
                    EmailAddress = entity.EmailAddress,
                    NameFull = entity.NameFull
                };
            }

            return result;
        }

        public static List<Models.UserModel> QueryAll()
        {
            List<Models.UserModel> result = new List<Models.UserModel>();

            try
            {
                using (var model = Data.Models.GetNewModelFileMonitor())
                {
                    var users = model.Users.Where(o => ((o.IsEnabled) && (o.EmailAddress != null))).ToList();

                    users.ForEach(o =>
                    {
                        var item = Load(o);

                        if(item!= null)
                        {
                            result.Add(item);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MultiPack.EventLogFileLib.Write(MultiPack.EventLogFileLib.Levels.ERROR, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.ToString());
            }

            return result;
        }
    }
}

