//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Provides the EventArgs for the GetTotla days engine call.
    /// </summary>
    public class TaskActivityDiscoverDateMetricsEventArgs : EventArgs
    {
        private Collection<DateTime> _storedDays;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivityDiscoverDateMetricsEventArgs"/> class.
        /// </summary>
        private TaskActivityDiscoverDateMetricsEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivityDiscoverDateMetricsEventArgs"/> class.
        /// </summary>
        /// <param name="storedDays">The stored days.</param>
        public TaskActivityDiscoverDateMetricsEventArgs(Collection<DateTime> storedDays)
        {
            _storedDays = storedDays;
        }

        /// <summary>
        /// Gets the stored days.
        /// </summary>
        /// <value>The stored days.</value>
        public Collection<DateTime> StoredDays
        {
            get { return _storedDays; }
        }
    }
}