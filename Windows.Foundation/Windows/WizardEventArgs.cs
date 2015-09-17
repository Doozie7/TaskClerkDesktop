using System;

namespace BritishMicro.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public class WizardEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public new static WizardEventArgs Empty = new WizardEventArgs();

        private WizardEventArgs()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="current"></param>
        /// <param name="next"></param>
        public WizardEventArgs(string action, int current, int next)
        {
            _action = action;
            _current = current;
            _next = next;
        }

        private string _action;
        private int _current;
        private int _next;
        private bool _cancel;

        /// <summary>
        /// 
        /// </summary>
        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Current
        {
            get { return _current; }
            set { _current = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Next
        {
            get { return _next; }
            set { _next = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
    }
}