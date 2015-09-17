using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using BritishMicro.TaskClerk.Plugins;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PrintSetupDialog : Form
    {

        private TaskClerkEngine _engine;
        private PrintDocument _document;
        private PrintConfiguration _config;

        private Collection<LoadableItem> _printFormatters;
        
        private Collection<TaskDescription> _taskDescriptions;
        private DateTime _start;
        private DateTime _end;

        //private Collection<TaskActivity> _taskActivities;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintSetupDialog"/> class.
        /// </summary>
        public PrintSetupDialog()
        {
            InitializeComponent();
            _printFormatters = new Collection<LoadableItem>();
            _config = new PrintConfiguration();
       }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintSetupDialog"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        public PrintSetupDialog(TaskClerkEngine engine) : this()
        {
            _engine = engine;

            PrepareFormatterList();
            LoadUIControlSettings();
            PrepareDescriptionsTree(_engine.TaskDescriptionsProvider.TaskDescriptions);
        }

        /// <summary>
        /// Loads the UI control settings.
        /// </summary>
        private void LoadUIControlSettings()
        {
            comboboxPrintFormat.Text = (string)_engine.SettingsProvider.Get("PrintSetupDialog_SelectedFormat", string.Empty);
            comboboxScheme.Text = (string)_engine.SettingsProvider.Get("PrintSetupDialog_SelectedScheme", string.Empty);

            DateTime tempStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime tempEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 1));

            _start = (DateTime)_engine.SettingsProvider.Get("PrintSetupDialog_FilterStartDate", tempStart);
            _end = (DateTime)_engine.SettingsProvider.Get("PrintSetupDialog_FilterEndDate", tempEnd);

            datetimepickerStartDate.Value = _start;
            datetimepickerStartTime.Value = _start;
            datetimepickerEndDate.Value = _end;
            datetimepickerEndTime.Value = _end;

            textBoxSelectedExplorerDate.Text = ((DateTime)_engine.SettingsProvider.Get("Selected Explorer Date", DateTime.Now)).ToLongDateString();
            textBoxSelectedExplorerDate.Tag = (DateTime)_engine.SettingsProvider.Get("Selected Explorer Date", DateTime.Now);
        }

        /// <summary>
        /// Saves the UI control settings.
        /// </summary>
        private void SaveUIControlSettings()
        {
            _engine.SettingsProvider.Set("PrintSetupDialog_SelectedFormat", comboboxPrintFormat.Text, PersistHint.AcrossSessions);
            _engine.SettingsProvider.Set("PrintSetupDialog_SelectedScheme", comboboxScheme.Text, PersistHint.AcrossSessions);

            _start = new DateTime(
                datetimepickerStartDate.Value.Year,
                datetimepickerStartDate.Value.Month,
                datetimepickerStartDate.Value.Day,
                datetimepickerStartTime.Value.Hour,
                datetimepickerStartTime.Value.Minute,
                datetimepickerStartTime.Value.Second);

            _end = new DateTime(
                datetimepickerEndDate.Value.Year,
                datetimepickerEndDate.Value.Month,
                datetimepickerEndDate.Value.Day,
                datetimepickerEndTime.Value.Hour,
                datetimepickerEndTime.Value.Minute,
                datetimepickerEndTime.Value.Second);

            _engine.SettingsProvider.Set("PrintSetupDialog_FilterStartDate", _start, PersistHint.AcrossSessions);
            _engine.SettingsProvider.Set("PrintSetupDialog_FilterEndDate", _end, PersistHint.AcrossSessions);
        }

        /// <summary>
        /// Prepares the formatter list.
        /// </summary>
        private void PrepareFormatterList()
        {
            comboboxPrintFormat.Items.Clear();
            foreach (LoadableItem loadableItem in AppContext.Current.PluginsProvider.Plugins)
            {
                if (loadableItem.ReturnType().IsSubclassOf(typeof(PluginPrintFormatter)))
                {
                    comboboxPrintFormat.Items.Add(loadableItem.DisplayName);
                    _printFormatters.Add(loadableItem);
                }
            }
            comboboxScheme.SelectedIndex = 0;
        }

        /// <summary>
        /// Prepares the descriptions tree.
        /// </summary>
        /// <param name="rootDescriptions">The root descriptions.</param>
        private void PrepareDescriptionsTree(Collection<TaskDescription> rootDescriptions)
        {            
            _taskDescriptions = new Collection<TaskDescription>();
            
            //clear the tree
            treeviewTaskDescriptions.Nodes.Clear();

            //create a root
            TreeNode root = new TreeNode("Task Descriptions");
            root.Tag = rootDescriptions;
            treeviewTaskDescriptions.Nodes.Add(root);

            foreach (TaskDescription taskDescription in rootDescriptions)
            {
                TreeNode treeNode = new TreeNode(taskDescription.Name);
                treeNode.ToolTipText = taskDescription.Description;
                treeNode.Tag = taskDescription;
                if (comboboxScheme.SelectedIndex == 0)
                {
                    treeNode.Checked = !taskDescription.IsPrivate;
                }
                if (comboboxScheme.SelectedIndex == 2)
                {
                    treeNode.Checked = true;
                }
                if (treeNode.Checked)
                {
                    _taskDescriptions.Add(taskDescription);
                }
                BuildTree(taskDescription, treeNode);
                treeviewTaskDescriptions.Nodes[0].Nodes.Add(treeNode);
                treeNode.EnsureVisible();
            }
        }

        /// <summary>
        /// Builds the tree.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="rootNode">The root node.</param>
        private void BuildTree(TaskDescription parent, TreeNode rootNode)
        {
            foreach (TaskDescription child in parent.Children)
            {
                TreeNode node = new TreeNode(child.Name);
                node.Tag = child;
                node.ToolTipText = child.Description;
                if (comboboxScheme.SelectedIndex == 0)
                {
                    node.Checked = !child.IsPrivate;
                }
                if (comboboxScheme.SelectedIndex == 2)
                {
                    node.Checked = true;
                }
                if (node.Checked)
                {
                    _taskDescriptions.Add(child);
                }
                rootNode.Nodes.Add(node);
                if (child.Children.Count > 0)
                {
                    BuildTree(child, node);
                }
            }
        }

        /// <summary>
        /// Prepares the print config.
        /// </summary>
        private void PreparePrintConfig()
        {
            Config.AllowedDescriptions = _taskDescriptions;
            Config.Start = _start;
            Config.End = _end;
            Config.Activities = Engine.TaskActivitiesProvider.LoadActivities(_start, _end, _taskDescriptions);
        }

        /// <summary>
        /// Gets or sets the engine.
        /// </summary>
        /// <value>The engine.</value>
        public TaskClerkEngine Engine
        {
            get { return _engine; }
        }

        /// <summary>
        /// Gets the list of task descriptions to be used in the filter.
        /// </summary>
        /// <value>The task descriptions.</value>
        public Collection<TaskDescription> TaskDescriptions
        {
            get { return _taskDescriptions; }
        }

        /// <summary>
        /// Gets the start.
        /// </summary>
        /// <value>The start.</value>
        public DateTime Start
        {
            get { return _start; }
        }

        /// <summary>
        /// Gets the end.
        /// </summary>
        /// <value>The end.</value>
        public DateTime End
        {
            get { return _end; }
        }

        /// <summary>
        /// Checks the children.
        /// </summary>
        /// <param name="parent">The parent.</param>
        private void CheckChildren(TreeNode parent)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                if (child.Checked != parent.Checked)
                {
                    child.Checked = parent.Checked;
                    TaskDescription taskDescription = child.Tag as TaskDescription;
                    if (child.Checked)
                    {
                        InsertItemInList(taskDescription);
                    }
                    else
                    {
                        RemoveItemFromList(taskDescription);
                    }
                }
                if (child.Nodes != null)
                {
                    CheckChildren(child);
                }
            }
        }

        /// <summary>
        /// Inserts the item in list.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        private void InsertItemInList(TaskDescription taskDescription)
        {
            if (!_taskDescriptions.Contains(taskDescription))
            {
                _taskDescriptions.Add(taskDescription);
            }
        }

        /// <summary>
        /// Removes the item from list.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        private void RemoveItemFromList(TaskDescription taskDescription)
        {
            if (_taskDescriptions.Contains(taskDescription))
            {
                _taskDescriptions.Remove(taskDescription);
            }
        }

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The config.</value>
        public PrintConfiguration Config
        {   
            get { return _config; }
        }

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        /// <value>The document.</value>
        public PrintDocument Document
        {
            get { return _document; }
            private set
            {
                _document = value;
                if (_document != null)
                {
                    pagesetupdialog.Document = _document;
                    buttonPageSetup.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Handles the Checked event on a TaskDescriptionNode in the tree.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void TaskDescriptionNode_Checked(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag.GetType() == typeof(TaskDescription))
            {
                comboboxScheme.SelectedIndex = 1;
                TaskDescription taskDescription = e.Node.Tag as TaskDescription;
                if (taskDescription != null)
                {
                    if (e.Node.Checked == true)
                    {
                        InsertItemInList(taskDescription);
                    }
                    else
                    {
                        RemoveItemFromList(taskDescription);
                    }
                    CheckChildren(e.Node);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            PreparePrintConfig();
            SaveUIControlSettings();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Handles the DropDownClosed event of the comboboxScheme control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void comboboxScheme_DropDownClosed(object sender, EventArgs e)
        {
            if (comboboxScheme.SelectedIndex == 0)
            {
                PrepareDescriptionsTree(_engine.TaskDescriptionsProvider.TaskDescriptions);
            }
            if (comboboxScheme.SelectedIndex == 2)
            {
                PrepareDescriptionsTree(_engine.TaskDescriptionsProvider.TaskDescriptions);
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonPageSetup control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonPageSetup_Click(object sender, EventArgs e)
        {
            pagesetupdialog.Document = this._document;
            pagesetupdialog.ShowDialog();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboboxPrintFormat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void comboboxPrintFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (LoadableItem li in _printFormatters)
            {
                if (li.DisplayName == comboboxPrintFormat.Text)
                {
                    _document = li.CreateInstance( _engine, _config) as PluginPrintFormatter;
                    textboxPrintFormatDescription.Text = li.Description;
                    if (((PluginPrintFormatter)_document).RequiresDateRange() == false)
                    {
                        datetimepickerEndDate.Enabled = false;
                        datetimepickerEndDate.Value = datetimepickerStartDate.Value;
                        break;
                    }
                    else
                    {
                        datetimepickerEndDate.Enabled = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the datetimepickerStartDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void datetimepicker_Changed(object sender, EventArgs e)
        {
            if (!datetimepickerEndDate.Enabled)
            {
                datetimepickerEndDate.Value = datetimepickerStartDate.Value;
            }
            radioButtonCustom.Checked = true;
            SetStartAndEndProperties();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the radioButtonToday control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radioButtonToday_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonToday.Checked == true)
            {
                int year = DateTime.Today.Year;
                int month = DateTime.Today.Month;
                int day = DateTime.Today.Day;
                datetimepickerEndDate.Value = DateTime.Today;
                datetimepickerEndTime.Value = new DateTime(year, month, day, 23, 59, 59);
                datetimepickerStartDate.Value = DateTime.Today;
                datetimepickerStartTime.Value = new DateTime(year, month, day, 0, 0, 0);
                SetStartAndEndProperties();
            }
        }

        /// <summary>
        /// Sets the start and end properties.
        /// </summary>
        private void SetStartAndEndProperties()
        {
            _start = new DateTime(
                datetimepickerStartDate.Value.Year,
                datetimepickerStartDate.Value.Month,
                datetimepickerStartDate.Value.Day,
                datetimepickerStartTime.Value.Hour,
                datetimepickerStartTime.Value.Minute,
                datetimepickerStartTime.Value.Second);

            _end = new DateTime(
                datetimepickerEndDate.Value.Year,
                datetimepickerEndDate.Value.Month,
                datetimepickerEndDate.Value.Day,
                datetimepickerEndTime.Value.Hour,
                datetimepickerEndTime.Value.Minute,
                datetimepickerEndTime.Value.Second);
        }

        private void radioButtonSelectedExplorerDate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSelectedExplorerDate.Checked == true)
            {
                int year = ((DateTime)textBoxSelectedExplorerDate.Tag).Year;
                int month = ((DateTime)textBoxSelectedExplorerDate.Tag).Month;
                int day = ((DateTime)textBoxSelectedExplorerDate.Tag).Day;
                datetimepickerEndDate.Value = (DateTime)textBoxSelectedExplorerDate.Tag;
                datetimepickerEndTime.Value = new DateTime(year, month, day, 23, 59, 59);
                datetimepickerStartDate.Value = (DateTime)textBoxSelectedExplorerDate.Tag;
                datetimepickerStartTime.Value = new DateTime(year, month, day, 0, 0, 0);
                SetStartAndEndProperties();
            }
        }
    }
}