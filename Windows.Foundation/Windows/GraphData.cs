#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows
//	Class:		GraphData
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		1 October 2002
//
//	Copyright (c) 2002/3 John Powell
//	Copyright (c) 2006 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System.Collections.Generic;

namespace BritishMicro.Windows
{
    //------------------------------------------------------------------
    //using specifications
    //------------------------------------------------------------------
    
    /// <summary>
    /// Represents GraphData information in the model
    /// </summary>
    public class GraphData
    {
        //---------------------------------------------------------------------
        //private member variables
        //---------------------------------------------------------------------

        private string _title;
        private List<GraphRange> _ranges;

        #region constructors

        //---------------------------------------------------------------------
        //constructors
        //---------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        public GraphData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        public GraphData(string title)
        {
            Title = title;
        }

        #endregion

        //---------------------------------------------------------------------
        //public properties and methods
        //---------------------------------------------------------------------
        /// <summary>
        /// The title of the graph.
        /// </summary>
        public string Title
        {
            get
            {
                if (_title == null)
                    _title = string.Empty;
                return _title;
            }
            set { _title = value; }
        }

        /// <summary>
        /// The list of ranges
        /// </summary>
        public List<GraphRange> Ranges
        {
            get
            {
                if (_ranges == null)
                {
                    _ranges = new List<GraphRange>();
                }
                return _ranges;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GraphRange FindGraphRange(string name)
        {
            GraphRange foundRange = null;
            foreach (GraphRange range in Ranges)
            {
                if (range.Label == name)
                {
                    foundRange = range;
                    break;
                }
            }
            return foundRange;
        }
    }
}