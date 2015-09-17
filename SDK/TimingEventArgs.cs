using System;
using System.Reflection;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// Provides timing information for provider method calls.
    /// </summary>
    public class TimingEventArgs : EventArgs
    {
        private MethodBase _method;
        private DateTime _timerStart;
        private DateTime _timerEnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimingEventArgs"/> class.
        /// </summary>
        private TimingEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimingEventArgs"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="timerStart">The timer start.</param>
        /// <param name="timerEnd">The timer end.</param>
        public TimingEventArgs(MethodBase method, DateTime timerStart, DateTime timerEnd)
        {
            _method = method;
            _timerStart = timerStart;
            _timerEnd = timerEnd;
        }

        /// <summary>
        /// Gets or sets the provider menthod.
        /// </summary>
        /// <value>The provider menthod.</value>
        public MethodBase Method
        {
            get { return _method; }
        }

        /// <summary>
        /// Gets or sets the timer start.
        /// </summary>
        /// <value>The timer start.</value>
        public DateTime TimerStart
        {
            get { return _timerStart; }
        }

        /// <summary>
        /// Gets or sets the timer end.
        /// </summary>
        /// <value>The timer end.</value>
        public DateTime TimerEnd
        {
            get { return _timerEnd; }
        }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public TimeSpan Duration
        {
            get { return _timerEnd.Subtract(_timerStart); }
        }	
    }
}
