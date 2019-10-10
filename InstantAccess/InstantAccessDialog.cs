//----------------------------------------------------------------------
//	Developed by:
//          compilewith.net
//          United Kingdom
//
//  Copyright (c) compilewith.net 2007
//
//  Date  : 6th June 2007
//	Author: Paul Jackson (paul@compilewith.net)
//          Architect
//----------------------------------------------------------------------
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.InstantAccess
{
    /// <summary>
    /// Represents the Instance Access Dialog.
    /// </summary>
    public partial class InstantAccessDialog : Form
    {
        private const int MaximumItemCount = 5;

        private readonly TreeNode _nodeRoot;
        private readonly TreeNode _nodePinned;
        private readonly TreeNode _nodeMru;
        private readonly TreeNode _nodeMfu;
        private readonly InstantAccessData _data;
        private bool _isAdvancedMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstantAccessDialog"/> class.
        /// </summary>
        public InstantAccessDialog()
        {
            InitializeComponent();

            this._nodeRoot = this._treeViewActions.Nodes["_nodeRoot"];
            this._nodePinned = this._nodeRoot.Nodes["_nodePinned"];
            this._nodeMru = this._nodeRoot.Nodes["_nodeMru"];
            this._nodeMfu = this._nodeRoot.Nodes["_nodeMfu"];

            this._nodeRoot.Expand();
            this._nodePinned.Expand();
            this._nodeMru.Expand();
            this._nodeMfu.Expand();

            this._data = InstantAccessData.Create();

            BuildTree();
            BuildCurrentActivity();
        }

        /// <summary>
        /// Builds the tree.
        /// </summary>
        private void BuildTree()
        {
            AppendTaskDescriptionsToNode(this._nodeMfu, MostRecentlyUsedService.FindTopMostFrequentlyUsed(MaximumItemCount));
            AppendTaskDescriptionsToNode(this._nodeMru, MostRecentlyUsedService.FindTopMostRecentlyUsed(MaximumItemCount));
            AppendTaskDescriptionsToNode(this._nodePinned, this._data.PinnedTaskDescriptions);

            Sort(this._nodePinned);
        }

        /// <summary>
        /// Appends the task descriptions to node.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="descriptions">The descriptions.</param>
        private void AppendTaskDescriptionsToNode(TreeNode parent, IEnumerable descriptions)
        {
            TaskActivity currentActivity = AppContext.Current.CurrentActivity;
            TaskDescription currentDescription =
                (currentActivity != null) ? currentActivity.TaskDescription : null;

            parent.Nodes.Clear();
            foreach (TaskDescription description in descriptions)
            {
                TreeNode node = new TreeNode(description.Name);
                parent.Nodes.Add(node);

                node.Tag = description;
                node.ToolTipText = description.Description;
                if (description.IsEvent)
                    node.NodeFont = new Font(this.Font, FontStyle.Italic);
                if (description.Name == currentDescription.Name)
                {
                    node.NodeFont = new Font(this.Font, FontStyle.Strikeout);
                    node.ForeColor = SystemColors.InactiveCaptionText;
                }
            }
        }

        /// <summary>
        /// Builds the current activity.
        /// </summary>
        private void BuildCurrentActivity()
        {
            TaskActivity activity = AppContext.Current.CurrentActivity;
            if (activity != null && !activity.IsEmpty())
            {
                string message = activity.ToSummaryString().Replace("\r\n", " ");
                int index = message.IndexOf("on the");
                if (index != -1)
                {
                    this._textBoxCurrentTask.Text = message.Substring(0, index);
                }
                else
                {
                    this._textBoxCurrentTask.Text = message;
                }
            }
        }

        /// <summary>
        /// Sorts the specified parent.
        /// </summary>
        /// <param name="parent">The parent.</param>
        private static void Sort(TreeNode parent)
        {
            if (parent.Nodes.Count == 0)
                return;

            TreeNode[] nodes = new TreeNode[parent.Nodes.Count];
            parent.Nodes.CopyTo(nodes, 0);
            Array.Sort(nodes, delegate (TreeNode x, TreeNode y)
            {
                TaskDescription lhs = (TaskDescription)x.Tag;
                TaskDescription rhs = (TaskDescription)y.Tag;

                return lhs.Name.CompareTo(rhs.Name);
            });

            parent.Nodes.Clear();
            parent.Nodes.AddRange(nodes);
            parent.Expand();
        }

        /// <summary>
        /// Uses the task description.
        /// </summary>
        private void UseTaskDescription()
        {
            TreeNode currentNode = this._treeViewActions.SelectedNode;
            if (currentNode == null)
                return;

            if (currentNode.NodeFont != null && currentNode.NodeFont.Strikeout)
                return;

            if (!(currentNode.Tag is TaskDescription description))
                return;

            if (this._isAdvancedMode)
            {
                AdvancedSelectDialog dialog = new AdvancedSelectDialog(description);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    AppContext.Current.HandleNewTaskActivity(
                        dialog.Current,
                        dialog.CrossTab,
                        dialog.StartTime);

                    Close();
                }
            }
            else
            {
                AppContext.Current.HandleNewTaskActivity(description, DateTime.Now);
                Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the closeToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the exitTaskClerkToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void exitTaskClerkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppContext.Current.StopEngine();
        }

        /// <summary>
        /// Handles the DoubleClick event of the _treeViewActions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void _treeViewActions_DoubleClick(object sender, EventArgs e)
        {
            UseTaskDescription();
        }

        /// <summary>
        /// Handles the KeyDown event of the _treeViewActions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void _treeViewActions_KeyDown(object sender, KeyEventArgs e)
        {
            this._isAdvancedMode = e.Shift;

            if (e.KeyCode == Keys.Enter)
                UseTaskDescription();
        }

        /// <summary>
        /// Handles the KeyUp event of the _treeViewActions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void _treeViewActions_KeyUp(object sender, KeyEventArgs e)
        {
            this._isAdvancedMode = false;
        }

        /// <summary>
        /// Handles the Click event of the useSelectedToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void useSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseTaskDescription();
        }

        /// <summary>
        /// Handles the Click event of the advancedSelectToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void advancedSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._isAdvancedMode = true;
            try
            {
                UseTaskDescription();
            }
            finally
            {
                this._isAdvancedMode = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the pinToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void pinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskDescription selectedTask = AppContext.Current.ShowDescriptionExplorer();
            if (selectedTask.IsNotEmpty())
            {
                if (this._treeViewActions.Nodes.ContainsKey(selectedTask.Name))
                    return;

                this._data.AddPinnedTaskDescription(selectedTask);
                AppendTaskDescriptionsToNode(this._nodePinned, this._data.PinnedTaskDescriptions);
                Sort(this._nodePinned);
            }
        }

        /// <summary>
        /// Handles the Click event of the deleteToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this._treeViewActions.SelectedNode;
            if (selectedNode == null || selectedNode.Parent != this._nodePinned)
                return;

            this._data.RemovePinnedTaskDescription(selectedNode.Tag as TaskDescription);
            this._treeViewActions.Nodes.Remove(selectedNode);
        }

        /// <summary>
        /// Handles the Click event of the _buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void _buttonOK_Click(object sender, EventArgs e)
        {
            UseTaskDescription();
        }

        /// <summary>
        /// Handles the DropDownOpening event of the actionsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void actionsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            TreeNode selectedNode = this._treeViewActions.SelectedNode;

            this.deleteToolStripMenuItem.Enabled =
                (selectedNode != null && selectedNode.Parent == this._nodePinned);

            this.useSelectedToolStripMenuItem.Enabled =
                this.advancedSelectToolStripMenuItem.Enabled =
                    (selectedNode != null && selectedNode.Tag != null);
        }

        /// <summary>
        /// Handles the Click event of the viewTaskActicityExplorerToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void viewTaskActicityExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppContext.Current.UIProvider.ShowActivityExplorer();
            this.Close();
        }
    }
}