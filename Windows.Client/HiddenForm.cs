using BritishMicro.TaskClerk.Plugins;
using BritishMicro.TaskClerk.Properties;
using BritishMicro.TaskClerk.Providers;
using BritishMicro.TaskClerk.UI;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class HiddenForm : Form
    {
        private readonly string[] _args;

        /// <summary>
        /// Initializes a new instance of the <see cref="HiddenForm"/> class.
        /// </summary>
        /// <param name="args">The args.</param>
        public HiddenForm(string[] args)
        {
            _args = args;
            InitializeComponent();
            InitialiseEnvironment();
        }

        /// <summary>
        /// Handles the Load event of the Form control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Form_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            Hide();
            ConfigureCulture();
            ShowCurrentStatus();
            AppContext.Current.TaskActivitiesProvider.BeginningActivity
                += new EventHandler<TaskActivityEventArgs>(UpdateNotifyToolTip);
            UpdateNotifyToolTip(this, new TaskActivityEventArgs(AppContext.Current.CurrentActivity));
        }

        #region Worker Methods

        /// <summary>
        /// initialise the environment
        /// </summary>
        private void InitialiseEnvironment()
        {
            notifyIcon.BalloonTipText = Resources.BalloonTipText;
            notifyIcon.BalloonTipIcon = (ToolTipIcon)Enum.Parse(typeof(ToolTipIcon),
                                                                 Resources.BalloonTipIcon);
            notifyIcon.BalloonTipTitle = Resources.BalloonTipTitle;
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.Text = Resources.NotifyToolTip;

            //Insert the Explorer types into the context
            AppContext.Current.SettingsProvider.Set("TaskActivitiesExplorer", typeof(TaskActivitiesExplorer),
                                                    PersistHint.ThisSession);
            AppContext.Current.SettingsProvider.Set("TaskDescriptionsExplorer", typeof(TaskDescriptionsExplorer),
                                                    PersistHint.ThisSession);
            AppContext.Current.SettingsProvider.Set("OptionsExplorer", typeof(OptionsForm), PersistHint.ThisSession);
            ((WindowsUIProvider)AppContext.Current.UIProvider).NotifyIcon = notifyIcon;

            //Registration Service
            AppContext.Current.SettingsProvider.Set("RegistrationServer", Resources.RegistrationServer,
                                                    PersistHint.AcrossSessions);

            //Setup the nag message
            SetupNag((TaskActivity)AppContext.Current.SettingsProvider.Get("CurrentActivity", TaskActivity.Empty));

            RebuildDynamicMenu();
            LoadNotifyMenuPlugins();
        }

        //private void Current_InformationAvailableEvent(object o, InformationEventArgs ie)
        //{
        //    Trace.TraceInformation(ie.ToString());
        //}

        /// <summary>
        /// close the whole applocation down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseApplication(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// updates the notify icons tool tip.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.TaskActivityEventArgs"/> instance containing the event data.</param>
        void UpdateNotifyToolTip(object sender, TaskActivityEventArgs e)
        {
            TaskActivity current = e.TaskActivity;
            if (current.IsNotEmpty() && !current.TaskDescription.IsEvent)
            {
                if (current.TaskDescription.IsNotEmpty() & current.TaskDescription.IsEvent == false)
                {
                    int MAXSIZEOFNOTIFYTEXT = 63;
                    string notifyToolTip =
                        string.Format(CultureInfo.CurrentCulture,
                                      "{0}:{1}", Resources.NotifyToolTip, current.TaskDescription.Name);
                    notifyToolTip = notifyToolTip.Substring(
                        0,
                        (notifyToolTip.Length <= MAXSIZEOFNOTIFYTEXT ? notifyToolTip.Length : MAXSIZEOFNOTIFYTEXT));
                    notifyIcon.Text = notifyToolTip;
                }
            }
            else
            {
                notifyIcon.Text = Resources.NotifyToolTip;
            }
        }

        /// <summary>
        /// the primary menu builder
        /// </summary>
        private void RebuildDynamicMenu()
        {
            menuItemChangeActivity.MenuItems.Clear();
            Collection<TaskDescription> roots =
                AppContext.Current.TaskDescriptionsProvider.TaskDescriptions;
            if (roots != null)
            {
                foreach (TaskDescription taskDescription in roots)
                {
                    MenuItem menuItem = new MenuItem(taskDescription.Name);
                    menuItemChangeActivity.MenuItems.Add(menuItem);
                    menuItem.Enabled = taskDescription.IsValid();
                    menuItem.Tag = taskDescription;
                    menuItem.Click += new EventHandler(ChangeTask);
                    MenuBuilder(taskDescription, menuItem);
                }
            }

            //Add seperators
            MenuItem menuitemSeperator = new MenuItem("-");
            menuItemChangeActivity.MenuItems.Add(menuitemSeperator);

            //Add More...
            MenuItem menuitemMoreActivities = new MenuItem(Resources.TaskDescriptionMenuText)
            {
                Name = "menuitemMoreActivities"
            };
            menuItemChangeActivity.MenuItems.Add(menuitemMoreActivities);
            menuitemMoreActivities.Click += new EventHandler(ShowTaskSelector);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadNotifyMenuPlugins()
        {
            this.menuItemPluginSep.Visible = false;
            TaskClerkEngine engContext = AppContext.Current;
            foreach (LoadableItem pluginItem in AppContext.Current.PluginsProvider.Plugins)
            {
                if (pluginItem.IsSubclassOf(typeof(PluginNotifyMenuItem)))
                {
                    if (pluginItem.CreateInstance() is PluginNotifyMenuItem addinItem)
                    {
                        addinItem.PluginInit(engContext, contextMenu);
                        //addinItem.MenuItem.Tag = addinItem;
                        addinItem.MenuItem.Text = addinItem.MenuText;
                        addinItem.MenuItem.Click += delegate { addinItem.OnClick(EventArgs.Empty); };
                        contextMenu.MenuItems.Add(0, addinItem.MenuItem);
                        //becase at lease one plugin has been loaded make
                        //sure the seperator is visible.
                        this.menuItemPluginSep.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Configures the culture.
        /// </summary>
        private static void ConfigureCulture()
        {
            if (!(CultureInfo.CreateSpecificCulture(
                    (string)AppContext.Current.SettingsProvider.Get(
                                 "CurrentUserCulture", Thread.CurrentThread.CurrentCulture.Name)) is CultureInfo cultureInfo))
            {
                AppContext.Current.SettingsProvider.Set(
                    "CurrentUserCulture",
                    Thread.CurrentThread.CurrentCulture.Name,
                    PersistHint.AcrossSessions);
                cultureInfo = Thread.CurrentThread.CurrentCulture;
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
        }

        /// <summary>
        /// Handles the Popup event of the contextMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PopupContextMenu(object sender, EventArgs e)
        {
            foreach (MenuItem mi in contextMenu.MenuItems)
            {
                if (mi.Tag is PluginNotifyMenuItem pnmi)
                {
                    if ((pnmi.MenuText != null) && (pnmi.MenuText.Length > 0))
                    {
                        mi.Text = pnmi.MenuText;
                    }
                    else
                    {
                        mi.Text = pnmi.GetType().Name;
                    }
                }
            }
        }

        /// <summary>
        /// part of the menu builder tasks
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="parentMenu"></param>
        private void MenuBuilder(TaskDescription parent, MenuItem parentMenu)
        {
            int menubreak = 0;
            if (parent.Children != null)
            {
                foreach (TaskDescription child in parent.Children)
                {
                    if (child != null)
                    {
                        MenuItem menuItem = new MenuItem(child.Name)
                        {
                            Tag = child,
                            Enabled = child.IsValid()
                        };
                        if (child.IsEvent)
                        {
                            menuItem.Text = "[" + child.Name + "]";
                        }
                        menuItem.Click += new EventHandler(ChangeTask);
                        if (menubreak != child.MenuColumn)
                        {
                            menuItem.BarBreak = true;
                            menubreak = child.MenuColumn;
                        }

                        parentMenu.MenuItems.Add(menuItem);
                        MenuBuilder(child, menuItem);
                    }
                }
            }
        }

        /// <summary>
        /// show the current active task
        /// </summary>
        private void ShowCurrentStatus()
        {
            TaskActivity current =
                (TaskActivity)AppContext.Current.SettingsProvider.Get("CurrentActivity", TaskActivity.Empty) as
                TaskActivity;
            string message;
            if ((current.IsNotEmpty()) && (current.TaskDescription.IsNotEmpty()))
            {
                message = current.ToSummaryString();
            }
            else
            {
                message = Resources.BalloonTipText;
            }
            AppContext.Current.UIProvider.ShowNagMessage(message);
        }

        /// <summary>
        /// Show the nag message for the current active task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowNagBalloonTip(object sender, EventArgs e)
        {
            TaskActivity current =
                (TaskActivity)AppContext.Current.SettingsProvider.Get("CurrentActivity", TaskActivity.Empty) as
                TaskActivity;
            string message;
            if ((current.IsNotEmpty()) && (current.TaskDescription.IsNotEmpty()))
            {
                message = string.Format(CultureInfo.InvariantCulture, Resources.NagMessage, current.TaskDescription.Name, current.Duration);
            }
            else
            {
                message = Resources.BalloonTipText;
            }
            AppContext.Current.UIProvider.ShowNagMessage(message);
        }

        /// <summary>
        /// show the installed viewer
        /// </summary>
        private void ShowViewer(object sender, EventArgs e)
        {
            if (menuItemOpenViewer.Enabled == true)
            {
                menuItemOpenViewer.Enabled = false;
                AppContext.Current.UIProvider.ShowActivityExplorer();
                menuItemOpenViewer.Enabled = true;
            }
        }

        /// <summary>
        /// display the task selector form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowTaskSelector(object sender, EventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                menuItem.Enabled = false;
                UncheckTaskMenu(menuItemChangeActivity);
                AppContext.Current.SettingsProvider.Set("HideUseButton", false, PersistHint.AcrossSessions);
                TaskDescription selectedTask = AppContext.Current.ShowDescriptionExplorer();
                if (selectedTask.IsNotEmpty())
                {
                    AppContext.Current.HandleNewTaskActivity(selectedTask, DateTime.Now);
                    SetupNag(
                        (TaskActivity)AppContext.Current.SettingsProvider.Get("CurrentActivity", TaskActivity.Empty));
                }
                RebuildDynamicMenu();
                CheckTaskMenu();
                menuItem.Enabled = true;
            }
        }

        /// <summary>
        /// change the current task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeTask(object sender, EventArgs e)
        {
            UncheckTaskMenu(menuItemChangeActivity);
            if ((MenuItem)sender is MenuItem menuItem)
            {
                TaskDescription selected = (TaskDescription)menuItem.Tag;
                if (selected.IsDateBetweenValidPeriod(DateTime.Now) == false)
                {
                    MessageBox.Show(
                        string.Format(CultureInfo.CurrentCulture,
                                      Resources.InvalidActivity,
                                      selected.ValidFromDate,
                                      selected.ValidToDate),
                        Resources.InvalidActivityTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                AppContext.Current.HandleNewTaskActivity(selected, DateTime.Now);
                SetupNag((TaskActivity)AppContext.Current.SettingsProvider.Get("CurrentActivity", TaskActivity.Empty));
            }
            CheckTaskMenu();
        }

        /// <summary>
        /// setup the nag message for a task
        /// </summary>
        /// <param name="activity"></param>
        private void SetupNag(TaskActivity activity)
        {
            nagtimer.Enabled = false;
            if ((activity.IsNotEmpty()) & (activity.TaskDescription.NoNagMinutes > 0))
            {
                int MILLISECOND_MULTIPLIER = 60 * 1000;
                nagtimer.Interval = activity.TaskDescription.NoNagMinutes * MILLISECOND_MULTIPLIER;
                nagtimer.Enabled = true;
            }
        }

        /// <summary>
        /// navigate over the menu and uncheck all the menu items
        /// </summary>
        /// <param name="parent"></param>
        private void UncheckTaskMenu(MenuItem parent)
        {
            foreach (MenuItem menuItem in parent.MenuItems)
            {
                menuItem.Checked = false;
                UncheckTaskMenu(menuItem);
            }
        }

        /// <summary>
        /// navigate over the menu and check the current TaskDescription menuitem
        /// </summary>
        private void CheckTaskMenu()
        {
            TaskDescription selectedTask =
                (TaskDescription)AppContext.Current.SettingsProvider.Get(
                                      "SelectedTaskDescription", TaskDescription.Empty) as TaskDescription;
            if (selectedTask.IsNotEmpty())
            {
                MenuItem menuItem = FindMenuItem(menuItemChangeActivity, selectedTask.Id);
                if (menuItem != null)
                {
                    menuItem.Checked = true;
                }
            }
        }

        /// <summary>
        /// find a menuitem with a known id
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        private MenuItem FindMenuItem(MenuItem parent, Guid uniqueId)
        {
            MenuItem foundMenuItem = null;
            if (
                (parent.Tag != null) &&
                (parent.Tag.GetType() == typeof(TaskDescription)) &&
                (((TaskDescription)parent.Tag).Id == uniqueId)
                )
            {
                return parent;
            }
            else
            {
                if (parent.MenuItems.Count > 0)
                {
                    foreach (MenuItem childMenuItem in parent.MenuItems)
                    {
                        foundMenuItem = FindMenuItem(childMenuItem, uniqueId);
                        if (foundMenuItem != null)
                        {
                            break;
                        }
                    }
                }
            }
            return foundMenuItem;
        }

        #endregion
    }
}