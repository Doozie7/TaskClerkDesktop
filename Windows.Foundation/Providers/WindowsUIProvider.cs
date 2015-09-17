using System;
using System.Drawing;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// The windows UI provider
    /// </summary>
    public class WindowsUIProvider : UIProvider
    {
        private bool _activityExplorerVisible;
        private bool _descriptionExplorerVisible;

        /// <summary>
        /// Shows the activity explorer.
        /// </summary>
        /// <returns></returns>
        public override DialogResult ShowActivityExplorer()
        {
            DialogResult dResult = DialogResult.OK;
            if (_activityExplorerVisible == false)
            {
                _activityExplorerVisible = true;
                Type type = (Type) Engine.SettingsProvider.Get("TaskActivitiesExplorer");
                dResult = ShowForm(type);
                if (dResult == DialogResult.Abort)
                {
                    Application.Exit();
                }
                _activityExplorerVisible = false;
            }
            return dResult;
        }

        /// <summary>
        /// Shows the description explorer.
        /// </summary>
        /// <returns></returns>
        public override TaskDescription ShowDescriptionExplorer()
        {
            if (_descriptionExplorerVisible == false)
            {
                _descriptionExplorerVisible = true;
                Type type = (Type) Engine.SettingsProvider.Get("TaskDescriptionsExplorer");
                ShowForm(type);
                _descriptionExplorerVisible = false;
            }
            return (TaskDescription) Engine.SettingsProvider.Get("SelectedTaskDescription", TaskDescription.Empty);
        }

        /// <summary>
        /// Shows the form.
        /// </summary>
        /// <param name="formType">Type of the form.</param>
        /// <returns></returns>
        private DialogResult ShowForm(Type formType)
        {
            if (formType == null)
            {
                throw new ArgumentNullException("formType");
            }

            DialogResult dr = DialogResult.Ignore;
            Form form = (Form) Activator.CreateInstance(formType) as Form;
            if (form != null)
            {
                SetFormSizeAndLocation(form);
                dr = form.ShowDialog();
                StoreFormSizeAndLocation(form);
            }
            return dr;
        }

        /// <summary>
        /// Stores the form size and location.
        /// </summary>
        /// <param name="form">The form.</param>
        private void StoreFormSizeAndLocation(Form form)
        {
            if (form.WindowState == FormWindowState.Normal)
            {
                Engine.SettingsProvider.Set(
                    string.Format("{0}ClientSize",
                    form.GetType().Name),
                    form.ClientSize,
                    PersistHint.AcrossSessions);
                Engine.SettingsProvider.Set(
                    string.Format("{0}Location",
                    form.GetType().Name),
                    form.Location,
                    PersistHint.AcrossSessions);
            }
        }

        /// <summary>
        /// Sets the form size and location.
        /// </summary>
        /// <param name="form">The form.</param>
        private void SetFormSizeAndLocation(Form form)
        {
            form.WindowState = FormWindowState.Normal;
            form.ClientSize = (Size)Engine.SettingsProvider.Get(
                                         string.Format("{0}ClientSize", 
                                         form.GetType().Name), 
                                         form.Size);
            if (form.Width <= 0)
            {
                form.Width = 300;
            }
            if (form.Height <= 0)
            {
                form.Height = 100;
            }

            form.Location = (Point)Engine.SettingsProvider.Get(
                                        string.Format("{0}Location", 
                                        form.GetType().Name), 
                                        form.Location);
            if (form.Left < 0)
            {
                form.Left = 0;
            }

            if (form.Top < 0)
            {
                form.Top = 0;
            }

            form.TopLevel = true;
        }

        /// <summary>
        /// Shows the nag message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public override DialogResult ShowNagMessage(string message)
        {
            _notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            _notifyIcon.BalloonTipTitle = "Current Task";
            _notifyIcon.BalloonTipText = message;
            _notifyIcon.ShowBalloonTip(5);

            return DialogResult.OK; // MessageBox.Show(message);
        }

        /// <summary>
        /// Shows the options explorer.
        /// </summary>
        /// <returns></returns>
        public override DialogResult ShowOptionsExplorer()
        {
            Type type = (Type) Engine.SettingsProvider.Get("OptionsExplorer");
            return ShowForm(type);
        }

        private NotifyIcon _notifyIcon;

        /// <summary>
        /// Gets or sets the notify icon.
        /// </summary>
        /// <value>The notify icon.</value>
        public NotifyIcon NotifyIcon
        {
            get { return _notifyIcon; }
            set { _notifyIcon = value; }
        }
    }
}