using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// This dialog allows the user to create a new TaskDescription from the 
    /// most basic of information.
    /// </summary>
    public partial class NewTaskDescriptionDialog : Form
    {

        private TaskDescription _taskDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewTaskDescriptionDialog"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public NewTaskDescriptionDialog(TaskDescription parent)
        {
            InitializeComponent();
            _taskDescription = new TaskDescription();
            _taskDescription.Color = Color.Black;
            _taskDescription.Name = string.Empty;
            _taskDescription.NoNagMinutes = 0;
            if (parent != null)
            {
                this.buttonColorPicker.BackColor = Color.FromArgb(parent.Color.A - 2, parent.Color);
                this.textBoxColour.Text = TaskDescription.SerializeColor(this.buttonColorPicker.BackColor);
                this.maskedTextBoxNoNagMins.Text = parent.NoNagMinutes.ToString();
            }
            else
            {
                this.buttonColorPicker.BackColor = Color.FromArgb(Color.Black.A - 2, Color.Black);
                this.textBoxColour.Text = TaskDescription.SerializeColor(this.buttonColorPicker.BackColor);
                this.maskedTextBoxNoNagMins.Text = "0";
            }
        }

        private void buttonColorPicker_Click(object sender, EventArgs e)
        {
            colorDialog.Color = this.buttonColorPicker.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.buttonColorPicker.BackColor = colorDialog.Color;
                this.textBoxColour.Text = TaskDescription.SerializeColor(this.buttonColorPicker.BackColor);
            }
        }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>The task description.</value>
        public TaskDescription TaskDescription
        {
            get { return _taskDescription; }
            set { _taskDescription = value; }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            _taskDescription.Name = textBoxName.Text;
            _taskDescription.Color = this.buttonColorPicker.BackColor;
            _taskDescription.NoNagMinutes = int.Parse(maskedTextBoxNoNagMins.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }	
    }
}