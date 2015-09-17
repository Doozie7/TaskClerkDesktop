using System;
using System.ComponentModel;

namespace BritishMicro.TaskClerk.Plugins
{
    [TaskClerkPlugin]
    public partial class PluginToolMenuItem : PluginComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginToolMenuItem"/> class.
        /// </summary>
        public PluginToolMenuItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginToolMenuItem"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public PluginToolMenuItem(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> Click;

        /// <summary>
        /// Raises the <see cref="E:Click"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public virtual void OnClick(EventArgs e)
        {
            EventHandler<EventArgs> handler = Click;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}