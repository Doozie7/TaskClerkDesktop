//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Globalization;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Provides infromation for the TaskClerk event process
    /// </summary>
    public class InformationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InformationEventArgs"/> class.
        /// </summary>
        private InformationEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationEventArgs"/> class.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="description">The description.</param>
        public InformationEventArgs(string eventType, string description) : this(eventType, description, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InformationEventArgs"/> class.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="description">The description.</param>
        /// <param name="payload">The payload.</param>
        public InformationEventArgs(string eventType, string description, object payload)
        {
            _eventType = eventType;
            _description = description;
            _payload = payload;
        }

        private string _eventType;

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public string EventType
        {
            get { return _eventType; }
            set { _eventType = value; }
        }

        private string _description;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private object _payload;

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>The payload.</value>
        public object Payload
        {
            get { return _payload; }
            set { _payload = value; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            string payload;
            if (Payload == null)
            {
                payload = "";
            }
            else
            {
                payload = Payload.ToString();
            }

            string information = string.Format(
                CultureInfo.InvariantCulture,
                "{0}\t{1}\t{2}\t:\t{3}",
                DateTime.Now,
                EventType,
                Description,
                payload);

            return information;
        }
    }
}