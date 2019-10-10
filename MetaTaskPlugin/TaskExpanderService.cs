using BritishMicro.TaskClerk.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MetaTaskPlugin
{

    public class TaskExpanderService : PluginService
    {

        private readonly List<DescriptionToExpand> activitiesToExpand = new List<DescriptionToExpand>();

        protected override void OnStartup()
        {
            base.OnStartup();

            DirectoryInfo dataFolder = new DirectoryInfo(System.Windows.Forms.Application.UserAppDataPath);

            FileInfo[] files = dataFolder.GetFiles("TaskExpanderConfig.xml");
            if (files.Length != 0)
            {

            }
            else
            {
                activitiesToExpand.Add(DescriptionToExpand.Example());

                FileInfo fileToSave = new FileInfo(dataFolder.FullName + @"\" + @"TaskExpanderConfig.xml");
                if (fileToSave != null)
                {
                    using (FileStream fileStream = fileToSave.Open(FileMode.Create, FileAccess.Write))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(List<DescriptionToExpand>));
                        s.Serialize(fileStream, activitiesToExpand);
                    }
                }
            }

            this.Engine.TaskActivitiesProvider.CompletingActivity += new EventHandler<BritishMicro.TaskClerk.TaskActivityEventArgs>(TaskActivitiesProvider_CompletingActivity);
        }

        protected override void OnShutdown()
        {
            this.Engine.TaskActivitiesProvider.CompletingActivity -= new EventHandler<BritishMicro.TaskClerk.TaskActivityEventArgs>(TaskActivitiesProvider_CompletingActivity);
            base.OnShutdown();
        }

        void TaskActivitiesProvider_CompletingActivity(object sender, BritishMicro.TaskClerk.TaskActivityEventArgs e)
        {
            //activitiesToExpand.ForEach(Expand(
        }
    }
}
