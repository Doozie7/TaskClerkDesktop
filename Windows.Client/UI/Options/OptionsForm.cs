using BritishMicro.TaskClerk.Plugins;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class OptionsForm : Form
    {
        private readonly List<PluginOptionControl> _optionControls;

        public OptionsForm()
        {
            InitializeComponent();
            _optionControls = new List<PluginOptionControl>();
            foreach (LoadableItem li in AppContext.Current.PluginsProvider.Plugins)
            {
                if (li.IsSubclassOf(typeof(PluginOptionControl)))
                {
                    Type type = li.ReturnType();
                    PluginOptionControl poc = (PluginOptionControl)li.CreateInstance();
                    poc.TaskClerkInit(AppContext.Current, this);
                    _optionControls.Add(poc);
                }
            }
            _optionControls.Sort(PluginOptionControl.CompareOnDisplayOrder);
            RefreshView();
            listViewSelector.ItemSelectionChanged
                += new ListViewItemSelectionChangedEventHandler
                    (listViewSelector_ItemSelectionChanged);
        }

        private void checkBoxShowAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        private void RefreshView()
        {
            imageList.Images.Clear();
            listViewSelector.Items.Clear();
            foreach (PluginOptionControl obc in _optionControls)
            {
                if (obc.SelectorImage != null)
                {
                    imageList.Images.Add(obc.Name, obc.SelectorImage);
                }
                else
                {
                    imageList.Images.Add(obc.Name, obc.DefaultImage);
                }

                string displayName = string.Empty;
                if (obc.DisplayName != null)
                {
                    displayName = obc.DisplayName;
                }
                else
                {
                    displayName = obc.GetType().Name;
                }

                ListViewItem lvi = new ListViewItem(displayName)
                {
                    Tag = obc,
                    ImageKey = obc.Name
                };
                if (obc.IsAdvanced == false)
                {
                    listViewSelector.Items.Add(lvi);
                }
                else
                {
                    if (checkBoxShowAdvanced.Checked)
                    {
                        listViewSelector.Items.Add(lvi);
                    }
                }
            }
            if (listViewSelector.Items.Count > 0)
            {
                listViewSelector.Items[0].Selected = true;
            }
        }

        private void listViewSelector_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item.Tag is PluginOptionControl obc)
            {
                if (e.IsSelected == true)
                {
                    obc.Router("OnTaskClerkShow");
                    splitContainer.Panel2.Controls.Clear();
                    splitContainer.Panel2.Controls.Add(obc);
                    splitContainer.Panel2.Controls[0].Dock = DockStyle.Fill;
                }
                else
                {
                    obc.Router("OnTaskClerkHide");
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            foreach (PluginOptionControl obc in _optionControls)
            {
                obc.Router("OnTaskClerkSaveState");
            }
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            foreach (PluginOptionControl obc in _optionControls)
            {
                obc.Router("OnTaskClerkDumpState");
            }
            Close();
        }
    }
}