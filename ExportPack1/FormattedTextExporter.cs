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
    [DisplayName("Formatted Text Exporter (PRN)")]
    [Description("This formatter generates a text file\nusing a space to seperate fields.")]
    public class FormattedTextExporter : PluginExporter
    {
        /// <summary>
        /// The main export function
        /// </summary>
        public override void Execute()
        {
            SaveFileDialog fd = new SaveFileDialog
            {
                Filter = "Formatted Text (*.prn)|*.prn|All files (*.*)|*.*",
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
                        cscActivity.Append("Id");
                        cscActivity.Append("StartDate ");
                        cscActivity.Append("EndDate ");
                        cscActivity.Append("Duration ");
                        cscActivity.Append("CustomFlags ");
                        cscActivity.Append("TaskDescription.UniqueId ");
                        cscActivity.Append("TaskDescription.Name ");
                        cscActivity.Append("UserId");
                        sw.WriteLine(cscActivity.ToString());
                        cscActivity.Remove(0, cscActivity.Length);

                        //Now each row
                        foreach (TaskActivity activity in this.ProvidedTaskActivities())
                        {
                            cscActivity.Append(activity.Id.ToString("D") + " ");
                            cscActivity.Append(activity.StartDate.ToFileTimeUtc() + " ");
                            cscActivity.Append(activity.EndDate.ToFileTime() + " ");
                            cscActivity.Append(activity.Duration.ToString() + " ");
                            cscActivity.Append(activity.CustomFlags.ToString() + " ");
                            cscActivity.Append(activity.TaskDescription.Id.ToString("D") + " ");
                            cscActivity.Append((char)34 + activity.TaskDescription.Name + (char)34 + " ");
                            cscActivity.Append(activity.UserId);
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
    }
}
