#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		DrawRectangle
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		10 December 2003
//
//	Copyright (c) 2003 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System.Drawing;

namespace BritishMicro.Windows.Drawing
{
    internal class DrawRectangle : IDrawingElement
    {
        private Pen _pen;
        private RectangleF _rectangle;

        public DrawRectangle(Pen pen, float x, float y, float width, float height)
        {
            _pen = pen;
            _rectangle = new RectangleF(x, y, width, height);
        }

        public DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            _pen = pen;
            _rectangle = new RectangleF(x, y, width, height);
        }

        public DrawRectangle(Pen pen, Rectangle rectangle)
        {
            _pen = pen;
            _rectangle = new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public DrawRectangle(Pen pen, RectangleF rectangle)
        {
            _pen = pen;
            _rectangle = rectangle;
        }

        #region IDrawingElement Members

        public void Render(Graphics graphics)
        {
            graphics.DrawRectangle(
                _pen,
                _rectangle.X,
                _rectangle.Y,
                _rectangle.Width,
                _rectangle.Height);
        }

        #endregion
    }
}