#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		FillRectangle
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
    internal class FillRectangle : IDrawingElement
    {
        private Brush _brush;
        private RectangleF _rectangle;

        public FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            _brush = brush;
            _rectangle = new RectangleF(x, y, width, height);
        }

        public FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            _brush = brush;
            _rectangle = new RectangleF(x, y, width, height);
        }

        public FillRectangle(Brush brush, Rectangle rectangle)
        {
            _brush = brush;
            _rectangle = new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public FillRectangle(Brush brush, RectangleF rectangle)
        {
            _brush = brush;
            _rectangle = rectangle;
        }

        #region IDrawingElement Members

        public void Render(Graphics graphics)
        {
            graphics.FillRectangle(
                _brush,
                _rectangle);
        }

        #endregion
    }
}