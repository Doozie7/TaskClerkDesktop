namespace BritishMicro.TaskClerk
{
    partial class HiddenForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HiddenForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.changeTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.moreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openTaskExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemPluginSep = new System.Windows.Forms.MenuItem();
            this.menuItemChangeActivity = new System.Windows.Forms.MenuItem();
            this.menuItemTaskContainer = new System.Windows.Forms.MenuItem();
            this.menuItemSep2 = new System.Windows.Forms.MenuItem();
            this.menuItemOpenViewer = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.nagtimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.DoubleClick += new System.EventHandler(this.ShowViewer);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeTaskToolStripMenuItem,
            this.toolStripSeparator1,
            this.openTaskExplorerToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // changeTaskToolStripMenuItem
            // 
            this.changeTaskToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.moreToolStripMenuItem});
            this.changeTaskToolStripMenuItem.Name = "changeTaskToolStripMenuItem";
            resources.ApplyResources(this.changeTaskToolStripMenuItem, "changeTaskToolStripMenuItem");
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // moreToolStripMenuItem
            // 
            this.moreToolStripMenuItem.Name = "moreToolStripMenuItem";
            resources.ApplyResources(this.moreToolStripMenuItem, "moreToolStripMenuItem");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // openTaskExplorerToolStripMenuItem
            // 
            this.openTaskExplorerToolStripMenuItem.Image = global::BritishMicro.TaskClerk.Properties.Resources.TaskClerk;
            this.openTaskExplorerToolStripMenuItem.Name = "openTaskExplorerToolStripMenuItem";
            resources.ApplyResources(this.openTaskExplorerToolStripMenuItem, "openTaskExplorerToolStripMenuItem");
            this.openTaskExplorerToolStripMenuItem.Click += new System.EventHandler(this.ShowViewer);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.CloseApplication);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemPluginSep,
            this.menuItemChangeActivity,
            this.menuItemSep2,
            this.menuItemOpenViewer,
            this.menuItemExit});
            this.contextMenu.Popup += new System.EventHandler(this.PopupContextMenu);
            // 
            // menuItemPluginSep
            // 
            this.menuItemPluginSep.Index = 0;
            resources.ApplyResources(this.menuItemPluginSep, "menuItemPluginSep");
            // 
            // menuItemChangeActivity
            // 
            this.menuItemChangeActivity.Index = 1;
            this.menuItemChangeActivity.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemTaskContainer});
            resources.ApplyResources(this.menuItemChangeActivity, "menuItemChangeActivity");
            // 
            // menuItemTaskContainer
            // 
            this.menuItemTaskContainer.Index = 0;
            resources.ApplyResources(this.menuItemTaskContainer, "menuItemTaskContainer");
            // 
            // menuItemSep2
            // 
            this.menuItemSep2.Index = 2;
            resources.ApplyResources(this.menuItemSep2, "menuItemSep2");
            // 
            // menuItemOpenViewer
            // 
            this.menuItemOpenViewer.DefaultItem = true;
            this.menuItemOpenViewer.Index = 3;
            resources.ApplyResources(this.menuItemOpenViewer, "menuItemOpenViewer");
            this.menuItemOpenViewer.Click += new System.EventHandler(this.ShowViewer);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 4;
            resources.ApplyResources(this.menuItemExit, "menuItemExit");
            this.menuItemExit.Click += new System.EventHandler(this.CloseApplication);
            // 
            // nagtimer
            // 
            this.nagtimer.Interval = 60000;
            this.nagtimer.Tick += new System.EventHandler(this.ShowNagBalloonTip);
            // 
            // HiddenForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CausesValidation = false;
            this.ControlBox = false;
            this.ForeColor = System.Drawing.Color.PapayaWhip;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HiddenForm";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.Form_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem menuItemPluginSep;
        private System.Windows.Forms.MenuItem menuItemChangeActivity;
        private System.Windows.Forms.MenuItem menuItemSep2;
        private System.Windows.Forms.MenuItem menuItemOpenViewer;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemTaskContainer;
        private System.Windows.Forms.Timer nagtimer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem changeTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem openTaskExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moreToolStripMenuItem;
    }
}

