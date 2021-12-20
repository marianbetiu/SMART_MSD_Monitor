using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSD.MonitoringService.Models
{
    public class _BaseModel
    {
        public int Id { get; set; }

        public _BaseModel()
        {
            Id = 0;
        }
    }
}