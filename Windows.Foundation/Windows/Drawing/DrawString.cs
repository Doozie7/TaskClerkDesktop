#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Class:		DrawString
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
    internal class DrawString : IDrawingElement
    {
        private string _text;
        private Font _font;
        private Brush _brush;
        private RectangleF _rectangle;
        private StringFormat _format;

        public DrawString(string text, Font font, Brush brush, RectangleF rectangle, StringFormat format)
        {
            _text = text;
            _font = font;
            _brush = brush;
            _rectangle = rectangle;
            _format = format;
        }

        #region IDrawingElement Members

        public void Render(Graphics graphics)
        {
            graphics.DrawString(
                _text,
                _font,
                _brush,
                _rectangle,
                _format);
        }

        #endregion
    }
}