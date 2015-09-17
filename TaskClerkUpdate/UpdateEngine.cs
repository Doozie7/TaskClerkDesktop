using System;
using System.Windows.Forms;
using System.Diagnostics;
using BritishMicro.TaskClerk.Update.com.taskclerk.api;

namespace BritishMicro.TaskClerk.Update
{
    public class UpdateEngine
    {
        public static void CheckForUpdatedVersion(string runningVersion)
        {

            string rv = runningVersion;
            string lav = LatestAvailableVersion();

            if (rv != lav)
            {
                if (DialogResult.Yes == MessageBox.Show(
                    "A newer version of TaskClerk is available.\nWould you like to visit the download centre.",
                    "Update Available",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information))
                {
                    Process.Start("http://www.taskclerk.com/downloads.ashx");
                }
            }
            else
            {
                MessageBox.Show(
                    "You are currently running the latest version of TaskClerk.",
                    "Up To Date",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        public static string LatestAvailableVersion()
        {
            string _latestVersion = "unknown";
            using (TaskClerkApplicationServices ws = new TaskClerkApplicationServices())
            {
                _latestVersion = ws.LatestVersion();
            }
            return _latestVersion;
        }
    }
}
