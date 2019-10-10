using BritishMicro.TaskClerk.Plugins;
using BritishMicro.TaskClerk.Providers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Provides a print document that contains todays information.
    /// </summary>
    [DisplayName("Timesheet Report")]
    [Description("A timesheet report between two dates.")]
    [TaskClerkPluginAttribute]
    class TimesheetPrintFormatter : PluginPrintFormatter
    {

        DateTime _start;
        DateTime _end;
        Collection<TaskActivity> _taskActivities;
        int _currentTaskActivity;
        int _fontHeight;
        decimal _totalDuration;
        bool _introductionPrinted;


        /// <summary>
        /// Initializes a new instance of the <see cref="TimesheetPrintFormatter"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="config">The config.</param>
        public TimesheetPrintFormatter(TaskClerkEngine engine, PrintConfiguration config)
            : base(engine, config)
        {
        }

        /// <summary>
        /// Raises the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint"></see> event. It is called after the <see cref="M:System.Drawing.Printing.PrintDocument.Print"></see> method is called and before the first page of the document prints.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs"></see> that contains the event data.</param>
        protected override void OnBeginPrint(System.Drawing.Printing.PrintEventArgs e)
        {
            _taskActivities = TaskActivitiesProvider.RollupByTaskDescription(Config.Activities);
            _start = Config.Start;
            _end = Config.End;
            _fontHeight = SystemFonts.DefaultFont.Height + 4;
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
                "TaskClerk - Timesheet Report",
                SystemFonts.DefaultFont,
                SystemBrushes.ControlLight,
                headerRectangle);

            string pageNumber = string.Format("Page {0}", CurrentPageNumber);
            SizeF size = e.Graphics.MeasureString(pageNumber, SystemFonts.DefaultFont);

            e.Graphics.DrawString(
                pageNumber,
                SystemFonts.DefaultFont,
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
                SystemFonts.DefaultFont,
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

            int stepTotal = rectangle.Height / (_fontHeight);
            int currentStep = 0;

            Rectangle thRect = new Rectangle(
                    rectangle.Left,
                    rectangle.Top,
                    rectangle.Width,
                    _fontHeight);
            DrawTableHeader(e, thRect, _taskActivities);
            currentStep++;

            while (_currentTaskActivity < _taskActivities.Count)
            {
                Rectangle taRect = new Rectangle(
                    rectangle.Left,
                    rectangle.Top + ((_fontHeight) * currentStep),
                    rectangle.Width,
                    _fontHeight);
                DrawTaskActivity(e, taRect, _taskActivities[_currentTaskActivity]);
                _currentTaskActivity++;
                currentStep++;
                if (currentStep >= stepTotal)
                {
                    CreatePageBreak(e);
                    break;
                }
            }

            if (_currentTaskActivity == _taskActivities.Count)
            {
                Rectangle taRect = new Rectangle(
                    rectangle.Left,
                    rectangle.Top + ((_fontHeight) * currentStep),
                    rectangle.Width,
                    _fontHeight);
                DrawTotals(e, taRect, _taskActivities);
            }
        }

        public override bool RequiresDateRange()
        {
            return true;
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
            introRectangle.Height = 400;
            Font font = new Font(SystemFonts.DefaultFont.FontFamily, 14f, FontStyle.Bold & FontStyle.Italic);
            string intro = string.Format("\n{0}\n{1} - {2}",
                Engine.SettingsProvider.Get("CurrentUsersName",
                this.Engine.IdentityProvider.Principal.Identity.Name), _start.ToString("f"), _end.ToString("f"));
            e.Graphics.DrawString(intro, font, SystemBrushes.ControlDarkDark, introRectangle);

            //// draw graph
            //Image pie2d = TaskClerk.Windows.GraphGenerator.CreatePie2D(
            //    Engine,
            //    _taskActivities,
            //    600,
            //    300);
            //Rectangle pr = new Rectangle(introRectangle.X, introRectangle.Top + 80, introRectangle.Width, introRectangle.Height - 80);
            //e.Graphics.DrawImage(pie2d, pr); 

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

            e.Graphics.DrawString("Task", SystemFonts.DefaultFont, SystemBrushes.ControlDarkDark, taRect);
            SizeF size = e.Graphics.MeasureString("Duration", SystemFonts.DefaultFont);
            e.Graphics.DrawString("Duration", SystemFonts.DefaultFont, SystemBrushes.ControlDarkDark, taRect.X + taRect.Width - size.Width, taRect.Top);
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
            e.Graphics.DrawString("Total duration in minutes", SystemFonts.DefaultFont, SystemBrushes.ControlDarkDark, taRect);
            SizeF size = e.Graphics.MeasureString(_totalDuration.ToString(), SystemFonts.DefaultFont);
            e.Graphics.DrawString(_totalDuration.ToString(), SystemFonts.DefaultFont, SystemBrushes.ControlDarkDark, taRect.X + taRect.Width - size.Width, taRect.Top);
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

            e.Graphics.DrawString(taskActivity.TaskDescription.Name, SystemFonts.DefaultFont, SystemBrushes.ControlDark, taRect);
            SizeF size = e.Graphics.MeasureString(taskActivity.Duration.ToString(), SystemFonts.DefaultFont);
            e.Graphics.DrawString(taskActivity.Duration.ToString(), SystemFonts.DefaultFont, SystemBrushes.ControlDark, taRect.X + taRect.Width - size.Width, taRect.Top);
            _totalDuration += taskActivity.Duration;
        }

        #endregion
    }
}
