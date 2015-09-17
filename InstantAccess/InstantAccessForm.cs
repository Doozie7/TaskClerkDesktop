//----------------------------------------------------------------------
//	Developed by:
//          BritishMicro
//          United Kingdom
//
//  Copyright (c) BritishMicro 2006
//
//  Date  : 30th November 2006
//	Author: Paul Jackson (paul.jackson@britishmicro.com)
//          Architect
//----------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.InstantAccess
{
    /// <summary>
    /// Represents the instance access manager form for Task Clerk.
    /// </summary>
    public partial class InstantAccessForm : Form
    {
        private InstantAccessData _data;
        private bool _isAdvancedMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstantAccessForm"/> class.
        /// </summary>
        public InstantAccessForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        /// <summary>
        /// Initializes the form.
        /// </summary>
        private void InitializeForm()
        {
            _data = InstantAccessData.Create();
            if (_data.IsDataAvailable)
            {
                ShowTaskDescriptions();
            }
            else
            {
                HideTaskDescriptions();
            }

            ShowCurrentActivity();
        }

        private void ShowCurrentActivity()
        {

            TaskActivity currentActivity = AppContext.Current.CurrentActivity;

            // TODO: There should also be a format type on ToString for TaskActivity (ala GUID)
            if (currentActivity != null)
            {
                string message = currentActivity.ToSummaryString().Replace("\r\n", " ");
                int index = message.IndexOf("on the");
                if (index != -1)
                {
                    toolStripStatusLabelMain.Text = message.Substring(0, index);
                }
                else
                {
                    toolStripStatusLabelMain.Text = message;
                }
            }
            else
            {
                toolStripStatusLabelMain.Text = "Current Activity: None";
            }
        }

        /// <summary>
        /// Shows the task descriptions.
        /// </summary>
        private void ShowTaskDescriptions()
        {
            PopulateListView();

            listViewTaskDescriptions.Visible = true;
            listViewTaskDescriptions.Dock = DockStyle.Fill;
            panelNoTaskDescriptions.Visible = false;
            labelTaskDescription.Text = string.Empty;
            toolStripButtonDelete.Enabled = false;
            groupBoxTaskDescription.Enabled = true;

            if (listViewTaskDescriptions.Items.Count == 0)
            {
                HideTaskDescriptions();
            }
        }

        /// <summary>
        /// Hides the task descriptions.
        /// </summary>
        private void HideTaskDescriptions()
        {
            panelNoTaskDescriptions.Visible = true;
            panelNoTaskDescriptions.Dock = DockStyle.Fill;
            panelNoTaskDescriptions.Focus();
            listViewTaskDescriptions.Visible = false;
            labelTaskDescription.Text = string.Empty;
            groupBoxTaskDescription.Enabled = false;
        }

        /// <summary>
        /// Populates the list view.
        /// </summary>
        private void PopulateListView()
        {
            listViewTaskDescriptions.Items.Clear();
            ListViewGroup group = listViewTaskDescriptions.Groups["listViewGroupPinned"];
            foreach (TaskDescription description in _data.PinnedTaskDescriptions)
            {
                ListViewItem item = new ListViewItem();
                item.Group = group;
                item.ImageIndex = (description.IsEvent) ? 0 : 1;
                item.Text = description.Name;
                item.ToolTipText = description.Description;
                item.Tag = description;

                listViewTaskDescriptions.Items.Add(item);
            }
        }

        /// <summary>
        /// Adds the description handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AddDescriptionHandler(object sender, EventArgs e)
        {
            TaskDescription selectedTask = AppContext.Current.ShowDescriptionExplorer();
            if (selectedTask.IsNotEmpty())
            {
                _data.AddPinnedTaskDescription(selectedTask);
                ShowTaskDescriptions();
            }
        }

        /// <summary>
        /// Removes the description handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RemoveDescriptionHandler(object sender, EventArgs e)
        {
            if (listViewTaskDescriptions.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewTaskDescriptions.SelectedItems)
                {
                    _data.RemovePinnedTaskDescription((TaskDescription) item.Tag);
                }

                ShowTaskDescriptions();
            }
        }

        /// <summary>
        /// Uses the description handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseDescriptionHandler(object sender, EventArgs e)
        {
            if (listViewTaskDescriptions.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewTaskDescriptions.SelectedItems[0];
                TaskDescription description = (TaskDescription) item.Tag;

                if(this._isAdvancedMode)
                {
                    AdvancedSelectDialog dialog = new AdvancedSelectDialog(description);
                    if(dialog.ShowDialog(this) == DialogResult.OK)
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
        }

        /// <summary>
        /// Handles the KeyDown event of the InstantAccessForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void InstantAccessForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                Close();
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the listViewTaskDescriptions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void listViewTaskDescriptions_KeyDown(object sender, KeyEventArgs e)
        {
            this._isAdvancedMode = e.Shift;
            
            if (e.KeyCode == Keys.Enter && listViewTaskDescriptions.SelectedItems.Count > 0)
            {
                e.Handled = true;
                UseDescriptionHandler(sender, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the listViewTaskDescriptions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void listViewTaskDescriptions_KeyUp(object sender, KeyEventArgs e)
        {
            this._isAdvancedMode = false;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the listViewTaskDescriptions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void listViewTaskDescriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTaskDescriptions.SelectedItems.Count > 0)
            {
                TaskDescription description = (TaskDescription) listViewTaskDescriptions.SelectedItems[0].Tag;
                labelTaskDescription.Text = (description.Description != null && description.Description.Length > 0)
                                                ?
                                            description.Description
                                                : "Name: " + description.Name + ", ID: " + description.Id.ToString("D");

                toolStripButtonDelete.Enabled = true;
            }
            else
            {
                toolStripButtonDelete.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Opening event of the contextMenuStripMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void contextMenuStripMain_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripItem menuItem in contextMenuStripMain.Items)
            {
                menuItem.Enabled = false;
            }

            contextMenuStripMain.Items["addToolStripMenuItem"].Enabled = true;
            contextMenuStripMain.Items["exitTaskClerkToolStripMenuItem"].Enabled = true;

            if (listViewTaskDescriptions.SelectedItems.Count > 0)
            {
                contextMenuStripMain.Items["removeToolStripMenuItem"].Enabled = true;
                contextMenuStripMain.Items["useToolStripMenuItem"].Enabled = true;
            }
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
    }
}