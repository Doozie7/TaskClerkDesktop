#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		DrawLine
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
    internal class DrawLine : IDrawingElement
    {
        private Pen _pen;
        private PointF _pointA;
        private PointF _pointB;

        public DrawLine(Pen pen, PointF pointA, PointF pointB)
        {
            _pen = pen;
            _pointA = pointA;
            _pointB = pointB;
        }

        #region IDrawingElement Members

        public void Render(Graphics graphics)
        {
            graphics.DrawLine(
                _pen,
                _pointA,
                _pointB);
        }

        #endregion
    }
}