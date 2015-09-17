//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 01st of Jan 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using BritishMicro.TaskClerk.Properties;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Provides TaskActivity based event data.
    /// </summary>
    public class TaskActivityEventArgs : EventArgs
    {
        /// <summary>
        /// Empty TaskActivityEventArgs
        /// </summary>
        public new static readonly TaskActivityEventArgs Empty
            = new TaskActivityEventArgs(TaskActivity.Empty, DateTime.MinValue);

        private DateTime _effectiveDate;
        private TaskActivity _activity;
        private bool _cancel;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivityEventArgs"/> class.
        /// </summary>
        private TaskActivityEventArgs()
        {
        }

        /// <summary>
        /// Initalises a new instance of the TaskActivityEventArgs with a TaskActivity and 
        /// uses the TaskActivities StartDate as the effective date.
        /// </summary>
        /// <param name="activity"></param>
        public TaskActivityEventArgs(TaskActivity activity)
        {
            if (activity == null)
            {
                throw new InvalidOperationException(Resources.ActivityCannotBeSetToNull);
            }

            _activity = activity;
            _effectiveDate = activity.StartDate;
        }

        /// <summary>
        /// Initalises a new instance of the TaskActivityEventArgs with a TaskActivity and an effective date.
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="effectiveDate"></param>
        public TaskActivityEventArgs(TaskActivity activity, DateTime effectiveDate)
        {
            if (activity == null)
            {
                throw new InvalidOperationException(Resources.ActivityCannotBeSetToNull);
            }

            _activity = activity;
            _effectiveDate = effectiveDate;
        }

        /// <summary>
        /// The TaskActivity
        /// </summary>
        public TaskActivity TaskActivity
        {
            get { return _activity; }
        }

        /// <summary>
        /// The date the TaskActivity is associated with.
        /// </summary>
        public DateTime EffectiveDate
        {
            get { return _effectiveDate; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TaskActivityEventArgs"/> is canceled.
        /// </summary>
        /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
	
    }
}