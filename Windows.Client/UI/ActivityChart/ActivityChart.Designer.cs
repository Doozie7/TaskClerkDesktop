using System.Windows.Forms;
namespace BritishMicro.TaskClerk.UI
{
    partial class ActivityChart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityChart));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 17D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 4D);
            this.contextMenuStripGraph = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.minutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.percentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.groupByTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupByTaskPerentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.visualSummary = new BritishMicro.TaskClerk.UI.VisualSummary();
            this.contextMenuStripGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripGraph
            // 
            this.contextMenuStripGraph.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStripGraph.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minutesToolStripMenuItem,
            this.hoursToolStripMenuItem,
            this.percentToolStripMenuItem,
            this.taskToolStripMenuItem,
            this.toolStripMenuItem3,
            this.groupByTaskToolStripMenuItem,
            this.groupByTaskPerentToolStripMenuItem});
            this.contextMenuStripGraph.Name = "contextMenuStripGraph";
            resources.ApplyResources(this.contextMenuStripGraph, "contextMenuStripGraph");
            // 
            // minutesToolStripMenuItem
            // 
            this.minutesToolStripMenuItem.Checked = true;
            this.minutesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minutesToolStripMenuItem.Name = "minutesToolStripMenuItem";
            resources.ApplyResources(this.minutesToolStripMenuItem, "minutesToolStripMenuItem");
            this.minutesToolStripMenuItem.Tag = "view";
            this.minutesToolStripMenuItem.Click += new System.EventHandler(this.ChangeGraphView_Click);
            // 
            // hoursToolStripMenuItem
            // 
            this.hoursToolStripMenuItem.Name = "hoursToolStripMenuItem";
            resources.ApplyResources(this.hoursToolStripMenuItem, "hoursToolStripMenuItem");
            this.hoursToolStripMenuItem.Tag = "view";
            this.hoursToolStripMenuItem.Click += new System.EventHandler(this.ChangeGraphView_Click);
            // 
            // percentToolStripMenuItem
            // 
            this.percentToolStripMenuItem.Name = "percentToolStripMenuItem";
            resources.ApplyResources(this.percentToolStripMenuItem, "percentToolStripMenuItem");
            this.percentToolStripMenuItem.Tag = "view";
            this.percentToolStripMenuItem.Click += new System.EventHandler(this.ChangeGraphView_Click);
            // 
            // taskToolStripMenuItem
            // 
            this.taskToolStripMenuItem.Name = "taskToolStripMenuItem";
            resources.ApplyResources(this.taskToolStripMenuItem, "taskToolStripMenuItem");
            this.taskToolStripMenuItem.Tag = "view";
            this.taskToolStripMenuItem.Click += new System.EventHandler(this.ChangeGraphView_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // groupByTaskToolStripMenuItem
            // 
            this.groupByTaskToolStripMenuItem.Checked = true;
            this.groupByTaskToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.groupByTaskToolStripMenuItem.Name = "groupByTaskToolStripMenuItem";
            resources.ApplyResources(this.groupByTaskToolStripMenuItem, "groupByTaskToolStripMenuItem");
            this.groupByTaskToolStripMenuItem.Tag = "group";
            this.groupByTaskToolStripMenuItem.Click += new System.EventHandler(this.ChangeGraphGroup_Click);
            // 
            // groupByTaskPerentToolStripMenuItem
            // 
            this.groupByTaskPerentToolStripMenuItem.Name = "groupByTaskPerentToolStripMenuItem";
            resources.ApplyResources(this.groupByTaskPerentToolStripMenuItem, "groupByTaskPerentToolStripMenuItem");
            this.groupByTaskPerentToolStripMenuItem.Tag = "group";
            this.groupByTaskPerentToolStripMenuItem.Click += new System.EventHandler(this.ChangeGraphGroup_Click);
            // 
            // chart1
            // 
            resources.ApplyResources(this.chart1, "chart1");
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineWidth = 0;
            this.chart1.BorderSkin.BorderWidth = 0;
            chartArea1.BackColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.BackSecondaryColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowOffset = 4;
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.WhiteSmoke;
            legend1.BorderColor = System.Drawing.Color.Black;
            legend1.Name = "Legend1";
            legend1.ShadowOffset = 4;
            this.chart1.Legends.Add(legend1);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.LabelBackColor = System.Drawing.Color.Transparent;
            series1.LabelBorderColor = System.Drawing.Color.Transparent;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            dataPoint1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.None;
            dataPoint1.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.None;
            dataPoint1.Color = System.Drawing.Color.Red;
            dataPoint1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataPoint1.IsVisibleInLegend = true;
            dataPoint1.Label = "mike";
            dataPoint1.LabelBackColor = System.Drawing.Color.WhiteSmoke;
            dataPoint1.LabelBorderColor = System.Drawing.Color.Transparent;
            dataPoint1.LabelBorderWidth = 4;
            dataPoint1.LabelForeColor = System.Drawing.Color.Black;
            dataPoint1.ToolTip = "test";
            dataPoint2.BorderColor = System.Drawing.Color.Empty;
            dataPoint2.Color = System.Drawing.Color.Yellow;
            dataPoint2.Label = "geoff";
            dataPoint2.LabelToolTip = "12";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            this.chart1.Series.Add(series1);
            // 
            // visualSummary
            // 
            resources.ApplyResources(this.visualSummary, "visualSummary");
            this.visualSummary.Name = "visualSummary";
            // 
            // ActivityChart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.visualSummary);
            this.Name = "ActivityChart";
            this.contextMenuStripGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private VisualSummary visualSummary;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGraph;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem percentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taskToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem groupByTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupByTaskPerentToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
