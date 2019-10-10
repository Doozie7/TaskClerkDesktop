namespace BritishMicro.TaskClerk.UI
{
    partial class ActivityPanel
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivityPanel));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelRight = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.Transparent;
            this.panelRight.Cursor = System.Windows.Forms.Cursors.VSplit;
            resources.ApplyResources(this.panelRight, "panelRight");
            this.panelRight.Name = "panelRight";
            this.panelRight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelRight_MouseMove);
            this.panelRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.panelRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseUp);
            // 
            // ActivityPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelRight);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "ActivityPanel";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelLeft_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.MouseHover += new System.EventHandler(this.ActivityPanel_MouseHover);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panelRight;
    }
}
