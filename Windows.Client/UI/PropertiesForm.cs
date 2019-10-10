using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class PropertiesForm : Form
    {
        private TaskActivity _workingWithObject;
        private bool _isReadyForEdit;
        private readonly TaskClerkEngine _engine;

        public PropertiesForm()
        {
            InitializeComponent();
            activitiesDraw1 = new ActivitiesDraw();
            panelActivityDraw.Controls.Add(activitiesDraw1);
            activitiesDraw1.Dock = DockStyle.Fill;
            _isReadyForEdit = false;
            _engine = AppContext.Current;
            _engine.TaskActivitiesProvider.TaskActivitiesChanged += new EventHandler<TaskActivityEventArgs>(TaskActivitiesProvider_TaskActivitiesChanged);
        }

        void TaskActivitiesProvider_TaskActivitiesChanged(object sender, TaskActivityEventArgs e)
        {
            _isReadyForEdit = false;
            dateTimePickerStartDate.Text = e.TaskActivity.StartDate.ToLongDateString();
            dateTimePickerStartTime.Text = e.TaskActivity.StartDate.ToString("HH:mm");
            dateTimePickerEndDate.Text = e.TaskActivity.EndDate.ToLongDateString();
            dateTimePickerEndTime.Text = e.TaskActivity.EndDate.ToString("HH:mm");
            numericUpDown.Value = e.TaskActivity.Duration;
            _isReadyForEdit = true;
        }

        public TaskActivity WorkingWithObject
        {
            set
            {
                _workingWithObject = new TaskActivity
                {
                    Id = value.Id,
                    TaskDescription = value.TaskDescription,
                    CrosstabTaskDescription = value.CrosstabTaskDescription,
                    StartDate = value.StartDate,
                    EndDate = value.EndDate,
                    Duration = value.Duration,
                    CustomFlags = value.CustomFlags,
                    OriginalDate = value.OriginalDate,
                    Remarks = value.Remarks,
                    UserId = value.UserId
                };

                Collection<TaskActivity> single = new Collection<TaskActivity>();
                single.Add(_workingWithObject);
                activitiesDraw1.TaskActivities = single;
                activitiesDraw1.SelectedActivity = _workingWithObject;
            }
        }

        private void PropertiesForm_Load(object sender, EventArgs e)
        {
            if (_workingWithObject != null)
            {
                textBoxTaskDescription.Text = _workingWithObject.TaskDescription.Name;
                textBoxTaskDescription.Tag = _workingWithObject.TaskDescription;
                textBoxRemarks.Text = _workingWithObject.Remarks;
                dateTimePickerStartDate.Text = _workingWithObject.StartDate.ToLongDateString();
                dateTimePickerStartTime.Text = _workingWithObject.StartDate.ToString("HH:mm");
                dateTimePickerEndDate.Text = _workingWithObject.EndDate.ToLongDateString();
                dateTimePickerEndTime.Text = _workingWithObject.EndDate.ToString("HH:mm");
                numericUpDown.Value = _workingWithObject.Duration;

                //Advanced settings
                textBoxActivityId.Text = _workingWithObject.Id.ToString("d");
                if (_workingWithObject.CrosstabTaskDescription != null)
                {
                    textBoxCrosstabTaskDescription.Text = _workingWithObject.CrosstabTaskDescription.Name;
                    textBoxCrosstabTaskDescription.Tag = _workingWithObject.CrosstabTaskDescription;
                }
                textBoxFlags.Text = _workingWithObject.CustomFlags.ToString();
                textBoxOriginalDate.Text = _workingWithObject.OriginalDate.ToString("dd MMM yyyy HH:mm");
                textBoxUserId.Text = _workingWithObject.UserId;
                _isReadyForEdit = true;
            }
        }

        private void dateTimePickerStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (_isReadyForEdit == true)
            {
                DateTime previousValue = (DateTime)((DateTimePicker)sender).Tag;
                if ((previousValue.Minute == 0) || (previousValue.Minute == 59))
                {
                    TimeSpan previousDiff = dateTimePickerStartTime.Value - previousValue;
                    if ((previousValue.Minute == 0) && (previousDiff.Minutes == 59))
                    {
                        dateTimePickerStartTime.Value = dateTimePickerStartTime.Value.Subtract(new TimeSpan(1, 0, 0));
                    }
                    if ((previousValue.Minute == 59) && (previousDiff.Minutes == -59))
                    {
                        dateTimePickerStartTime.Value = dateTimePickerStartTime.Value.Add(new TimeSpan(1, 0, 0));
                    }
                }
                TimeSpan diff = ((DateTimePicker)dateTimePickerEndTime).Value - ((DateTimePicker)sender).Value;
                numericUpDown.Value = (decimal)diff.TotalMinutes;
                ObjectChanged(sender, e);
            }
            dateTimePickerStartTime.Tag = new DateTime(dateTimePickerStartTime.Value.Ticks);
        }

        private void dateTimePickerEndTime_ValueChanged(object sender, EventArgs e)
        {
            if (_isReadyForEdit == true)
            {
                DateTime previousValue = (DateTime)((DateTimePicker)sender).Tag;
                if ((previousValue.Minute == 0) || (previousValue.Minute == 59))
                {
                    TimeSpan previousDiff = dateTimePickerEndTime.Value - previousValue;
                    if ((previousValue.Minute == 0) && (previousDiff.Minutes == 59))
                    {
                        dateTimePickerEndTime.Value = dateTimePickerEndTime.Value.Subtract(new TimeSpan(1, 0, 0));
                    }
                    if ((previousValue.Minute == 59) && (previousDiff.Minutes == -59))
                    {
                        dateTimePickerEndTime.Value = dateTimePickerEndTime.Value.Add(new TimeSpan(1, 0, 0));
                    }
                }
                TimeSpan diff = ((DateTimePicker)sender).Value - ((DateTimePicker)dateTimePickerStartTime).Value;
                numericUpDown.Value = (decimal)diff.TotalMinutes;
                ObjectChanged(sender, e);
            }
            dateTimePickerEndTime.Tag = new DateTime(dateTimePickerEndTime.Value.Ticks);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            AppContext.Current.SettingsProvider.Set("SelectedActivity", TaskActivity.Empty, PersistHint.AcrossSessions);
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            AppContext.Current.TaskActivitiesProvider.CompleteActivity(_workingWithObject);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ObjectChanged(object sender, EventArgs e)
        {
            if (_isReadyForEdit)
            {
                _workingWithObject.Id = new Guid(textBoxActivityId.Text);
                _workingWithObject.TaskDescription = (TaskDescription)textBoxTaskDescription.Tag;
                _workingWithObject.Remarks = textBoxRemarks.Text;
                _workingWithObject.StartDate = new DateTime(
                    dateTimePickerStartDate.Value.Year,
                    dateTimePickerStartDate.Value.Month,
                    dateTimePickerStartDate.Value.Day,
                    dateTimePickerStartTime.Value.Hour,
                    dateTimePickerStartTime.Value.Minute,
                    0);
                _workingWithObject.EndDate = new DateTime(
                    dateTimePickerEndDate.Value.Year,
                    dateTimePickerEndDate.Value.Month,
                    dateTimePickerEndDate.Value.Day,
                    dateTimePickerEndTime.Value.Hour,
                    dateTimePickerEndTime.Value.Minute,
                    0);
                _workingWithObject.CrosstabTaskDescription = (TaskDescription)textBoxCrosstabTaskDescription.Tag;
                _workingWithObject.CustomFlags = Int32.Parse(textBoxFlags.Text);
                _workingWithObject.OriginalDate = DateTime.Parse(textBoxOriginalDate.Text);
                _workingWithObject.UserId = textBoxUserId.Text;

                //Update the mini gantt
                Collection<TaskActivity> single = new Collection<TaskActivity>();
                single.Add(_workingWithObject);
                activitiesDraw1.TaskActivities = single;
                activitiesDraw1.SelectedActivity = _workingWithObject;
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndTime.Value = dateTimePickerStartTime.Value.AddMinutes((double)numericUpDown.Value);
        }

        /// <summary>
        /// display the task selector form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowTaskSelector(object sender, EventArgs e)
        {
            TaskDescription selectedTask = AppContext.Current.ShowDescriptionExplorer();
            if (selectedTask.IsNotEmpty())
            {
                textBoxTaskDescription.Tag = selectedTask;
                textBoxTaskDescription.Text = selectedTask.Name;
            }
        }

        private void buttonAdvancedSimple_Click(object sender, EventArgs e)
        {
            if (buttonAdvancedSimple.Tag == null)
            {
                buttonAdvancedSimple.Tag = true;
                buttonAdvancedSimple.Text = "&View Simple";
                buttonCrossTabButton.TabStop = true;
                Height = 530;
            }
            else
            {
                buttonAdvancedSimple.Tag = null;
                buttonAdvancedSimple.Text = "&View Advanced";
                buttonCrossTabButton.TabStop = false;
                Height = 360;
            }
        }

        private void buttonCrossTabButton_Click(object sender, EventArgs e)
        {
            TaskDescription selectedTask = AppContext.Current.ShowDescriptionExplorer();
            if (selectedTask.IsNotEmpty())
            {
                textBoxCrosstabTaskDescription.Tag = selectedTask;
                textBoxCrosstabTaskDescription.Text = selectedTask.Name;
            }
        }
    }
}