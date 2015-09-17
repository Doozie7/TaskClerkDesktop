namespace BritishMicro.TaskClerk.Windows
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.datetimepickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.datetimepickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.datetimepickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.datetimepickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelTaskDescriptionsCount = new System.Windows.Forms.Label();
            this.treeviewTaskDescriptions = new System.Windows.Forms.TreeView();
            this.comboboxScheme = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboboxPrintFormat = new System.Windows.Forms.ComboBox();
            this.textboxPrintFormatDescription = new System.Windows.Forms.TextBox();
            this.buttonPageSetup = new System.Windows.Forms.Button();
            this.pagesetupdialog = new System.Windows.Forms.PageSetupDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.datetimepickerEndTime);
            this.groupBox1.Controls.Add(this.datetimepickerStartTime);
            this.groupBox1.Controls.Add(this.datetimepickerEndDate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.datetimepickerStartDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date and time range";
            // 
            // datetimepickerEndTime
            // 
            this.datetimepickerEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datetimepickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.datetimepickerEndTime.Location = new System.Drawing.Point(247, 81);
            this.datetimepickerEndTime.Name = "datetimepickerEndTime";
            this.datetimepickerEndTime.ShowUpDown = true;
            this.datetimepickerEndTime.Size = new System.Drawing.Size(79, 20);
            this.datetimepickerEndTime.TabIndex = 5;
            // 
            // datetimepickerStartTime
            // 
            this.datetimepickerStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.datetimepickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.datetimepickerStartTime.Location = new System.Drawing.Point(247, 41);
            this.datetimepickerStartTime.Name = "datetimepickerStartTime";
            this.datetimepickerStartTime.ShowUpDown = true;
            this.datetimepickerStartTime.Size = new System.Drawing.Size(79, 20);
            this.datetimepickerStartTime.TabIndex = 2;
            // 
            // datetimepickerEndDate
            // 
            this.datetimepickerEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.datetimepickerEndDate.CustomFormat = "";
            this.datetimepickerEndDate.Location = new System.Drawing.Point(17, 81);
            this.datetimepickerEndDate.Name = "datetimepickerEndDate";
            this.datetimepickerEndDate.Size = new System.Drawing.Size(224, 20);
            this.datetimepickerEndDate.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "To";
            // 
            // datetimepickerStartDate
            // 
            this.datetimepickerStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.datetimepickerStartDate.CustomFormat = "";
            this.datetimepickerStartDate.Location = new System.Drawing.Point(17, 41);
            this.datetimepickerStartDate.Name = "datetimepickerStartDate";
            this.datetimepickerStartDate.Size = new System.Drawing.Size(224, 20);
            this.datetimepickerStartDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = " From";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelTaskDescriptionsCount);
            this.groupBox2.Controls.Add(this.treeviewTaskDescriptions);
            this.groupBox2.Controls.Add(this.comboboxScheme);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 230);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 200);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Task description filter";
            // 
            // labelTaskDescriptionsCount
            // 
            this.labelTaskDescriptionsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTaskDescriptionsCount.AutoSize = true;
            this.labelTaskDescriptionsCount.Location = new System.Drawing.Point(14, 177);
            this.labelTaskDescriptionsCount.Name = "labelTaskDescriptionsCount";
            this.labelTaskDescriptionsCount.Size = new System.Drawing.Size(263, 13);
            this.labelTaskDescriptionsCount.TabIndex = 5;
            this.labelTaskDescriptionsCount.Text = "An unknown number of TaskDescriptions are selected";
            // 
            // treeviewTaskDescriptions
            // 
            this.treeviewTaskDescriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeviewTaskDescriptions.CheckBoxes = true;
            this.treeviewTaskDescriptions.FullRowSelect = true;
            this.treeviewTaskDescriptions.Location = new System.Drawing.Point(17, 69);
            this.treeviewTaskDescriptions.Name = "treeviewTaskDescriptions";
            this.treeviewTaskDescriptions.Size = new System.Drawing.Size(309, 103);
            this.treeviewTaskDescriptions.TabIndex = 3;
            this.treeviewTaskDescriptions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TaskDescriptionNode_Checked);
            // 
            // comboboxScheme
            // 
            this.comboboxScheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboboxScheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxScheme.FormattingEnabled = true;
            this.comboboxScheme.Items.AddRange(new object[] {
            "No Private Descriptions",
            "Custom",
            "All"});
            this.comboboxScheme.Location = new System.Drawing.Point(17, 42);
            this.comboboxScheme.Name = "comboboxScheme";
            this.comboboxScheme.Size = new System.Drawing.Size(309, 21);
            this.comboboxScheme.TabIndex = 1;
            this.comboboxScheme.DropDownClosed += new System.EventHandler(this.comboboxScheme_DropDownClosed);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Scheme";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(369, 13);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(369, 71);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.textboxPrintFormatDescription);
            this.groupBox3.Controls.Add(this.comboboxPrintFormat);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 90);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Print Format";
            // 
            // comboboxPrintFormat
            // 
            this.comboboxPrintFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboboxPrintFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboboxPrintFormat.FormattingEnabled = true;
            this.comboboxPrintFormat.Location = new System.Drawing.Point(17, 19);
            this.comboboxPrintFormat.Name = "comboboxPrintFormat";
            this.comboboxPrintFormat.Size = new System.Drawing.Size(309, 21);
            this.comboboxPrintFormat.TabIndex = 0;
            // 
            // textboxPrintFormatDescription
            // 
            this.textboxPrintFormatDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxPrintFormatDescription.BackColor = System.Drawing.SystemColors.Control;
            this.textboxPrintFormatDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textboxPrintFormatDescription.Location = new System.Drawing.Point(17, 46);
            this.textboxPrintFormatDescription.Multiline = true;
            this.textboxPrintFormatDescription.Name = "textboxPrintFormatDescription";
            this.textboxPrintFormatDescription.ReadOnly = true;
            this.textboxPrintFormatDescription.Size = new System.Drawing.Size(309, 38);
            this.textboxPrintFormatDescription.TabIndex = 1;
            this.textboxPrintFormatDescription.Text = "No print format selected.\r\n\r\n";
            // 
            // buttonPageSetup
            // 
            this.buttonPageSetup.Location = new System.Drawing.Point(369, 42);
            this.buttonPageSetup.Name = "buttonPageSetup";
            this.buttonPageSetup.Size = new System.Drawing.Size(75, 23);
            this.buttonPageSetup.TabIndex = 5;
            this.buttonPageSetup.Text = "Page Setup";
            this.buttonPageSetup.UseVisualStyleBackColor = true;
            this.buttonPageSetup.Click += new System.EventHandler(this.buttonPageSetup_Click);
            // 
            // TaskActivityFilterDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(453, 442);
            this.Controls.Add(this.buttonPageSetup);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaskActivityFilterDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Print Setup";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Label labelTaskDescriptionsCount;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textboxPrintFormatDescription;
        private System.Windows.Forms.ComboBox comboboxPrintFormat;
        private System.Windows.Forms.Button buttonPageSetup;
        private System.Windows.Forms.PageSetupDialog pagesetupdialog;
    }
}