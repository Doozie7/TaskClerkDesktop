using System;
using BritishMicro.TaskClerk.Plugins;
using System.ComponentModel;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// Provides access to the configuration settings for when TaskClerk starts and stops.
    /// </summary>
    [Description("Provides access to the configuration settings for when TaskClerk starts and stops.")]
    internal partial class StartAndStop : PluginOptionControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartAndStop"/> class.
        /// </summary>
        public StartAndStop()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is called after the engine has initalised the component
        /// </summary>
        protected override void OnTaskClerkInit()
        {
            base.OnTaskClerkInit();
            startCheckBox.Checked = (bool) Engine.SettingsProvider.Get("StartTaskEnabled", startCheckBox.Checked);
            stopCheckBox.Checked = (bool) Engine.SettingsProvider.Get("StopTaskEnabled", stopCheckBox.Checked);
            suspendCheckBox.Checked = (bool) Engine.SettingsProvider.Get("SuspendTaskEnabled", stopCheckBox.Checked);

            TaskDescription startTaskDescription = Engine.SettingsProvider.Get("StartTaskDescription", TaskDescription.Empty) as TaskDescription;
            if (startTaskDescription != null)
            {
                startEventTextBox.Text = startTaskDescription.Name;
            }

            TaskDescription startTaskDescription2 = Engine.SettingsProvider.Get("StartTaskDescription2", TaskDescription.Empty) as TaskDescription;
            if (startTaskDescription2 != null)
            {
                startActivityTextBox.Text = startTaskDescription2.Name;
            }

            TaskDescription stopTaskDescription = Engine.SettingsProvider.Get("StopTaskDescription", TaskDescription.Empty) as TaskDescription;
            if (stopTaskDescription != null)
            {
                stopTaskTextBox.Text = stopTaskDescription.Name;
            }

            TaskDescription suspendTaskDescription = Engine.SettingsProvider.Get("SuspendTaskDescription", TaskDescription.Empty) as TaskDescription;
            if (suspendTaskDescription != null)
            {
                suspendTaskTextBox.Text = suspendTaskDescription.Name;
            }

            dateTimePicker1.Value = (DateTime) Engine.SettingsProvider.Get("StartWorkingHours", dateTimePicker1.Value);
            dateTimePicker2.Value = (DateTime) Engine.SettingsProvider.Get("StopWorkingHours", dateTimePicker2.Value);
            mainBreakStartDateTimePicker.Value =
                (DateTime) Engine.SettingsProvider.Get("StartMainBreak", mainBreakStartDateTimePicker.Value);
            mainBreakEndDateTimePicker.Value =
                (DateTime) Engine.SettingsProvider.Get("StopMainBreak", mainBreakEndDateTimePicker.Value);

            DayDuration_ValueChanged(this, EventArgs.Empty);
            BreakDuration_ValueChanged(this, EventArgs.Empty);
            textBoxTimeZone.Text = TimeZone.CurrentTimeZone.StandardName;
        }

        /// <summary>
        /// Handles the ValueChanged event of the DayDuration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DayDuration_ValueChanged(object sender, EventArgs e)
        {
            textBoxTotalHours.Text = CalculateDuration(dateTimePicker1.Value, dateTimePicker2.Value);
            Engine.SettingsProvider.Set("StartWorkingHours", dateTimePicker1.Value, PersistHint.AcrossSessions);
            Engine.SettingsProvider.Set("StopWorkingHours", dateTimePicker2.Value, PersistHint.AcrossSessions);
        }

        /// <summary>
        /// Calculates the duration.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        private string CalculateDuration(DateTime start, DateTime end)
        {
            TimeSpan timeSpan = new TimeSpan(end.Hour, end.Minute, 0);
            timeSpan -= new TimeSpan(start.Hour, start.Minute, 0);
            return timeSpan.TotalHours.ToString();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the startCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void startCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            startEventTextBox.Enabled = startCheckBox.Checked;
            startActivityTextBox.Enabled = startCheckBox.Checked;
            startEventButton.Enabled = startCheckBox.Checked;
            startActivityButton.Enabled = startCheckBox.Checked;
            Engine.SettingsProvider.Set("StartTaskEnabled", startCheckBox.Checked, PersistHint.AcrossSessions);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the stopCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void stopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            stopTaskTextBox.Enabled = stopCheckBox.Checked;
            stopButton.Enabled = stopCheckBox.Checked;
            Engine.SettingsProvider.Set("StopTaskEnabled", stopCheckBox.Checked, PersistHint.AcrossSessions);
        }

        /// <summary>
        /// Handles the ValueChanged event of the BreakDuration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BreakDuration_ValueChanged(object sender, EventArgs e)
        {
            breakDurationTextBox.Text =
                CalculateDuration(mainBreakStartDateTimePicker.Value, mainBreakEndDateTimePicker.Value);
            Engine.SettingsProvider.Set("StartMainBreak", mainBreakStartDateTimePicker.Value,
                                        PersistHint.AcrossSessions);
            Engine.SettingsProvider.Set("StopMainBreak", mainBreakEndDateTimePicker.Value, PersistHint.AcrossSessions);
        }

        /// <summary>
        /// Handles the Click event of the startButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void startButton_Click(object sender, EventArgs e)
        {
            TaskDescription start = Engine.UIProvider.ShowDescriptionExplorer();
            if (start != null)
            {
                if (start.IsEvent)
                {
                    Engine.SettingsProvider.Set("StartTaskDescription", start, PersistHint.AcrossSessions);
                    startEventTextBox.Text = start.Name;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the stopButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void stopButton_Click(object sender, EventArgs e)
        {
            TaskDescription stop = Engine.UIProvider.ShowDescriptionExplorer();
            if (stop != null)
            {
                Engine.SettingsProvider.Set("StopTaskDescription", stop, PersistHint.AcrossSessions);
                stopTaskTextBox.Text = stop.Name;
            }
        }

        /// <summary>
        /// Handles the Click event of the suspendButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void suspendButton_Click(object sender, EventArgs e)
        {
            TaskDescription suspend = Engine.UIProvider.ShowDescriptionExplorer();
            if (suspend != null)
            {
                Engine.SettingsProvider.Set("SuspendTaskDescription", suspend, PersistHint.AcrossSessions);
                suspendTaskTextBox.Text = suspend.Name;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the suspendCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void suspendCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            suspendTaskTextBox.Enabled = suspendCheckBox.Checked;
            suspendButton.Enabled = suspendCheckBox.Checked;
            Engine.SettingsProvider.Set("SuspendTaskEnabled", suspendCheckBox.Checked, PersistHint.AcrossSessions);
        }

        /// <summary>
        /// Handles the Click event of the startActivityButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void startActivityButton_Click(object sender, EventArgs e)
        {
            TaskDescription start = Engine.UIProvider.ShowDescriptionExplorer();
            if (start != null)
            {
                Engine.SettingsProvider.Set("StartTaskDescription2", start, PersistHint.AcrossSessions);
                startActivityTextBox.Text = start.Name;
            }
        }

        private void startEventTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}