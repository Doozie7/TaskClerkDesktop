#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		GraphMaths
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		3 October 2002
//
//	Copyright (c) 2002/3 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System;
using System.Drawing;

namespace BritishMicro.Windows.Drawing
{
    //------------------------------------------------------------------
    //using specifications
    //------------------------------------------------------------------
    
    /// <summary>
    /// Provides static maths function for building graphs.
    /// </summary>
    internal class GraphMaths
    {
        private GraphMaths()
        {
        }

        /// <summary>
        /// Calculate a PointF(X, Y) on circumference of a circle. Developed by Cello.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static PointF PointOnCircleF(int angle, RectangleF rect)
        {
            float x;
            float y;
            float radius;
            float offset;
            float radian = ((float) Math.PI*2/360)*(angle - 90);

            radius = rect.Width/2;
            offset = (rect.X + (rect.Width/2));
            x = radius*(float) Math.Cos(radian) + offset;

            radius = rect.Height/2;
            offset = (rect.Y + (rect.Height/2));
            y = radius*(float) Math.Sin(radian) + offset;

            return new PointF(x, y);
        }

        /// <summary>
        /// Calculate a Point(X, Y) on circumference of a circle. Developed by Cello
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Point PointOnCircle(int angle, Rectangle rect)
        {
            RectangleF tempRect = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
            PointF tempPoint = PointOnCircleF(angle, tempRect);
            return new Point(Convert.ToInt32(tempPoint.X), Convert.ToInt32(tempPoint.Y));
        }

        /// <summary>
        /// Converts a RectangleF to a Rectangle
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Rectangle ConvertToRectangle(RectangleF rect)
        {
            return new Rectangle(
                Convert.ToInt32(rect.X),
                Convert.ToInt32(rect.Y),
                Convert.ToInt32(rect.Width),
                Convert.ToInt32(rect.Height));
        }

        /// <summary>
        /// Converts a Rectangle to a RectangleF
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static RectangleF ConvertToRectangleF(Rectangle rect)
        {
            return new RectangleF((float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height);
        }
    }
}