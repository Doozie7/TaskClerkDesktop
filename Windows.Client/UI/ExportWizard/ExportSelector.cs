using BritishMicro.TaskClerk.Plugins;
using BritishMicro.Windows;
using System;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class ExportSelector : WizardUserControl
    {

        private readonly ExportForm _mainForm;

        public ExportSelector(ExportForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            RebuildList();
        }

        private void RebuildList()
        {
            listView.Items.Clear();
            foreach (LoadableItem loadableItem in AppContext.Current.PluginsProvider.GetPluginsOfSubclass(typeof(PluginExporter)))
            {
                if (loadableItem.CreateInstance() is PluginExporter eb)
                {
                    ListViewItem lvi = new ListViewItem(loadableItem.DisplayName)
                    {
                        Tag = loadableItem,
                        ToolTipText = loadableItem.Description
                    };
                    listView.Items.Add(lvi);
                }
            }
            listView.Items[0].Selected = true;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView.Items)
            {
                if (lvi.Selected == true)
                {
                    _mainForm.SelectedExporter = lvi.Tag as LoadableItem;
                }
            }
        }

        private void listView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            toolTip.SetToolTip(listView, e.Item.ToolTipText);
        }
    }
}