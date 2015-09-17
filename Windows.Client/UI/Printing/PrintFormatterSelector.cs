using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BritishMicro.TaskClerk.Plugins;

namespace BritishMicro.TaskClerk.UI.Printing
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PrintFormatterSelector : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrintStyleSelector"/> class.
        /// </summary>
        public PrintFormatterSelector()
        {
            InitializeComponent();
            RebuildPrintFormatterList();
            SelectCurrentPrintFormatter();
        }

        private void SelectCurrentPrintFormatter()
        {
            try
            {
                object testO = (string)AppContext.Current.SettingsProvider.Get("CurrentPrintFormatter");
                if (testO != null)
                {
                    string Current_PrintStyle = (string)testO;
                    foreach (ListViewItem lvi in listviewPrintFormatters.Items)
                    {
                        lvi.Selected = false;
                        if (lvi.Tag.ToString() == Current_PrintStyle)
                        {
                            lvi.Selected = true;
                        }
                    }
                }
            }
            catch (InvalidOperationException)
            {
                // ignore this failure
            }
        }

        private void RebuildPrintFormatterList()
        {
            listviewPrintFormatters.Items.Clear();
            foreach (LoadableItem loadableItem in AppContext.Current.PluginsProvider.Plugins)
            {
                if(loadableItem.ReturnType().IsSubclassOf(typeof(PluginPrintFormatter)))
                {
                    ListViewItem lvi = new ListViewItem(loadableItem.DisplayName);
                    lvi.Tag = loadableItem.ReturnType().Name;
                    lvi.ToolTipText = loadableItem.Description;
                    listviewPrintFormatters.Items.Add(lvi);
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            AppContext.Current.SettingsProvider.Set(
                "CurrentPrintFormatter", 
                listviewPrintFormatters.SelectedItems[0].Tag, 
                PersistHint.AcrossSessions);
            this.Close();
        }

        private void listviewPrintFormatters_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            toolTip.SetToolTip(listviewPrintFormatters, e.Item.ToolTipText);
        }

        private void listviewPrintFormatters_DoubleClick(object sender, EventArgs e)
        {
            AppContext.Current.SettingsProvider.Set(
                "CurrentPrintFormatter",
                listviewPrintFormatters.SelectedItems[0].Tag,
                PersistHint.AcrossSessions);
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}