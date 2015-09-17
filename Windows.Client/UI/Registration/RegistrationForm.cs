using System;
using System.Windows.Forms;
using BritishMicro.TaskClerk.Properties;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// Allows a user to register.
    /// </summary>
    public partial class RegistrationForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationForm"/> class.
        /// </summary>
        public RegistrationForm()
        {
            InitializeComponent();

            //Registration Service
            AppContext.Current.SettingsProvider.Set(
                "RegistrationServer",
                Resources.RegistrationServer,
                PersistHint.AcrossSessions);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}