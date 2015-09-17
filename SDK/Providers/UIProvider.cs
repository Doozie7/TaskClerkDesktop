using System;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// The UIProvider is the base provider for UI functionality in the application.
    /// </summary>
    public abstract class UIProvider : TaskClerkProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:UIProvider"/> class.
        /// </summary>
        protected UIProvider()
        {
        }

        /// <summary>
        /// Shows the activity explorer.
        /// </summary>
        /// <returns></returns>
        public abstract DialogResult ShowActivityExplorer();
        /// <summary>
        /// Shows the description explorer.
        /// </summary>
        /// <returns></returns>
        public abstract TaskDescription ShowDescriptionExplorer();
        /// <summary>
        /// Shows the options explorer.
        /// </summary>
        /// <returns></returns>
        public abstract DialogResult ShowOptionsExplorer();
        /// <summary>
        /// Shows the nag message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public abstract DialogResult ShowNagMessage(string message);

        private Type _activityExplorer;
        private Type _descriptionExplorer;

        /// <summary>
        /// Gets or sets the activity explorer.
        /// </summary>
        /// <value>The activity explorer.</value>
        public Type ActivityExplorer
        {
            get { return _activityExplorer; }
            set { _activityExplorer = value; }
        }

        /// <summary>
        /// Gets or sets the description explorer.
        /// </summary>
        /// <value>The description explorer.</value>
        public Type DescriptionExplorer
        {
            get { return _descriptionExplorer; }
            set { _descriptionExplorer = value; }
        }
    }
}