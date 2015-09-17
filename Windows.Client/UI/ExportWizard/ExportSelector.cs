using System;
using System.Windows.Forms;
using BritishMicro.TaskClerk.Plugins;
using BritishMicro.Windows;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class ExportSelector : WizardUserControl
    {

        private ExportForm _mainForm;

        public ExportSelector(ExportForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            RebuildList();
        }

        private void RebuildList()
        {
            listView.Items.Clear();
            foreach (LoadableItem loadableItem in AppContext.Current.PluginsProvider.GetPlugisOfSubcalss(typeof(PluginExporter)))
            {
                PluginExporter eb = loadableItem.CreateInstance() as PluginExporter;
                if (eb != null)
                {
                    ListViewItem lvi = new ListViewItem(loadableItem.DisplayName);
                    lvi.Tag = loadableItem;
                    lvi.ToolTipText = loadableItem.Description;
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