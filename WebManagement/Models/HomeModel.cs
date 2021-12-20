using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebManagement.Models
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

        public static async Task<List<ViewModels.HomeItemViewModel>> GetItems(int minQty, int maxRemTime)
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
                var collection = model.ReelsActions.Where(a=>a.Action.ToUpper() == "PROVIDE").ToList();
                foreach(var item in collection)
                {
                    var remTime = (item.RemainingTime - DateTime.Now).TotalMinutes;
                    if (remTime < maxRemTime && remTime > -50)
                    {
                        var qty = await Task.FromResult(Data.Franco.GetReelQty(item.Identificator));
                        if (qty > minQty)
                        {
                            result.Add(new ViewModels.HomeItemViewModel
                            {
                                Identifier = item.Identificator,
                                RemTime = (int)remTime,
                                Material = GetMaterialFromIdentifier(item.Identificator),
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