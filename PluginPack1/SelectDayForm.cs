using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk
{
    public partial class SelectDayForm : Form
    {

        private DateTime _selectedDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectDayForm"/> class.
        /// </summary>
        public SelectDayForm()
        {
            InitializeComponent();
            dateTimePicker.Value = DateTime.Today;
        }

        /// <summary>
        /// Gets or sets the selected date.
        /// </summary>
        /// <value>The selected date.</value>
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; }
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SelectedDate = dateTimePicker.Value;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	
    }
}