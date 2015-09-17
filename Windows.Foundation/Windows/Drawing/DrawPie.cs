#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		DrawPie
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
    internal class DrawPie : IDrawingElement
    {
        private Pen _pen;
        private RectangleF _rectangle;
        private float _startAngle;
        private float _endAngle;

        public DrawPie(Pen pen, RectangleF rectangle, float startAngle, float endAngle)
        {
            _pen = pen;
            _rectangle = rectangle;
            _startAngle = startAngle;
            _endAngle = endAngle;
        }

        #region IDrawingElement Members

        public void Render(Graphics graphics)
        {
            graphics.DrawPie(
                _pen,
                _rectangle,
                _startAngle,
                _endAngle);
        }

        #endregion
    }
}