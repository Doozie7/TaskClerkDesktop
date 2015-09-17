using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BritishMicro.TaskClerk.Providers.Sql
{
    /// <summary>
    /// This form provides a user with the opertunity
    /// </summary>
    internal partial class SqlLogin : Form
    {
        private SqlIdentityProvider _sip;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlLogin"/> class.
        /// </summary>
        public SqlLogin(SqlIdentityProvider sip)
        {
            InitializeComponent();
            _sip = sip;
            Height = 210;
        }

        /// <summary>
        /// Handles the Click event of the buttonOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            labelError.Text = "";
            string error = string.Empty;
            if (!groupBoxChangePassword.Enabled)
            {
                error = _sip.AttemptLogin(textBoxUsername.Text, textBoxPassword.Text);
            }
            else
            {
                if (textboxNew1.Text == textboxNew2.Text)
                {
                    error = _sip.ChangePassword(textBoxUsername.Text, textBoxPassword.Text, textboxNew1.Text);
                }
                else
                {
                    error = "New passwords must match, change your password.";
                }
                groupBoxChangePassword.Enabled = false;
                Height = 210;
            }
            if (error == string.Empty)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                System.Threading.Thread.Sleep(250);
                labelError.Text = error;
                if (error.Contains("change your password"))
                {
                    ChangePassword();
                }
                return;
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ChangePassword()
        {
            Height = 365;
            groupBoxChangePassword.Enabled = true;
            textboxNew1.Text = string.Empty;
            textboxNew2.Text = string.Empty;
            textboxNew1.Focus();
        }
    }
}