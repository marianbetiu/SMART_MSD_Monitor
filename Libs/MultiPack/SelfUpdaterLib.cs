using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MultiPack
{
    public class SelfUpdaterEventArgs : EventArgs
    {
        private string EventInfo;
        public SelfUpdaterEventArgs(string Text)
        {
            EventInfo = Text;
        }
        public string GetInfo()
        {
            return EventInfo;
        }
    }

    //Event handler for new file found
    public delegate void OnUpdateFoundEventHandler(object source, SelfUpdaterEventArgs e);

    public class SelfUpdaterLib
    {
        public event OnUpdateFoundEventHandler OnUpdateFound;

        public FileVersionInfo AppInfo;
        public string UpdateFolder;

        //Timer that checks for updated file
        public System.Threading.Timer TimerCheckForUpdates;
        //Timer check interval in seconds
        int TimerCheckInterval = 10;

        public SelfUpdaterLib()
        {
            TimerCheckForUpdates = new System.Threading.Timer(TimerCheckForUpdates_Tick, null, TimerCheckInterval * 1000, System.Threading.Timeout.Infinite);
        }

        public SelfUpdaterLib(FileVersionInfo appInfo, string updateFolder) : this()
        {
            AppInfo = appInfo;
            UpdateFolder = updateFolder;
        }

        private void TimerCheckForUpdates_Tick(object state)
        {
            //To make sure we only trigger the event if a handler is present
            //we check the event to make sure it's not null. After that we can check for new file
            if (OnUpdateFound != null)
            {
                if (CheckIfNewFileVersionExists(AppInfo, UpdateFolder))
                {
                    OnUpdateFound(this, new SelfUpdaterEventArgs("Found!!"));
                }
            }
            TimerCheckForUpdates.Change(TimerCheckInterval * 1000, System.Threading.Timeout.Infinite);
        }

        //Check if new file exists
        public static bool CheckIfNewFileVersionExists(FileVersionInfo appInfo, string updateFolder)
        {
            bool result = false;

            var updateFile = updateFolder + @"\" + appInfo.ProductName + @"\" + appInfo.OriginalFilename;

            if (File.Exists(updateFile))
            {
                var versionNewInfo = FileVersionInfo.GetVersionInfo(updateFile);
                //string versionNew = versionNewInfo.ProductVersion;
                if (!string.IsNullOrEmpty(versionNewInfo.ProductVersion))
                {
                    //result = (appInfo.ProductVersion != versionNew);
                    //check if new file version is greater than current
                    result = (new Version(versionNewInfo.ProductVersion) > new Version(appInfo.ProductVersion));
                }
            }

            return result;
        }
    }
}
