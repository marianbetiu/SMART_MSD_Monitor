using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace WebManagement.ViewModels.Shared
{
    public class MessageViewModel
    {
        public enum Levels
        {
            [Description("Info")]
            INFO,

            [Description("Success")]
            SUCCESS,

            [Description("Error")]
            ERROR,

            [Description("Warning")]
            WARNING
        }

        public Levels Level { get; set; }
        public string Content { get; set; }

        public string LevelDescription { get { return MultiPack.EnumLib.GetDescriptionFromEnum(Level); } }

        public string BootstrapLevelDescription
        {
            get
            {
                string result = "alert-info";
                switch (Level)
                {
                    case Levels.ERROR: result = "alert-danger"; break;
                    case Levels.SUCCESS: result = "alert-success"; break;
                    case Levels.WARNING: result = "alert-warning"; break;
                }
                return result;
            }
        }

        public MessageViewModel()
        {
            Level = Levels.INFO;
            Content = string.Empty;
        }

        public MessageViewModel(Levels level, string content)
        {
            Level = level;
            Content = content;
        }
    }
}