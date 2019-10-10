using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.Plugins
{
    /// <summary>
    /// Provides the base class for all NotifyMenu plugins
    /// </summary>
    [TaskClerkPlugin]
    public partial class PluginNotifyMenuItem : PluginComponent
    {
        private MenuItem menuItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginNotifyMenuItem"/> class.
        /// </summary>
        public PluginNotifyMenuItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginNotifyMenuItem"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        protected PluginNotifyMenuItem(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// The Click event
        /// </summary>
        public event EventHandler<EventArgs> Click;

        /// <summary>
        /// Raises the <see cref="E:Click"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public virtual void OnClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        private string _menuText;

        /// <summary>
        /// The text to be used on the menu
        /// </summary>
        public virtual string MenuText
        {
            get { return _menuText; }
            set { _menuText = value; }
        }

        /// <summary>
        /// Gets the menuitem.
        /// </summary>
        /// <value>The menu item.</value>
        public MenuItem MenuItem
        {
            get { return menuItem; }
        }


    }
}