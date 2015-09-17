namespace BritishMicro.TaskClerk.UI
{
    partial class VisualSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualSummary));
            this.panel1 = new System.Windows.Forms.Panel();
            this.totalHoursTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.totalMinutesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.totalHoursTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.totalMinutesTextBox);
            this.panel1.Controls.Add(this.label1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // totalHoursTextBox
            // 
            resources.ApplyResources(this.totalHoursTextBox, "totalHoursTextBox");
            this.totalHoursTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.totalHoursTextBox.Name = "totalHoursTextBox";
            this.totalHoursTextBox.ReadOnly = true;
            this.totalHoursTextBox.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // totalMinutesTextBox
            // 
            resources.ApplyResources(this.totalMinutesTextBox, "totalMinutesTextBox");
            this.totalMinutesTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.totalMinutesTextBox.Name = "totalMinutesTextBox";
            this.totalMinutesTextBox.ReadOnly = true;
            this.totalMinutesTextBox.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // VisualSummary
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "VisualSummary";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox totalMinutesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox totalHoursTextBox;
    }
}
