using FraMES.Natives.WIPMaMa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class Franco
    {
        public static void Init()
        {
            try
            {
                WIPMaMa.SetConfigPath("c:\\FraMes\\Frames.cfg");
                WIPMaMa.Init();
            }
            catch (Exception e)
            {
                var error = e.ToString();
            }
        }

        public static async Task<double?> GetAsyncReelQty(string identificator)
        {
            try
            {
                // init reel in MaMa
                var myReel = new Batch(identificator);
                //Part.GetPart(new FraMES.Natives.Common.Identifier("RAW", identificator)); //new Batch(identificator);
                return await Task.FromResult(myReel.Quantity);
                //(myReel.GetQuantity(Part.Quantity.Actual));
            }

            catch
            {
                return null;
            }
        }

        public static double GetReelQty(string identificator)
        {
            try
            {
                // init reel in MaMa
                var myReel = new Batch(identificator);
                return myReel.Quantity;
            }

            catch
            {
                return -1;
            }
        }

        public static string GetReelLocation(string identificator)
        {
            try
            {
                // init reel in MaMa
                var myReel = new Batch(identificator);
                var result = string.Format("{0}", myReel.BatchBaseInfo.Operator);
                if (!string.IsNullOrEmpty(myReel.BatchBaseInfo.LocationDetail))
                {
                    result += string.Format(" [{0}]", myReel.BatchBaseInfo.LocationDetail);
                }

                return result;
            }
            catch
            {
                return "- UNKNOWN -";
            }
        }

        public static string GetReelMSL(string identificator)
        {
            try
            {
                // init reel in MaMa
                var myReel = new Batch(identificator);
                var result = string.Format("{0}", myReel.BatchBaseInfo.MoistureLevel);

                if (result == "")
                {
                    var myMaterial = new Material(myReel.Material);
                    ExtendedFieldText extFld = new ExtendedFieldText();
                    myMaterial.GetExtField<ExtendedFieldText>("MSL_LEVEL", extFld);
                    result = extFld.content;
                }
                return result;
            }
            catch
            {
                return "- UNKNOWN -";
            }
        }

        public static bool SetExtFieldLong(string materialNumber, int value, out string errorMsg)
        {
            errorMsg = string.Empty;
            bool result = false;
            try
            {
                // check if the material exist
                Material material = new Material(materialNumber);

                if(material.Exists())
                {
                    // set Extended Field MAX_DISASSY_COUNTER
                    ExtendedFieldLong myExtFld = new ExtendedFieldLong(value, "Set by Quality Dep");

                    material.SetExtField("MAX_DISASSY_COUNTER", myExtFld);

                    result = true;
                }
                else
                {
                    errorMsg = "The material number does not exist in MES System";
                }
            }
            catch { }
            return result;
        }

        public static bool CheckMaterialExist(string materialNumber, out string materialDescription)
        {
            bool result = false;
            materialDescription = string.Empty;
            try
            {
                Material material = new Material(materialNumber);
                if (material.Exists())
                {
                    materialDescription = material.Name;
                    result = true;
                }
            }
            catch { }

            return result;
        }
    }
}
