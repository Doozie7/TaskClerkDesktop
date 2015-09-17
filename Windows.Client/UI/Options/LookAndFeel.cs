using System;
using System.Drawing;
using System.Windows.Forms;
using BritishMicro.TaskClerk.Plugins;
using System.ComponentModel;

namespace BritishMicro.TaskClerk.UI.Options
{
    /// <summary>
    /// The look and feel options dialog provides the user with the opertunity to 
    /// change basic look and feel.
    /// </summary>
    [Description("Provides look and feel configuration options for the TaskClerk windows client.")]
    public partial class LookAndFeel : PluginOptionControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookAndFeel"/> class.
        /// </summary>
        public LookAndFeel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is called after the engine has initalised the component
        /// </summary>
        protected override void OnTaskClerkInit()
        {
            base.OnTaskClerkInit();

            Font font = new Font("Arial", 9);
            font = (Font) Engine.SettingsProvider.Get("GeneralFont", font);
            textBox1.Text = font.ToString();

            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                bool set = (bool) Engine.SettingsProvider.Get(checkedListBox.Items[i], true);
                checkedListBox.SetItemChecked(i, set);
            }

            checkedListBox.ItemCheck +=
                new ItemCheckEventHandler(checkedListBox_ItemCheck);
        }

        private void checkedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Engine.SettingsProvider.Set(
                checkedListBox.Items[e.Index],
                (e.NewValue == CheckState.Unchecked ? false : true),
                PersistHint.AcrossSessions);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fontDialog.Font = (Font) Engine.SettingsProvider.Get("GeneralFont", Font);
            fontDialog.ShowDialog(this);
            textBox1.Text = fontDialog.Font.ToString();
            Engine.SettingsProvider.Set("GeneralFont", fontDialog.Font, PersistHint.AcrossSessions);
        }
    }
}