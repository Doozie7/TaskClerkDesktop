using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class ActivitiesDraw : UserControl
    {
        private static readonly object syncLoc = new object();

        private Collection<TaskActivity> _taskActivities;
        private TaskActivity _selectedTaskActivity;

        public ActivitiesDraw()
        {
            InitializeComponent();
        }

        public Collection<TaskActivity> TaskActivities
        {
            set
            {
                _taskActivities = value;
                lock (syncLoc)
                {
                    DrawTaskActivities();
                }
            }
        }

        public TaskActivity SelectedActivity
        {
            set
            {
                if ((_selectedTaskActivity != value) && (_taskActivities != null))
                {
                    _selectedTaskActivity = value;
                    foreach (TaskActivity taskActivity in _taskActivities)
                    {
                        if ((ActivityPanel)Controls[taskActivity.Id.ToString()] is ActivityPanel panel)
                        {
                            if (taskActivity == _selectedTaskActivity)
                            {
                                panel.BorderStyle = BorderStyle.FixedSingle;
                                panel.BringToFront();
                            }
                            else
                            {
                                panel.BorderStyle = BorderStyle.None;
                            }
                        }
                    }
                }
            }
        }

        [DebuggerStepThroughAttribute]
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            Pen backgroundPen = new Pen(new SolidBrush(Color.LightGray), 1);
            float width = (float)ClientRectangle.Width;
            float height = (float)ClientRectangle.Height;
            for (float timeMarkers = 0; timeMarkers < 24; timeMarkers++)
            {
                float pos = (width / 24) * timeMarkers;
                PointF p1 = new PointF(pos, 0);
                PointF p2 = new PointF(pos, height);
                g.DrawLine(backgroundPen, p1, p2);
                p2.Y -= 12;
                g.DrawString(
                    timeMarkers.ToString(),
                    new Font(FontFamily.GenericMonospace, 8),
                    new SolidBrush(Color.Gray),
                    p2);
            }
        }

        private void DrawTaskActivities()
        {
            Controls.Clear();
            if (_taskActivities != null)
            {
                if (_taskActivities.Count > 0)
                {
                    foreach (TaskActivity taskActivity in _taskActivities)
                    {
                        DrawTaskActivity(taskActivity);
                    }
                }
            }
        }

        private void DrawTaskActivity(TaskActivity taskActivity)
        {
            if (taskActivity.TaskDescription.Equals(TaskDescription.Empty))
            {
                return;
            }

            float width = (float)ClientRectangle.Width;
            float step = width / (24 * 60);
            float startPosX = ((taskActivity.StartDate.Hour * 60) + taskActivity.StartDate.Minute) * step;
            float endPosX = ((taskActivity.EndDate.Hour * 60) + taskActivity.EndDate.Minute) * step;
            float startPosY = ClientRectangle.Height;
            float endPosY = ClientRectangle.Height;
            if (taskActivity.TaskDescription.IsEvent)
            {
                startPosY = startPosY / 4;
                endPosY = (endPosY / 3) * 2;
                endPosX += 1;
            }
            else
            {
                startPosY = startPosY / 3;
                endPosY = (endPosY / 3) * 2;
            }

            Rectangle rect = new Rectangle(
                (int)startPosX,
                (int)startPosY,
                (int)endPosX - (int)startPosX,
                (int)endPosY - (int)startPosY);

            ActivityPanel ap = new ActivityPanel(taskActivity, rect, step)
            {
                Name = taskActivity.Id.ToString(),
                Parent = this,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            Controls.Add(ap);
        }
    }
}