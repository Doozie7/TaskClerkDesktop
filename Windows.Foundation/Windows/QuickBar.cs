#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows
//	Class:		QuickBar
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		7 October 2002
//
//	Copyright (c) 2002/3 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
//using BritishMicro.Windows.Drawing;

namespace BritishMicro.Windows
{
    //------------------------------------------------------------------
    //using specifications
    //------------------------------------------------------------------
    
    /// <summary>
    /// Represents QuickBar information in the model
    /// </summary>
    public class QuickBar : GraphCanvas
    {
        //---------------------------------------------------------------------
        //private member variables
        //---------------------------------------------------------------------

        private Font textFont;
        private Color textColor;
        private StringFormat textStringFormat;

        private double maxValue;

        private void InitObject()
        {
        }

        #region constructors

        //---------------------------------------------------------------------
        //constructors
        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the QuickBar class.
        /// </summary>
        public QuickBar(GraphData data, int width, int height) : base(data, width, height)
        {
            textColor = Color.Black;
            textFont = new Font("Verdana", 8, FontStyle.Regular);
            textStringFormat = new StringFormat();
            textStringFormat.Alignment = StringAlignment.Center;
            textStringFormat.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="maxvalue"></param>
        /// <param name="quickbarvalue"></param>
        public QuickBar(int width, int height, double maxvalue, double quickbarvalue)
            : base(new GraphData(), width, height)
        {
            //this.data = new GraphRange();
            //GraphRange gr = new GraphRange("", Color.Red, Color.Pink, Math.Abs(quickbarvalue));
            GraphData.Ranges.Add(
                new GraphRange(quickbarvalue.ToString(),
                               Color.Red, Color.Pink,
                               Math.Abs(quickbarvalue)));

            MaxValue = Math.Abs(maxvalue);

            textColor = Color.Black;
            textFont = new Font("Verdana", 8, FontStyle.Regular);
            textStringFormat = new StringFormat();
            textStringFormat.Alignment = StringAlignment.Center;
            textStringFormat.LineAlignment = StringAlignment.Center;
        }

        #endregion

        //---------------------------------------------------------------------
        //public properties and methods
        //---------------------------------------------------------------------

        /// <summary>
        /// QuickBars require the max value to be setup befor draw, this value is used
        /// to calulate what percentage of the bar is filled
        /// </summary>
        public double MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Paint()
        {
            //calculate the size of fill
            RectangleF bitmapRectangle = new RectangleF(0, 0, Bitmap.Width - 1, Bitmap.Height - 1);
            DrawingElements.Add(new DrawBackGround(Color.White, bitmapRectangle));

            RectangleF barArea = new RectangleF(1, 1, bitmapRectangle.Width - 2, bitmapRectangle.Height - 2);

            double valueInQuickBar = ((GraphRange) GraphData.Ranges[0]).SumOfValues;
            string displayValue = Convert.ToString(valueInQuickBar);
            if (maxValue < valueInQuickBar)
            {
                maxValue = valueInQuickBar;
                displayValue += "...";
            }

            if (maxValue == 0)
                maxValue = 1;

            double withOfBar = (barArea.Width/maxValue)*valueInQuickBar;
            double widthOfLabel =
                ((SizeF) (Graphics.MeasureString(Convert.ToString(valueInQuickBar), textFont) + new SizeF(4, 4))).Width;
            if (widthOfLabel > withOfBar)
                withOfBar = widthOfLabel;
            RectangleF barRectangle = new RectangleF(barArea.X, barArea.Y, (float) withOfBar, (float) barArea.Height);

            //draw the fill
            Graphics.FillRectangle(
                new LinearGradientBrush(new PointF(0, 0),
                                        new PointF(bitmapRectangle.X + bitmapRectangle.Width,
                                                   bitmapRectangle.Y + bitmapRectangle.Height),
                                        ((GraphRange) GraphData.Ranges[0]).SecondaryColor,
                                        ((GraphRange) GraphData.Ranges[0]).PrimaryColor), barRectangle);
            Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1), GraphMaths.ConvertToRectangle(barRectangle));

            if (displayValue.EndsWith("..."))
                Graphics.DrawLine(new Pen(new SolidBrush(((GraphRange) GraphData.Ranges[0]).PrimaryColor), 1),
                                  new PointF(bitmapRectangle.X + bitmapRectangle.Width - 1, bitmapRectangle.Y + 2),
                                  new PointF(bitmapRectangle.X + bitmapRectangle.Width - 1,
                                             bitmapRectangle.Y + bitmapRectangle.Height - 2));

            //draw the text
            Graphics.DrawString(displayValue, textFont, new SolidBrush(textColor), barRectangle, textStringFormat);

            base.Paint();
        }
    }
}