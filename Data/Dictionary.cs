using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class Dictionary
    {
        public static string Placeholder_ALL { get { return "-- ALL --"; } }
        public static string Placeholder_NONE { get { return "-- NONE --"; } }
        public static string Placeholder_UNLIMITED { get { return "-- UNLIMITED --"; } }
        public static string Placeholder_INHERITED { get { return "-- INHERITED --"; } }
        public static string Placeholder_NA { get { return "-- N/A --"; } }

        public static string Format_DATETIME_ISO { get { return "yyyy-MM-dd HH:mm:ss"; } }

        public static string Format_DATETIME_RO { get { return "dd.MM.yyyy HH:mm:ss"; } }

        public static string Format_DATE_ISO { get { return "yyyy-MM-dd"; } }

        public static string Format_DATE_RO { get { return "dd.MM.yyyy"; } }

        public static System.Drawing.Rectangle Rectangle_Picture_Big { get { return new System.Drawing.Rectangle(0, 0, 1422, 800); } }

        public static System.Drawing.Rectangle Rectangle_Picture_Medium { get { return new System.Drawing.Rectangle(0, 0, 1024, 576); } }

        public static System.Drawing.Rectangle Rectangle_Picture_Thumbnail { get { return new System.Drawing.Rectangle(0, 0, 240, 120); } }
    }
}