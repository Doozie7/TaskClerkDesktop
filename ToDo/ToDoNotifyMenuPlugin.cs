using BritishMicro.TaskClerk.Plugins;
using System;

namespace BritishMicro.TaskClerk.ToDoPlugin
{
    /// <summary>
    /// Adds the menuitem for the ToDo dialog.
    /// </summary>
    [TaskClerkPlugin]
    public class ToDoNotifyMenuPlugin : PluginNotifyMenuItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoNotifyMenuPlugin"/> class.
        /// </summary>
        public ToDoNotifyMenuPlugin()
        {
            MenuText = "ToDo List";
        }

        /// <summary>
        /// Raises the <see cref="E:Click"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            using (ToDoExplorer tde = new ToDoExplorer(Engine))
            {
                this.MenuItem.Enabled = false;
                tde.ShowDialog();
                this.MenuItem.Enabled = true;
            }
        }
    }
}
