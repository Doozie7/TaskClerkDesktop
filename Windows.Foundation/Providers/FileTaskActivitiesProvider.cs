using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Security;
using System.Diagnostics;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// This provider stores all TaskActivities in Files in a 
    /// location on the local computer.
    /// </summary>
    public class FileTaskActivitiesProvider : TaskActivitiesProvider
    {
        private const string STRING_FILEPATTERN = ".tc*.xml";
        private const string STRING_FILESTORE_PATTERN = "{0:yyyy-MM-dd}" + STRING_FILEPATTERN;
        private DirectoryInfo _dataFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTaskActivitiesProvider"/> class.
        /// </summary>
        public FileTaskActivitiesProvider()
        {
        }

        /// <summary>
        /// The OnInit method provides implementers with an opportunity to
        /// setup their providers after a valid taskclerk engine has been assigned to
        /// the Engine property.
        /// </summary>
        protected override void OnInit()
        {
            base.OnInit();
            _dataFolder = new DirectoryInfo(
                TryGetFromSettings(this, 
                "defaultDataFolder",
                System.Windows.Forms.Application.UserAppDataPath));
        }

        /// <summary>
        /// When overridden in a provider, it discovers the date metrics stored in the users data area.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>
        protected override Collection<DateTime> ProviderDiscoverDateMetrics(
            TaskActivitiesProvider.MetricQuestion question)
        {

            // This provider does not understand any question and alwys returns the dates
            // where data has been stored.

            Collection<DateTime> dateMetrics = new Collection<DateTime>();
            string message = "Hackers change this code.";
            message += " I loose.";
            bool registered =
                (Engine.SettingsProvider.Get("CurrentUserRegistrationKey", "").ToString().Length == 50
                     ?
                 true
                     : false);
            bool purchased = (bool) Engine.SettingsProvider.Get("CurrentUserRegistrationKeyPurchased", false);

            List<DateTime> storedDays = new List<DateTime>();
            FileInfo[] files = _dataFolder.GetFiles("*" + STRING_FILEPATTERN);
            foreach (FileInfo fileInfo in files)
            {
                // remove the file from the list
                if (fileInfo.Length == 0)
                {
                    fileInfo.MoveTo(fileInfo.FullName + "_err");
                    continue;
                }

                // process the file
                using (FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer s = new XmlSerializer(typeof (List<TaskActivity>));
                    foreach (TaskActivity ta in (List<TaskActivity>) s.Deserialize(fileStream))
                    {
                        if (storedDays.Contains(ta.StartDate) == false)
                        {
                            storedDays.Add(ta.StartDate);
                        }
                    }
                }
            }
            
            foreach (DateTime dt in storedDays)
            {
                dateMetrics.Add(dt);
            }
            return dateMetrics;
        }

        /// <summary>
        /// When overridden in a provider, it loads and returns the activities based on the supplied parameters.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="allowedTaskDescriptions">The allowed task descriptions.</param>
        /// <returns></returns>
        protected override Collection<TaskActivity> ProviderLoadActivities(
            DateTime start, DateTime end, Collection<TaskDescription> allowedTaskDescriptions)
        {
            Collection<TaskActivity> final = new Collection<TaskActivity>();
            double daysDiff = ((TimeSpan)end.Subtract(start)).TotalDays;
            for (double loop = 0; loop <= daysDiff; loop++)
            {
                DateTime current = start.AddDays(loop);
                FileInfo fi = FileInfoFromDate(current);
                List<TaskActivity> activities = new List<TaskActivity>();
                if ((fi != null) && (fi.Exists) && (fi.Length != 0))
                {
                    using (FileStream fileStream = fi.Open(FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(List<TaskActivity>));
                        activities = (List<TaskActivity>)s.Deserialize(fileStream);
                    }
                }

                foreach (TaskActivity ta in activities)
                {
                    if (allowedTaskDescriptions.Count != 0)
                    {
                        if (!allowedTaskDescriptions.Contains(ta.TaskDescription))
                        {
                            continue;
                        }
                    }
                    final.Add(ta);
                }
            }
            return final;
        }

        /// <summary>
        /// When overridden in a provider, it saves the activities.
        /// </summary>
        /// <param name="activities">The activities.</param>
        protected override void ProviderSaveActivities(Collection<TaskActivity> activities)
        {
            if (activities.Count > 0)
            {
                List<TaskActivity> localActivities = new List<TaskActivity>(activities);
                localActivities.Sort(TaskActivity.CompareTasksOnStartDate);
                FileInfo fileInfo = FileInfoFromDate(localActivities[0].StartDate);
                if (fileInfo != null)
                {
                    using (FileStream fileStream = fileInfo.Open(FileMode.Create, FileAccess.Write))
                    {
                        XmlSerializer s = new XmlSerializer(typeof (List<TaskActivity>));
                        s.Serialize(fileStream, localActivities);
                    }
                }
            }
        }

        /// <summary>
        /// When overridden in a provider, it adds an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected override void ProviderBeginActivity(TaskActivity activity)
        {
        }

        /// <summary>
        /// When overridden in a provider, it completes an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected override void ProviderCompleteActivity(TaskActivity activity)
        {
            Collection<TaskActivity> activities = LoadActivities(activity.StartDate);
            if (activities.Contains(activity))
            {
                activities.Remove(activity);
            }
            activities.Add(activity);
            SaveActivities(activities);   
        }

        /// <summary>
        /// When overridden in a provider, it removes an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected override void ProviderRemoveActivity(TaskActivity activity)
        {
            Collection<TaskActivity> activities = LoadActivities(activity.StartDate);
            if (activities.Contains(activity))
            {
                // If we are deleting the last activity in the file 
                //  just delete the file
                if (activities.Count <= 1)
                {
                    FileInfoFromDate(activity.StartDate).Delete();
                }
                else
                {
                    activities.Remove(activity);
                    SaveActivities(activities);
                }
            }
        }

        /// <summary>
        /// When overridden in a provider, it performs an archive action on the activity data.
        /// </summary>
        protected override void ProviderArchive()
        {
            string destinationfolder = (string)Engine.SettingsProvider.Get("BackupDirectory", string.Empty);
            if (destinationfolder.Length > 0)
            {
                DirectoryInfo source = new DirectoryInfo(System.Windows.Forms.Application.UserAppDataPath);
                DirectoryInfo destination = new DirectoryInfo(destinationfolder);
                Copy(source, destination);
            }
        }

        #region private code

        /// <summary>
        /// Create a FileInfo object from the supplied date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        private FileInfo FileInfoFromDate(DateTime date)
        {
            const string STRING_FILEPATTERN = ".tc*.xml";
            const string STRING_FILESTORE_PATTERN = "{0:yyyy-MM-dd}" + STRING_FILEPATTERN;

            FileInfo result = null;
            string filename = string.Format(STRING_FILESTORE_PATTERN, date);
            FileInfo[] files = _dataFolder.GetFiles(filename);
            if (files.Length > 0)
            {
                result = files[0];
            }
            else
            {
                FileInfo fileInfo = null;
                filename = string.Format(_dataFolder.FullName + "\\" + STRING_FILESTORE_PATTERN, date);
                filename = filename.Replace("*", "");
                fileInfo = new FileInfo(filename);
                fileInfo.Refresh();
                result = fileInfo;
            }
            return result;
        }

        /// <summary>
        /// Copies the specified source directory to the specified destination directory.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        private static void Copy(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            foreach (FileInfo sourceFile in source.GetFiles())
            {
                try
                {
                    sourceFile.CopyTo(destination.FullName + @"\" + sourceFile.Name, true);
                }
                catch (IOException ioe)
                {
                    MessageBox.Show(ioe.Message, "CopyTo Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (SecurityException se)
                {
                    MessageBox.Show(se.Message, "CopyTo Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (UnauthorizedAccessException uae)
                {
                    MessageBox.Show(uae.Message, "CopyTo Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            foreach (DirectoryInfo subdirectory in source.GetDirectories())
            {
                DirectoryInfo subDestination = new DirectoryInfo(destination.FullName + @"\" + subdirectory.Name);
                Copy(subdirectory, new DirectoryInfo(destination.FullName + @"\" + subdirectory.Name));
            }
        }

        #endregion

    }
}