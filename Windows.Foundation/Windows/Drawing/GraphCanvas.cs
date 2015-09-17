#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		GraphCanvas
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		8 October 2002
//
//	Copyright (c) 2002/3 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace BritishMicro.Windows.Drawing
{
    //------------------------------------------------------------------
    //using specifications
    //------------------------------------------------------------------
    
    /// <summary>
    /// Represents GraphCanvas information in the model
    /// </summary>
    public abstract class GraphCanvas : IDisposable
    {
        //---------------------------------------------------------------------
        //private member variables
        //---------------------------------------------------------------------

        private Bitmap _bitmap;
        private Graphics _graphics;
        private GraphData _graphData;
        private RectangleF _graphCanvasRectangle;

        private ArrayList _dataPoints;
        private Color _backgroundColor;
        private ArrayList _drawingElements;
        private bool _drawTitle;
        private bool _drawLegend;
        private RectangleF _titleRectangle;
        private RectangleF _legendRectangle;
        private RectangleF _graphRectangle;
        private Font _titleFont;
        private Color _titleColor;
        private StringFormat _titleStringFormat;
        private Font _legendFont;
        private StringFormat _legendStringFormat;

#if GRAPHDEBUG
        internal bool debugMode;
        internal Pen debugPen;
        internal SolidBrush debugBrush;
#endif

        #region constructors

        //---------------------------------------------------------------------
        //constructors
        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the GraphCanvas class.
        /// </summary>
        protected GraphCanvas(GraphData data, int width, int height, Color backgroundColor)
        {

            if (data == null)
            {
                return;
            }

            if (width <= 0)
            {
                width = 1;
            }

            if (height <= 0)
            {
                height = 1;
            }

#if GRAPHDEBUG
            //debug switch
            debugMode = false;
            debugBrush = new SolidBrush(Color.LightGray);
            debugPen = new Pen(debugBrush, 1);
            //debug
#endif
            _graphData = data;
            _bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            _graphics = Graphics.FromImage((Image) _bitmap);
            _graphics.SmoothingMode = SmoothingMode.AntiAlias;
            backgroundColor = Color.FromArgb(20, backgroundColor);
            _graphics.CompositingQuality = CompositingQuality.HighQuality;
            _graphics.CompositingMode = CompositingMode.SourceOver;
            _graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            _drawingElements = new ArrayList();

            _titleFont = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size + 2, FontStyle.Bold);
            _legendFont = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Regular);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected GraphCanvas(GraphData data, int width, int height) : this(data, width, height, Color.White)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        protected GraphCanvas(GraphData data) : this(data, 200, 100, Color.White)
        {
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="GraphCanvas"/> is reclaimed by garbage collection.
        /// </summary>
        ~GraphCanvas()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> [disposing].</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _bitmap.Dispose();
#if GRAPHDEBUG
                debugMode = false;
                debugPen.Dispose();
                debugBrush.Dispose();
#endif
            }

            GC.SuppressFinalize(this);
        }

        #endregion

        //---------------------------------------------------------------------
        //public properties and methods
        //---------------------------------------------------------------------

        /// <summary>
        /// The Graph Canvas Rectangle of the chart.
        /// </summary>
        public virtual RectangleF GraphCanvasRectangle
        {
            get { return _graphCanvasRectangle; }
            set { _graphCanvasRectangle = value; }
        }


        /// <summary>
        /// The bitmap of the chart.
        /// </summary>
        public virtual Bitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        /// <summary>
        /// The graphics object of the chart
        /// </summary>
        public Graphics Graphics
        {
            get { return _graphics; }
            set { _graphics = value; }
        }

        /// <summary>
        /// ;
        /// </summary>
        public GraphData GraphData
        {
            get { return _graphData; }
            set { _graphData = value; }
        }

        /// <summary>
        /// The Jagged array of points the graph ranges.
        /// </summary>
        public virtual ArrayList DataPoints
        {
            get
            {
                if (_dataPoints == null)
                {
                    _dataPoints = new ArrayList();
                }
                return _dataPoints;
            }
        }

        /// <summary>
        /// The background color of the chart.
        /// </summary>
        public virtual Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        /// <summary>
        /// Draw the title of the chart.
        /// </summary>
        public virtual bool DrawTitle
        {
            get { return _drawTitle; }
            set { _drawTitle = value; }
        }

        /// <summary>
        /// Draw the Legend of the chart.
        /// </summary>
        public virtual bool DrawLegend
        {
            get { return _drawLegend; }
            set { _drawLegend = value; }
        }

        /// <summary>
        /// Gets and sets the _titleRectangle property
        /// </summary>
        public RectangleF TitleRectangle
        {
            get { return _titleRectangle; }
            set { _titleRectangle = value; }
        }

        /// <summary>
        /// Gets and sets the _legendRectangle property
        /// </summary>
        public RectangleF LegendRectangle
        {
            get { return _legendRectangle; }
            set { _legendRectangle = value; }
        }

        /// <summary>
        /// Gets and sets the _graphRectangle property
        /// </summary>
        public RectangleF GraphRectangle
        {
            get { return _graphRectangle; }
            set { _graphRectangle = value; }
        }

        /// <summary>
        /// Gets and sets the _titleFont property
        /// </summary>
        public Font TitleFont
        {
            get { return _titleFont; }
            set { _titleFont = value; }
        }

        /// <summary>
        /// Gets and sets the _titleColor property
        /// </summary>
        public Color TitleColor
        {
            get { return _titleColor; }
            set { _titleColor = value; }
        }

        /// <summary>
        /// Gets and sets the _titleStringFormat property
        /// </summary>
        public StringFormat TitleStringFormat
        {
            get { return _titleStringFormat; }
            set { _titleStringFormat = value; }
        }

        /// <summary>
        /// Gets and sets the _legendFont property
        /// </summary>
        public Font LegendFont
        {
            get { return _legendFont; }
            set { _legendFont = value; }
        }

        /// <summary>
        /// Gets and sets the _legendStringFormat property
        /// </summary>
        public StringFormat LegendStringFormat
        {
            get { return _legendStringFormat; }
            set { _legendStringFormat = value; }
        }

        /// <summary>
        /// The background color of the chart.
        /// </summary>
        public ArrayList DrawingElements
        {
            get { return _drawingElements; }
        }

        /// <summary>
        /// The chart paint method.
        /// </summary>
        public virtual void Paint()
        {
            foreach (IDrawingElement element in _drawingElements)
            {
                ((IDrawingElement) element).Render(_graphics);
            }
        }
    }
}