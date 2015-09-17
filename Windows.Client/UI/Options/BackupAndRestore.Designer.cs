namespace BritishMicro.TaskClerk.UI
{
    partial class BackupAndRestore
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupAndRestore));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelLastBackup = new System.Windows.Forms.Label();
            this.labelLastBackupTitle = new System.Windows.Forms.Label();
            this.buttonBackupNow = new System.Windows.Forms.Button();
            this.comboBoxBackupFrequency = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBackupDirectory = new System.Windows.Forms.Button();
            this.textBoxBackupDirectory = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.textBoxRestoreDirectory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelLastBackup);
            this.groupBox1.Controls.Add(this.labelLastBackupTitle);
            this.groupBox1.Controls.Add(this.buttonBackupNow);
            this.groupBox1.Controls.Add(this.comboBoxBackupFrequency);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonBackupDirectory);
            this.groupBox1.Controls.Add(this.textBoxBackupDirectory);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelLastBackup
            // 
            resources.ApplyResources(this.labelLastBackup, "labelLastBackup");
            this.labelLastBackup.Name = "labelLastBackup";
            // 
            // labelLastBackupTitle
            // 
            resources.ApplyResources(this.labelLastBackupTitle, "labelLastBackupTitle");
            this.labelLastBackupTitle.Name = "labelLastBackupTitle";
            // 
            // buttonBackupNow
            // 
            resources.ApplyResources(this.buttonBackupNow, "buttonBackupNow");
            this.buttonBackupNow.Name = "buttonBackupNow";
            this.buttonBackupNow.UseVisualStyleBackColor = true;
            this.buttonBackupNow.Click += new System.EventHandler(this.buttonBackupNow_Click);
            // 
            // comboBoxBackupFrequency
            // 
            this.comboBoxBackupFrequency.FormattingEnabled = true;
            this.comboBoxBackupFrequency.Items.AddRange(new object[] {
            resources.GetString("comboBoxBackupFrequency.Items"),
            resources.GetString("comboBoxBackupFrequency.Items1"),
            resources.GetString("comboBoxBackupFrequency.Items2"),
            resources.GetString("comboBoxBackupFrequency.Items3")});
            resources.ApplyResources(this.comboBoxBackupFrequency, "comboBoxBackupFrequency");
            this.comboBoxBackupFrequency.Name = "comboBoxBackupFrequency";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonBackupDirectory
            // 
            resources.ApplyResources(this.buttonBackupDirectory, "buttonBackupDirectory");
            this.buttonBackupDirectory.Name = "buttonBackupDirectory";
            this.buttonBackupDirectory.UseVisualStyleBackColor = true;
            this.buttonBackupDirectory.Click += new System.EventHandler(this.buttonBackupDirectory_Click);
            // 
            // textBoxBackupDirectory
            // 
            resources.ApplyResources(this.textBoxBackupDirectory, "textBoxBackupDirectory");
            this.textBoxBackupDirectory.Name = "textBoxBackupDirectory";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.textBoxRestoreDirectory);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button3);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.buttonRestoreDirectory_Click);
            // 
            // textBoxRestoreDirectory
            // 
            resources.ApplyResources(this.textBoxRestoreDirectory, "textBoxRestoreDirectory");
            this.textBoxRestoreDirectory.Name = "textBoxRestoreDirectory";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.Personal;
            // 
            // BackupAndRestore
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DisplayName = "Backup and Restore";
            this.DisplayOrder = 2;
            this.Name = "BackupAndRestore";
            this.SelectorImage = ((System.Drawing.Image)(resources.GetObject("$this.SelectorImage")));
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxBackupFrequency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBackupDirectory;
        private System.Windows.Forms.TextBox textBoxBackupDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBackupNow;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBoxRestoreDirectory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label labelLastBackup;
        private System.Windows.Forms.Label labelLastBackupTitle;
    }
}
