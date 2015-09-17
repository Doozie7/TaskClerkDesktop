namespace BritishMicro.TaskClerk.InstantAccess
{
    partial class InstantAccessDialog
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
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Pinned");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Most Recently Used");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Most Frequently Used");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Task Descriptions", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOK = new System.Windows.Forms.Button();
            this._groupBoxCurrentTask = new System.Windows.Forms.GroupBox();
            this._textBoxCurrentTask = new System.Windows.Forms.TextBox();
            this._groupBoxTaskDescriptions = new System.Windows.Forms.GroupBox();
            this._treeViewActions = new System.Windows.Forms.TreeView();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitTaskClerkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTaskActicityExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedSelectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.pinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._groupBoxCurrentTask.SuspendLayout();
            this._groupBoxTaskDescriptions.SuspendLayout();
            this._menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(242, 425);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 23);
            this._buttonCancel.TabIndex = 2;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _buttonOK
            // 
            this._buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOK.Location = new System.Drawing.Point(161, 425);
            this._buttonOK.Name = "_buttonOK";
            this._buttonOK.Size = new System.Drawing.Size(75, 23);
            this._buttonOK.TabIndex = 1;
            this._buttonOK.Text = "&OK";
            this._buttonOK.UseVisualStyleBackColor = true;
            this._buttonOK.Click += new System.EventHandler(this._buttonOK_Click);
            // 
            // _groupBoxCurrentTask
            // 
            this._groupBoxCurrentTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._groupBoxCurrentTask.Controls.Add(this._textBoxCurrentTask);
            this._groupBoxCurrentTask.Location = new System.Drawing.Point(13, 33);
            this._groupBoxCurrentTask.Name = "_groupBoxCurrentTask";
            this._groupBoxCurrentTask.Size = new System.Drawing.Size(304, 50);
            this._groupBoxCurrentTask.TabIndex = 3;
            this._groupBoxCurrentTask.TabStop = false;
            this._groupBoxCurrentTask.Text = "Current Task Activity";
            // 
            // _textBoxCurrentTask
            // 
            this._textBoxCurrentTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxCurrentTask.Location = new System.Drawing.Point(6, 19);
            this._textBoxCurrentTask.Name = "_textBoxCurrentTask";
            this._textBoxCurrentTask.ReadOnly = true;
            this._textBoxCurrentTask.Size = new System.Drawing.Size(292, 20);
            this._textBoxCurrentTask.TabIndex = 0;
            this._textBoxCurrentTask.TabStop = false;
            // 
            // _groupBoxTaskDescriptions
            // 
            this._groupBoxTaskDescriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._groupBoxTaskDescriptions.Controls.Add(this._treeViewActions);
            this._groupBoxTaskDescriptions.Location = new System.Drawing.Point(13, 90);
            this._groupBoxTaskDescriptions.Name = "_groupBoxTaskDescriptions";
            this._groupBoxTaskDescriptions.Size = new System.Drawing.Size(304, 329);
            this._groupBoxTaskDescriptions.TabIndex = 0;
            this._groupBoxTaskDescriptions.TabStop = false;
            this._groupBoxTaskDescriptions.Text = "&Select a Task Description";
            // 
            // _treeViewActions
            // 
            this._treeViewActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._treeViewActions.HideSelection = false;
            this._treeViewActions.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(180)))), ((int)(((byte)(209)))));
            this._treeViewActions.Location = new System.Drawing.Point(3, 16);
            this._treeViewActions.Name = "_treeViewActions";
            treeNode5.Name = "_nodePinned";
            treeNode5.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode5.Text = "Pinned";
            treeNode6.Name = "_nodeMru";
            treeNode6.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode6.Text = "Most Recently Used";
            treeNode7.Name = "_nodeMfu";
            treeNode7.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode7.Text = "Most Frequently Used";
            treeNode8.Name = "_nodeRoot";
            treeNode8.Text = "Task Descriptions";
            this._treeViewActions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode8});
            this._treeViewActions.ShowNodeToolTips = true;
            this._treeViewActions.ShowPlusMinus = false;
            this._treeViewActions.ShowRootLines = false;
            this._treeViewActions.Size = new System.Drawing.Size(298, 310);
            this._treeViewActions.TabIndex = 0;
            this._treeViewActions.DoubleClick += new System.EventHandler(this._treeViewActions_DoubleClick);
            this._treeViewActions.KeyDown += new System.Windows.Forms.KeyEventHandler(this._treeViewActions_KeyDown);
            this._treeViewActions.KeyUp += new System.Windows.Forms.KeyEventHandler(this._treeViewActions_KeyUp);
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.actionsToolStripMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._menuStrip.Size = new System.Drawing.Size(327, 24);
            this._menuStrip.TabIndex = 3;
            this._menuStrip.Text = "_menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitTaskClerkToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(146, 6);
            // 
            // exitTaskClerkToolStripMenuItem
            // 
            this.exitTaskClerkToolStripMenuItem.Name = "exitTaskClerkToolStripMenuItem";
            this.exitTaskClerkToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.exitTaskClerkToolStripMenuItem.Text = "E&xit Task Clerk";
            this.exitTaskClerkToolStripMenuItem.Click += new System.EventHandler(this.exitTaskClerkToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewTaskActicityExplorerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // viewTaskActicityExplorerToolStripMenuItem
            // 
            this.viewTaskActicityExplorerToolStripMenuItem.Name = "viewTaskActicityExplorerToolStripMenuItem";
            this.viewTaskActicityExplorerToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.viewTaskActicityExplorerToolStripMenuItem.Text = "&View Task Acticity Explorer...";
            this.viewTaskActicityExplorerToolStripMenuItem.Click += new System.EventHandler(this.viewTaskActicityExplorerToolStripMenuItem_Click);
            // 
            // actionsToolStripMenuItem
            // 
            this.actionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useSelectedToolStripMenuItem,
            this.advancedSelectToolStripMenuItem,
            this.toolStripMenuItem2,
            this.pinToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.actionsToolStripMenuItem.Name = "actionsToolStripMenuItem";
            this.actionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.actionsToolStripMenuItem.Text = "&Actions";
            this.actionsToolStripMenuItem.DropDownOpening += new System.EventHandler(this.actionsToolStripMenuItem_DropDownOpening);
            // 
            // useSelectedToolStripMenuItem
            // 
            this.useSelectedToolStripMenuItem.Name = "useSelectedToolStripMenuItem";
            this.useSelectedToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.useSelectedToolStripMenuItem.Text = "&Use Selected";
            this.useSelectedToolStripMenuItem.Click += new System.EventHandler(this.useSelectedToolStripMenuItem_Click);
            // 
            // advancedSelectToolStripMenuItem
            // 
            this.advancedSelectToolStripMenuItem.Name = "advancedSelectToolStripMenuItem";
            this.advancedSelectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Insert)));
            this.advancedSelectToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.advancedSelectToolStripMenuItem.Text = "&Advanced Select...";
            this.advancedSelectToolStripMenuItem.Click += new System.EventHandler(this.advancedSelectToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(167, 6);
            // 
            // pinToolStripMenuItem
            // 
            this.pinToolStripMenuItem.Name = "pinToolStripMenuItem";
            this.pinToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.pinToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.pinToolStripMenuItem.Text = "Add...";
            this.pinToolStripMenuItem.Click += new System.EventHandler(this.pinToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // InstantAccessDialog
            // 
            this.AcceptButton = this._buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(327, 460);
            this.Controls.Add(this._groupBoxTaskDescriptions);
            this.Controls.Add(this._groupBoxCurrentTask);
            this.Controls.Add(this._buttonOK);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this._menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstantAccessDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task Select";
            this._groupBoxCurrentTask.ResumeLayout(false);
            this._groupBoxCurrentTask.PerformLayout();
            this._groupBoxTaskDescriptions.ResumeLayout(false);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Button _buttonOK;
        private System.Windows.Forms.GroupBox _groupBoxCurrentTask;
        private System.Windows.Forms.TextBox _textBoxCurrentTask;
        private System.Windows.Forms.GroupBox _groupBoxTaskDescriptions;
        private System.Windows.Forms.TreeView _treeViewActions;
        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitTaskClerkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedSelectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem pinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTaskActicityExplorerToolStripMenuItem;
    }
}