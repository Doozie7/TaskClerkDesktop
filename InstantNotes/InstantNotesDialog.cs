using System;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.InstantNotes
{
    public partial class InstantNotesDialog : Form
    {
        public InstantNotesDialog()
        {
            InitializeComponent();
            TaskActivity currentActivity = AppContext.Current.CurrentActivity;
            if (currentActivity.IsNotEmpty())
            {
                textBoxRemark.Text = currentActivity.Remarks;
                textBoxRemark.Enabled = true;
                textBoxRemark.Focus();
            }
            else
            {
                textBoxRemark.Text = "no currently task";
                textBoxRemark.Enabled = false;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            TaskActivity currentActivity = AppContext.Current.CurrentActivity;
            if (currentActivity.IsNotEmpty())
            {
                currentActivity.Remarks = textBoxRemark.Text;
            }
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
