using BritishMicro.TaskClerk.Plugins;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.ExportPack1
{

    /// <summary>
    /// Comma Seperated File (CVS).
    /// </summary>
    [DisplayName("CSV Exporter (CSV)")]
    [Description("This exporter creates a comma delimited file.")]
    public class CSVExporter : PluginExporter
    {
        /// <summary>
        /// The main export function
        /// </summary>
        public override void Execute()
        {
            SaveFileDialog fd = new SaveFileDialog
            {
                Filter = "Comma Delimited (*.csv)|*.csv|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (DialogResult.OK == fd.ShowDialog())
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(fd.OpenFile()))
                    {
                        StringBuilder cscActivity = new StringBuilder();

                        //output the header first
                        cscActivity.Append(StringOut("Id"));
                        cscActivity.Append(StringOut("StartDate"));
                        cscActivity.Append(StringOut("EndDate"));
                        cscActivity.Append(NumberOut("Duration"));
                        cscActivity.Append(StringOut("CustomFlags"));
                        cscActivity.Append(StringOut("TaskDescription.UniqueId"));
                        cscActivity.Append(StringOut("TaskDescription.Name"));
                        cscActivity.Append(StringOut("UserId"));
                        sw.WriteLine(cscActivity.ToString());
                        cscActivity.Remove(0, cscActivity.Length);

                        //Now each row
                        foreach (TaskActivity activity in this.ProvidedTaskActivities())
                        {
                            cscActivity.Append(StringOut(activity.Id));
                            cscActivity.Append(StringOut(activity.StartDate));
                            cscActivity.Append(StringOut(activity.EndDate));
                            cscActivity.Append(NumberOut(activity.Duration));
                            cscActivity.Append(StringOut(activity.CustomFlags));
                            cscActivity.Append(StringOut(activity.TaskDescription.Id));
                            cscActivity.Append(StringOut(activity.TaskDescription.Name));
                            cscActivity.Append(StringOut(activity.UserId));
                            sw.WriteLine(cscActivity.ToString());
                            cscActivity.Remove(0, cscActivity.Length);
                        }
                    }
                }
                catch (IOException ioe)
                {
                    MessageBox.Show(ioe.Message, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string StringOut(object value)
        {
            string s = "";
            if (value != null)
            {
                s = value.ToString();
                if (s.Contains(","))
                {
                    s = (char)34 + s + (char)34;
                }
                s += ", ";
            }
            return s;
        }

        private string NumberOut(object value)
        {
            string s = "";
            if (value != null)
            {
                s += value.ToString();
            }
            s += ", ";
            return s;
        }
    }
}
