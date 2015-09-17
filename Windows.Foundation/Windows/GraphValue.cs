#region Copyright (c) 2003 John Powell

//-----------------------------------------------------------------------------
//
//	Namespace:	BritishMicro.Windows
//	Class:		GraphValue
//	Written by:	jpowell (doozie7@hotmail.com)
//	Date:		1 October 2002
//
//	Copyright (c) 2002/3 John Powell
//
//-----------------------------------------------------------------------------

#endregion

using System;

namespace BritishMicro.Windows
{
    //------------------------------------------------------------------
    //using specifications
    //------------------------------------------------------------------
    
    /// <summary>
    /// Represents GraphValue information in the model
    /// </summary>
    public class GraphValue : IComparable
    {
        //---------------------------------------------------------------------
        //private member variables
        //---------------------------------------------------------------------

        private double point;
        private double min;
        private double max;

        #region constructors

        //---------------------------------------------------------------------
        //constructors
        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the GraphValue class based on a supplied double.
        /// </summary>
        /// <param name="point"></param>
        public GraphValue(double point) : this(point, point, point)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GraphValue class based on a supplied integer.
        /// </summary>
        /// <param name="point"></param>
        public GraphValue(int point) : this((double) point, (double) point, (double) point)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphValue"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        public GraphValue(decimal point)
            : this((double) point, (double) point, (double) point)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GraphValue class
        /// </summary>
        /// <param name="point"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public GraphValue(double point, double min, double max)
        {
            this.point = point;
            this.min = min;
            this.max = max;
        }

        #endregion

        //---------------------------------------------------------------------
        //public properties and methods
        //---------------------------------------------------------------------

        /// <summary>
        /// The primary point of this value.
        /// </summary>
        public double Point
        {
            get { return point; }
            set { point = value; }
        }

        /// <summary>
        /// The min point of this value.
        /// </summary>
        public double Min
        {
            get { return min; }
            set { min = value; }
        }

        /// <summary>
        /// The max point of this value.
        /// </summary>
        public double Max
        {
            get { return max; }
            set { max = value; }
        }

        /// <summary>
        /// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType()) return false;
            GraphValue working = (GraphValue) obj;
            return (Point == working.Point);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than obj. Zero This instance is equal to obj. Greater than zero This instance is greater than obj.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">obj is not the same type as this instance. </exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (!(obj is GraphValue))
            {
                throw new ArgumentException("Must be of type GraphValue");
            }
            return Point.CompareTo(((GraphValue) obj).Point);
        }
    }
}