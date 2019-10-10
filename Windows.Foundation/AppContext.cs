using BritishMicro.TaskClerk.Properties;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Represent the context for the application (TaskClerkEngine)
    /// </summary>
    public class AppContext : TaskClerkEngine
    {
        #region Static

        private static AppContext __singleton;

        /// <summary>
        /// Gets the current instance of the AppContext (singleton).
        /// </summary>
        /// <value>The current.</value>
        public static AppContext Current
        {
            get
            {
                if (__singleton == null)
                {
                    __singleton = new AppContext();
                }
                return __singleton;
            }
        }

        #endregion

        private const string STRING_CURRENTTASK = "CurrentActivity";
        private const string STRING_PREVIOUSTASK = "PreviousActivity";
        private const string STRING_SELECTEDTASKDESCRIPTION = "SelectedTaskDescription";
        //private const string STRING_ISOROOTFOLDER = "IsoRootFolder";
        private const string STRING_APPINSTANCEKEY = "AppInstanceKey";
        private const string STRING_APPINSTANCECREATEDATE = "AppInstanceCreateDate";

        private bool _descriptionExplorerVisible;

        /// <summary>
        /// Initialises the application context, this is private because I only want
        /// one per application instance (Singleton)
        /// </summary>
        private AppContext()
        {
            try
            {
                Initialise();
            }
            catch (InvalidOperationException ioe)
            {
                Trace.TraceError("AppContext:ctor[{0}]", ioe.Message);
                MessageBox.Show(ioe.Message, "Application Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            catch (Exception e)
            {
                Trace.TraceError("AppContext:ctor[{0}]", e.Message);
                throw new InvalidOperationException(Resources.AppContextCreateFailure, e);
            }
        }

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        public void Initialise()
        {
            if (SettingsProvider == null)
            {
                return;
            }

            if (SettingsProvider.Get(STRING_APPINSTANCEKEY, string.Empty).ToString().Length == 0)
            {
                SettingsProvider.Set(STRING_APPINSTANCEKEY, Guid.NewGuid().ToString("D"), PersistHint.AcrossSessions);
                SettingsProvider.Set(STRING_APPINSTANCECREATEDATE, DateTime.Now.ToString("F"),
                                     PersistHint.AcrossSessions);
            }

            TaskDescriptionsProvider.LoadDescriptions();

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            Trace.TraceInformation("AppContext:Initialise");
        }

        /// <summary>
        /// Tasks the clerk engine start.
        /// </summary>
        public override void StartEngine()
        {
            base.StartEngine();

            DateTime lastShutdown = (DateTime)SettingsProvider.Get("LastStopEngine", DateTime.MinValue);
            if (lastShutdown != DateTime.MinValue)
            {
                ForcedShutDownDetected(lastShutdown);
            }

            this.HeartBeat += AppContext_HeartBeat;

            bool startTaskActivity = (bool)SettingsProvider.Get("StartTaskEnabled", false);
            if (startTaskActivity == true)
            {
                TaskDescription taskDescription =
                    (TaskDescription)SettingsProvider.Get("StartTaskDescription", TaskDescription.Empty);
                if (taskDescription.IsNotEmpty())
                {
                    HandleNewTaskActivity(taskDescription, DateTime.Now);
                }

                taskDescription =
                    SettingsProvider.Get("StartTaskDescription2", TaskDescription.Empty) as TaskDescription;
                if (taskDescription != null && taskDescription.IsNotEmpty())
                {
                    HandleNewTaskActivity(taskDescription, DateTime.Now);
                }
            }

            BackupData();

            Trace.TraceInformation(
                "AppContext:TaskClerkEngineStart[TaskClerk has successfully started and is ready for user input.]");
        }

        /// <summary>
        /// Handles the HeartBeat event of the AppContext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void AppContext_HeartBeat(object sender, EventArgs e)
        {
            Trace.TraceInformation(string.Format("AppContext:AppContext_HeartBeat Event @ {0}", DateTime.Now.ToShortTimeString()));
        }

        /// <summary>
        /// Tasks the clerk engine end.
        /// </summary>
        public override void StopEngine()
        {
            base.StopEngine();

            SettingsProvider.Set("AppEndTime", DateTime.Now, PersistHint.AcrossSessions);
            bool stopTaskActivity = (bool)SettingsProvider.Get("StopTaskEnabled", false);
            if (stopTaskActivity == true)
            {
                TaskDescription taskDescription =
                    (TaskDescription)SettingsProvider.Get("StopTaskDescription", TaskDescription.Empty);
                if (taskDescription.IsNotEmpty())
                {
                    HandleNewTaskActivity(taskDescription, DateTime.Now);
                }
            }
            Trace.TraceInformation("AppContext:TaskClerkEngineEnd[TaskClerk has successfully shutdown.]");
            Application.Exit();
        }

        /// <summary>
        /// Forceds the shut down detected.
        /// </summary>
        /// <param name="lastHeartBeat">The last heart beat.</param>
        private void ForcedShutDownDetected(DateTime lastHeartBeat)
        {
            bool stopTaskActivity = (bool)SettingsProvider.Get("StopTaskEnabled", false);
            if (stopTaskActivity == true)
            {
                if (SettingsProvider.Get("StopTaskDescription", TaskDescription.Empty) is TaskDescription taskDescription && taskDescription.IsNotEmpty())
                {
                    HandleNewTaskActivity(taskDescription, lastHeartBeat); //.AddMinutes(5));
                    Trace.TraceInformation("AppContext:ForcedShutDownDetected");
                }
                else
                {
                    Trace.TraceInformation("AppContext:ForcedShutDownDetected - Not handled");
                }
            }
        }

        /// <summary>
        /// Backups the data.
        /// </summary>
        private void BackupData()
        {
            string frequency = (string)SettingsProvider.Get("BackupFrequency", "Never");
            DateTime lastBackup = (DateTime)SettingsProvider.Get("LastBackup", DateTime.MinValue);
            if (AppContext.Current.TaskActivitiesProvider.Backup(frequency, lastBackup))
            {
                AppContext.Current.SettingsProvider.Set("LastBackup", DateTime.Now, PersistHint.AcrossSessions);
            }
            Trace.TraceInformation("AppContext:BackupData");
        }

        /// <summary>
        /// Provides the business logic for handling a new TaskActivity
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        /// <param name="effectiveDateTime">The effective date time.</param>
        public void HandleNewTaskActivity(TaskDescription taskDescription, DateTime effectiveDateTime)
        {
            HandleNewTaskActivity(taskDescription, null, effectiveDateTime);
        }

        /// <summary>
        /// Provides the business logic for handling a new TaskActivity
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        /// <param name="crossTabTaskDescription">The task description for cross tab.</param>
        /// <param name="effectiveDateTime">The effective date time.</param>
        public void HandleNewTaskActivity(TaskDescription taskDescription, TaskDescription crossTabTaskDescription,
            DateTime effectiveDateTime)
        {
            if (taskDescription == null)
            {
                throw new ArgumentNullException("taskDescription");
            }

            if (taskDescription.IsNotEmpty())
            {
                // update SelectedTaskDescription
                SettingsProvider.Set(STRING_SELECTEDTASKDESCRIPTION, taskDescription, PersistHint.AcrossSessions);

                // Close off the current activity
                TaskActivity currentActivity =
                    (TaskActivity)SettingsProvider.Get(STRING_CURRENTTASK, TaskActivity.Empty);
                if (currentActivity.IsNotEmpty())
                {
                    if (currentActivity.EndDate == DateTime.MinValue)
                    {
                        currentActivity.EndDate = effectiveDateTime;
                        currentActivity.UserId = IdentityProvider.Principal.Identity.Name;
                        TaskActivitiesProvider.CompleteActivity(currentActivity);
                    }
                    SettingsProvider.Set(STRING_PREVIOUSTASK, currentActivity, PersistHint.AcrossSessions);
                }

                // create a new task activity
                TaskActivity newActivity = new TaskActivity(taskDescription, IdentityProvider.Principal.Identity.Name)
                {
                    StartDate = effectiveDateTime
                };
                if (crossTabTaskDescription != null)
                    newActivity.CrosstabTaskDescription = crossTabTaskDescription;
                SettingsProvider.Set(STRING_CURRENTTASK, newActivity, PersistHint.AcrossSessions);
                TaskActivitiesProvider.BeginActivity(newActivity);

                if (taskDescription.IsEvent)
                {
                    // because the selected event is an event also close it off
                    newActivity.EndDate = newActivity.StartDate.AddSeconds(1);
                    newActivity.UserId = IdentityProvider.Principal.Identity.Name;
                    TaskActivitiesProvider.CompleteActivity(newActivity);
                    // now set the current activity to null
                    SettingsProvider.Set(STRING_CURRENTTASK, TaskActivity.Empty, PersistHint.AcrossSessions);
                }
                Trace.TraceInformation("AppContext:HandleNewTaskActivity");
            }
        }

        /// <summary>
        /// Shows the description explorer.
        /// </summary>
        /// <returns></returns>
        public TaskDescription ShowDescriptionExplorer()
        {
            if (_descriptionExplorerVisible == false)
            {
                _descriptionExplorerVisible = true;
                Type type = (Type)SettingsProvider.Get("TaskDescriptionsExplorer");
                Current.ShowForm(type);
                _descriptionExplorerVisible = false;
            }
            return (TaskDescription)SettingsProvider.Get("SelectedTaskDescription", TaskDescription.Empty);
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="formType">Type of the form.</param>
        /// <returns></returns>
        private DialogResult ShowForm(Type formType)
        {
            if (formType == null)
            {
                throw new ArgumentNullException("formType");
            }

            DialogResult dr = DialogResult.Ignore;
            if ((Form)Activator.CreateInstance(formType) is Form form)
            {
                form.ClientSize = (Size)SettingsProvider.Get(
                                             string.Format("{0}ClientSize", formType.Name), form.Size);
                form.Location = (Point)SettingsProvider.Get(
                                            string.Format("{0}Location", formType.Name), form.Location);

                form.WindowState = (FormWindowState)SettingsProvider.Get(
                                                         string.Format("{0}FormWindowState", formType.Name),
                                                         FormWindowState.Normal);

                form.TopLevel = true;
                dr = form.ShowDialog();
                if (form.WindowState != FormWindowState.Normal)
                {
                    SettingsProvider.Set(string.Format("{0}FormWindowState", formType.Name), form.WindowState,
                                         PersistHint.AcrossSessions);
                }
                else
                {
                    SettingsProvider.Set(string.Format("{0}FormWindowState", formType.Name), form.WindowState,
                                         PersistHint.AcrossSessions);
                    SettingsProvider.Set(string.Format("{0}ClientSize", formType.Name), form.ClientSize,
                                         PersistHint.AcrossSessions);
                    SettingsProvider.Set(string.Format("{0}Location", formType.Name), form.Location,
                                         PersistHint.AcrossSessions);
                }
            }
            return dr;
        }

        /// <summary>
        /// Registries the setup.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        public void RegistrySetup(Assembly assembly, bool enable)
        {
            string assembleyName = assembly.GetName().Name;
            RegistryKey key =
                Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key == null)
            {
                key =
                    Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
            }
            if (enable == true)
            {
                key.SetValue(assembleyName, string.Format("\"{0}\"", assembly.Location));
            }
            else
            {
                if (key.GetValue(assembleyName) != null)
                {
                    key.DeleteValue(assembleyName, false);
                }
            }
        }

        /// <summary>
        /// Determines whether [is auto start registry set] [the specified assembly].
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// 	<c>true</c> if [is auto start registry set] [the specified assembly]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAutoStartRegistrySet(Assembly assembly)
        {
            string assembleyName = assembly.GetName().Name;
            RegistryKey key =
                Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key != null)
            {
                if (key.GetValue(assembleyName) != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Handles the PowerModeChanged event of the SystemEvents control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Win32.PowerModeChangedEventArgs"/> instance containing the event data.</param>
        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend)
            {
                SuspendApplication();
            }
        }

        /// <summary>
        /// Suspends the application.
        /// </summary>
        public void SuspendApplication()
        {
            bool suspendTaskActivity = (bool)SettingsProvider.Get("SuspendTaskEnabled", false);
            if (suspendTaskActivity == true)
            {
                TaskDescription taskDescription =
                    (TaskDescription)SettingsProvider.Get("SuspendTaskDescription", TaskDescription.Empty);
                if (taskDescription.IsNotEmpty())
                {
                    HandleNewTaskActivity(taskDescription, DateTime.Now);
                }
            }
        }

        /// <summary>
        /// Gets the assembly title.
        /// </summary>
        /// <value>The assembly title.</value>
        public static string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title.Length > 0)
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        /// <value>The assembly version.</value>
        public static string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        /// <summary>
        /// Gets the assembly description.
        /// </summary>
        /// <value>The assembly description.</value>
        public static string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// Gets the assembly product.
        /// </summary>
        /// <value>The assembly product.</value>
        public static string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Gets the assembly copyright.
        /// </summary>
        /// <value>The assembly copyright.</value>
        public static string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// Gets the assembly company.
        /// </summary>
        /// <value>The assembly company.</value>
        public static string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
    }
}