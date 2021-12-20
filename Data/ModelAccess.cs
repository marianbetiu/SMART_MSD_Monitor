using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class ModelAccess
    {
        public static string ConnStr
        {
            get { return "metadata=res://*/MSD_PSS_Model.csdl|res://*/MSD_PSS_Model.ssdl|res://*/MSD_PSS_Model.msl;provider=System.Data.SqlClient;provider connection string=\"data source=tmas275a\\i0001;initial catalog=MSL_PSS;persist security info=True;user id=MSL_PSS;password=Q!w2e3r4t5;MultipleActiveResultSets=True;App=EntityFramework\""; }
        }
        public static MSL_Entities GetNewDBModel()
        {
            return new MSL_Entities(ConnStr);
        }
    }
}
