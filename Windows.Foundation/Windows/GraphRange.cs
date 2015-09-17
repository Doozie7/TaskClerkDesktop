#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows
//	Class:		GraphRange
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		1 October 2002
//
//	Copyright (c) 2002/3 John Powell
//	Copyright (c) 2006 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Drawing;

namespace BritishMicro.Windows
{
    //------------------------------------------------------------------
    //using specifications
    //------------------------------------------------------------------
    
    /// <summary>
    /// Represents GraphRange information in the model
    /// </summary>
    public class GraphRange
    {
        //---------------------------------------------------------------------
        //private member variables
        //---------------------------------------------------------------------

        private int number;
        private string label;
        private List<GraphValue> _values;
        private Color primaryColor;
        private Color secondaryColor;
        private int opacity;

        #region constructors

        //---------------------------------------------------------------------
        //constructors
        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the GraphRange class.
        /// </summary>
        public GraphRange()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphRange"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        public GraphRange(string label)
            : this(label, 255, GetRandomColor(), GetRandomColor(), null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GraphRange class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="firstcolor">The firstcolor.</param>
        /// <param name="secondcolor">The secondcolor.</param>
        /// <param name="singleValue">The single value.</param>
        public GraphRange(string label, Color firstcolor, Color secondcolor, double singleValue)
            : this(label, 255, firstcolor, secondcolor, null)
        {
            Values.Add(new GraphValue(singleValue));
        }

        /// <summary>
        /// Initializes a new instance of the GraphRange class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="firstcolor">The firstcolor.</param>
        /// <param name="secondcolor">The secondcolor.</param>
        /// <param name="values">The values.</param>
        public GraphRange(string label, Color firstcolor, Color secondcolor, List<double> values)
            : this(label, 255, firstcolor, secondcolor, values)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphRange"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="color">The color.</param>
        public GraphRange(string label, Color color)
            : this(label, 255, color, color, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphRange"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="color">The color.</param>
        /// <param name="quickValues">The quick values.</param>
        public GraphRange(string label, Color color, double[] quickValues)
            : this(label, 255, color, color, null)
        {
            if (quickValues != null)
            {
                foreach (double d in quickValues)
                {
                    Values.Add(new GraphValue(d));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the GraphRange class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="opacity">The opacity.</param>
        /// <param name="firstcolor">The firstcolor.</param>
        /// <param name="secondcolor">The secondcolor.</param>
        /// <param name="values">The values.</param>
        public GraphRange(string label, int opacity, Color firstcolor, Color secondcolor, List<double> values)
        {
            this.label = label;
            primaryColor = firstcolor;
            secondaryColor = secondcolor;
            this.opacity = opacity;
            if (values != null)
            {
                foreach (double d in values)
                {
                    Values.Add(new GraphValue(d));
                }
            }
        }

        #endregion

        //---------------------------------------------------------------------
        //public properties and methods
        //---------------------------------------------------------------------

        /// <summary>
        /// The unique number of this range
        /// </summary>
        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        /// <summary>
        /// The label associated with this range
        /// </summary>
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        /// <summary>
        /// The primary color associated with this range
        /// </summary>
        public Color PrimaryColor
        {
            get { return primaryColor; }
            set { primaryColor = value; }
        }

        /// <summary>
        /// The secondary color associated with this range
        /// </summary>
        public Color SecondaryColor
        {
            get { return secondaryColor; }
            set { secondaryColor = value; }
        }

        /// <summary>
        /// The opacity of this range values from 0=Transparent to 255=Opaque
        /// </summary>
        public int Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }

        /// <summary>
        /// The values associates with this range
        /// </summary>
        public List<GraphValue> Values
        {
            get
            {
                if (_values == null)
                {
                    _values = new List<GraphValue>();
                }
                return _values;
            }
            set { _values = value; }
        }

        /// <summary>
        /// Gets the sum of values.
        /// </summary>
        /// <value>The sum of values.</value>
        public double SumOfValues
        {
            get
            {
                Values.Sort();
                double sumOfValues = 0;
                foreach (GraphValue v in _values)
                {
                    sumOfValues += v.Point;
                }
                return sumOfValues;
            }
        }

        /// <summary>
        /// Gets the min value.
        /// </summary>
        /// <value>The min value.</value>
        public double MinValue
        {
            get
            {
                Values.Sort();
                if (_values.Count > 1)
                {
                    return (double) _values[0].Point;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the max value.
        /// </summary>
        /// <value>The max value.</value>
        public double MaxValue
        {
            get
            {
                Values.Sort();
                if (_values.Count > 1)
                {
                    return (double) _values[_values.Count - 1].Point;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets the random color.
        /// </summary>
        /// <returns></returns>
        public static Color GetRandomColor()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            return Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
        }
    }
}