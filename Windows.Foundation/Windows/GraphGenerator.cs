using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;
using BritishMicro.Windows;

namespace BritishMicro.TaskClerk.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphGenerator
    {
        /// <summary>
        /// Creates the a 2D pie chart image.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static Image CreatePie2D(TaskClerkEngine engine, Collection<TaskActivity> activities, int width, int height)
        {
            string graphViewStyle = (string)engine.SettingsProvider.Get("GraphViewStyle", "Minutes");
            string graphGroupStyle =
                (string)engine.SettingsProvider.Get("GraphGroupStyle", "Group by Task");

            decimal totalMinutes = 0;
            GraphData data = new GraphData();
            data.Title = string.Format("{0} / {1}", graphGroupStyle, graphViewStyle);            
            foreach (TaskActivity activity in activities)
            {

                string label = activity.TaskDescription.Name;
                Color color = activity.TaskDescription.Color;
                if (activity.TaskDescription.IsEmpty())
                {
                    label = "Empty";
                    color = Color.Black;
                }

                if (graphGroupStyle == "Group by Category")
                {
                    TaskDescription category =
                        (TaskDescription)engine.TaskDescriptionsProvider.CategoryFromTaskDescription(activity.TaskDescription);
                    if (category != null)
                    {
                        label = category.Name;
                        color = category.Color;
                    }
                    else
                    {
                        label = "Unknown";
                        color = Color.Black;
                    }
                }

                // only show activities and activities that have at least 0.01 minute of duration.
                if ((!activity.TaskDescription.IsEvent) && (activity.Duration > 0))
                {
                    GraphRange range = data.FindGraphRange(label);
                    if (range == null)
                    {
                        range = new GraphRange(
                            label,
                            color,
                            Color.DimGray,
                            0);
                        data.Ranges.Add(range);
                    }
                    range.Values.Add(new GraphValue(activity.Duration));
                    totalMinutes += activity.Duration;
                }
            }

            switch (graphViewStyle)
            {
                case "Hours":
                    foreach (GraphRange gr in data.Ranges)
                    {
                        foreach (GraphValue gv in gr.Values)
                        {
                            gv.Point = gv.Point / 60;
                        }
                    }
                    break;
                case "Percent (%)":
                    double divider = (double)(totalMinutes / 100);
                    foreach (GraphRange gr in data.Ranges)
                    {
                        double tot = 0;
                        foreach (GraphValue gv in gr.Values)
                        {
                            tot += gv.Point;
                        }
                        double answer = tot / divider;
                        gr.Values.Clear();
                        gr.Values.Add(new GraphValue(answer));
                    }
                    break;
                default:
                    break;
            }

            //PieChart
            Pie2D pie = new Pie2D(data, width, height);
            Font temp = (Font)AppContext.Current.SettingsProvider.Get("GeneralFont", SystemFonts.DefaultFont);
            pie.TitleFont = new Font(temp.FontFamily, temp.Size+2, FontStyle.Regular);
            pie.LegendFont = new Font(temp.FontFamily, temp.Size, FontStyle.Regular);
            pie.DrawLegend = true;
            pie.DrawTitle = true;
            if (graphViewStyle == "Task")
            {
                pie.TextLabels = true;
            }
            pie.Paint();
            return pie.Bitmap;
        }
    }
}
