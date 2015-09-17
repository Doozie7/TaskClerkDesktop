using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BritishMicro.TaskClerk;

namespace BritishMicro.TaskClerk.ToDoPlugin
{
    public partial class ToDoItem : UserControl
    {
        private bool _isFirstAccess;
        private bool _isChangeingTime;
        private bool _isChangeingPriority;
        private bool _isChangeingProgress;
        private int _mouseStartPos;
        private DateTime _startTime;
        private ToDoTask _todoTask;

        public ToDoItem() : this(new ToDoTask())
        {
        }

        public ToDoItem(ToDoTask todoTask)
        {
            InitializeComponent();
            _todoTask = todoTask;
            if (string.IsNullOrEmpty(_todoTask.Description))
            {
                this.textBoxDescription.Text = "A short description of what you want to do.";
                this.textBoxDescription.ForeColor = System.Drawing.SystemColors.ActiveBorder;
                _isFirstAccess = true;
            }
            else
            {
                ControlValuesSet();
            }

        }

        private void textBoxDescription_Enter(object sender, EventArgs e)
        {
            if (_isFirstAccess)
            {
                textBoxDescription.ForeColor = SystemColors.WindowText;
                textBoxDescription.Text = "";
                _isFirstAccess = false;
            }
        }

        private void dateTimePickerActionTime_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._isChangeingTime = true;
                this._mouseStartPos = e.Y;
                this._startTime = this.dateTimePickerActionTime.Value.AddSeconds(-this.dateTimePickerActionTime.Value.Second);
            }
        }

        private void dateTimePickerActionTime_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._isChangeingTime)
            {
                DateTime modifiedTime = this._startTime.AddMinutes(this._mouseStartPos - e.Y);
                if (modifiedTime <= this.dateTimePickerActionTime.MaxDate)
                    this.dateTimePickerActionTime.Value = modifiedTime;
            }
        }

        private void dateTimePickerActionTime_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._isChangeingTime = false;
                ControlValuesChanged();
            }
        }

        private void buttonTaskDescription_Click(object sender, EventArgs e)
        {
            TaskDescription taskDescription = AppContext.Current.UIProvider.ShowDescriptionExplorer();
            if (taskDescription != null)
            {
                textBoxTaskDescription.Text = taskDescription.Name;
                _todoTask.TaskDescription = taskDescription;
            }
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            ControlValuesChanged();
        }

        private void comboBoxActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlValuesChanged();
        }

        private void dateTimePickerActionDate_ValueChanged(object sender, EventArgs e)
        {
            ControlValuesChanged();
        }

        private void checkBoxPopupAlarm_CheckedChanged(object sender, EventArgs e)
        {
            ControlValuesChanged();
        }

        private void textBoxDescription_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxDescription.Text))
            {
                this.textBoxDescription.Text = "A short description of what you want to do.";
                this.textBoxDescription.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            }
        }

        public ToDoTask ToDoTask
        {
            get { return _todoTask; }
        }

        private void ControlValuesSet()
        {
            textBoxDescription.Text = _todoTask.Description;
            comboBoxActionType.Text = _todoTask.RemindType;
            dateTimePickerActionDate.Value = _todoTask.RemindDate;
            checkBoxPopupAlarm.Checked = _todoTask.PopupAlarm;
            numericUpDownPriority.Value = _todoTask.Priority;
            numericUpDownProgress.Value = _todoTask.Progress;
        }

        private void ControlValuesChanged()
        {
            _todoTask.Description = textBoxDescription.Text;
            _todoTask.RemindType = comboBoxActionType.Text;
            _todoTask.RemindDate = dateTimePickerActionDate.Value.Date + dateTimePickerActionTime.Value.TimeOfDay;
            _todoTask.PopupAlarm = checkBoxPopupAlarm.Checked;
            _todoTask.Priority = Convert.ToInt32(numericUpDownPriority.Value);
            _todoTask.Progress = Convert.ToInt32(numericUpDownProgress.Value);
        }

        private void numericUpDownPriority_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._isChangeingPriority = true;
                this._mouseStartPos = e.Y;
            }
        }

        private void numericUpDownPriority_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._isChangeingPriority = false;
                ControlValuesChanged();
            }
        }

        private void numericUpDownPriority_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._isChangeingPriority)
            {
                decimal val = numericUpDownPriority.Value + this._mouseStartPos - e.Y;

                if(val < numericUpDownPriority.Minimum)
                    val = numericUpDownPriority.Minimum;

                if(val > numericUpDownPriority.Maximum)
                    val = numericUpDownPriority.Maximum;

                numericUpDownPriority.Value = val;
            }
        }

        private void numericUpDownProgress_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._isChangeingProgress = true;
                this._mouseStartPos = e.Y;
            }
        }

        private void numericUpDownProgress_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._isChangeingProgress = false;
                ControlValuesChanged();
            }
        }

        private void numericUpDownProgress_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._isChangeingProgress)
            {
                decimal val = numericUpDownProgress.Value + this._mouseStartPos - e.Y;

                if (val < numericUpDownProgress.Minimum)
                    val = numericUpDownProgress.Minimum;

                if (val > numericUpDownProgress.Maximum)
                    val = numericUpDownProgress.Maximum;

                numericUpDownProgress.Value = val;
            }
        }
    }
}
