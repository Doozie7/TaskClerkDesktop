using System;
using System.Drawing;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class ActivityPanel : UserControl
    {
        private bool InMoveMode;
        private int OffsetX;
        public TaskActivity Activity;
        public float _step;

        public ActivityPanel(TaskActivity activity, Rectangle clientRectangle, float step)
        {
            InitializeComponent();
            //Visible = false;
            TabStop = false;
            Activity = activity;
            Location = new Point(clientRectangle.X, clientRectangle.Y);
            Size = new Size(clientRectangle.Width, clientRectangle.Height);
            _step = step;
            BackColor = Color.FromArgb(177, Activity.TaskDescription.Color);
            if (Activity.TaskDescription.IsEvent)
            {
                panelRight.Visible = false;
            }
            Anchor = AnchorStyles.Left & AnchorStyles.Right;
            SetToolTip();
            InMoveMode = false;
        }

        private void SetToolTip()
        {
            if (Activity.TaskDescription.IsEvent)
            {
                toolTip.SetToolTip(this, Activity.TaskDescription.Name + " @ " + Activity.StartDate.ToString("HH:mm"));
            }
            else
            {
                string message = string.Format("{0}\n{1:HH:mm} to {2:HH:mm}\n{3:0.0} minutes",
                                               Activity.TaskDescription.Name,
                                               Activity.StartDate,
                                               Activity.EndDate,
                                               Activity.Duration);
                toolTip.SetToolTip(this, message);
            }
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                InMoveMode = true;
                OffsetX = e.X;
            }
        }

        private void panelRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (InMoveMode)
            {
                Width += e.X;
            }
        }

        private void panelLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (InMoveMode)
            {
                Location = new Point(Location.X + e.X - OffsetX, Location.Y);
            }
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            InMoveMode = false;
            DateTime startDate = DateTime.Parse(Activity.StartDate.ToShortDateString());
            DateTime endDate = startDate;
            float start = (((float) Location.X)/_step) + 1;
            float end = (((float) (Location.X + Width))/_step) + 1;
            DateTime newStartTime = TaskActivity.StripSeconds(startDate.AddMinutes((double) start));
            DateTime newEndTime = TaskActivity.StripSeconds(endDate.AddMinutes((double) end));
            if ((Activity.StartDate != newStartTime) || (Activity.EndDate != newEndTime))
            {
                Activity.StartDate = newStartTime;
                Activity.EndDate = newEndTime;
                AppContext.Current.TaskActivitiesProvider.CompleteActivity(Activity);
            }
        }

        private void ActivityPanel_MouseHover(object sender, EventArgs e)
        {
            SetToolTip();
        }
    }
}