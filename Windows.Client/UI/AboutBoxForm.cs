using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using System.Globalization;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class AboutBoxForm : Form
    {
        public AboutBoxForm()
        {
            InitializeComponent();
        }

        public static string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public static string AssemblyProduct
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        private void AboutBoxForm_Load(object sender, EventArgs e)
        {
            this.labelUINameAndVersion.Text = string.Format(CultureInfo.InvariantCulture, "{0} Version {1}\n{2}\nAll rights reserved.", 
                AssemblyProduct, AssemblyVersion, AssemblyCopyright);
            listBoxInstalledComponents.Items.AddRange(AppContext.Current.ListProviders());
            listBoxInstalledComponents.Items.AddRange(AppContext.Current.PluginsProvider.ListPlugins());
            using (MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(Properties.Resources.LicenceRes)))
            {
                this.rtfLicense.LoadFile(ms, RichTextBoxStreamType.RichText);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBoxInstalledComponents_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(listBoxInstalledComponents.SelectedItem.ToString(), "Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}