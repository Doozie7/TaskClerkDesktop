using System.Globalization;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// 
    /// </summary>
    internal partial class VisualSummary : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualSummary"/> class.
        /// </summary>
        public VisualSummary()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the total minutes.
        /// </summary>
        /// <value>The total minutes.</value>
        public decimal TotalMinutes
        {
            set
            {
                totalMinutesTextBox.Text = value.ToString("0", CultureInfo.InvariantCulture);
                decimal hours = value/60;
                totalHoursTextBox.Text = hours.ToString("0.00", CultureInfo.InvariantCulture);
            }
        }
    }
}