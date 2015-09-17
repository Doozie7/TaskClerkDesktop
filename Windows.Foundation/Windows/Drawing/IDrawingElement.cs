#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows.Drawing
//	Interface:	IDrawingElement
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
    internal interface IDrawingElement
    {
        void Render(Graphics graphics);

    }
}