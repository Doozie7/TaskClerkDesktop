namespace BritishMicro.TaskClerk.InstantAccess
{
    partial class InstantAccessForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstantAccessForm));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Task Descriptions", System.Windows.Forms.HorizontalAlignment.Left);
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelNoTaskDescriptions = new System.Windows.Forms.Panel();
            this.labelNoTaskDescriptionsHelp = new System.Windows.Forms.Label();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.useToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitTaskClerkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewTaskDescriptions = new System.Windows.Forms.ListView();
            this.imageListListViewLarge = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxTaskDescription = new System.Windows.Forms.GroupBox();
            this.labelTaskDescription = new System.Windows.Forms.Label();
            this.statusStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panelNoTaskDescriptions.SuspendLayout();
            this.contextMenuStripMain.SuspendLayout();
            this.groupBoxTaskDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMain});
            this.statusStripMain.Location = new System.Drawing.Point(0, 405);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(472, 22);
            this.statusStripMain.TabIndex = 0;
            // 
            // toolStripStatusLabelMain
            // 
            this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
            this.toolStripStatusLabelMain.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabelMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonDelete});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(472, 25);
            this.toolStripMain.TabIndex = 0;
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(58, 22);
            this.toolStripButtonAdd.Text = "&Add...";
            this.toolStripButtonAdd.ToolTipText = "Adds a new Task Description to the Instant Access Manager.";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.AddDescriptionHandler);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Enabled = false;
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDelete.Text = "Delete";
            this.toolStripButtonDelete.ToolTipText = "Removes selected Task Descriptions from the Instant Access Manager.";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.RemoveDescriptionHandler);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 25);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.panelNoTaskDescriptions);
            this.splitContainerMain.Panel1.Controls.Add(this.listViewTaskDescriptions);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.groupBoxTaskDescription);
            this.splitContainerMain.Size = new System.Drawing.Size(472, 380);
            this.splitContainerMain.SplitterDistance = 299;
            this.splitContainerMain.TabIndex = 3;
            this.splitContainerMain.TabStop = false;
            // 
            // panelNoTaskDescriptions
            // 
            this.panelNoTaskDescriptions.Controls.Add(this.labelNoTaskDescriptionsHelp);
            this.panelNoTaskDescriptions.Location = new System.Drawing.Point(133, 100);
            this.panelNoTaskDescriptions.Name = "panelNoTaskDescriptions";
            this.panelNoTaskDescriptions.Size = new System.Drawing.Size(200, 100);
            this.panelNoTaskDescriptions.TabIndex = 1;
            // 
            // labelNoTaskDescriptionsHelp
            // 
            this.labelNoTaskDescriptionsHelp.ContextMenuStrip = this.contextMenuStripMain;
            this.labelNoTaskDescriptionsHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNoTaskDescriptionsHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoTaskDescriptionsHelp.ForeColor = System.Drawing.Color.DimGray;
            this.labelNoTaskDescriptionsHelp.Location = new System.Drawing.Point(0, 0);
            this.labelNoTaskDescriptionsHelp.Name = "labelNoTaskDescriptionsHelp";
            this.labelNoTaskDescriptionsHelp.Size = new System.Drawing.Size(200, 100);
            this.labelNoTaskDescriptionsHelp.TabIndex = 0;
            this.labelNoTaskDescriptionsHelp.Text = "Select the \"Add...\" button from the toolbar to add Task Descriptions.";
            this.labelNoTaskDescriptionsHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripMenuItemSep1,
            this.useToolStripMenuItem,
            this.toolStripMenuItemSep2,
            this.exitTaskClerkToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(224, 104);
            this.contextMenuStripMain.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripMain_Opening);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.addToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.addToolStripMenuItem.Text = "&Add...";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.AddDescriptionHandler);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.removeToolStripMenuItem.Text = "&Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.RemoveDescriptionHandler);
            // 
            // toolStripMenuItemSep1
            // 
            this.toolStripMenuItemSep1.Name = "toolStripMenuItemSep1";
            this.toolStripMenuItemSep1.Size = new System.Drawing.Size(220, 6);
            // 
            // useToolStripMenuItem
            // 
            this.useToolStripMenuItem.Name = "useToolStripMenuItem";
            this.useToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.useToolStripMenuItem.Text = "&Use";
            this.useToolStripMenuItem.Click += new System.EventHandler(this.UseDescriptionHandler);
            // 
            // toolStripMenuItemSep2
            // 
            this.toolStripMenuItemSep2.Name = "toolStripMenuItemSep2";
            this.toolStripMenuItemSep2.Size = new System.Drawing.Size(220, 6);
            // 
            // exitTaskClerkToolStripMenuItem
            // 
            this.exitTaskClerkToolStripMenuItem.Name = "exitTaskClerkToolStripMenuItem";
            this.exitTaskClerkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.F4)));
            this.exitTaskClerkToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.exitTaskClerkToolStripMenuItem.Text = "E&xit Task Clerk";
            this.exitTaskClerkToolStripMenuItem.Click += new System.EventHandler(this.exitTaskClerkToolStripMenuItem_Click);
            // 
            // listViewTaskDescriptions
            // 
            this.listViewTaskDescriptions.ContextMenuStrip = this.contextMenuStripMain;
            this.listViewTaskDescriptions.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup1.Header = "Task Descriptions";
            listViewGroup1.Name = "listViewGroupPinned";
            this.listViewTaskDescriptions.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.listViewTaskDescriptions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewTaskDescriptions.LargeImageList = this.imageListListViewLarge;
            this.listViewTaskDescriptions.Location = new System.Drawing.Point(0, 0);
            this.listViewTaskDescriptions.Name = "listViewTaskDescriptions";
            this.listViewTaskDescriptions.Size = new System.Drawing.Size(472, 299);
            this.listViewTaskDescriptions.TabIndex = 1;
            this.listViewTaskDescriptions.UseCompatibleStateImageBehavior = false;
            this.listViewTaskDescriptions.View = System.Windows.Forms.View.Tile;
            this.listViewTaskDescriptions.DoubleClick += new System.EventHandler(this.UseDescriptionHandler);
            this.listViewTaskDescriptions.SelectedIndexChanged += new System.EventHandler(this.listViewTaskDescriptions_SelectedIndexChanged);
            this.listViewTaskDescriptions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewTaskDescriptions_KeyDown);
            this.listViewTaskDescriptions.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewTaskDescriptions_KeyUp);
            // 
            // imageListListViewLarge
            // 
            this.imageListListViewLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListListViewLarge.ImageStream")));
            this.imageListListViewLarge.TransparentColor = System.Drawing.Color.Magenta;
            this.imageListListViewLarge.Images.SetKeyName(0, "Activity.bmp");
            this.imageListListViewLarge.Images.SetKeyName(1, "Event.bmp");
            // 
            // groupBoxTaskDescription
            // 
            this.groupBoxTaskDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTaskDescription.Controls.Add(this.labelTaskDescription);
            this.groupBoxTaskDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxTaskDescription.Location = new System.Drawing.Point(3, 2);
            this.groupBoxTaskDescription.Name = "groupBoxTaskDescription";
            this.groupBoxTaskDescription.Size = new System.Drawing.Size(465, 71);
            this.groupBoxTaskDescription.TabIndex = 0;
            this.groupBoxTaskDescription.TabStop = false;
            this.groupBoxTaskDescription.Text = "Task Description Information";
            // 
            // labelTaskDescription
            // 
            this.labelTaskDescription.AutoEllipsis = true;
            this.labelTaskDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTaskDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelTaskDescription.Location = new System.Drawing.Point(3, 16);
            this.labelTaskDescription.Name = "labelTaskDescription";
            this.labelTaskDescription.Size = new System.Drawing.Size(459, 52);
            this.labelTaskDescription.TabIndex = 0;
            // 
            // InstantAccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 427);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.statusStripMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InstantAccessForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task Clerk Instance Access Manager";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InstantAccessForm_KeyDown);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.panelNoTaskDescriptions.ResumeLayout(false);
            this.contextMenuStripMain.ResumeLayout(false);
            this.groupBoxTaskDescription.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.GroupBox groupBoxTaskDescription;
        private System.Windows.Forms.Label labelTaskDescription;
        private System.Windows.Forms.ListView listViewTaskDescriptions;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSep1;
        private System.Windows.Forms.ToolStripMenuItem useToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSep2;
        private System.Windows.Forms.ToolStripMenuItem exitTaskClerkToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListListViewLarge;
        private System.Windows.Forms.Panel panelNoTaskDescriptions;
        private System.Windows.Forms.Label labelNoTaskDescriptionsHelp;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMain;
    }
}