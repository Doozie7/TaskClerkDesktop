using BritishMicro.Windows;
using System.Text;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class ExportSummary : WizardUserControl
    {

        private readonly ExportForm _mainForm;

        public ExportSummary(ExportForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        public override void PrepareContents()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("The export engine will carry out the following,\n");
            sb.Append("\nUsing the following exporter:");
            sb.Append("\n\t");
            sb.Append(_mainForm.SelectedExporter.ReturnType().FullName);
            sb.Append("\n\nBetween the following dates");
            sb.Append("\n\t");
            sb.Append(_mainForm.StartDateAndTime.ToString("dd MMM yyyy HH:mm"));
            sb.Append("\n\t");
            sb.Append(_mainForm.EndDateAndTime.ToString("dd MMM yyyy HH:mm"));
            sb.Append("\n\nExporting the following TaskDescription types");
            foreach (
                TaskDescription td in _mainForm.SelectedTaskDescriptions)
            {
                sb.Append("\n\t");
                sb.Append(td.ToString());
            }
            richTextBox.Text = sb.ToString();
        }
    }
}