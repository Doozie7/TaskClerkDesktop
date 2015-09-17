using System;
using System.Collections.Generic;
using System.Text;
using BritishMicro.TaskClerk.Plugins;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace BritishMicro.TaskClerk.ExportPack1
{
    /// <summary>
    /// iCal File (ICS).
    /// </summary>
    [DisplayName("iCal Exporter (ICS)")]
    [Description("This exporter creates an iCal File,\nthe output from this exporter can be used in the OutLook and Google calendar systems.")]
    class FileiCalExporter : PluginExporter
    {
        /// <summary>
        /// The main export function
        /// </summary>
        public override void Execute()
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "iCal files (*.ics)|*.ics|All files (*.*)|*.*";
            fd.FilterIndex = 1;
            fd.RestoreDirectory = true;

            if (DialogResult.OK == fd.ShowDialog())
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fd.OpenFile()))
                    {
                        string cname = AppContext.Current.IdentityProvider.Principal.Identity.Name;
                        string emailaddress = AppContext.Current.SettingsProvider.Get("CurrentUserEmailAddress", "unknown@unknown") as string;
                        string timezone = TimeZone.CurrentTimeZone.StandardName;
                        string calanderscale = "GREGORIAN";

                        sw.WriteLine("BEGIN:VCALENDAR");
                        sw.WriteLine("PRODID:-//BritishMicro//TaskClerk 1.0.1//EN");
                        sw.WriteLine("VERSION:1.0");
                        sw.WriteLine(string.Format("CALSCALE:{0}", calanderscale));
                        sw.WriteLine("METHOD:PUBLISH");
                        sw.WriteLine(string.Format("X-WR-CALNAME:{0}", cname));
                        sw.WriteLine(string.Format("X-WR-TIMEZONE:{0}", timezone));
                        sw.WriteLine("BEGIN:VTIMEZONE");
                        sw.WriteLine(string.Format("TZID:{0}", timezone));
                        sw.WriteLine("X-LIC-LOCATION:{0}", timezone);
                        sw.WriteLine("BEGIN:DAYLIGHT");
                        sw.WriteLine("TZOFFSETFROM:+0000");
                        sw.WriteLine("TZOFFSETTO:+0100");
                        sw.WriteLine("TZNAME:BST");
                        sw.WriteLine("DTSTART:19700329T010000");
                        sw.WriteLine("RRULE:FREQ=YEARLY;BYMONTH=3;BYDAY=-1SU");
                        sw.WriteLine("END:DAYLIGHT");
                        sw.WriteLine("BEGIN:STANDARD");
                        sw.WriteLine("TZOFFSETFROM:+0100");
                        sw.WriteLine("TZOFFSETTO:+0000");
                        sw.WriteLine("TZNAME:GMT");
                        sw.WriteLine("DTSTART:19701025T020000");
                        sw.WriteLine("RRULE:FREQ=YEARLY;BYMONTH=10;BYDAY=-1SU");
                        sw.WriteLine("END:STANDARD");
                        sw.WriteLine("END:VTIMEZONE");

                        //Now each row
                        foreach (TaskActivity activity in this.ProvidedTaskActivities())
                        {
                            sw.WriteLine("BEGIN:VEVENT");
                            sw.WriteLine(string.Format("DTSTART;TZID={0}:{1}", timezone, activity.StartDate.ToString("yyyyMMddTHHmmss")));
                            sw.WriteLine(string.Format("DTEND;TZID={0}:{1}", timezone, activity.EndDate.ToString("yyyyMMddTHHmmss")));
                            sw.WriteLine(string.Format("DTSTAMP:{0}", activity.OriginalDate.ToUniversalTime()));
                            sw.WriteLine(string.Format("ORGANIZER;CN=\"{0}\":MAILTO:{1}", cname, emailaddress));
                            sw.WriteLine("UID:" + activity.Id.ToString());
                            sw.WriteLine("COMMENT:" + activity.Remarks);
                            sw.WriteLine("CLASS:PRIVATE");
                            sw.WriteLine(string.Format("CREATED:{0}", activity.OriginalDate.ToString("yyyyMMddTHHmmss")));
                            sw.WriteLine(string.Format("LAST-MODIFIED:{0}", activity.OriginalDate.ToString("yyyyMMddTHHmmss")));
                            sw.WriteLine("SEQUENCE:0");
                            sw.WriteLine("STATUS:CONFIRMED");
                            sw.WriteLine(string.Format("SUMMARY:{0}", activity.TaskDescription.Name));
                            sw.WriteLine("TRANSP:OPAQUE");
                            sw.WriteLine("X-MICROSOFT-CDO-BUSYSTATUS:BUSY");
                            sw.WriteLine("END:VEVENT");
                        }
                        sw.WriteLine("END:VCALENDAR");
                    }
                }
                catch (IOException ioe)
                {
                    MessageBox.Show(ioe.Message, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
