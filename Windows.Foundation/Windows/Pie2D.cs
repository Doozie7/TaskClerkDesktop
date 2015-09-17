#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows
//	Class:		Pie2D
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		2 October 2002
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
using BritishMicro.Windows.Drawing;

namespace BritishMicro.Windows
{
    //------------------------------------------------------------------
    //using specifications
    //------------------------------------------------------------------
    
    /// <summary>
    /// Builds a 2D Pie chart from some GraphData
    /// </summary>
    public class Pie2D : GraphCanvas
    {
        //---------------------------------------------------------------------
        //private member variables
        //---------------------------------------------------------------------

        private bool _textLabels;

        #region constructors

        //---------------------------------------------------------------------
        //constructors
        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the Pie2D class requiring graph data.
        /// </summary>
        /// <param name="data"></param>
        public Pie2D(GraphData data) : base(data)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Pie2D class requiring graph data, width and height.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Pie2D(GraphData data, int width, int height) : base(data, width, height)
        {
            GraphCanvasRectangle = new Rectangle(0, 0, width, height);
            GraphRectangle = GraphCanvasRectangle;

            TitleColor = Color.Black;
            //TitleFont = new Font("Verdana", 12, FontStyle.Bold);
            TitleStringFormat = new StringFormat();
            TitleStringFormat.Alignment = StringAlignment.Center;
            TitleStringFormat.LineAlignment = StringAlignment.Center;

            //LegendFont = new Font("Tahoma", 9, FontStyle.Regular);
            LegendStringFormat = new StringFormat();
            LegendStringFormat.Alignment = StringAlignment.Center;
            LegendStringFormat.LineAlignment = StringAlignment.Center;
        }

        #endregion

        //---------------------------------------------------------------------
        //public properties and methods
        //---------------------------------------------------------------------

        /// <summary>
        /// Get or Sets the draw title property.
        /// </summary>
        public override bool DrawTitle
        {
            get { return base.DrawTitle; }
            set
            {
                base.DrawTitle = value;
                //RecalcAreas();
            }
        }

        /// <summary>
        /// Get or Sets the draw legend property.
        /// </summary>
        public override bool DrawLegend
        {
            get { return base.DrawLegend; }
            set { base.DrawLegend = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [text labels].
        /// </summary>
        /// <value><c>true</c> if [text labels]; otherwise, <c>false</c>.</value>
        public bool TextLabels
        {
            get { return _textLabels; }
            set { _textLabels = value; }
        }

        /// <summary>
        /// Recalcs the areas.
        /// </summary>
        private void RecalcAreas()
        {
            if (DrawLegend == true)
            {
                if (DrawTitle == true)
                {
                    TitleRectangle = new RectangleF(percentX(1), percentY(1), percentX(98), percentY(8));
                    LegendRectangle = new RectangleF(percentX(65), percentY(10), percentX(34), percentY(89));
                    GraphRectangle = new RectangleF(percentX(1), percentY(10), percentX(70), percentY(89));
                }
                else
                {
                    TitleRectangle = new RectangleF(percentX(0), percentY(0), percentX(0), percentY(0));
                    LegendRectangle = new RectangleF(percentX(65), percentY(1), percentX(34), percentY(98));
                    GraphRectangle = new RectangleF(percentX(1), percentY(1), percentX(70), percentY(98));
                }
            }
            else
            {
                if (DrawTitle == true)
                {
                    TitleRectangle = new RectangleF(percentX(1), percentY(1), percentX(98), percentY(8));
                    LegendRectangle = new RectangleF(percentX(0), percentY(0), percentX(0), percentY(0));
                    GraphRectangle = new RectangleF(percentX(1), percentY(10), percentX(98), percentY(89));
                }
                else
                {
                    TitleRectangle = new RectangleF(percentX(0), percentY(0), percentX(0), percentY(0));
                    LegendRectangle = new RectangleF(percentX(0), percentY(0), percentX(0), percentY(0));
                    GraphRectangle = new RectangleF(percentX(1), percentY(1), percentX(98), percentY(98));
                }
            }
        }

        /// <summary>
        /// Percents the X.
        /// </summary>
        /// <param name="percent">The percent.</param>
        /// <returns></returns>
        private float percentX(float percent)
        {
            if (Bitmap != null)
            {
                return (Bitmap.Width / 100.0F) * percent;
            }
            return 10;
        }

        /// <summary>
        /// Percents the Y.
        /// </summary>
        /// <param name="percent">The percent.</param>
        /// <returns></returns>
        private float percentY(float percent)
        {
            if (Bitmap != null)
            {
                return (Bitmap.Height / 100.0F) * percent;
            }
            return 10;
        }

        /// <summary>
        /// The override for the Paint method. This method paints the graph onto the canvas.
        /// </summary>
        private void Construct()
        {
            Graphics.CompositingQuality = CompositingQuality.HighQuality;
            Graphics.CompositingMode = CompositingMode.SourceOver;
            Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            ImageAttributes ia = new ImageAttributes();
            ia.SetGamma(50, ColorAdjustType.Brush);

            DrawingElements.Add(new DrawBackGround(Color.White, GraphCanvasRectangle));

            Pen outline = new Pen(new SolidBrush(Color.DarkGray));

            //DrawingElements.Add(new DrawRectangle(outline, 0, 0, Bitmap.Width - 1, Bitmap.Height - 1));

#if GRAPHDEBUG

                DrawingElements.Add(new DrawRectangle(debugPen, TitleRectangle));
                DrawingElements.Add(new DrawRectangle(debugPen, LegendRectangle));
                DrawingElements.Add(new DrawRectangle(debugPen, GraphRectangle));
#endif

            //graph
            Graphics.Clip = new Region(GraphRectangle);

            //adjuct the rectangle to be square			
            if (GraphRectangle.Width > GraphRectangle.Height)
                GraphRectangle =
                    new RectangleF(GraphRectangle.X + ((GraphRectangle.Width - GraphRectangle.Height)) / 2,
                                   GraphRectangle.Y, GraphRectangle.Height, GraphRectangle.Height);
            else
                GraphRectangle =
                    new RectangleF(GraphRectangle.X, GraphRectangle.Y, GraphRectangle.Width, GraphRectangle.Width);

#if GRAPHDEBUG
                DrawingElements.Add(
                    new DrawString(string.Format("Width={0}, Height={1}", GraphRectangle.Width, GraphRectangle.Height),
                                   LegendFont, debugBrush, GraphRectangle, LegendStringFormat));
                DrawingElements.Add(new DrawEllipse(debugPen, GraphRectangle));
#endif

            float total = 0;
            foreach (GraphRange gr in GraphData.Ranges)
            {
                total += (float)gr.SumOfValues;
            }

            float start = 0;
            float end = 0;
            float step = (360 / total);

            RectangleF labelRectangle =
                new RectangleF(GraphRectangle.X + (GraphRectangle.Width / 100 * 10),
                               GraphRectangle.Y + (GraphRectangle.Height / 100 * 10),
                               GraphRectangle.Width - (GraphRectangle.Width / 100 * 20),
                               GraphRectangle.Height - (GraphRectangle.Height / 100 * 20));
            RectangleF pieRectangle =
                new RectangleF(GraphRectangle.X + (GraphRectangle.Width / 100 * 20),
                               GraphRectangle.Y + (GraphRectangle.Height / 100 * 20),
                               GraphRectangle.Width - (GraphRectangle.Width / 100 * 40),
                               GraphRectangle.Height - (GraphRectangle.Height / 100 * 40));
            PointF centrePie =
                new PointF(pieRectangle.X + (pieRectangle.Width / 2), pieRectangle.Y + (pieRectangle.Height / 2));

            ArrayList differedLabelDraw = new ArrayList();

            foreach (GraphRange gr in GraphData.Ranges)
            {
                end = step * (float)gr.SumOfValues;
                PointF centreLabel = GraphMaths.PointOnCircleF(Convert.ToInt32(start + (end / 2)), labelRectangle);
                PointF edgeOfPie = GraphMaths.PointOnCircleF(Convert.ToInt32(start + (end / 2)), pieRectangle);

#if GRAPHDEBUG
                DrawingElements.Add(new DrawLine(debugPen, centreLabel, centrePie));
                DrawingElements.Add(new DrawPie(debugPen, pieRectangle, start - 90, end));
#endif
                DrawingElements.Add(new DrawLine(outline, centreLabel, edgeOfPie));
                DrawingElements.Add(
                    new FillPie(
                        new LinearGradientBrush(pieRectangle, gr.PrimaryColor, gr.SecondaryColor,
                                                LinearGradientMode.ForwardDiagonal), pieRectangle, start - 90, end));
                if (_textLabels == true)
                {
                    DrawingElements.Add(new FillRectangle(new SolidBrush(Color.White),
                                                          GraphHelpers.TextRectangleFromCentre(Graphics, gr.Label,
                                                                                               LegendFont,
                                                                                               centreLabel)));
                    DrawingElements.Add(
                        new DrawString(gr.Label, LegendFont, new SolidBrush(Color.Black),
                                       GraphHelpers.TextRectangleFromCentre(Graphics, gr.Label, LegendFont,
                                                                            centreLabel), null));
                }
                else
                {
                    DrawingElements.Add(new FillRectangle(new SolidBrush(Color.White),
                                                          GraphHelpers.TextRectangleFromCentre(Graphics,
                                                                                               gr.SumOfValues.
                                                                                                   ToString("F"),
                                                                                               LegendFont,
                                                                                               centreLabel)));
                    DrawingElements.Add(
                        new DrawString(gr.SumOfValues.ToString("F"), LegendFont, new SolidBrush(Color.Black),
                                       GraphHelpers.TextRectangleFromCentre(Graphics, gr.SumOfValues.ToString("F"),
                                                                            LegendFont, centreLabel), null));
                }
                differedLabelDraw.Add(centreLabel);


                start = start + end;
            }

            Graphics.ResetClip();

            //title
            if (DrawTitle == true)
            {
#if GRAPHDEBUG
                DrawingElements.Add(new DrawRectangle(debugPen, TitleRectangle));
#endif
                DrawingElements.Add(
                    new DrawString(GraphData.Title, TitleFont, new SolidBrush(TitleColor), TitleRectangle,
                                   TitleStringFormat));

            }

            //legend
            if (DrawLegend == true)
            {
                //work out text height
                int textHeight = (int)((Graphics.MeasureString("X", LegendFont).Height) / 100 * 120);

#if GRAPHDEBUG
                    DrawingElements.Add(
                        new DrawRectangle(debugPen, LegendRectangle.X, LegendRectangle.Y, LegendRectangle.Width - 1,
                                          GraphData.Ranges.Count*textHeight));
#endif

                DrawingElements.Add(new FillRectangle(new SolidBrush(Color.White),
                                                      LegendRectangle.X, LegendRectangle.Y,
                                                      LegendRectangle.Width - 1,
                                                      GraphData.Ranges.Count * textHeight));

                int lentry = 0;
                foreach (GraphRange gr in GraphData.Ranges)
                {
                    RectangleF currentRectangle = new RectangleF(LegendRectangle.X + 20,
                                                                 (textHeight * lentry++) + LegendRectangle.Y,
                                                                 LegendRectangle.Width - 20,
                                                                 textHeight);

                    RectangleF colorRectangle = currentRectangle;
                    colorRectangle.Offset(-20, 0);
                    colorRectangle.Width = 20;

#if GRAPHDEBUG
                        DrawingElements.Add(new DrawRectangle(debugPen, currentRectangle));
                        DrawingElements.Add(new DrawRectangle(debugPen, colorRectangle));
#endif
                    string lable = gr.Label;
                    if (lable.Length > 26)
                    {
                        lable = lable.Substring(0, 26);
                    }

                    string val = gr.SumOfValues.ToString("F");
                    DrawingElements.Add(new DrawString(
                                            string.Format("{0} = {1}", lable, val),
                                            LegendFont,
                                            new SolidBrush(Color.Black),
                                            currentRectangle,
                                            LegendStringFormat));

                    char symbol = (char)162;
                    DrawingElements.Add(new DrawString(
                                            symbol.ToString(),
                                            new Font("Wingdings 2", 12),
                                            new SolidBrush(gr.PrimaryColor),
                                            colorRectangle,
                                            LegendStringFormat));

                }
                DrawingElements.Add(
                    new DrawRectangle(outline, LegendRectangle.X, LegendRectangle.Y, LegendRectangle.Width - 1,
                                      GraphData.Ranges.Count * textHeight));
            }
        }

        /// <summary>
        /// The chart paint method.
        /// </summary>
        public override void Paint()
        {
            RecalcAreas();
            Construct();
            base.Paint();
        }
    }
}