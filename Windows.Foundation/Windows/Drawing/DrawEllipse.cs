#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		DrawEllipse
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
    internal class DrawEllipse : IDrawingElement
    {
        private Pen _pen;
        private RectangleF _rectangle;

        public DrawEllipse(Pen pen, RectangleF rectangle)
        {
            _pen = pen;
            _rectangle = rectangle;
        }

        #region IDrawingElement Members

        public void Render(Graphics graphics)
        {
            graphics.DrawEllipse(
                _pen,
                _rectangle);
        }

        #endregion
    }
}