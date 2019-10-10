using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class RecordedTasks : UserControl
    {
        private BindingSource _bindingSource;
        private DateTime _effectiveDate;
        //private TaskActivity _underEdit;
        private int sortColumn;
        private bool sortDirection;

        public RecordedTasks()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode == false)
            {
                if (AppContext.Current.SettingsProvider != null)
                {
                    dataGridView.Font = (Font)AppContext.Current.SettingsProvider.Get("GeneralFont", Font);

                    activitiesDraw1.Visible =
                        (bool)AppContext.Current.SettingsProvider.Get("Show Activities Gantt", true);

                    Visible =
                        (bool)AppContext.Current.SettingsProvider.Get("Show Recorded Activities Grid", true);

                    sortColumn = (int)AppContext.Current.SettingsProvider.Get("RecordedTasksSortOnColumn", 0);
                    sortDirection = false; // (bool)AppContext.Current.SettingsProvider.Get("RecordedTasksSortDirection", 0);

                    _bindingSource = new BindingSource();
                    dataGridView.AutoGenerateColumns = false;
                    //dataGridView.Sort(dataGridView.Columns[0], sortDirection);
                    AppContext.Current.TaskActivitiesProvider.TaskActivitiesChanged +=
                        Current_TaskActivitiesChangedEvent;
                    dataGridView.PreviewKeyDown +=
                        dataGridView_PreviewKeyDown;

                    foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                    {
                        dgvc.Width =
                            (int)
                            AppContext.Current.SettingsProvider.Get(string.Format("ColWidth-{0}", dgvc.Name), dgvc.Width);
                    }
                    dataGridView.ColumnWidthChanged += dataGridView_ColumnWidthChanged;
                }
            }
        }

        private void Current_TaskActivitiesChangedEvent(object o, TaskActivityEventArgs e)
        {
            if (e != TaskActivityEventArgs.Empty)
            {
                if (e.TaskActivity.StartDate.ToShortDateString() == _effectiveDate.ToShortDateString())
                {
                    Activities =
                        AppContext.Current.TaskActivitiesProvider.LoadActivities(_effectiveDate);
                    dataGridView_CurrentCellChanged(this, EventArgs.Empty);
                }
            }
        }

        public void Reset()
        {
            Activities = null;
            _effectiveDate = DateTime.MinValue;
        }

        public void Reset(DateTime date, Collection<TaskActivity> activities)
        {
            Activities = activities;
            _effectiveDate = date;
        }

        public void DisplayProperties()
        {
            if ((dataGridView.CurrentRow != null) && (dataGridView.CurrentRow.DataBoundItem != null))
            {
                propertiesToolStripMenuItem_Click(this, EventArgs.Empty);
            }
        }


        public override bool Focused
        {
            get { return dataGridView.Focused; }
        }


        public DateTime EffectiveDate
        {
            get { return _effectiveDate; }
        }

        private Collection<TaskActivity> Activities
        {
            set
            {
                if (value != null)
                {
                    List<TaskActivity> sortedList = new List<TaskActivity>(value);
                    RedrawGrid(sortedList);
                }
                else
                {
                    _bindingSource.DataSource = null;
                    dataGridView.Rows.Clear();
                    dataGridView.Enabled = false;
                    summaryNumberToolStripLabel.Text = "";
                    summaryToolStripLabel.Text = "";
                    if (activitiesDraw1.Visible == true)
                    {
                        activitiesDraw1.TaskActivities = null;
                    }
                }
            }
        }

        private void RedrawGrid(List<TaskActivity> sortedList)
        {
            switch (sortColumn)
            {
                case 1:
                    if (sortDirection)
                    {
                        sortedList.Sort(TaskActivity.CompareTasksOnStartDate);
                    }
                    else
                    {
                        sortedList.Sort(TaskActivity.ReverseCompareTasksOnStartDate);
                    }
                    break;
                case 2:
                    if (sortDirection)
                    {
                        sortedList.Sort(TaskActivity.CompareTasksOnEndDate);
                    }
                    else
                    {
                        sortedList.Sort(TaskActivity.ReverseCompareTasksOnEndDate);
                    }
                    break;
                case 3:
                    if (sortDirection)
                    {
                        sortedList.Sort(TaskActivity.CompareTasksOnDuration);
                    }
                    else
                    {
                        sortedList.Sort(TaskActivity.ReverseCompareTasksOnDuration);
                    }
                    break;
                case 4:
                    if (sortDirection)
                    {
                        sortedList.Sort(TaskActivity.CompareTasksOnTaskName);
                    }
                    else
                    {
                        sortedList.Sort(TaskActivity.ReverseCompareTasksOnTaskName);
                    }
                    break;
                default:
                    break;
            }
            Collection<TaskActivity> sortedCollection = new Collection<TaskActivity>(sortedList);

            _bindingSource.DataSource = sortedCollection;
            dataGridView.DataSource = _bindingSource;
            dataGridView.Enabled = true;
            summaryNumberToolStripLabel.Text = sortedCollection.Count.ToString();
            if (sortedCollection.Count != 1)
            {
                summaryToolStripLabel.Text = "Activities";
            }
            else
            {
                summaryToolStripLabel.Text = "Activity";
            }
            if (activitiesDraw1.Visible == true)
            {
                activitiesDraw1.TaskActivities = sortedCollection;
            }
        }

        public List<TaskActivity> GetSelectedTaskActivities()
        {
            List<TaskActivity> entries = new List<TaskActivity>();
            foreach (DataGridViewCell cell in dataGridView.SelectedCells)
            {
                if (cell.OwningRow.DataBoundItem is TaskActivity ta)
                {
                    if (!entries.Contains(ta))
                    {
                        entries.Add(ta);
                    }
                }
            }
            return entries;
        }

        public void SelectAll()
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Selected = true;
            }
        }

        #region Grid Events

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            if ((e.RowIndex >= 0) && (e.ColumnIndex >= 0))
            {
                if (dataGridView.CurrentRow.DataBoundItem is TaskActivity current)
                {
                    AppContext.Current.TaskActivitiesProvider.CompleteActivity(current, _effectiveDate);
                }
            }
        }

        private void dataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (AppContext.Current.SettingsProvider != null)
            {
                AppContext.Current.SettingsProvider.Set(
                    string.Format("ColWidth-{0}", e.Column.Name),
                    e.Column.Width,
                    PersistHint.AcrossSessions);
            }
        }

        private void dataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if (activitiesDraw1.Visible == true)
            {
                if (dataGridView.CurrentRow != null)
                {
                    if (dataGridView.CurrentRow.DataBoundItem is TaskActivity current)
                    {
                        activitiesDraw1.SelectedActivity = current;
                    }
                }
            }
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("The data you entered is invalid, please amment the value.",
                            "Invalid Input Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "dataGridViewTaskDescription")
            {
                TaskDescription taskDescription = AppContext.Current.ShowDescriptionExplorer();
                if (taskDescription.IsNotEmpty())
                {
                    if (dataGridView.Rows[e.RowIndex].DataBoundItem is TaskActivity current)
                    {
                        current.TaskDescription = taskDescription;
                        AppContext.Current.TaskActivitiesProvider.CompleteActivity(current);
                    }
                }
            }
        }

        #endregion

        #region Context Menu

        private void contextMenuStripForGrid_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 1)
            {
                alignToolStripMenuItem.Enabled = true;
                //this.mergeToolStripMenuItem.Enabled = true;
            }
            else
            {
                alignToolStripMenuItem.Enabled = false;
                //this.mergeToolStripMenuItem.Enabled = false;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime selectedRowTime;
            if ((this.dataGridView.SelectedRows[0] != null) && (this.dataGridView.SelectedRows[0].Cells[1] != null))
            {
                selectedRowTime = Convert.ToDateTime(this.dataGridView.SelectedRows[0].Cells[1].Value);
                selectedRowTime = selectedRowTime.AddSeconds(-10);

                TaskDescription taskDescription = AppContext.Current.TaskDescriptionsProvider.TaskDescriptions[0];
                TaskActivity pastTaskActivity =
                    new TaskActivity(taskDescription,
                                     AppContext.Current.IdentityProvider.Principal.Identity.Name)
                    {
                        StartDate = selectedRowTime,
                        EndDate = selectedRowTime.AddSeconds(1),
                        Remarks = "Inserted"
                    };
                AppContext.Current.TaskActivitiesProvider.CompleteActivity(pastTaskActivity);
                SelectRow(pastTaskActivity);
            }
            else if (_effectiveDate > DateTime.MinValue)
            {
                _effectiveDate = _effectiveDate.AddSeconds(-10);

                TaskDescription taskDescription = AppContext.Current.TaskDescriptionsProvider.TaskDescriptions[0];
                TaskActivity pastTaskActivity =
                    new TaskActivity(taskDescription,
                                     AppContext.Current.IdentityProvider.Principal.Identity.Name)
                    {
                        StartDate = _effectiveDate,
                        EndDate = _effectiveDate.AddSeconds(1),
                        Remarks = "Inserted"
                    };
                AppContext.Current.TaskActivitiesProvider.CompleteActivity(pastTaskActivity);
                SelectRow(pastTaskActivity);
            }
        }

        private void deleteCurrentRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<TaskActivity> forDelete = new List<TaskActivity>();
            for (int i = dataGridView.Rows.Count - 1; i >= 0; i--)
            {
                if (dataGridView.Rows[i].Selected == true)
                {
                    if (dataGridView.Rows[i].DataBoundItem is TaskActivity current)
                    {
                        forDelete.Add(current);
                    }
                }
            }
            foreach (TaskActivity taskActivity in forDelete)
            {
                AppContext.Current.TaskActivitiesProvider.RemoveActivity(taskActivity);
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<TaskActivity> selectedActivities = GetSelectedTaskActivities();
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

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<TaskActivity> entries = null;
            using (StringReader sr = new StringReader(Clipboard.GetText()))
            {
                entries = (List<TaskActivity>)new XmlSerializer(typeof(List<TaskActivity>)).Deserialize(sr);
            }

            if (entries != null)
            {
                foreach (TaskActivity ta in entries)
                {
                    ta.Id = Guid.NewGuid();
                    AppContext.Current.TaskActivitiesProvider.CompleteActivity(ta);
                }
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow.DataBoundItem is TaskActivity taskActivity)
            {
                PropertiesForm pf = new PropertiesForm
                {
                    WorkingWithObject = taskActivity
                };
                pf.ShowDialog(this);
                SelectRow(taskActivity);
            }
        }

        private void alignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskActivity current = null;
            int insequence = -100;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Selected == true)
                {
                    if ((dataGridView.Rows[i].DataBoundItem is TaskActivity next) && (current != null))
                    {
                        if (i - 1 == insequence)
                        {
                            TaskActivity.AlignEndToStart(current, next);
                            AppContext.Current.TaskActivitiesProvider.CompleteActivity(current);
                        }
                    }
                    current = dataGridView.Rows[i].DataBoundItem as TaskActivity;
                    insequence = i;
                }
            }
        }

        #endregion

        public void SelectRow(TaskActivity taskActivity)
        {
            if (taskActivity != null)
            {
                dataGridView.Focus();
                foreach (DataGridViewRow dgvr in dataGridView.Rows)
                {
                    if (((TaskActivity)dgvr.DataBoundItem).Id == taskActivity.Id)
                    {
                        dgvr.Selected = true;
                        dataGridView.CurrentCell = dataGridView[1, dgvr.Index];
                    }
                    else
                    {
                        dgvr.Selected = false;
                    }
                }
            }
        }


        private void dataGridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (e.Alt == true))
            {
                DisplayProperties();
                SendKeys.Send("{ESCAPE}");
            }

            if (e.KeyCode == Keys.Delete)
            {
                deleteCurrentRowToolStripMenuItem_Click(this, EventArgs.Empty);
            }

            if (e.KeyCode == Keys.Insert)
            {
                addToolStripMenuItem_Click(this, EventArgs.Empty);
            }
        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "dataGridViewTaskDescription")
            {
                changeTaskDescriptionToolStripMenuItem.Enabled = true;
            }
            else
            {
                changeTaskDescriptionToolStripMenuItem.Enabled = false;
            }
        }

        private void changeTaskDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewCell dgc = dataGridView.CurrentCell;
            dataGridView_CellContentDoubleClick(dataGridView,
                                                new DataGridViewCellEventArgs(dgc.ColumnIndex, dgc.RowIndex));
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sortColumn != e.ColumnIndex)
            {
                sortColumn = e.ColumnIndex;
            }
            else
            {
                sortDirection = !sortDirection;
            }
            AppContext.Current.SettingsProvider.Set("RecordedTasksSortOnColumn", sortColumn, PersistHint.AcrossSessions);
            AppContext.Current.SettingsProvider.Set("RecordedTasksSortDirection", sortDirection, PersistHint.AcrossSessions);
            RedrawGrid(new List<TaskActivity>((Collection<TaskActivity>)_bindingSource.DataSource));
        }
    }
}