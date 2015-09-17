namespace BritishMicro.TaskClerk.UI
{
    partial class AboutBoxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBoxForm));
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelStatic1 = new System.Windows.Forms.Label();
            this.labelLicensedTo = new System.Windows.Forms.Label();
            this.labelStatic2 = new System.Windows.Forms.Label();
            this.listBoxInstalledComponents = new System.Windows.Forms.ListBox();
            this.rtfLicense = new System.Windows.Forms.RichTextBox();
            this.labelUINameAndVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // labelStatic1
            // 
            resources.ApplyResources(this.labelStatic1, "labelStatic1");
            this.labelStatic1.BackColor = System.Drawing.Color.Transparent;
            this.labelStatic1.ForeColor = System.Drawing.Color.Yellow;
            this.labelStatic1.Name = "labelStatic1";
            // 
            // labelLicensedTo
            // 
            resources.ApplyResources(this.labelLicensedTo, "labelLicensedTo");
            this.labelLicensedTo.BackColor = System.Drawing.Color.Transparent;
            this.labelLicensedTo.ForeColor = System.Drawing.Color.White;
            this.labelLicensedTo.Name = "labelLicensedTo";
            // 
            // labelStatic2
            // 
            resources.ApplyResources(this.labelStatic2, "labelStatic2");
            this.labelStatic2.Name = "labelStatic2";
            // 
            // listBoxInstalledComponents
            // 
            resources.ApplyResources(this.listBoxInstalledComponents, "listBoxInstalledComponents");
            this.listBoxInstalledComponents.FormattingEnabled = true;
            this.listBoxInstalledComponents.Name = "listBoxInstalledComponents";
            this.listBoxInstalledComponents.DoubleClick += new System.EventHandler(this.listBoxInstalledComponents_DoubleClick);
            // 
            // rtfLicense
            // 
            this.rtfLicense.BackColor = System.Drawing.Color.White;
            this.rtfLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtfLicense.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.rtfLicense, "rtfLicense");
            this.rtfLicense.Name = "rtfLicense";
            this.rtfLicense.ReadOnly = true;
            // 
            // labelUINameAndVersion
            // 
            resources.ApplyResources(this.labelUINameAndVersion, "labelUINameAndVersion");
            this.labelUINameAndVersion.Name = "labelUINameAndVersion";
            // 
            // AboutBoxForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.labelUINameAndVersion);
            this.Controls.Add(this.rtfLicense);
            this.Controls.Add(this.listBoxInstalledComponents);
            this.Controls.Add(this.labelStatic2);
            this.Controls.Add(this.labelLicensedTo);
            this.Controls.Add(this.labelStatic1);
            this.Controls.Add(this.buttonOk);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBoxForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.AboutBoxForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelStatic1;
        private System.Windows.Forms.Label labelLicensedTo;
        private System.Windows.Forms.Label labelStatic2;
        private System.Windows.Forms.ListBox listBoxInstalledComponents;
        private System.Windows.Forms.RichTextBox rtfLicense;
        private System.Windows.Forms.Label labelUINameAndVersion;

    }
}
