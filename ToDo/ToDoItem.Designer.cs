namespace BritishMicro.TaskClerk.ToDoPlugin
{
    partial class ToDoItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToDoItem));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.dateTimePickerActionDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerActionTime = new System.Windows.Forms.DateTimePicker();
            this.buttonTaskDescription = new System.Windows.Forms.Button();
            this.textBoxTaskDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxActionType = new System.Windows.Forms.ComboBox();
            this.checkBoxPopupAlarm = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownProgress = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Enter += new System.EventHandler(this.textBoxDescription_Enter);
            this.textBoxDescription.Leave += new System.EventHandler(this.textBoxDescription_Leave);
            this.textBoxDescription.TextChanged += new System.EventHandler(this.textBoxDescription_TextChanged);
            // 
            // dateTimePickerActionDate
            // 
            resources.ApplyResources(this.dateTimePickerActionDate, "dateTimePickerActionDate");
            this.dateTimePickerActionDate.Name = "dateTimePickerActionDate";
            this.dateTimePickerActionDate.ValueChanged += new System.EventHandler(this.dateTimePickerActionDate_ValueChanged);
            // 
            // dateTimePickerActionTime
            // 
            this.dateTimePickerActionTime.Cursor = System.Windows.Forms.Cursors.SizeNS;
            resources.ApplyResources(this.dateTimePickerActionTime, "dateTimePickerActionTime");
            this.dateTimePickerActionTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerActionTime.Name = "dateTimePickerActionTime";
            this.dateTimePickerActionTime.ShowUpDown = true;
            this.dateTimePickerActionTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dateTimePickerActionTime_MouseDown);
            this.dateTimePickerActionTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dateTimePickerActionTime_MouseUp);
            this.dateTimePickerActionTime.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dateTimePickerActionTime_MouseMove);
            // 
            // buttonTaskDescription
            // 
            resources.ApplyResources(this.buttonTaskDescription, "buttonTaskDescription");
            this.buttonTaskDescription.Name = "buttonTaskDescription";
            this.buttonTaskDescription.UseVisualStyleBackColor = true;
            this.buttonTaskDescription.Click += new System.EventHandler(this.buttonTaskDescription_Click);
            // 
            // textBoxTaskDescription
            // 
            resources.ApplyResources(this.textBoxTaskDescription, "textBoxTaskDescription");
            this.textBoxTaskDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxTaskDescription.Name = "textBoxTaskDescription";
            this.textBoxTaskDescription.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // comboBoxActionType
            // 
            resources.ApplyResources(this.comboBoxActionType, "comboBoxActionType");
            this.comboBoxActionType.FormattingEnabled = true;
            this.comboBoxActionType.Items.AddRange(new object[] {
            resources.GetString("comboBoxActionType.Items"),
            resources.GetString("comboBoxActionType.Items1"),
            resources.GetString("comboBoxActionType.Items2"),
            resources.GetString("comboBoxActionType.Items3"),
            resources.GetString("comboBoxActionType.Items4"),
            resources.GetString("comboBoxActionType.Items5")});
            this.comboBoxActionType.Name = "comboBoxActionType";
            this.comboBoxActionType.SelectedIndexChanged += new System.EventHandler(this.comboBoxActionType_SelectedIndexChanged);
            // 
            // checkBoxPopupAlarm
            // 
            resources.ApplyResources(this.checkBoxPopupAlarm, "checkBoxPopupAlarm");
            this.checkBoxPopupAlarm.Name = "checkBoxPopupAlarm";
            this.checkBoxPopupAlarm.UseVisualStyleBackColor = true;
            this.checkBoxPopupAlarm.CheckedChanged += new System.EventHandler(this.checkBoxPopupAlarm_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // numericUpDownPriority
            // 
            resources.ApplyResources(this.numericUpDownPriority, "numericUpDownPriority");
            this.numericUpDownPriority.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.MouseDown += new System.Windows.Forms.MouseEventHandler(this.numericUpDownPriority_MouseDown);
            this.numericUpDownPriority.MouseUp += new System.Windows.Forms.MouseEventHandler(this.numericUpDownPriority_MouseUp);
            this.numericUpDownPriority.MouseMove += new System.Windows.Forms.MouseEventHandler(this.numericUpDownPriority_MouseMove);
            // 
            // numericUpDownProgress
            // 
            resources.ApplyResources(this.numericUpDownProgress, "numericUpDownProgress");
            this.numericUpDownProgress.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.numericUpDownProgress.Name = "numericUpDownProgress";
            this.numericUpDownProgress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.numericUpDownProgress_MouseDown);
            this.numericUpDownProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.numericUpDownProgress_MouseUp);
            this.numericUpDownProgress.MouseMove += new System.Windows.Forms.MouseEventHandler(this.numericUpDownProgress_MouseMove);
            // 
            // ToDoItem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownProgress);
            this.Controls.Add(this.numericUpDownPriority);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxPopupAlarm);
            this.Controls.Add(this.comboBoxActionType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonTaskDescription);
            this.Controls.Add(this.textBoxTaskDescription);
            this.Controls.Add(this.dateTimePickerActionTime);
            this.Controls.Add(this.dateTimePickerActionDate);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.label1);
            this.Name = "ToDoItem";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.DateTimePicker dateTimePickerActionDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerActionTime;
        private System.Windows.Forms.Button buttonTaskDescription;
        private System.Windows.Forms.TextBox textBoxTaskDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxActionType;
        private System.Windows.Forms.CheckBox checkBoxPopupAlarm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        private System.Windows.Forms.NumericUpDown numericUpDownProgress;
    }
}
