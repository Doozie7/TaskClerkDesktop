namespace BritishMicro.TaskClerk.UI
{
    partial class QuickHelpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickHelpForm));
            this.commandAreaPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.horizontalLine = new BritishMicro.Windows.HorizontalLine();
            this.panel = new System.Windows.Forms.Panel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.commandAreaPanel.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandAreaPanel
            // 
            this.commandAreaPanel.Controls.Add(this.label1);
            this.commandAreaPanel.Controls.Add(this.closeButton);
            this.commandAreaPanel.Controls.Add(this.horizontalLine);
            resources.ApplyResources(this.commandAreaPanel, "commandAreaPanel");
            this.commandAreaPanel.Name = "commandAreaPanel";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Name = "label1";
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // horizontalLine
            // 
            resources.ApplyResources(this.horizontalLine, "horizontalLine");
            this.horizontalLine.Name = "horizontalLine";
            // 
            // panel
            // 
            this.panel.Controls.Add(this.webBrowser);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // webBrowser
            // 
            resources.ApplyResources(this.webBrowser, "webBrowser");
            this.webBrowser.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // QuickHelpForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.commandAreaPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickHelpForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.commandAreaPanel.ResumeLayout(false);
            this.commandAreaPanel.PerformLayout();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel commandAreaPanel;
        private System.Windows.Forms.Button closeButton;
        private BritishMicro.Windows.HorizontalLine horizontalLine;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Label label1;
    }
}