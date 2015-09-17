namespace BritishMicro.TaskClerk.InstantAccess
{
    partial class AdvancedSelectDialog
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
            if(disposing && (components != null))
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
            this._buttonOK = new System.Windows.Forms.Button();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._groupBoxCrossTab = new System.Windows.Forms.GroupBox();
            this._treeViewTaskDescriptions = new System.Windows.Forms.TreeView();
            this._labelStartTimeCaption = new System.Windows.Forms.Label();
            this._dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this._groupBoxSelectedTask = new System.Windows.Forms.GroupBox();
            this._textBoxSelectedTask = new System.Windows.Forms.TextBox();
            this._groupBoxCrossTab.SuspendLayout();
            this._groupBoxSelectedTask.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonOK
            // 
            this._buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._buttonOK.Location = new System.Drawing.Point(180, 430);
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(75, 23);
            this._buttonOK.TabIndex = 2;
            this._buttonOK.Text = "&OK";
            this._buttonOK.UseVisualStyleBackColor = true;
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._buttonCancel.Location = new System.Drawing.Point(261, 430);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 3;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _groupBoxCrossTab
            // 
            this._groupBoxCrossTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._groupBoxCrossTab.Controls.Add(this._treeViewTaskDescriptions);
            this._groupBoxCrossTab.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._groupBoxCrossTab.Location = new System.Drawing.Point(12, 69);
            this._groupBoxCrossTab.Name = "_groupBoxCrossTab";
            this._groupBoxCrossTab.Size = new System.Drawing.Size(324, 355);
            this._groupBoxCrossTab.TabIndex = 0;
            this._groupBoxCrossTab.TabStop = false;
            this._groupBoxCrossTab.Text = "&Cross Tab Task Description";
            // 
            // _treeViewTaskDescriptions
            // 
            this._treeViewTaskDescriptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeViewTaskDescriptions.HideSelection = false;
            this._treeViewTaskDescriptions.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this._treeViewTaskDescriptions.Location = new System.Drawing.Point(3, 16);
            this._treeViewTaskDescriptions.Name = "_treeViewTaskDescriptions";
            this._treeViewTaskDescriptions.ShowPlusMinus = false;
            this._treeViewTaskDescriptions.ShowRootLines = false;
            this._treeViewTaskDescriptions.Size = new System.Drawing.Size(318, 336);
            this._treeViewTaskDescriptions.TabIndex = 0;
            this._treeViewTaskDescriptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._treeViewTaskDescriptions_AfterSelect);
            // 
            // _labelStartTimeCaption
            // 
            this._labelStartTimeCaption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._labelStartTimeCaption.AutoSize = true;
            this._labelStartTimeCaption.Location = new System.Drawing.Point(19, 435);
            this._labelStartTimeCaption.Name = "_labelStartTimeCaption";
            this._labelStartTimeCaption.Size = new System.Drawing.Size(58, 13);
            this._labelStartTimeCaption.TabIndex = 4;
            this._labelStartTimeCaption.Text = "&Start Time:";
            this._labelStartTimeCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _dateTimePickerStartTime
            // 
            this._dateTimePickerStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._dateTimePickerStartTime.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this._dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this._dateTimePickerStartTime.Location = new System.Drawing.Point(78, 431);
            this._dateTimePickerStartTime.Name = "_dateTimePickerStartTime";
            this._dateTimePickerStartTime.ShowUpDown = true;
            this._dateTimePickerStartTime.Size = new System.Drawing.Size(87, 20);
            this._dateTimePickerStartTime.TabIndex = 1;
            this._dateTimePickerStartTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this._dateTimePickerStartTime_MouseDown);
            this._dateTimePickerStartTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this._dateTimePickerStartTime_MouseUp);
            this._dateTimePickerStartTime.MouseMove += new System.Windows.Forms.MouseEventHandler(this._dateTimePickerStartTime_MouseMove);
            // 
            // _groupBoxSelectedTask
            // 
            this._groupBoxSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._groupBoxSelectedTask.Controls.Add(this._textBoxSelectedTask);
            this._groupBoxSelectedTask.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._groupBoxSelectedTask.Location = new System.Drawing.Point(15, 13);
            this._groupBoxSelectedTask.Name = "_groupBoxSelectedTask";
            this._groupBoxSelectedTask.Size = new System.Drawing.Size(321, 50);
            this._groupBoxSelectedTask.TabIndex = 6;
            this._groupBoxSelectedTask.TabStop = false;
            this._groupBoxSelectedTask.Text = "Selected Task Description";
            // 
            // _textBoxSelectedTask
            // 
            this._textBoxSelectedTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxSelectedTask.Location = new System.Drawing.Point(7, 20);
            this._textBoxSelectedTask.Name = "_textBoxSelectedTask";
            this._textBoxSelectedTask.ReadOnly = true;
            this._textBoxSelectedTask.Size = new System.Drawing.Size(308, 20);
            this._textBoxSelectedTask.TabIndex = 0;
            this._textBoxSelectedTask.TabStop = false;
            // 
            // AdvancedSelectDialog
            // 
            this.AcceptButton = this._buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(348, 463);
            this.Controls.Add(this._groupBoxSelectedTask);
            this.Controls.Add(this._dateTimePickerStartTime);
            this.Controls.Add(this._labelStartTimeCaption);
            this.Controls.Add(this._groupBoxCrossTab);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedSelectDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Extended Task Select";
            this._groupBoxCrossTab.ResumeLayout(false);
            this._groupBoxSelectedTask.ResumeLayout(false);
            this._groupBoxSelectedTask.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonOK;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.GroupBox _groupBoxCrossTab;
        private System.Windows.Forms.TreeView _treeViewTaskDescriptions;
        private System.Windows.Forms.Label _labelStartTimeCaption;
        private System.Windows.Forms.DateTimePicker _dateTimePickerStartTime;
        private System.Windows.Forms.GroupBox _groupBoxSelectedTask;
        private System.Windows.Forms.TextBox _textBoxSelectedTask;
    }
}