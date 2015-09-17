using System;
using System.Windows.Forms;
using BritishMicro.TaskClerk.Plugins;
using System.ComponentModel;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// Provides backup and restore functionality for the file based data store in TaskClerk.
    /// </summary>
    [Description("Provides backup and restore functionality for the file based data store in TaskClerk.")]
    internal partial class BackupAndRestore : PluginOptionControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackupAndRestore"/> class.
        /// </summary>
        public BackupAndRestore()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is called after the engine has initalised the component
        /// </summary>
        protected override void OnTaskClerkInit()
        {
            base.OnTaskClerkInit();
            textBoxBackupDirectory.Text =
                (string) Engine.SettingsProvider.Get("BackupDirectory", textBoxBackupDirectory.Text);
            textBoxRestoreDirectory.Text =
                (string) Engine.SettingsProvider.Get("RestoreDirectory", textBoxRestoreDirectory.Text);
            comboBoxBackupFrequency.Text =
                (string) Engine.SettingsProvider.Get("BackupFrequency", comboBoxBackupFrequency.Text);
            DateTime lastBackup = (DateTime)Engine.SettingsProvider.Get("LastBackup", DateTime.MinValue);
            labelLastBackup.Text = lastBackup.ToString("f");

            //Now wire up the events
            textBoxBackupDirectory.TextChanged += new EventHandler(ChangedEvent);
            textBoxRestoreDirectory.TextChanged += new EventHandler(ChangedEvent);
            comboBoxBackupFrequency.TextChanged += new EventHandler(ChangedEvent);

            //now call the event to ensure the view is correct
            ChangedEvent(this, EventArgs.Empty);
        }

        /// <summary>
        /// Changeds the event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ChangedEvent(object sender, EventArgs e)
        {
            Engine.SettingsProvider.Set("BackupDirectory", textBoxBackupDirectory.Text, PersistHint.AcrossSessions);
            Engine.SettingsProvider.Set("RestoreDirectory", textBoxRestoreDirectory.Text, PersistHint.AcrossSessions);
            Engine.SettingsProvider.Set("BackupFrequency", comboBoxBackupFrequency.Text, PersistHint.AcrossSessions);
        }

        /// <summary>
        /// Handles the Click event of the buttonBackupDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonBackupDirectory_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Please select the location where to backup your data files too.";
            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                textBoxBackupDirectory.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonRestoreDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonRestoreDirectory_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Please select the location where to restore your data files from.";
            if (DialogResult.OK == folderBrowserDialog.ShowDialog())
            {
                textBoxRestoreDirectory.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonBackupNow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonBackupNow_Click(object sender, EventArgs e)
        {
            if (Engine.TaskActivitiesProvider.Backup())
            {
                Engine.SettingsProvider.Set("LastBackup", DateTime.Now, PersistHint.AcrossSessions);
            }
            DateTime lastBackup = (DateTime)Engine.SettingsProvider.Get("LastBackup", DateTime.MinValue);
            labelLastBackup.Text = lastBackup.ToString("f");
            MessageBox.Show(
                "Backup complete",
                "Backup",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}