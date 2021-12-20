using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Data.ViewModels.Shared
{
    public class LogEventViewModel
    {
        public int UserId { get; set; }
        public string UserNameFull { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return Source + " - " + Message;
        }
    }
}
