using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Represents the data configuration part in the print process.
    /// </summary>
    public class PrintConfiguration
    {
        private Type _printFormat;
        private DateTime _start;
        private DateTime _end;
        private Collection<TaskDescription> _allowedDescriptions;
        private Collection<TaskActivity> _activities;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintConfiguration"/> class.
        /// </summary>
        public PrintConfiguration()
        {
            _allowedDescriptions = new Collection<TaskDescription>();
            _activities = new Collection<TaskActivity>();
        }

        /// <summary>
        /// Gets or sets the print format.
        /// </summary>
        /// <value>The print format.</value>
        public Type PrintFormat
        {
            get { return _printFormat; }
            set { _printFormat = value; }
        }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>The start.</value>
        public DateTime Start
        {
            get { return _start; }
            set { _start = value; }
        }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>The end.</value>
        public DateTime End
        {
            get { return _end; }
            set { _end = value; }
        }

        /// <summary>
        /// Gets or sets the allowed descriptions.
        /// </summary>
        /// <value>The allowed descriptions.</value>
        public Collection<TaskDescription> AllowedDescriptions
        {
            get { return _allowedDescriptions; }
            set { _allowedDescriptions = value; }
        }

        /// <summary>
        /// Gets task activities.
        /// </summary>
        /// <value>The activities.</value>
        public Collection<TaskActivity> Activities
        {
            get { return _activities; }
            set { _activities = value; }
        }
	
    }
}
