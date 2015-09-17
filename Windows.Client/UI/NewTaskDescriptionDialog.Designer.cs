namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// Allows the user to create a new TaskDesctiption with the most basic infroamtion.
    /// </summary>
    partial class NewTaskDescriptionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTaskDescriptionDialog));
            this.panelContentArea = new System.Windows.Forms.Panel();
            this.maskedTextBoxNoNagMins = new System.Windows.Forms.MaskedTextBox();
            this.buttonColorPicker = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxColour = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.panelContentArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContentArea
            // 
            this.panelContentArea.Controls.Add(this.maskedTextBoxNoNagMins);
            this.panelContentArea.Controls.Add(this.buttonColorPicker);
            this.panelContentArea.Controls.Add(this.pictureBox1);
            this.panelContentArea.Controls.Add(this.label2);
            this.panelContentArea.Controls.Add(this.buttonCancel);
            this.panelContentArea.Controls.Add(this.buttonOk);
            this.panelContentArea.Controls.Add(this.label5);
            this.panelContentArea.Controls.Add(this.textBoxColour);
            this.panelContentArea.Controls.Add(this.label4);
            this.panelContentArea.Controls.Add(this.textBoxName);
            this.panelContentArea.Controls.Add(this.label3);
            this.panelContentArea.Controls.Add(this.label1);
            resources.ApplyResources(this.panelContentArea, "panelContentArea");
            this.panelContentArea.Name = "panelContentArea";
            // 
            // maskedTextBoxNoNagMins
            // 
            resources.ApplyResources(this.maskedTextBoxNoNagMins, "maskedTextBoxNoNagMins");
            this.maskedTextBoxNoNagMins.Name = "maskedTextBoxNoNagMins";
            this.maskedTextBoxNoNagMins.ValidatingType = typeof(int);
            // 
            // buttonColorPicker
            // 
            this.buttonColorPicker.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.buttonColorPicker, "buttonColorPicker");
            this.buttonColorPicker.Name = "buttonColorPicker";
            this.buttonColorPicker.UseVisualStyleBackColor = false;
            this.buttonColorPicker.Click += new System.EventHandler(this.buttonColorPicker_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BritishMicro.TaskClerk.Properties.Resources.Setup;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxColour
            // 
            resources.ApplyResources(this.textBoxColour, "textBoxColour");
            this.textBoxColour.Name = "textBoxColour";
            this.textBoxColour.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Name = "label1";
            // 
            // NewTaskDescriptionDialog
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panelContentArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTaskDescriptionDialog";
            this.panelContentArea.ResumeLayout(false);
            this.panelContentArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContentArea;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxColour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonColorPicker;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxNoNagMins;
    }
}