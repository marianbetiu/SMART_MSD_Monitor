using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Data.Models
{
    public class HomeModel
    {
        public static int GetUserIdByLogin(string userLogin)
        {
            var result = 0;
            using (var model = Data.ModelAccess.GetNewDBModel())
            {
                result = model.Users.FirstOrDefault(a => a.UserName == userLogin).UserId;
            }
            return result;
        }

        public static async Task<List<ViewModels.HomeItemViewModel>> GetItems(int minQty, int maxRemTime, string msl)
        {
            var result = new List<ViewModels.HomeItemViewModel>();

            try
            {
                // init Franco
                Data.Franco.Init();
            }
            catch { }
            using(var model = Data.ModelAccess.GetNewDBModel())
            {
                //model.ReelsActions.Where(a => a.Action.ToUpper() == "PROVIDE").ToList();
                var collection = new List<ReelsAction>();
                var collectionGroupped = model.ReelsActions.GroupBy(a => a.Identificator); //.OrderByDescending(a => a.DateTimeRegistered);
                var dtNow = DateTime.Now;
                //.Where(a => a.Action.ToUpper() == "PROVIDE").ToList(); //.Where(a=>((a.RemainingTime-dtNow).TotalMinutes < maxRemTime) && ((a.RemainingTime - dtNow).TotalMinutes > -50)).ToList();
                
                foreach(var row in collectionGroupped)
                {
                    var current = row.OrderByDescending(a=>a.DateTimeRegistered).First();
                    
                    if (current.Action.ToUpper() == "PROVIDE")
                    {
                        var remTimeMinutes = (current.RemainingTime - dtNow).TotalMinutes;
                        if(remTimeMinutes < maxRemTime && remTimeMinutes > -480)
                        {
                            collection.Add(current);
                        }
                    }
                         
                }
                foreach(var item in collection)
                {
                    #region old code
                    //var msLevel = Franco.GetReelMSL(item.Identificator);
                    //if (msLevel != string.Empty && msLevel.ToUpper() == msl.ToUpper())
                    //{
                    //    var remTime = (item.RemainingTime - DateTime.Now).TotalMinutes;
                    //    if (remTime < maxRemTime && remTime > -50)
                    //    {
                    //        var qty = await Task.FromResult(Data.Franco.GetReelQty(item.Identificator));
                    //        if (qty > minQty)
                    //        {
                    //            result.Add(new ViewModels.HomeItemViewModel
                    //            {
                    //                Identifier = item.Identificator,
                    //                RemTime = (int)remTime,
                    //                Material = GetMaterialFromIdentifier(item.Identificator),
                    //                MSL_Level = msLevel,
                    //                Location = await Task.FromResult(Data.Franco.GetReelLocation(item.Identificator)),
                    //                Quantity = qty
                    //            });
                    //        }
                    //    }
                    //}
                    #endregion

                    var msLevel = Franco.GetReelMSL(item.Identificator);
                    if (msLevel != string.Empty && msLevel.ToUpper() == msl.ToUpper())
                    {
                        var qty = await Task.FromResult(Data.Franco.GetReelQty(item.Identificator));
                        if (qty > minQty)
                        {
                            result.Add(new ViewModels.HomeItemViewModel
                            {
                                Identifier = item.Identificator,
                                RemTime = (int)(item.RemainingTime - dtNow).TotalMinutes,
                                Material = GetMaterialFromIdentifier(item.Identificator),
                                MSL_Level = msLevel,
                                Location = await Task.FromResult(Data.Franco.GetReelLocation(item.Identificator)),
                                Quantity = qty
                            });
                        }
                    }
                }
            }

            return result;
        }

        private static string GetMaterialFromIdentifier(string iden)
        {
            var result = string.Empty;
            var idenSplit = iden.Split('@');
            if (idenSplit.Count() > 0)
            {
                result = idenSplit[0];
                while(result.Substring(0,1) == "0")
                {
                    result = result.Substring(1, result.Length - 1);
                }
            }
            return result;
        }
    }
}