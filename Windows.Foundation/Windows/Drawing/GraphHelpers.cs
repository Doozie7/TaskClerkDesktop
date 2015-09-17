#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		GraphHelpers
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		1 October 2002
//
//	Copyright (c) 2002/3 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System.Drawing;

namespace BritishMicro.Windows.Drawing
{
    /// <summary>
    /// Provides static helpers for building graphs and 
    /// placing imagery on the graphics object.
    /// </summary>
    internal class GraphHelpers
    {
        //---------------------------------------------------------------------
        //public properties and methods
        //---------------------------------------------------------------------

        /// <summary>
        /// Draws a string on the graphics object using the centre point supplied and working out
        /// the left top corner to draw from.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <param name="fillbrush"></param>
        /// <param name="centrepoint"></param>
        public static void DrawStringUsingCentrePoint(Graphics g, string text, Font font, Brush brush, Brush fillbrush,
                                                      PointF centrepoint)
        {
            SizeF stringSize = g.MeasureString(text, font) + new SizeF(4, 4);
                //remember to add the size of the borders in
            PointF newStartPoint =
                new PointF((centrepoint.X - (stringSize.Width/2)), (centrepoint.Y - (stringSize.Height/2)));
            RectangleF clipRect = new RectangleF(newStartPoint, new SizeF(stringSize.Width, stringSize.Height));
            Region currentClip = g.Clip;
            g.Clip = new Region(clipRect);

            RectangleF lableRect =
                new RectangleF(newStartPoint.X + 1, newStartPoint.Y + 1, stringSize.Width - 2, stringSize.Height - 2);
            g.FillRectangle(fillbrush, lableRect);
            g.DrawRectangle(new Pen(new SolidBrush(Color.DarkGray), 1), GraphMaths.ConvertToRectangle(lableRect));
            RectangleF textArea =
                new RectangleF(lableRect.X + 1, lableRect.Y + 1, lableRect.Width - 2, lableRect.Height - 2);
            g.DrawString(text, font, brush, textArea);
            g.Clip = currentClip;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="centrepoint"></param>
        /// <returns></returns>
        public static PointF TextPointFromCentre(Graphics g, string text, Font font, PointF centrepoint)
        {
            SizeF stringSize = g.MeasureString(text, font);
            return new PointF((centrepoint.X - (stringSize.Width/2)), (centrepoint.Y - (stringSize.Height/2)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="centrepoint"></param>
        /// <returns></returns>
        public static RectangleF TextRectangleFromCentre(Graphics g, string text, Font font, PointF centrepoint)
        {
            SizeF stringSize = g.MeasureString(text, font);
            PointF newStartPoint =
                new PointF((centrepoint.X - (stringSize.Width/2)), (centrepoint.Y - (stringSize.Height/2)));
            return new RectangleF(newStartPoint, new SizeF(stringSize.Width, stringSize.Height));
        }
    }
}