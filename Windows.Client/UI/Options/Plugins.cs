using System;
using System.Diagnostics;
using System.Windows.Forms;
using BritishMicro.TaskClerk.Plugins;
using System.ComponentModel;
using System.IO;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// Provides the plugins options control and manages the "do not load" list of plugins.
    /// </summary>
    [Description("Provides this dialog box manages the 'do not load' list.")]
    internal partial class Plugins : PluginOptionControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Plugins"/> class.
        /// </summary>
        public Plugins()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is called after the engine has initalised the component
        /// </summary>
        protected override void OnTaskClerkInit()
        {
            base.OnTaskClerkInit();
            foreach (LoadableItem li in Engine.PluginsProvider.Plugins)
            {
                ListViewItem lvi = new ListViewItem(li.ReturnType().FullName);
                lvi.Tag = li;
                if (li.Enabled)
                {
                    lvi.Checked = true;
                }
                pluginsListView.Items.Add(lvi);
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonManage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonManage_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(Engine.ApplicationFolder.FullName + @"\Plugins");
            if (di.Exists == false)
            {
                di.Create();
            }
            Process.Start(Engine.ApplicationFolder.FullName + @"\Plugins");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the pluginsListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void pluginsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pluginsListView.SelectedItems.Count == 1)
            {
                if (pluginsListView.SelectedItems[0].Tag != null)
                {
                    textBoxDescription.Text =
                        ((LoadableItem) pluginsListView.SelectedItems[0].Tag).Description;
                }
            }
        }

        private void pluginsListView_DoubleClick(object sender, EventArgs e)
        {
            LoadableItem li = pluginsListView.SelectedItems[0].Tag as LoadableItem;
            if (li != null)
            {
                MessageBox.Show(
                    li.ReturnType().ToString() + "\n" + 
                    "The plugins description is \n" + (li.Description != null?li.Description.ToString():"") + "\n" + 
                    "The plugin was found in the following file\n" + li.AssemblyFile.FullName, "Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}