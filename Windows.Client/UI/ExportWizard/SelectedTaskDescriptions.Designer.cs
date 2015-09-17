namespace BritishMicro.TaskClerk.UI
{
    partial class SelectedTaskDescriptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectedTaskDescriptions));
            this.treeviewTaskDescriptions = new System.Windows.Forms.TreeView();
            this.labelCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeviewTaskDescriptions
            // 
            resources.ApplyResources(this.treeviewTaskDescriptions, "treeviewTaskDescriptions");
            this.treeviewTaskDescriptions.CheckBoxes = true;
            this.treeviewTaskDescriptions.FullRowSelect = true;
            this.treeviewTaskDescriptions.HideSelection = false;
            this.treeviewTaskDescriptions.Name = "treeviewTaskDescriptions";
            this.treeviewTaskDescriptions.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.UpdateExportList);
            // 
            // labelCount
            // 
            resources.ApplyResources(this.labelCount, "labelCount");
            this.labelCount.Name = "labelCount";
            // 
            // SelectedTaskDescriptions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.treeviewTaskDescriptions);
            this.Description = "Select the TaskDescriptions to be exported.";
            this.Name = "SelectedTaskDescriptions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeviewTaskDescriptions;
        private System.Windows.Forms.Label labelCount;
    }
}
