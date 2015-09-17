namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// 
    /// </summary>
    partial class OpenDataFolder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenDataFolder));
            // 
            // OpenDataFolder
            // 
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Text = "Open data folder...";
            this.Click += new System.EventHandler<System.EventArgs>(this.OpenDataFolder_Click);

        }

        #endregion
    }
}
