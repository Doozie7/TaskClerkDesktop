namespace BritishMicro.TaskClerk.UI
{
    partial class PrintSetupDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintSetupDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxSelectedExplorerDate = new System.Windows.Forms.TextBox();
            this.radioButtonSelectedExplorerDate = new System.Windows.Forms.RadioButton();
            this.radioButtonToday = new System.Windows.Forms.RadioButton();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.datetimepickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.datetimepickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.datetimepickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.datetimepickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.treeviewTaskDescriptions = new System.Windows.Forms.TreeView();
            this.comboboxScheme = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textboxPrintFormatDescription = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboboxPrintFormat = new System.Windows.Forms.ComboBox();
            this.buttonPageSetup = new System.Windows.Forms.Button();
            this.pagesetupdialog = new System.Windows.Forms.PageSetupDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.textBoxSelectedExplorerDate);
            this.groupBox1.Controls.Add(this.radioButtonSelectedExplorerDate);
            this.groupBox1.Controls.Add(this.radioButtonToday);
            this.groupBox1.Controls.Add(this.radioButtonCustom);
            this.groupBox1.Controls.Add(this.datetimepickerEndTime);
            this.groupBox1.Controls.Add(this.datetimepickerStartTime);
            this.groupBox1.Controls.Add(this.datetimepickerEndDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.datetimepickerStartDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // textBoxSelectedExplorerDate
            // 
            resources.ApplyResources(this.textBoxSelectedExplorerDate, "textBoxSelectedExplorerDate");
            this.textBoxSelectedExplorerDate.Name = "textBoxSelectedExplorerDate";
            this.textBoxSelectedExplorerDate.ReadOnly = true;
            // 
            // radioButtonSelectedExplorerDate
            // 
            resources.ApplyResources(this.radioButtonSelectedExplorerDate, "radioButtonSelectedExplorerDate");
            this.radioButtonSelectedExplorerDate.Name = "radioButtonSelectedExplorerDate";
            this.radioButtonSelectedExplorerDate.UseVisualStyleBackColor = true;
            this.radioButtonSelectedExplorerDate.CheckedChanged += new System.EventHandler(this.radioButtonSelectedExplorerDate_CheckedChanged);
            // 
            // radioButtonToday
            // 
            resources.ApplyResources(this.radioButtonToday, "radioButtonToday");
            this.radioButtonToday.Name = "radioButtonToday";
            this.radioButtonToday.UseVisualStyleBackColor = true;
            this.radioButtonToday.CheckedChanged += new System.EventHandler(this.radioButtonToday_CheckedChanged);
            // 
            // radioButtonCustom
            // 
            resources.ApplyResources(this.radioButtonCustom, "radioButtonCustom");
            this.radioButtonCustom.Checked = true;
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.TabStop = true;
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            // 
            // datetimepickerEndTime
            // 
            resources.ApplyResources(this.datetimepickerEndTime, "datetimepickerEndTime");
            this.datetimepickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.datetimepickerEndTime.Name = "datetimepickerEndTime";
            this.datetimepickerEndTime.ShowUpDown = true;
            this.datetimepickerEndTime.CloseUp += new System.EventHandler(this.datetimepicker_Changed);
            // 
            // datetimepickerStartTime
            // 
            resources.ApplyResources(this.datetimepickerStartTime, "datetimepickerStartTime");
            this.datetimepickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.datetimepickerStartTime.Name = "datetimepickerStartTime";
            this.datetimepickerStartTime.ShowUpDown = true;
            this.datetimepickerStartTime.CloseUp += new System.EventHandler(this.datetimepicker_Changed);
            // 
            // datetimepickerEndDate
            // 
            resources.ApplyResources(this.datetimepickerEndDate, "datetimepickerEndDate");
            this.datetimepickerEndDate.Name = "datetimepickerEndDate";
            this.datetimepickerEndDate.CloseUp += new System.EventHandler(this.datetimepicker_Changed);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // datetimepickerStartDate
            // 
            resources.ApplyResources(this.datetimepickerStartDate, "datetimepickerStartDate");
            this.datetimepickerStartDate.Name = "datetimepickerStartDate";
            this.datetimepickerStartDate.CloseUp += new System.EventHandler(this.datetimepicker_Changed);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.treeviewTaskDescriptions);
            this.groupBox2.Controls.Add(this.comboboxScheme);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // treeviewTaskDescriptions
            // 
            resources.ApplyResources(this.treeviewTaskDescriptions, "treeviewTaskDescriptions");
            this.treeviewTaskDescriptions.CheckBoxes = true;
            this.treeviewTaskDescriptions.FullRowSelect = true;
            this.treeviewTaskDescriptions.Name = "treeviewTaskDescriptions";
            this.treeviewTaskDescriptions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TaskDescriptionNode_Checked);
            // 
            // comboboxScheme
            // 
            resources.ApplyResources(this.comboboxScheme, "comboboxScheme");
            this.comboboxScheme.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboboxScheme.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboboxScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxScheme.FormattingEnabled = true;
            this.comboboxScheme.Items.AddRange(new object[] {
            resources.GetString("comboboxScheme.Items"),
            resources.GetString("comboboxScheme.Items1"),
            resources.GetString("comboboxScheme.Items2")});
            this.comboboxScheme.Name = "comboboxScheme";
            this.comboboxScheme.SelectedIndexChanged += new System.EventHandler(this.comboboxScheme_DropDownClosed);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.textboxPrintFormatDescription);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.comboboxPrintFormat);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // textboxPrintFormatDescription
            // 
            resources.ApplyResources(this.textboxPrintFormatDescription, "textboxPrintFormatDescription");
            this.textboxPrintFormatDescription.BackColor = System.Drawing.SystemColors.Info;
            this.textboxPrintFormatDescription.Name = "textboxPrintFormatDescription";
            this.textboxPrintFormatDescription.ReadOnly = true;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comboboxPrintFormat
            // 
            resources.ApplyResources(this.comboboxPrintFormat, "comboboxPrintFormat");
            this.comboboxPrintFormat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboboxPrintFormat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboboxPrintFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxPrintFormat.FormattingEnabled = true;
            this.comboboxPrintFormat.Name = "comboboxPrintFormat";
            this.comboboxPrintFormat.SelectedIndexChanged += new System.EventHandler(this.comboboxPrintFormat_SelectedIndexChanged);
            // 
            // buttonPageSetup
            // 
            resources.ApplyResources(this.buttonPageSetup, "buttonPageSetup");
            this.buttonPageSetup.Name = "buttonPageSetup";
            this.buttonPageSetup.UseVisualStyleBackColor = true;
            this.buttonPageSetup.Click += new System.EventHandler(this.buttonPageSetup_Click);
            // 
            // PrintSetupDialog
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.buttonPageSetup);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintSetupDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker datetimepickerStartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datetimepickerEndTime;
        private System.Windows.Forms.DateTimePicker datetimepickerStartTime;
        private System.Windows.Forms.DateTimePicker datetimepickerEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboboxScheme;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeviewTaskDescriptions;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboboxPrintFormat;
        private System.Windows.Forms.Button buttonPageSetup;
        private System.Windows.Forms.PageSetupDialog pagesetupdialog;
        private System.Windows.Forms.RadioButton radioButtonToday;
        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textboxPrintFormatDescription;
        private System.Windows.Forms.RadioButton radioButtonSelectedExplorerDate;
        private System.Windows.Forms.TextBox textBoxSelectedExplorerDate;
    }
}