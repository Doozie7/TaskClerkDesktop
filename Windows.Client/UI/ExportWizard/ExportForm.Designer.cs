using BritishMicro.Windows;
namespace BritishMicro.TaskClerk.UI
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            this.wizardHost = new BritishMicro.Windows.WizardHost();
            this.SuspendLayout();
            // 
            // wizardHost
            // 
            resources.ApplyResources(this.wizardHost, "wizardHost");
            this.wizardHost.CurrentPage = 0;
            this.wizardHost.HeadingImage = null;
            this.wizardHost.HeadingText = "";
            this.wizardHost.Name = "wizardHost";
            this.wizardHost.WizardHostActionEvent += new System.EventHandler<BritishMicro.Windows.WizardEventArgs>(this.wizardHost_WizardHostActionEvent);
            // 
            // ExportForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardHost);
            this.Name = "ExportForm";
            this.Load += new System.EventHandler(this.ExportForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WizardHost wizardHost;

    }
}