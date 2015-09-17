#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		FillPie
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
    internal class FillPie : IDrawingElement
    {
        private Brush _brush;
        private RectangleF _rectangle;
        private float _startAngle;
        private float _endAngle;

        public FillPie(Brush brush, RectangleF rectangle, float startAngle, float endAngle)
        {
            _brush = brush;
            _rectangle = rectangle;
            _startAngle = startAngle;
            _endAngle = endAngle;
        }

        #region IDrawingElement Members

        public void Render(Graphics graphics)
        {
            graphics.FillPie(
                _brush,
                _rectangle.X,
                _rectangle.Y,
                _rectangle.Width,
                _rectangle.Height,
                _startAngle,
                _endAngle);
        }

        #endregion
    }
}