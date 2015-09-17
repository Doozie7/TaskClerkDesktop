#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows
//	Class:		LineGraph
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		11 December 2003
//
//	Copyright (c) 2003 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System.Drawing;
using BritishMicro.Windows.Drawing;

namespace BritishMicro.Windows
{
    //using System.Collections.Generic;

    /// <summary>
    /// The line grapg generates a line graph
    /// </summary>
    public class LineGraph : GraphCanvas
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineGraph"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public LineGraph(GraphData data)
            : base(data)
        {
            GraphRectangle = new RectangleF(0, 0, 500, 500);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LineGraph"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public LineGraph(GraphData data, int width, int height)
            : base(data, width, height)
        {
            GraphRectangle = new Rectangle(0, 0, width, height);
        }

        /// <summary>
        /// The chart paint method.
        /// </summary>
        public override void Paint()
        {
            DrawingElements.Add(new DrawBackGround(Color.White, GraphRectangle));
            DrawLineGraph();
            base.Paint();
        }

        /// <summary>
        /// Draws the line graph.
        /// </summary>
        private void DrawLineGraph()
        {
            GenerateTitle();
        }

        /// <summary>
        /// Generates the title.
        /// </summary>
        private void GenerateTitle()
        {
            if (DrawTitle == true)
            {
#if GRAPHDEBUG
                    DrawingElements.Add(new DrawRectangle(debugPen, TitleRectangle));
#endif
                DrawingElements.Add(
                        new DrawString(GraphData.Title, TitleFont, new SolidBrush(TitleColor), TitleRectangle,
                                       TitleStringFormat));
            }
        }
    }
}