using System;
using System.Drawing;
using System.Drawing.Printing;

namespace BritishMicro.TaskClerk.Plugins
{
    /// <summary>
    /// Print Formatters provide print layout capabilities for the Windows TaskClerk Client
    /// </summary>
    [TaskClerkPluginAttribute]
    public class PluginPrintFormatter : PrintDocument
    {

        private readonly TaskClerkEngine _engine;
        private readonly PrintConfiguration _config;
        private int _currentPageNumber;
        private Font _font;
        private bool _cancel;


        /// <summary>
        /// Initializes a new instance of the <see cref="PluginPrintFormatter"/> class.
        /// </summary>
        public PluginPrintFormatter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginPrintFormatter"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="config">The config.</param>
        public PluginPrintFormatter(TaskClerkEngine engine, PrintConfiguration config)
        {
            _engine = engine;
            _config = config;
            _currentPageNumber = 1;
            _font = (Font)_engine.SettingsProvider.Get("GeneralFont", SystemFonts.DefaultFont);
        }

        /// <summary>
        /// Gets the TaskClerk Engine.
        /// </summary>
        /// <value>The engine.</value>
        public TaskClerkEngine Engine
        {
            get { return _engine; }
        }

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <value>The config.</value>
        public PrintConfiguration Config
        {
            get { return _config; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PluginPrintFormatter"/> is to be canceled.
        /// </summary>
        /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage"></see> event. It is called before a page prints.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs"></see> that contains the event data.</param>
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            if (Cancel == true)
            {
                e.Cancel = true;
                return;
            }
            base.OnPrintPage(e);
            Rectangle headerRec = DrawHeader(e);
            Rectangle footerRec = DrawFooter(e);
            DrawContent(e, CalculateContentRectangle(e, headerRec, footerRec));
        }

        /// <summary>
        /// Draws the header.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        protected virtual Rectangle DrawHeader(PrintPageEventArgs e)
        { throw new NotImplementedException(); }

        /// <summary>
        /// Draws the footer.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        protected virtual Rectangle DrawFooter(PrintPageEventArgs e)
        { throw new NotImplementedException(); }

        /// <summary>
        /// Draws the content.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <param name="rectangle">The rectangle.</param>
        protected virtual void DrawContent(PrintPageEventArgs e, Rectangle rectangle)
        { throw new NotImplementedException(); }

        /// <summary>
        /// Recalculates the content rectangle after the Header and Footer have been drawn.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        /// <param name="headerRectangle">The header rectangle.</param>
        /// <param name="footerRectangle">The footer rectangle.</param>
        /// <returns></returns>
        protected virtual Rectangle CalculateContentRectangle(System.Drawing.Printing.PrintPageEventArgs e,
            Rectangle headerRectangle, Rectangle footerRectangle)
        {
            Rectangle contentRec = new Rectangle(e.MarginBounds.X,
                e.MarginBounds.Top + headerRectangle.Height,
                e.MarginBounds.Width,
                e.MarginBounds.Height - (headerRectangle.Height + footerRectangle.Height));
            return contentRec;
        }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        /// <value>The current page number.</value>
        public int CurrentPageNumber
        {
            get { return _currentPageNumber; }
            set { _currentPageNumber = value; }
        }

        /// <summary>
        /// Creates the page break.
        /// </summary>
        /// <param name="e">The <see cref="System.Drawing.Printing.PrintPageEventArgs"/> instance containing the event data.</param>
        protected void CreatePageBreak(PrintPageEventArgs e)
        {
            _currentPageNumber++;
            e.HasMorePages = true;
        }

        /// <summary>
        /// Requireses a date range.
        /// </summary>
        /// <returns></returns>
        public virtual bool RequiresDateRange()
        {
            return false;
        }
    }
}
