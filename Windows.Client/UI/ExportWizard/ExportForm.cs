using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using BritishMicro.TaskClerk.Plugins;
using BritishMicro.Windows;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class ExportForm : Form
    {
        public ExportForm()
        {
            InitializeComponent();
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            if (AppContext.Current.PluginsProvider.CountOfType(typeof (PluginExporter)) == 0)
            {
                MessageBox.Show(
                    "No plugins of type [PluginExporter] were found.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
                Close();
            }
            else
            {
                wizardHost.AddWizardItem(new ExportSelector(this));
                wizardHost.AddWizardItem(new DatePeriodSelector(this));
                wizardHost.AddWizardItem(new SelectedTaskDescriptions(this));
                wizardHost.AddWizardItem(new ExportSummary(this));
                wizardHost.Bind();
            }
        }

        private void ExportEngineRun()
        {
            List<TaskActivity> appropriateActivities = new List<TaskActivity>();
            TimeSpan totalDays = _endDateAndTime - _startDateAndTime;
            for (int day = 0; day <= totalDays.Days; day++)
            {
                DateTime date = _startDateAndTime.AddDays(day);
                Collection<TaskActivity> allActivities =
                    AppContext.Current.TaskActivitiesProvider.LoadActivities(date);
                foreach (TaskActivity activity in allActivities)
                {
                    if (_selectedTaskDescriptions.Contains(activity.TaskDescription))
                    {
                        if ((activity.StartDate >= _startDateAndTime) && (activity.StartDate <= _endDateAndTime))
                        {
                            appropriateActivities.Add(activity);
                        }
                    }
                }
            }

            LoadableItem loadableItem = _selectedExporter;
            if (loadableItem != null)
            {
                PluginExporter exporter = loadableItem.CreateInstance() as PluginExporter;
                if (exporter != null)
                {
                    exporter.AcceptData(_startDateAndTime, _endDateAndTime, appropriateActivities.ToArray(), null);
                    exporter.BeforeExecuteSummary();
                    exporter.Execute();
                }
            }
            Close();
        }

        private void ExportEngineCancel()
        {
            Close();
        }

        private void wizardHost_WizardHostActionEvent(object sender, WizardEventArgs e)
        {
            if (e.Action == "cancel")
            {
                ExportEngineCancel();
            }

            if (e.Action == "finish")
            {
                ExportEngineRun();
            }
        }

        private LoadableItem _selectedExporter;
        private DateTime _startDateAndTime;
        private DateTime _endDateAndTime;
        private List<TaskDescription> _selectedTaskDescriptions;

        /// <summary>
        /// Gets or sets the selected exporter.
        /// </summary>
        /// <value>The selected exporter.</value>
        internal LoadableItem SelectedExporter
        {
            get { return _selectedExporter; }
            set { _selectedExporter = value; }
        }

        /// <summary>
        /// Gets or sets the start date and time.
        /// </summary>
        /// <value>The start date and time.</value>
        internal DateTime StartDateAndTime
        {
            get { return _startDateAndTime; }
            set { _startDateAndTime = value; }
        }

        /// <summary>
        /// Gets or sets the end date and time.
        /// </summary>
        /// <value>The end date and time.</value>
        internal DateTime EndDateAndTime
        {
            get { return _endDateAndTime; }
            set { _endDateAndTime = value; }
        }

        /// <summary>
        /// Gets or sets the selected task descriptions.
        /// </summary>
        /// <value>The selected task descriptions.</value>
        internal List<TaskDescription> SelectedTaskDescriptions
        {
            get { return _selectedTaskDescriptions; }
            set { _selectedTaskDescriptions = value; }
        }
	


    }
}