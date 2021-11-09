using BritishMicro.TaskClerk.Plugins;
using BritishMicro.TaskClerk.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// The TaskActivities dialog provides the user with a TaskActivity
    /// information manager and the ability to change and manage their
    /// TaskActivity information.
    /// </summary>
    public partial class TaskActivitiesExplorer : Form
    {
        private Size _formSize;
        private TreeNode _ensureVisibleNode;
        private readonly TaskClerkEngine _engine;
        //private TaskActivity _currentActivity;
        private Collection<DateTime> _days;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivitiesExplorer"/> class.
        /// </summary>
        public TaskActivitiesExplorer()
        {
            InitializeComponent();

            _engine = AppContext.Current;

            languageToolStripStatusLabel.Text = Thread.CurrentThread.CurrentCulture.Name;
            optionsToolStripMenuItem.Image = Resources.Options;
            webSiteToolStripMenuItem.Image = Resources.Home;
            statusPurchased.Image = Resources.Lock;
            statusRegistered.Image = Resources.Key;

            if (DesignMode == false)
            {
                LoadToolMenuPlugins();
            }
        }

        private void LoadToolMenuPlugins()
        {
            int InsertPoint = toolsToolStripMenuItem.DropDownItems.IndexOf(toolsSepEnd);
            foreach (LoadableItem pluginItem in _engine.PluginsProvider.Plugins)
            {
                if (pluginItem.IsSubclassOf(typeof(PluginToolMenuItem)))
                {
                    if (pluginItem.CreateInstance() is PluginToolMenuItem addinItem)
                    {
                        addinItem.PluginInit(_engine, toolsToolStripMenuItem);
                        ToolStripMenuItem menuItem = new ToolStripMenuItem(addinItem.Text)
                        {
                            Tag = addinItem,
                            Image = addinItem.Image
                        };
                        menuItem.Click += delegate { addinItem.OnClick(EventArgs.Empty); };
                        toolsToolStripMenuItem.DropDownItems.Insert(InsertPoint, menuItem);
                    }
                }
            }
        }

        private void TaskActivitiesExplorer_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                SuspendLayout();

                treeView.Font = (Font)_engine.SettingsProvider.Get("GeneralFont", Font);

                splitContainerDetail.Panel1Collapsed =
                    !(bool)_engine.SettingsProvider.Get("Show Navigator Tree", true);

                splitContainer.Panel2Collapsed =
                    !(bool)_engine.SettingsProvider.Get("Show Recorded Activities Grid", true);

                if (((bool)_engine.SettingsProvider.Get("Show Current User", true)))
                {
                    systemUserToolStripStatusLabel.Text =
                        _engine.IdentityProvider.Principal.Identity.Name;
                }

                if (((bool)_engine.SettingsProvider.Get("Show Web Browser", true)))
                {
                    webBrowser.Visible = true;
                }
                else
                {
                    webBrowser.Visible = false;
                }

                statusRegistered.Visible = false;
                if (_engine.SettingsProvider.Get("CurrentUserRegistrationKey", "").ToString().Length == 50)
                {
                    statusRegistered.Visible = true;
                }
                if ((bool)_engine.SettingsProvider.Get("CurrentUserRegistrationKeyPurchased", false) == true)
                {
                    statusPurchased.Visible = true;
                }

                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Normal;
                }

                splitContainer.SplitterDistance =
                    (int)_engine.SettingsProvider.Get(
                              "HorizontalSplitterDistance", splitContainer.SplitterDistance);
                splitContainerDetail.SplitterDistance =
                    (int)_engine.SettingsProvider.Get(
                              "VerticalSplitterDistance", splitContainerDetail.SplitterDistance);

                //webBrowser.Url = new Uri(
                //    (string)_engine.SettingsProvider.Get(
                //              "WebStartPage", Resources.WebStartPage)
                //    );

                _days = _engine.TaskActivitiesProvider.DiscoverDateMetrics(
                    BritishMicro.TaskClerk.Providers.TaskActivitiesProvider.MetricQuestion.AvailableDays);

                UpdateStatusBarText();

                RebuildTree();
                RefreshViews(DateTime.Now);
                _engine.TaskActivitiesProvider.TaskActivitiesChanged +=
                    new EventHandler<TaskActivityEventArgs>(Current_TaskActivitiesChangedEvent);
                Activate();
                ResumeLayout();
                _formSize = Size;
            }
        }

        private void UpdateStatusBarText()
        {
            TaskActivity current = _engine.SettingsProvider.Get("CurrentActivity", TaskActivity.Empty) as TaskActivity;
            if (current.IsNotEmpty())
            {
                toolStripStatusLabel.Text = current.ToSummaryString().Replace("\r\n", " ");
            }
            else
            {
                toolStripStatusLabel.Text = "There is no activity running.";
            }
        }

        private void Current_ConfigurationChangedEvent(object sender, EventArgs e)
        {
            languageToolStripStatusLabel.Text = Thread.CurrentThread.CurrentCulture.Name;
        }

        private void RefreshViews(object info)
        {
            if (DesignMode == false)
            {
                SuspendLayout();
                if (info != null)
                {
                    if (info.GetType() == typeof(DateTime))
                    {
                        DateTime date = (DateTime)info;
                        if (date != DateTime.MinValue)
                        {
                            _engine.SettingsProvider.Set("Selected Explorer Date", date, PersistHint.AcrossSessions);
                            RefreshViews(date);
                        }
                    }

                    if (info.GetType() == typeof(List<DateTime>))
                    {
                        List<DateTime> range = GenerateDateRange((List<DateTime>)info);
                        Collection<TaskActivity> focusActivities = _engine.TaskActivitiesProvider.LoadActivities(range[0], range[range.Count - 1]);

                        if ((focusActivities != null) && (focusActivities.Count > 0))
                        {
                            recordedTasks1.Reset();
                            propertiesToolStripMenuItem.Enabled = false;
                            recordedTasks1.TabStop = false;
                            activityChart1.Reset(range[0], focusActivities);
                            graphOptionsToolStripMenuItem.Enabled = true;
                            webBrowser.Visible = false;
                            webBrowser.TabStop = false;
                        }
                    }
                }
                else
                {
                    recordedTasks1.Reset();
                    propertiesToolStripMenuItem.Enabled = false;
                    recordedTasks1.TabStop = false;
                    activityChart1.Reset();
                    graphOptionsToolStripMenuItem.Enabled = false;
                    if (((bool)_engine.SettingsProvider.Get("Show Web Browser", true)))
                    {
                        webBrowser.Visible = true;
                    }
                    webBrowser.TabStop = true;
                }
                ResumeLayout();
            }
        }

        private void RefreshViews(DateTime date)
        {
            if (!DesignMode)
            {
                Collection<TaskActivity> activities = _engine.TaskActivitiesProvider.LoadActivities(date);
                if (activities != null)
                {
                    webBrowser.Visible = false;
                    webBrowser.TabStop = false;
                    recordedTasks1.Reset(date, activities);
                    propertiesToolStripMenuItem.Enabled = true;
                    recordedTasks1.TabStop = true;
                    activityChart1.Reset(date, activities);
                    graphOptionsToolStripMenuItem.Enabled = true;
                }
                else
                {
                    recordedTasks1.Reset();
                    propertiesToolStripMenuItem.Enabled = false;
                    recordedTasks1.TabStop = false;
                    activityChart1.Reset();
                    graphOptionsToolStripMenuItem.Enabled = false;
                    webBrowser.Visible = true;
                    //webBrowser.Url = new Uri(@"http://localhost:63112/NoActivities.htm");
                    webBrowser.TabStop = true;
                }
            }
        }

        private void RebuildTree()
        {
            RebuildTree("");
        }

        private void RebuildTree(string selectedNode)
        {
            treeView.Nodes.Clear();
            BuildTree(selectedNode);
        }

        private void BuildTree(string selectedNode)
        {
            treeView.SuspendLayout();
            treeView.AfterSelect -= new TreeViewEventHandler(treeView_AfterSelect);
            int found = 0;
            TreeNode defaultNode = null;
            string userName = _engine.IdentityProvider.Principal.Identity.Name;
            if ((userName == null) || (userName.Length == 0))
            {
                Trace.TraceError("TaskActivitiesExplorer:BuildTree[Unable to determine the current user.]");
                throw new ArgumentNullException("Unable to determine the current user.");
            }

            TreeNode root = new TreeNode(userName)
            {
                Name = SafeName(userName),
                ImageIndex = 1,
                SelectedImageIndex = 1
            };
            foreach (DateTime dt in _days)
            {
                TreeNode year = new TreeNode(dt.Year.ToString())
                {
                    Tag = new List<DateTime>()
                };
                DateTime syd = new DateTime(dt.Year, 1, 1);
                DateTime eyd = new DateTime(dt.AddYears(1).Year, 1, 1).AddDays(-1);
                ((List<DateTime>)year.Tag).Add(syd);
                ((List<DateTime>)year.Tag).Add(eyd);
                year.ToolTipText = string.Format("{0:dd MMMM yyyy} to {1:dd MMMM yyyy}", syd, eyd);
                foreach (TreeNode tn in root.Nodes)
                {
                    if (tn.Text == year.Text)
                    {
                        year = tn;
                        found = 1;
                    }
                }
                if (found == 0)
                {
                    root.Nodes.Add(year);
                    year.Name = SafeName(year.Text);
                }
                found = 0;

                TreeNode month = new TreeNode(string.Format("{0:MMMM}", dt))
                {
                    Tag = new List<DateTime>()
                };
                DateTime smd = new DateTime(dt.Year, dt.Month, 1);
                DateTime emd = new DateTime(dt.AddMonths(1).Year, dt.AddMonths(1).Month, 1).AddDays(-1);
                ((List<DateTime>)month.Tag).Add(smd);
                ((List<DateTime>)month.Tag).Add(emd);
                month.ToolTipText = string.Format("{0:dd MMMM yyyy} to {1:dd MMMM yyyy}", smd, emd);
                foreach (TreeNode tn in year.Nodes)
                {
                    if (tn.Text == month.Text)
                    {
                        month = tn;
                        found = 1;
                    }
                }
                if (found == 0)
                {
                    year.Nodes.Add(month);
                    month.Name = SafeName(month.Text);
                }
                found = 0;

                TreeNode day = new TreeNode(dt.ToString("dd - dddd"))
                {
                    Tag = dt
                };
                foreach (TreeNode tn in month.Nodes)
                {
                    if (tn.Text == day.Text)
                    {
                        day = tn;
                        found = 1;
                    }
                }
                if (found == 0)
                {
                    month.Nodes.Add(day);
                    day.Name = SafeName(day.Text);
                    if (DateTime.Now.Date == dt.Date)
                    {
                        _ensureVisibleNode = day;
                        defaultNode = day;
                    }
                }
            }
            treeView.Nodes.Clear();
            treeView.Nodes.Add(root);
            treeView.SelectedNode = _ensureVisibleNode;

            if (selectedNode.Length != 0)
            {
                TreeNode[] foundNode = treeView.Nodes.Find(selectedNode, true);
                if (foundNode.Length > 0)
                {
                    treeView.SelectedNode = foundNode[0];
                }
            }
            else
            {
                treeView.SelectedNode = defaultNode;
            }

            treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
            treeView.ResumeLayout();
        }

        private string SafeName(string text)
        {
            return text.Replace(" ", "_");
        }

        private List<DateTime> GenerateDateRange(List<DateTime> limits)
        {
            List<DateTime> range = new List<DateTime>();
            TimeSpan diff = limits[1].Subtract(limits[0]);
            for (int day = 0; day <= diff.TotalDays; day++)
            {
                range.Add(limits[0].AddDays(day));
            }
            return range;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBoxForm about = new AboutBoxForm();
            about.ShowDialog(this);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _engine.UIProvider.ShowOptionsExplorer();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RefreshViews(e.Node.Tag);
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((treeView.SelectedNode != null)
                && (treeView.SelectedNode.Tag != null)
                && (treeView.SelectedNode.Tag.GetType() == typeof(DateTime)))
            {
                splitContainerDetail.Visible = false;
            }
        }

        private void chartViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainerDetail.Visible = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
            Close();
        }

        private void webSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(string.Format(Resources.TaskClerkHomePage));
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string safeurl = AppContext.AssemblyVersion.Replace(".", "_");
            Process.Start(string.Format(Resources.TaskClerkUpdates, safeurl));
        }

        private void productFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(string.Format(Resources.TaskClerkFeedback));
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            splitContainerDetail.Panel2Collapsed = !splitContainerDetail.Panel2Collapsed;
        }

        private void shutdowmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            UpdateStatusBarText();
        }

        private void Current_TaskActivitiesChangedEvent(object sender, TaskActivityEventArgs e)
        {
            if (e == TaskActivityEventArgs.Empty)
            {
                return;
            }

            _days = _engine.TaskActivitiesProvider.DiscoverDateMetrics(
                BritishMicro.TaskClerk.Providers.TaskActivitiesProvider.MetricQuestion.AvailableDays);

            if ((treeView.SelectedNode != null) && (treeView.SelectedNode.Tag != null))
            {
                if (treeView.SelectedNode.Tag.GetType() == typeof(DateTime))
                {
                    if (e.TaskActivity.StartDate.ToShortDateString() !=
                        ((DateTime)treeView.SelectedNode.Tag).ToShortDateString())
                    {
                        RebuildTree(treeView.SelectedNode.Name);
                    }
                    if (treeView.SelectedNode != null)
                    {
                        RefreshViews(treeView.SelectedNode.Tag);
                    }
                }
            }
            else
            {
                RebuildTree();
            }
            //_currentActivity = e.TaskActivity;
            UpdateStatusBarText();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportForm ef = new ExportForm();
            ef.ShowDialog(this);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();

            if (recordedTasks1.Focused)
            {
                List<TaskActivity> selectedActivities = recordedTasks1.GetSelectedTaskActivities();
                if (selectedActivities.Count > 0)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        new XmlSerializer(typeof(List<TaskActivity>)).Serialize(memoryStream, selectedActivities);
                        memoryStream.Position = 0;
                        using (StreamReader streamReader = new StreamReader(memoryStream))
                        {
                            Clipboard.SetText(streamReader.ReadToEnd());
                        }
                    }
                }
            }

            if (treeView.Focused)
            {
                if ((treeView.SelectedNode != null) && (treeView.SelectedNode.Tag != null))
                {
                    if (treeView.SelectedNode.Tag.GetType() == typeof(DateTime))
                    {
                        DateTime date = (DateTime)treeView.SelectedNode.Tag;
                        Collection<TaskActivity> selectedActivities =
                            _engine.TaskActivitiesProvider.LoadActivities(date);
                        if (selectedActivities.Count > 0)
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                new XmlSerializer(typeof(List<TaskActivity>)).Serialize(memoryStream,
                                                                                         selectedActivities);
                                memoryStream.Position = 0;
                                using (StreamReader streamReader = new StreamReader(memoryStream))
                                {
                                    Clipboard.SetText(streamReader.ReadToEnd());
                                }
                            }
                        }
                    }
                }
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((recordedTasks1.Focused) && (recordedTasks1.EffectiveDate != DateTime.MinValue))
            {
                List<TaskActivity> entries;
                string clipboardText = Clipboard.GetText();
                if (clipboardText.Length > 0)
                {
                    using (StringReader sr = new StringReader(clipboardText))
                    {
                        entries = (List<TaskActivity>)new XmlSerializer(typeof(List<TaskActivity>)).Deserialize(sr);
                    }
                    if (entries != null)
                    {
                        foreach (TaskActivity ta in entries)
                        {
                            ta.Id = Guid.NewGuid();
                            ta.ChangeDate(recordedTasks1.EffectiveDate);
                            ta.Remarks = "Paste Operation";
                            _engine.TaskActivitiesProvider.CompleteActivity(ta);
                        }
                    }
                    RefreshViews(treeView.SelectedNode.Tag);
                }
            }

            if (treeView.Focused)
            {
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recordedTasks1.Focused)
            {
                recordedTasks1.SelectAll();
            }
        }

        private void AddNewTaskActivity(object sender, EventArgs e)
        {
            if (treeView.SelectedNode.Tag.GetType() == typeof(DateTime))
            {
                DateTime workingWithDate = (DateTime)treeView.SelectedNode.Tag;
                TaskDescription taskDescription = _engine.TaskDescriptionsProvider.TaskDescriptions[0];
                TaskActivity pastTaskActivity = new TaskActivity(
                    taskDescription,
                    _engine.IdentityProvider.Principal.Identity.Name)
                {
                    StartDate = workingWithDate,
                    EndDate = workingWithDate,
                    Remarks = "Inserted"
                };
                _engine.TaskActivitiesProvider.CompleteActivity(pastTaskActivity);
                RefreshViews(workingWithDate);
            }
        }

        private void ViewerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                DialogResult = DialogResult.Ignore;
                Close();
            }
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(string.Format(Resources.HelpContents));
        }

        private void buyTaskClerkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (QuickHelpForm qhf = new QuickHelpForm(
                string.Format(Resources.PurchaseUrl,
                              _engine.SettingsProvider.Get("CurrentUserRegistrationKey", "Unknown")
                    )))
            {
                qhf.ShowDialog(this);
            }
        }

        private void contextMenuStripStatusBar_Opening(object sender, CancelEventArgs e)
        {
            TaskActivity currentAvtivity =
                (TaskActivity)_engine.SettingsProvider.Get("CurrentActivity", TaskActivity.Empty);

            if (currentAvtivity.IsNotEmpty())
            {
                toolStripTextBoxNewStart.TextBox.Text = currentAvtivity.StartDate.ToShortTimeString();
                toolStripTextBoxNewStart.Enabled = true;
                commitToolStripMenuItem.Enabled = true;
            }
            else
            {
                toolStripTextBoxNewStart.Enabled = false;
                commitToolStripMenuItem.Enabled = false;
            }
        }

        private void commitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskActivity currentAvtivity =
                (TaskActivity)_engine.SettingsProvider.Get("CurrentActivity", TaskActivity.Empty);
            if (currentAvtivity.IsNotEmpty())
            {
                try
                {
                    DateTime change =
                        DateTime.Parse(currentAvtivity.StartDate.ToShortDateString() +
                                       " " + toolStripTextBoxNewStart.TextBox.Text);
                    currentAvtivity.StartDate = change;
                    _engine.SettingsProvider.Set("CurrentActivity", currentAvtivity, PersistHint.AcrossSessions);
                }
                catch (FormatException fe)
                {
                    Trace.TraceError("TaskActivitiesExplorer:commitToolStripMenuItem_Click[{0}]", fe.Message);
                    //ignore this error, the user has made a mistake
                }
            }
        }

        private void rebuildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedNodeName = null;
            TreeNode node = treeView.SelectedNode;
            if (node != null)
            {
                selectedNodeName = node.Name;
            }
            RebuildTree(selectedNodeName);
            treeView.Focus();
        }

        private void graphOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activityChart1.DisplayMenu();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            recordedTasks1.DisplayProperties();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (RegistrationForm registrationForm = new RegistrationForm())
            {
                registrationForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Handles the Click event of the taskDescriptionsExplorerToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void taskDescriptionsExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _engine.SettingsProvider.Set("HideUseButton", true, PersistHint.ThisSession);
            _engine.UIProvider.ShowDescriptionExplorer();
            _engine.SettingsProvider.Set("HideUseButton", false, PersistHint.ThisSession);
        }

        /// <summary>
        /// Handles the ResizeEnd event of the TaskActivitiesExplorer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TaskActivitiesExplorer_ResizeEnd(object sender, EventArgs e)
        {
            if (_formSize != Size)
            {
                _formSize = Size;
            }
        }

        /// <summary>
        /// Handles the SplitterMoved event of the splitContainer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.SplitterEventArgs"/> instance containing the event data.</param>
        private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                RefreshViews(treeView.SelectedNode.Tag);
            }
        }

        /// <summary>
        /// Handles the SplitterMoved event of the splitContainerDetail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.SplitterEventArgs"/> instance containing the event data.</param>
        private void splitContainerDetail_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                RefreshViews(treeView.SelectedNode.Tag);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the TaskActivitiesExplorer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void TaskActivitiesExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                _engine.SettingsProvider.Set("VerticalSplitterDistance",
                        splitContainerDetail.SplitterDistance,
                        PersistHint.AcrossSessions);
                _engine.SettingsProvider.Set("HorizontalSplitterDistance",
                        splitContainer.SplitterDistance,
                        PersistHint.AcrossSessions);
            }
        }

        ///// <summary>
        ///// Gets the current print formatter.
        ///// </summary>
        ///// <returns></returns>
        //private PrintDocument GetCurrentPrintFormatter()
        //{
        //    foreach(LoadableItem li in _engine.PluginsProvider.GetPluginsOfSubclass(typeof(PluginPrintFormatter)))
        //    {
        //        string printFormatter = null;
        //        if(!_engine.SettingsProvider.TryGet("CurrentPrintFormatter", out printFormatter))
        //        {
        //            Printing.PrintFormatterSelector pss = new BritishMicro.TaskClerk.UI.Printing.PrintFormatterSelector();
        //            if(pss.ShowDialog(this) == DialogResult.Cancel)
        //            {
        //                return null;
        //            }

        //            printFormatter = (string)_engine.SettingsProvider.Get("CurrentPrintFormatter");
        //        }

        //        if(li.ReturnType().Name == printFormatter)
        //        {
        //            return li.CreateInstance(_engine) as PrintDocument;
        //        }
        //    }

        //    return null;
        //}

        /// <summary>
        /// Handles the Click event of the printToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrintSetupDialog psd = new PrintSetupDialog(_engine))
            {
                if (psd.ShowDialog() == DialogResult.OK)
                {
                    if (psd.Document != null)
                    {
                        printDialog.Document = psd.Document;
                        printDialog.Document.Print();
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the printPreviewToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrintSetupDialog psd = new PrintSetupDialog(_engine))
            {
                if (psd.ShowDialog() == DialogResult.OK)
                {
                    if (psd.Document != null)
                    {
                        printPreviewDialog.Document = psd.Document;
                        printPreviewDialog.ShowDialog(this);
                    }
                }
            }
        }

        private void registerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            RegistrationForm form = new RegistrationForm();
            form.ShowDialog();
        }
    }
}