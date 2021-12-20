using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebManagement
{
    public class DictionaryConfig
    {
        public static string Placeholder_ALL { get { return "-- ALL --"; } }
        public static string Placeholder_NONE { get { return "-- NONE --"; } }
        public static string Placeholder_UNLIMITED { get { return "-- UNLIMITED --"; } }
        public static string Placeholder_INHERITED { get { return "-- INHERITED --"; } }
        public static string Placeholder_NA { get { return "-- N/A --"; } }

        public static string Format_DATETIME { get { return "yyyy-MM-dd HH:mm:ss"; } }
    }
}