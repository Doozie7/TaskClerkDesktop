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
    /// The TaskActivityLoadEventArgs contains information for manipulating the TaskActivity provider.
    /// </summary>
    public class TaskActivityLoadEventArgs : EventArgs
    {
        private readonly Collection<TaskActivity> _activities;
        private readonly DateTime _start;
        private readonly DateTime _end;
        private readonly Collection<TaskDescription> _allowedTaskDescriptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivityLoadEventArgs"/> class.
        /// </summary>
        private TaskActivityLoadEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivityLoadEventArgs"/> class.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="allowedTaskDescriptions">The allowed task descriptions.</param>
        /// <param name="activities">The activities.</param>
        public TaskActivityLoadEventArgs(DateTime start, DateTime end, Collection<TaskDescription> allowedTaskDescriptions, Collection<TaskActivity> activities)
        {
            _start = start;
            _end = end;
            _allowedTaskDescriptions = allowedTaskDescriptions;
            _activities = activities;
        }

        /// <summary>
        /// Gets the start date and time.
        /// </summary>
        /// <value>The start.</value>
        public DateTime Start
        {
            get { return _start; }
        }

        /// <summary>
        /// Gets the end date and time.
        /// </summary>
        /// <value>The end.</value>
        public DateTime End
        {
            get { return _end; }
        }

        /// <summary>
        /// Gets the allowed task descriptions.
        /// </summary>
        /// <value>The allowed task descriptions.</value>
        public Collection<TaskDescription> AllowedTaskDescriptions
        {
            get { return _allowedTaskDescriptions; }
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