using System;
using System.ComponentModel;
using BritishMicro.TaskClerk.Plugins;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Provides a simple baloon popup showing the currently running task activity
    /// </summary>
    public partial class Status : PluginNotifyMenuItem
    {
        /// <summary>
        /// This method is called after the engine has initalised the component
        /// </summary>
        protected override void OnTaskClerkInit()
        {
            base.OnTaskClerkInit();

            this.MenuText = "Status";
            this.Text = "Status";
            this.Click += new System.EventHandler<System.EventArgs>(this.Status_Click);
        }

        /// <summary>
        /// Handles the Click event of the Status control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Status_Click(object sender, EventArgs e)
        {
            ShowCurrentStatus();
        }

        /// <summary>
        /// Show the current active task in a boaloon tip
        /// </summary>
        private void ShowCurrentStatus()
        {
            string message = null;
            TaskActivity current = Engine.CurrentActivity;
            if ((current.IsNotEmpty()) && (current.TaskDescription.IsNotEmpty()))
            {
                message = current.ToSummaryString();
            }
            else
            {
                message = @"The TaskClerk system is ready and idle.
There is currently no Task Activity running.
Use the help to discover how you can start and stop Tasks.";
            }
            Engine.UIProvider.ShowNagMessage(message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public Status(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}