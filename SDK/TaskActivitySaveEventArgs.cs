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
    /// The TaskActivitySaveEventArgs contains information for Taskactivity provider
    /// </summary>
    public class TaskActivitySaveEventArgs : EventArgs
    {
        private Collection<TaskActivity> _activities;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivitySaveEventArgs"/> class.
        /// </summary>
        private TaskActivitySaveEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivitySaveEventArgs"/> class.
        /// </summary>
        /// <param name="activities">The activities.</param>
        public TaskActivitySaveEventArgs(Collection<TaskActivity> activities)
        {
            _activities = activities;
        }

        /// <summary>
        /// Gets the task activities.
        /// </summary>
        /// <value>The task activities.</value>
        public Collection<TaskActivity> TaskActivities
        {
            get { return _activities; }
        }
    }
}