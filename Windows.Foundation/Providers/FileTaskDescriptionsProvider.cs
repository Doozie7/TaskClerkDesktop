//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using BritishMicro.TaskClerk.Properties;
using System.Collections.ObjectModel;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public class FileTaskDescriptionsProvider : TaskDescriptionsProvider
    {

        /// <summary>
        /// 
        /// </summary>
        public FileTaskDescriptionsProvider()
        {
        }

        /// <summary>
        /// When overridden in a provider, it loads and returns the descriptions.
        /// </summary>
        /// <returns></returns>
        protected override Collection<TaskDescription> ProviderLoadDescriptions()
        {
            List<TaskDescription> taskDescriptions = null;
            if (SourceFile.Exists == false)
            {
                // use the default assembly resource
                using (StringReader sr = new StringReader(Resources.DefaultTaskDescriptions))
                {
                    taskDescriptions =
                        (List<TaskDescription>)new XmlSerializer(typeof(List<TaskDescription>)).Deserialize(sr);
                }
                // save the descriptions to the file strore
                using (FileStream fileStream = SourceFile.Open(FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer s = new XmlSerializer(typeof(List<TaskDescription>));
                    s.Serialize(fileStream, taskDescriptions);
                }
            }
            else
            {
                if (SourceFile.Length > 0)
                {
                    using (FileStream fileStream = SourceFile.Open(FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(List<TaskDescription>));
                        taskDescriptions = (List<TaskDescription>)s.Deserialize(fileStream);
                        taskDescriptions.Sort(TaskDescription.CompareTaskDescriptionOnSequence);
                    }
                }
            }
            return new Collection<TaskDescription>(taskDescriptions);
        }

        /// <summary>
        /// When overridden in a provider, it saves the descriptions.
        /// </summary>
        /// <param name="descriptions">The descriptions.</param>
        protected override void ProviderSaveDescriptions(Collection<TaskDescription> descriptions)
        {
            using (FileStream fileStream = SourceFile.Open(FileMode.Create, FileAccess.Write))
            {
                List<TaskDescription> temp = new List<TaskDescription>();
                temp.AddRange(descriptions);
                XmlSerializer s = new XmlSerializer(typeof(List<TaskDescription>));
                s.Serialize(fileStream, temp);
            }
        }

        /// <summary>
        /// When overridden in a provider, adds a description.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="description">The description.</param>
        protected override void ProviderAddDescription(TaskDescription parent, TaskDescription description)
        {
            Collection<TaskDescription> descriptions = ProviderLoadDescriptions();
            TaskDescription foundParent = FindChildInHierarchy(descriptions, parent);
            foundParent.Children.Add(description);
            ProviderSaveDescriptions(descriptions);
        }

        /// <summary>
        /// When overridden in a provider, removes a description.
        /// </summary>
        /// <param name="description">The description.</param>
        protected override void ProviderRemoveDescription(TaskDescription description)
        {
            Collection<TaskDescription> descriptions = ProviderLoadDescriptions();
            TaskDescription foundParent = null;
            foreach (TaskDescription child in descriptions)
            {
                foundParent = FindParentInHierarchy(child, description);
                if (foundParent != null)
                {
                    break;
                }
            }
            if (foundParent != null)
            {
                foundParent.Children.Remove(description);
                ProviderSaveDescriptions(descriptions);
            }
        }

        /// <summary>
        /// When overridden in a provider, archives the descriptions.
        /// </summary>
        protected override void ProviderArchive()
        {
            //No solution developed
        }

        private FileInfo SourceFile
        {
            get
            {
                DirectoryInfo dataFolder = new DirectoryInfo(System.Windows.Forms.Application.UserAppDataPath);
                string STRING_DESCRIPTIONSFILE = "TaskDescriptions.xml";
                string fileName = dataFolder + "\\" + STRING_DESCRIPTIONSFILE;

                return new FileInfo(fileName);
            }
        }
    }
}