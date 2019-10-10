using BritishMicro.Windows;
using System;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class DatePeriodSelector : WizardUserControl
    {
        private readonly ExportForm _mainForm;

        public DatePeriodSelector(ExportForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            StoreInContext(this, EventArgs.Empty);
        }

        private void StoreInContext(object sender, EventArgs e)
        {
            DateTime d = dateTimePickerStart.Value.Date;
            DateTime t = dateTimePickerStartTime.Value.Date;
            _mainForm.StartDateAndTime = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);

            d = dateTimePickerEnd.Value.Date;
            t = dateTimePickerEndTime.Value.Date;
            _mainForm.EndDateAndTime = new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, t.Second);
        }
    }
}