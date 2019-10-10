using System;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.ToDoPlugin
{
    public partial class ToDoOptionsDialog : Form
    {
        public ToDoOptionsDialog(ToDoOptions options)
        {
            InitializeComponent();
            propertyGrid.SelectedObject = options;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public ToDoOptions Options
        {
            get { return this.propertyGrid.SelectedObject as ToDoOptions; }
        }

    }
}