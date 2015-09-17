using System;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk
{
    internal partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            Show();
            timer.Interval = 4000;
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}