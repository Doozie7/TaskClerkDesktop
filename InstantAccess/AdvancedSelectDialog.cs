//----------------------------------------------------------------------
//	Developed by:
//          compilewith.net
//          United Kingdom
//
//  Copyright (c) compilewith.net 2007
//
//  Date  : 31st May 2007
//	Author: Paul Jackson (paul@compilewith.net)
//          Architect
//----------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace BritishMicro.TaskClerk.InstantAccess
{
    /// <summary>
    /// Represents a dialog for selecting more options for a TaskActivity. 
    /// </summary>
    public partial class AdvancedSelectDialog : Form
    {
        private TaskDescription _crossTab;
        private TaskDescription _current;
        private bool _isChangeingTime;
        private int _mouseStartPos;
        private DateTime _startTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedSelectDialog"/> class.
        /// </summary>
        public AdvancedSelectDialog()
        {
            InitializeComponent();

            this._dateTimePickerStartTime.Value = DateTime.Now;
            this._dateTimePickerStartTime.MaxDate = DateTime.Now;
            
            BuildTree();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedSelectDialog"/> class.
        /// </summary>
        /// <param name="current">The current.</param>
        public AdvancedSelectDialog(TaskDescription current) : this()
        {
            this.Current = current;
        }

        /// <summary>
        /// Builds the treeview control - without recursion.
        /// </summary>
        private void BuildTree()
        {
            TreeNode rootNode = new TreeNode("Task Descriptions");
            rootNode.Tag = AppContext.Current.TaskDescriptionsProvider.TaskDescriptions;
            rootNode.ExpandAll();

            Stack<TreeNode> treeNodeStack = new Stack<TreeNode>();
            treeNodeStack.Push(rootNode);

            this._treeViewTaskDescriptions.Nodes.Clear();
            this._treeViewTaskDescriptions.Nodes.Add(rootNode);

            do
            {
                TreeNode parent = treeNodeStack.Pop();
                parent.Expand();
                
                if(parent.Tag != null)
                {
                    IList<TaskDescription> descriptions = parent.Tag as IList<TaskDescription>;
                    foreach(TaskDescription description in descriptions)
                    {
                        TreeNode child = new TreeNode(description.Name);
                        parent.Nodes.Add(child);
                        child.Tag = description;
                        if(description.IsEvent) 
                            child.NodeFont = new Font(this.Font, FontStyle.Italic);

                        if(description.Children != null && description.Children.Count > 0)
                        {
                            child.NodeFont = new Font(this.Font, FontStyle.Bold);
                            child.Tag = description.Children;
                            treeNodeStack.Push(child);
                        }
                    }
                }
            }
            while(treeNodeStack.Count > 0);
        }

        /// <summary>
        /// Handles the AfterSelect event of the _treeViewTaskDescriptions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.TreeViewEventArgs"/> instance containing the event data.</param>
        private void _treeViewTaskDescriptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //  ...it's worth noting here that the "Tag" property could also be an IList<>
            //  object, therefore setting the CrossTab value to null. This is the desired 
            //  behaviour as only child nodes should be selected
            this._crossTab = e.Node.Tag as TaskDescription;
        }

        /// <summary>
        /// Handles the MouseDown event of the _dateTimePickerStartTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void _dateTimePickerStartTime_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this._isChangeingTime = true;
                this._mouseStartPos = e.Y;
                this._startTime = this._dateTimePickerStartTime.Value.AddSeconds(-this._dateTimePickerStartTime.Value.Second);
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the _dateTimePickerStartTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void _dateTimePickerStartTime_MouseMove(object sender, MouseEventArgs e)
        {
            if(this._isChangeingTime)
            {
                DateTime modifiedTime = this._startTime.AddMinutes(this._mouseStartPos - e.Y);
                if(modifiedTime <= this._dateTimePickerStartTime.MaxDate)
                    this._dateTimePickerStartTime.Value = modifiedTime;
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the _dateTimePickerStartTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void _dateTimePickerStartTime_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                this._isChangeingTime = false;
        }

        /// <summary>
        /// Gets the cross tab TaskDescription.
        /// </summary>
        /// <value>The cross tab.</value>
        public TaskDescription CrossTab
        {
            get { return this._crossTab; }
        }

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>The current.</value>
        public TaskDescription Current
        {
            get { return this._current; }
            set
            {
                this._current = value;
                this._textBoxSelectedTask.Text = (this._current != null) ?
                    this._current.Name : string.Empty;
            }
        }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime StartTime
        {
            get { return this._dateTimePickerStartTime.Value; }
        }
    }
}