using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Threading;
using BritishMicro.TaskClerk.Plugins;
using System.Diagnostics;
using BritishMicro.TaskClerk.Properties;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// This options dialog provides TaskClerk with some general
    /// information.
    /// </summary>
    [Description("Provides general settings for TaskClerk in the windows environment.")]
    internal partial class General : PluginOptionControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="General"/> class.
        /// </summary>
        public General()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is called after the engine has initalised the component
        /// </summary>
        protected override void OnTaskClerkInit()
        {
            base.OnTaskClerkInit();
            textboxUserName.Text =
                (string)Engine.SettingsProvider.Get("CurrentUsersName", "Unknown");
            textboxEmailAddress.Text =
                (string) Engine.SettingsProvider.Get("CurrentUserEmailAddress", "unknown@unknown");
            textboxCulture.Text =
                (string) Engine.SettingsProvider.Get("CurrentUserCulture", Thread.CurrentThread.CurrentCulture.Name);

            checkBoxAutoStart.Checked = AppContext.Current.IsAutoStartRegistrySet(Assembly.GetEntryAssembly());

            textboxWebStartPage.Text =
                (string)Engine.SettingsProvider.Get("WebStartPage", Resources.WebStartPage);

            textboxUserName.TextChanged += new EventHandler(Settings_Changed);
            textboxEmailAddress.TextChanged += new EventHandler(Settings_Changed);
            textboxCulture.TextChanged += new EventHandler(Settings_Changed);
            checkBoxAutoStart.CheckedChanged += new EventHandler(Settings_Changed);
            textboxWebStartPage.TextChanged += new EventHandler(Settings_Changed);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture =
                CultureInfo.CreateSpecificCulture(
                    (string) Engine.SettingsProvider.Get("CurrentUserCulture", Thread.CurrentThread.CurrentCulture.Name));
            textboxCulture.Text = Thread.CurrentThread.CurrentCulture.Name;
        }

        private void Settings_Changed(object sender, EventArgs e)
        {
            Engine.SettingsProvider.Set("CurrentUsersName", textboxUserName.Text, PersistHint.AcrossSessions);
            Engine.SettingsProvider.Set("CurrentUserEmailAddress", textboxEmailAddress.Text, PersistHint.AcrossSessions);
            Engine.SettingsProvider.Set("CurrentUserCulture", textboxCulture.Text, PersistHint.AcrossSessions);
            Engine.SettingsProvider.Set("WebStartPage", textboxWebStartPage.Text, PersistHint.AcrossSessions);
            Engine.SettingsProvider.Set("AutoStart", checkBoxAutoStart.Checked, PersistHint.AcrossSessions);
            AppContext.Current.RegistrySetup(Assembly.GetEntryAssembly(), checkBoxAutoStart.Checked);
        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.ietf.org/rfc/rfc1766.txt?number=1766");
        }
    }
}