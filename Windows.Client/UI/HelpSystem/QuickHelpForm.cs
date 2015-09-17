using System;
using System.Drawing;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class QuickHelpForm : Form
    {
        public QuickHelpForm(string url)
        {
            InitializeComponent();
            Size = new Size(850, 650);
            webBrowser.Url = new Uri(url);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;
            Cursor.Current = Cursors.Default;
            Text = "Quick Help.";
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Text = "Pelase wait, contacting : " + e.Url.Host;
            Cursor.Current = Cursors.WaitCursor;
        }
    }
}