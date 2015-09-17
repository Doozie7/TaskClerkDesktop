using System;
using BritishMicro.TaskClerk.Plugins;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

namespace BritishMicro.TaskClerk.ExportPack1
{
    /// <summary>
    /// Send Task Activities to Clipboard in XML.
    /// </summary>
    [DisplayName("Clipboard Exporter.")]
    [Description("This exporter sends the selected Task Activities\nto the local clipboard in an XML format.")]
    class ClipBoardXMLExporter : PluginExporter
    {
        /// <summary>
        /// The main export function
        /// </summary>
        public override void Execute()
        {
            Clipboard.SetText(ConvertActivities());
        }

        private string ConvertActivities()
        {
            List<TaskActivity> localActivities = new List<TaskActivity>(this.ProvidedTaskActivities());
            if (localActivities.Count > 0)
            {
                localActivities.Sort(TaskActivity.CompareTasksOnStartDate);
                using (MemoryStream memStream = new MemoryStream())
                {
                    XmlSerializer s = new XmlSerializer(typeof(List<TaskActivity>));
                    s.Serialize(memStream, localActivities);
                    return Encoding.Default.GetString(memStream.GetBuffer());
                }
            }
            return string.Empty;
        }
    }
}
