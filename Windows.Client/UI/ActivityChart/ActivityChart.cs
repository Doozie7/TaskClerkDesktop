using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class ActivityChart : UserControl
    {
        private Collection<TaskActivity> _activities;
        private DateTime _effectiveDate;

        public ActivityChart()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public void Reset()
        {
            _activities = null;
            _effectiveDate = DateTime.MinValue;
        }

        public void Reset(DateTime date, Collection<TaskActivity> activities)
        {
            visualSummary.Visible = true;
            _activities = activities;
            _effectiveDate = date;
            Draw();
        }

        public void DisplayMenu()
        {
            //contextMenuStripGraph.Show(this.radChart1, 50, 50);
        }

        public void Draw()
        {

            chart1.Series[0].Points.Clear();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;

            Dictionary<string, decimal> tags = new Dictionary<string, decimal>();
            decimal totalMinutes = 0;
            foreach (TaskActivity activity in _activities)
            {
                if (activity.Duration > 0)
                {
                    DataPoint dp = new DataPoint()
                        {
                            Label = activity.TaskDescription.Name,
                            YValues = new double[] { double.Parse(activity.Duration.ToString()) },
                            Color =  activity.TaskDescription.Color,
                            LabelForeColor = System.Drawing.Color.Black,
                            LabelBackColor = System.Drawing.Color.WhiteSmoke,
                            LabelToolTip = activity.Duration.ToString() + " mins"
                    };

                    bool dpFound = false;
                    foreach(DataPoint dpItem in chart1.Series[0].Points)
                    {
                        if(dpItem.Label == dp.Label)
                        {
                            dpItem.YValues[0] = dpItem.YValues[0] + double.Parse(activity.Duration.ToString());
                            dpFound = true;
                            dpItem.LabelToolTip = dpItem.YValues[0].ToString() + " mins";
                            break;
                        }
                    }

                    if (dpFound == false)
                    {
                        chart1.Series[0].Points.Add(dp);
                    }

                    totalMinutes += activity.Duration;
                }
            }

            visualSummary.TotalMinutes = totalMinutes;

        }

        private void ChangeGraphView_Click(object sender, EventArgs e)
        {
            string selectedItem = ((ToolStripMenuItem)sender).Text;
            switch (selectedItem)
            {
                case "Minutes":
                case "Hours":
                case "Percent (%)":
                case "Task":
                    AppContext.Current.SettingsProvider.Set("GraphViewStyle", ((ToolStripMenuItem)sender).Text,
                                                            PersistHint.AcrossSessions);
                    Draw();
                    break;
                default:
                    Draw();
                    break;
            }
            UpdateGraphMenu();
        }

        private void UpdateGraphMenu()
        {
            foreach (ToolStripItem tsmi in contextMenuStripGraph.Items)
            {
                if (tsmi.Tag != null)
                {
                    if (tsmi.Tag.ToString() == "view")
                    {
                        ((ToolStripMenuItem)tsmi).Checked = false;
                        if (tsmi.Text ==
                            (string)AppContext.Current.SettingsProvider.Get("GraphViewStyle", string.Empty))
                        {
                            ((ToolStripMenuItem)tsmi).Checked = true;
                        }
                    }

                    if (tsmi.Tag.ToString() == "group")
                    {
                        ((ToolStripMenuItem)tsmi).Checked = false;
                        if (tsmi.Text ==
                            (string)AppContext.Current.SettingsProvider.Get("GraphGroupStyle", string.Empty))
                        {
                            ((ToolStripMenuItem)tsmi).Checked = true;
                        }
                    }
                }
            }
        }

        private void ChangeGraphGroup_Click(object sender, EventArgs e)
        {
            string selectedItem = ((ToolStripMenuItem)sender).Text;
            switch (selectedItem)
            {
                case "Group by Category":
                case "Group by Task":
                    AppContext.Current.SettingsProvider.Set("GraphGroupStyle", ((ToolStripMenuItem)sender).Text,
                                                            PersistHint.AcrossSessions);
                    Draw();
                    break;
                default:
                    break;
            }
            UpdateGraphMenu();
        }
    }
}