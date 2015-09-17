using System;
using BritishMicro.TaskClerk.Plugins;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Provides a print document that contains todays information.
    /// </summary>
    [Description("Produces a single page report containing the recorded entries for a day.")]
    [DisplayName("Day Report - No Chart")]
    [TaskClerkPluginAttribute]
    class NoChartDayPrintFormatter : PluginPrintFormatter
    {

        int _currentTaskActivity;
        int _fontHeight;
        decimal _totalDuration;
        bool _introductionPrinted;


        /// <summary>
        /// Initializes a new instance of the <see cref="DayPrintFormatter"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="config">The config.</param>
        public NoChartDayPrintFormatter(TaskClerkEngine engine, PrintConfiguration config)
            : base(engine, config)
        {
        }

        /// <summary>
        /// Raises the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint"></see> event. It is called after the <see cref="M:System.Drawing.Printing.PrintDocument.Print"></see> method is called and before the first page of the document prints.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs"></see> that contains the event data.</param>
        protected override void OnBeginPrint(System.Drawing.Printing.PrintEventArgs e)
        {
            _fontHeight = Font.Height + 4;
            _totalDuration = 0;   

            base.OnBeginPrint(e);
        }


        /// <summary>
        /// Draws the header.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        protected override Rectangle DrawHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle headerRectangle = new Rectangle(
                e.MarginBounds.X,
                e.MarginBounds.Y,
                e.MarginBounds.Width,
                _fontHeight);
#if DEBUG
            //debug
            //e.Graphics.DrawRectangle(new Pen(SystemBrushes.ControlLight), headerRectangle);
#endif
            e.Graphics.DrawString(
                "TaskClerk - Day Report",
                Font, 
                SystemBrushes.ControlLight, 
                headerRectangle);
            
            string pageNumber = string.Format("Page {0}", CurrentPageNumber);
            SizeF size = e.Graphics.MeasureString(pageNumber, Font);

            e.Graphics.DrawString(
                pageNumber,
                Font,
                SystemBrushes.ControlLight,
                headerRectangle.Right - size.Width,
                headerRectangle.Y
                );

            e.Graphics.DrawLine(new Pen(SystemBrushes.ControlLight),
                headerRectangle.Left, headerRectangle.Bottom,
                headerRectangle.Right, headerRectangle.Bottom);

            return headerRectangle;
        }

        /// <summary>
        /// Draws the footer.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        protected override Rectangle DrawFooter(System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle footerRectangle = new Rectangle(
                e.MarginBounds.X,
                e.MarginBounds.Bottom - _fontHeight,
                e.MarginBounds.Width,
                _fontHeight);
#if DEBUG
            //debug
            //e.Graphics.DrawRectangle(new Pen(SystemBrushes.ControlLight), footerRectangle);
#endif
            e.Graphics.DrawString(
                string.Format("{0}", DateTime.Now.ToString("f")),
                Font,
                SystemBrushes.ControlLight,
                footerRectangle);

            e.Graphics.DrawLine(new Pen(SystemBrushes.ControlLight),
                footerRectangle.Left, footerRectangle.Top,
                footerRectangle.Right, footerRectangle.Top);

            return footerRectangle;
        }

        /// <summary>
        /// Draws the content.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <param name="rectangle">The rectangle.</param>
        protected override void DrawContent(System.Drawing.Printing.PrintPageEventArgs e, Rectangle rectangle)
        {

#if DEBUG
            //debug
            //e.Graphics.DrawRectangle(new Pen(SystemBrushes.ControlLight), rectangle);
#endif

            if (_introductionPrinted == false)
            {
                _introductionPrinted = true;
                Rectangle introRectangle = DrawIntroduction(e, rectangle);
                rectangle = new Rectangle(rectangle.X, rectangle.Top + introRectangle.Height,
                    rectangle.Width, rectangle.Height - introRectangle.Height);
            }

            int stepTotal = rectangle.Height / (_fontHeight * 2);
            int currentStep = 0;

            Rectangle thRect = new Rectangle(
                    rectangle.Left,
                    rectangle.Top + ((_fontHeight * 2) * currentStep),
                    rectangle.Width,
                    _fontHeight * 2);
            DrawTableHeader(e, thRect, Config.Activities);
            currentStep++;

            while (_currentTaskActivity < Config.Activities.Count)
            {
                Rectangle taRect = new Rectangle(
                    rectangle.Left,
                    rectangle.Top + ((_fontHeight * 2) * currentStep),
                    rectangle.Width,
                    _fontHeight * 2);
                DrawTaskActivity(e, taRect, Config.Activities[_currentTaskActivity]);
                _currentTaskActivity++;
                currentStep++;
                if (currentStep >= stepTotal)
                {
                    CreatePageBreak(e);
                    break;
                }
            }

            if (_currentTaskActivity == Config.Activities.Count)
            {
                Rectangle taRect = new Rectangle(
                    rectangle.Left,
                    rectangle.Top + ((_fontHeight * 2) * currentStep),
                    rectangle.Width,
                    _fontHeight * 2);
                DrawTotals(e, taRect, Config.Activities);
            }
        }

        #region Local methods

        /// <summary>
        /// Draws the introduction.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns></returns>
        private Rectangle DrawIntroduction(System.Drawing.Printing.PrintPageEventArgs e, Rectangle rectangle)
        {
            Rectangle introRectangle = rectangle;
            introRectangle.Height = 100;
            Font font = new Font(Font.FontFamily, 14f, FontStyle.Bold & FontStyle.Italic);
            string intro = string.Format("\n{0}\n{1}",
                Engine.SettingsProvider.Get("CurrentUsersName",
                this.Engine.IdentityProvider.Principal.Identity.Name), Config.Start.ToLongDateString());
            e.Graphics.DrawString(intro, font, SystemBrushes.ControlDarkDark, introRectangle);
            return introRectangle;
        }

        /// <summary>
        /// Draws the table header.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <param name="taRect">The ta rect.</param>
        /// <param name="_taskActivities">The _task activities.</param>
        private void DrawTableHeader(System.Drawing.Printing.PrintPageEventArgs e, Rectangle taRect, Collection<TaskActivity> _taskActivities)
        {
#if DEBUG
            //debug
            //e.Graphics.DrawRectangle(new Pen(SystemBrushes.ControlLight), taRect);
#endif
            e.Graphics.DrawLine(new Pen(SystemBrushes.ControlDarkDark),
                taRect.X, taRect.Bottom,
                taRect.X + taRect.Width, taRect.Bottom);

            taRect.Offset(0, _fontHeight);
            e.Graphics.DrawString("Task", Font, SystemBrushes.ControlDarkDark, taRect);
            taRect.Offset(250, 0);
            e.Graphics.DrawString("Start Date / End Date", Font, SystemBrushes.ControlDarkDark, taRect);
            taRect.Offset(150, 0);
            Rectangle durRect = taRect;
            durRect.Width = 80;
            SizeF size = e.Graphics.MeasureString("Duration", Font);
            e.Graphics.DrawString("Duration", Font, SystemBrushes.ControlDarkDark, durRect.Right - size.Width, durRect.Top);
            taRect.Offset(100, 0);
            e.Graphics.DrawString("Remarks", Font, SystemBrushes.ControlDarkDark, taRect);
        }

        /// <summary>
        /// Draws the totals.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <param name="taRect">The ta rect.</param>
        /// <param name="_taskActivities">The _task activities.</param>
        private void DrawTotals(System.Drawing.Printing.PrintPageEventArgs e, Rectangle taRect, Collection<TaskActivity> _taskActivities)
        {
            e.Graphics.DrawLine(new Pen(SystemBrushes.ControlDark),
                            taRect.X, taRect.Y,
                            taRect.X + taRect.Width, taRect.Y);
            taRect.Offset(0, 10);
            e.Graphics.DrawString("Total duration in minutes", Font, SystemBrushes.ControlDarkDark, taRect);
            taRect.Offset(400, 0);
            taRect.Width = 80;
            SizeF size = e.Graphics.MeasureString(_totalDuration.ToString(), Font);
            e.Graphics.DrawString(_totalDuration.ToString(), Font, SystemBrushes.ControlDarkDark, taRect.Right - size.Width, taRect.Top);
        }

        /// <summary>
        /// Draws the task activity.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <param name="taRect">The ta rect.</param>
        /// <param name="taskActivity">The task activity.</param>
        private void DrawTaskActivity(System.Drawing.Printing.PrintPageEventArgs e, Rectangle taRect, TaskActivity taskActivity)
        {

#if DEBUG
            //debug
            //e.Graphics.DrawRectangle(new Pen(SystemBrushes.ControlLight), taRect);
#endif

            e.Graphics.DrawLine(new Pen(SystemBrushes.ControlLight),
                taRect.X, taRect.Bottom,
                taRect.X + taRect.Width, taRect.Bottom);

            e.Graphics.DrawString(taskActivity.TaskDescription.Name, Font, SystemBrushes.ControlDark, taRect);
            taRect.Offset(250, 0);
            e.Graphics.DrawString(taskActivity.StartDate.ToString("f"), Font, SystemBrushes.ControlDark, taRect);
            taRect.Offset(0, _fontHeight);
            e.Graphics.DrawString(taskActivity.EndDate.ToString("f"), Font, SystemBrushes.ControlDark, taRect);
            taRect.Offset(150, 0);
            Rectangle durRect = taRect;
            durRect.Width = 80;
            SizeF size = e.Graphics.MeasureString(taskActivity.Duration.ToString(), Font);
            e.Graphics.DrawString(taskActivity.Duration.ToString(), Font, SystemBrushes.ControlDark, durRect.Right - size.Width, durRect.Top);
            _totalDuration += taskActivity.Duration;
            taRect.Offset(100, 0);
            e.Graphics.DrawString(taskActivity.Remarks, Font, SystemBrushes.ControlDark, taRect);
        }

        #endregion

    }
}
