//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using BritishMicro.TaskClerk.Properties;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// TaskDescription Event Args are passed around by effents that require
    /// TaskDescription information to available.
    /// </summary>
    public class TaskDescriptionEventArgs : EventArgs
    {
        /// <summary>
        /// The Empty TaskDescription event Arg
        /// </summary>
        public new static TaskDescriptionEventArgs Empty = new TaskDescriptionEventArgs();

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDescriptionEventArgs"/> class.
        /// </summary>
        private TaskDescriptionEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDescriptionEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public TaskDescriptionEventArgs(TaskDescription item) : this(item, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDescriptionEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="parent">The parent.</param>
        public TaskDescriptionEventArgs(TaskDescription item, TaskDescription parent)
        {
            if (item == null)
            {
                throw new InvalidOperationException(Resources.DescriptionCannotBeSetToNull);
            }

            _item = item;
            _parent = parent;
        }

        private TaskDescription _item;

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>The task description.</value>
        public TaskDescription TaskDescription
        {
            get { return _item; }
            set { _item = value; }
        }

        private TaskDescription _parent;

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public TaskDescription Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        private bool _cancel;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TaskDescriptionEventArgs"/> is cancel.
        /// </summary>
        /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
    }
}