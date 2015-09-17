#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		DrawRectangle
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		15 March 2006
//
//	Copyright (c) 2006 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BritishMicro.Windows.Drawing
{
    internal class DrawRoundedRectangle : IDrawingElement
    {
        private Pen _pen;
        private RectangleF _rectangle;
        private float _cornerSize;

        public DrawRoundedRectangle(Pen pen, float x, float y, float width, float height, float cornerSize)
        {
            _pen = pen;
            _rectangle = new RectangleF(x, y, width, height);
            _cornerSize = cornerSize;
        }

        public DrawRoundedRectangle(Pen pen, int x, int y, int width, int height, int cornerSize)
        {
            _pen = pen;
            _rectangle = new RectangleF(x, y, width, height);
            _cornerSize = cornerSize;
        }

        public DrawRoundedRectangle(Pen pen, Rectangle rectangle, int cornerSize)
        {
            _pen = pen;
            _rectangle = new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            _cornerSize = cornerSize;
        }

        public DrawRoundedRectangle(Pen pen, RectangleF rectangle, float cornerSize)
        {
            _pen = pen;
            _rectangle = rectangle;
            _cornerSize = cornerSize;
        }

        #region IDrawingElement Members

        public void Render(Graphics graphics)
        {
            // Define the size of the rectangle used for each of the 4 arcs.

            Size CornerSize = new Size((int) _cornerSize, (int) _cornerSize);

            //  Define the buffer between the rounded rectangle and the image itself.

            int x = 5;
            int y = 5;

            // Set positions for the arcs and center positions for the rectangle

            int w = (int) _rectangle.Width;
            int h = (int) _rectangle.Height;
            int xr = Convert.ToInt32(w - (x + CornerSize.Width));
            int yr = Convert.ToInt32(h - (y + CornerSize.Height));
            //int iw = Convert.ToInt32(w - xr);
            //int ih = Convert.ToInt32(h - yr);

            // Standard angle for each arc.

            float sweepAngle = 90.0f;

            SolidBrush fillBrush;
            //Color FillColor;

            // Create 4 10 x 10 rectangles for our arcs to fit in. tl=topleft, br=bottomright

            Rectangle tl = new Rectangle(x, y, CornerSize.Width, CornerSize.Height);
            Rectangle tr = new Rectangle(xr, y, CornerSize.Width, CornerSize.Height);
            Rectangle bl = new Rectangle(x, yr, CornerSize.Width, CornerSize.Height);
            Rectangle br = new Rectangle(xr, yr, CornerSize.Width, CornerSize.Height);

            // Create an inner rectangle to fill the middle

            Rectangle innerRect = new Rectangle(x, CornerSize.Width, x + xr, yr - y);

            // Here's how it is all bound together.  We need to add the arcs and the
            // inner rectangle to a single GraphicsPath object.  This allows us to call
            // the .FillPath method to only fill in the section inside the arcs and our rectangle.

            GraphicsPath path = new GraphicsPath();

            path.AddArc(tl, 180, sweepAngle);
            path.AddArc(tr, 270, sweepAngle);
            path.AddRectangle(innerRect);
            path.AddArc(bl, 90, sweepAngle);
            path.AddArc(br, 360, sweepAngle);

            //if (ImageSetting.ApplyInternalFill)
            //{
            //    FillColor = ImageSetting.BackColor;
            //}
            //else
            //{
            //    FillColor = ImageSetting.BackGroundColor;
            //}
            fillBrush = new SolidBrush(_pen.Color);

            // Connect all border lines drawn in the entire path

            path.CloseAllFigures();

            graphics.FillPath(fillBrush, path);
        }

        #endregion
    }
}