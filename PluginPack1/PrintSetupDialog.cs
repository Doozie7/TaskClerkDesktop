using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PrintSetupDialog : Form
    {

        private TaskClerkEngine _engine;
        private Collection<TaskDescription> _taskDescriptions;
        private Collection<TaskActivity> _taskActivities;
        private DateTime _start;
        private DateTime _end;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivityFilterDialog"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        public PrintSetupDialog(TaskClerkEngine engine)
        {
            InitializeComponent();
            _engine = engine;
            _taskDescriptions = new Collection<TaskDescription>();

            DateTime tempStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day); 
            DateTime tempEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1).Subtract(new TimeSpan(0,0,1));

            _start = (DateTime)_engine.SettingsProvider.Get("FilterStartDate", tempStart);
            _end = (DateTime)_engine.SettingsProvider.Get("FilterEndDate", tempEnd);

            datetimepickerStartDate.Value = _start;
            datetimepickerStartTime.Value = _start;
            datetimepickerEndDate.Value = _end;
            datetimepickerEndTime.Value = _end;

            comboboxScheme.SelectedIndex = 0;
            ReBuildTree(_engine.TaskDescriptionsProvider.TaskDescriptions);

            this.datetimepickerEndTime.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.datetimepickerStartTime.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.datetimepickerEndDate.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.datetimepickerStartDate.ValueChanged += new System.EventHandler(this.ValueChanged);
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
        /// Gets the collection of task activities.
        /// </summary>
        /// <value>The task activities.</value>
        public Collection<TaskActivity> TaskActivities
        {
            get { return _taskActivities; }
        }

        public DateTime Start
        {
            get { return _start; }
        }

        public DateTime End
        {
            get { return _end; }
        }
        
        private void ReBuildTree(Collection<TaskDescription> rootDescriptions)
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
            labelTaskDescriptionsCount.Text = string.Format("{0} TaskDescriptions selected.", _taskDescriptions.Count);
        }

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
                //CheckParents(e.Node);
            }
        }

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

        private void CheckParents(TreeNode child)
        {
            if (child.Checked == true)
            {
                if (child.Parent != null)
                {
                    child.Parent.Checked = true;
                    CheckParents(child.Parent);
                }
            }
        }

        private void InsertItemInList(TaskDescription taskDescription)
        {
            if (!_taskDescriptions.Contains(taskDescription))
            {
                _taskDescriptions.Add(taskDescription);
            }
            labelTaskDescriptionsCount.Text = string.Format("{0} TaskDescriptions selected.", _taskDescriptions.Count);
        }

        private void RemoveItemFromList(TaskDescription taskDescription)
        {
            if (_taskDescriptions.Contains(taskDescription))
            {
                _taskDescriptions.Remove(taskDescription);
            }
            labelTaskDescriptionsCount.Text = string.Format("{0} TaskDescriptions selected.", _taskDescriptions.Count);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            _taskActivities = Engine.TaskActivitiesProvider.LoadActivities(_start, _end, _taskDescriptions);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboboxScheme_DropDownClosed(object sender, EventArgs e)
        {
            if (comboboxScheme.SelectedIndex == 0)
            {
                ReBuildTree(_engine.TaskDescriptionsProvider.TaskDescriptions);
            }
            if (comboboxScheme.SelectedIndex == 2)
            {
                ReBuildTree(_engine.TaskDescriptionsProvider.TaskDescriptions);
            }
        }

        private void ValueChanged(object sender, EventArgs e)
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

            _engine.SettingsProvider.Set("FilterStartDate", _start, PersistHint.AcrossSessions);
            _engine.SettingsProvider.Set("FilterEndDate", _end, PersistHint.AcrossSessions);
        }

        private void buttonPageSetup_Click(object sender, EventArgs e)
        {
            pagesetupdialog.ShowDialog(this);
        }
    }
}