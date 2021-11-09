using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BritishMicro.TaskClerk.UI;
using BritishMicro.TaskClerk.Properties;

namespace BritishMicro.TaskClerk.UI
{
    partial class TaskActivitiesExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskActivitiesExplorer));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taskDescriptionsExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsSepStart = new System.Windows.Forms.ToolStripSeparator();
            this.toolsSepEnd = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.webSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productFeedbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainerDetail = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStripTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.rebuildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panel = new System.Windows.Forms.Panel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.activityChart1 = new BritishMicro.TaskClerk.UI.ActivityChart();
            this.toolStripNavigator = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.recordedTasks1 = new BritishMicro.TaskClerk.UI.RecordedTasks();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.contextMenuStripStatusBar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripTextBoxNewStart = new System.Windows.Forms.ToolStripTextBox();
            this.commitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusPurchased = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusRegistered = new System.Windows.Forms.ToolStripStatusLabel();
            this.languageToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.systemUserToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDetail)).BeginInit();
            this.splitContainerDetail.Panel1.SuspendLayout();
            this.splitContainerDetail.Panel2.SuspendLayout();
            this.splitContainerDetail.SuspendLayout();
            this.contextMenuStripTree.SuspendLayout();
            this.panel.SuspendLayout();
            this.toolStripNavigator.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.contextMenuStripStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // printToolStripMenuItem
            // 
            resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // printPreviewToolStripMenuItem
            // 
            resources.ApplyResources(this.printPreviewToolStripMenuItem, "printPreviewToolStripMenuItem");
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            resources.ApplyResources(this.exitToolStripMenuItem1, "exitToolStripMenuItem1");
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.shutdowmToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // cutToolStripMenuItem
            // 
            resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            // 
            // copyToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taskDescriptionsExplorerToolStripMenuItem,
            this.toolStripSeparator6,
            this.refreshToolStripMenuItem,
            this.graphOptionsToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // taskDescriptionsExplorerToolStripMenuItem
            // 
            this.taskDescriptionsExplorerToolStripMenuItem.Name = "taskDescriptionsExplorerToolStripMenuItem";
            resources.ApplyResources(this.taskDescriptionsExplorerToolStripMenuItem, "taskDescriptionsExplorerToolStripMenuItem");
            this.taskDescriptionsExplorerToolStripMenuItem.Click += new System.EventHandler(this.taskDescriptionsExplorerToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            resources.ApplyResources(this.refreshToolStripMenuItem, "refreshToolStripMenuItem");
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.rebuildToolStripMenuItem_Click);
            // 
            // graphOptionsToolStripMenuItem
            // 
            resources.ApplyResources(this.graphOptionsToolStripMenuItem, "graphOptionsToolStripMenuItem");
            this.graphOptionsToolStripMenuItem.Name = "graphOptionsToolStripMenuItem";
            this.graphOptionsToolStripMenuItem.Click += new System.EventHandler(this.graphOptionsToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            resources.ApplyResources(this.propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.toolsSepStart,
            this.toolsSepEnd,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolsSepStart
            // 
            this.toolsSepStart.Name = "toolsSepStart";
            resources.ApplyResources(this.toolsSepStart, "toolsSepStart");
            // 
            // toolsSepEnd
            // 
            this.toolsSepEnd.Name = "toolsSepEnd";
            resources.ApplyResources(this.toolsSepEnd, "toolsSepEnd");
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.toolStripSeparator5,
            this.webSiteToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.productFeedbackToolStripMenuItem,
            this.toolStripSeparator3,
            this.aboutToolStripMenuItem,
            this.registerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            resources.ApplyResources(this.contentsToolStripMenuItem, "contentsToolStripMenuItem");
            this.contentsToolStripMenuItem.Click += new System.EventHandler(this.contentsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // webSiteToolStripMenuItem
            // 
            this.webSiteToolStripMenuItem.Name = "webSiteToolStripMenuItem";
            resources.ApplyResources(this.webSiteToolStripMenuItem, "webSiteToolStripMenuItem");
            this.webSiteToolStripMenuItem.Click += new System.EventHandler(this.webSiteToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            resources.ApplyResources(this.checkForUpdatesToolStripMenuItem, "checkForUpdatesToolStripMenuItem");
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // productFeedbackToolStripMenuItem
            // 
            this.productFeedbackToolStripMenuItem.Name = "productFeedbackToolStripMenuItem";
            resources.ApplyResources(this.productFeedbackToolStripMenuItem, "productFeedbackToolStripMenuItem");
            this.productFeedbackToolStripMenuItem.Click += new System.EventHandler(this.productFeedbackToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            resources.ApplyResources(this.registerToolStripMenuItem, "registerToolStripMenuItem");
            this.registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click_1);
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.splitContainerDetail);
            this.splitContainer.Panel1.Controls.Add(this.toolStripNavigator);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.recordedTasks1);
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
            // 
            // splitContainerDetail
            // 
            resources.ApplyResources(this.splitContainerDetail, "splitContainerDetail");
            this.splitContainerDetail.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerDetail.Name = "splitContainerDetail";
            // 
            // splitContainerDetail.Panel1
            // 
            this.splitContainerDetail.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainerDetail.Panel2
            // 
            this.splitContainerDetail.Panel2.Controls.Add(this.panel);
            this.splitContainerDetail.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainerDetail_SplitterMoved);
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.ContextMenuStrip = this.contextMenuStripTree;
            resources.ApplyResources(this.treeView, "treeView");
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.ImageList = this.imageList;
            this.treeView.LineColor = System.Drawing.Color.White;
            this.treeView.Name = "treeView";
            this.treeView.ShowLines = false;
            this.treeView.ShowNodeToolTips = true;
            // 
            // contextMenuStripTree
            // 
            this.contextMenuStripTree.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStripTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1,
            this.pasteToolStripMenuItem1,
            this.toolStripSeparator1,
            this.rebuildToolStripMenuItem});
            this.contextMenuStripTree.Name = "contextMenuStripTree";
            resources.ApplyResources(this.contextMenuStripTree, "contextMenuStripTree");
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            resources.ApplyResources(this.copyToolStripMenuItem1, "copyToolStripMenuItem1");
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            resources.ApplyResources(this.pasteToolStripMenuItem1, "pasteToolStripMenuItem1");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // rebuildToolStripMenuItem
            // 
            this.rebuildToolStripMenuItem.Name = "rebuildToolStripMenuItem";
            resources.ApplyResources(this.rebuildToolStripMenuItem, "rebuildToolStripMenuItem");
            this.rebuildToolStripMenuItem.Click += new System.EventHandler(this.rebuildToolStripMenuItem_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "PaperClock2.png");
            this.imageList.Images.SetKeyName(1, "PaperClock2.png");
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.Window;
            this.panel.Controls.Add(this.webBrowser);
            this.panel.Controls.Add(this.activityChart1);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // webBrowser
            // 
            resources.ApplyResources(this.webBrowser, "webBrowser");
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Url = new System.Uri("http://www.taskclerk.com/AppWebStart.ashx", System.UriKind.Absolute);
            // 
            // activityChart1
            // 
            this.activityChart1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.activityChart1, "activityChart1");
            this.activityChart1.Name = "activityChart1";
            this.activityChart1.TabStop = false;
            // 
            // toolStripNavigator
            // 
            resources.ApplyResources(this.toolStripNavigator, "toolStripNavigator");
            this.toolStripNavigator.CanOverflow = false;
            this.toolStripNavigator.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStripNavigator.Name = "toolStripNavigator";
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // recordedTasks1
            // 
            resources.ApplyResources(this.recordedTasks1, "recordedTasks1");
            this.recordedTasks1.Name = "recordedTasks1";
            // 
            // statusStrip
            // 
            this.statusStrip.ContextMenuStrip = this.contextMenuStripStatusBar;
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.statusPurchased,
            this.statusRegistered,
            this.languageToolStripStatusLabel,
            this.systemUserToolStripStatusLabel});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip.ShowItemToolTips = true;
            // 
            // contextMenuStripStatusBar
            // 
            this.contextMenuStripStatusBar.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStripStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxNewStart,
            this.commitToolStripMenuItem});
            this.contextMenuStripStatusBar.Name = "contextMenuStripStatusBar";
            resources.ApplyResources(this.contextMenuStripStatusBar, "contextMenuStripStatusBar");
            this.contextMenuStripStatusBar.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripStatusBar_Opening);
            // 
            // toolStripTextBoxNewStart
            // 
            this.toolStripTextBoxNewStart.Name = "toolStripTextBoxNewStart";
            resources.ApplyResources(this.toolStripTextBoxNewStart, "toolStripTextBoxNewStart");
            // 
            // commitToolStripMenuItem
            // 
            this.commitToolStripMenuItem.Name = "commitToolStripMenuItem";
            resources.ApplyResources(this.commitToolStripMenuItem, "commitToolStripMenuItem");
            this.commitToolStripMenuItem.Click += new System.EventHandler(this.commitToolStripMenuItem_Click);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
            this.toolStripStatusLabel.Spring = true;
            // 
            // statusPurchased
            // 
            this.statusPurchased.Name = "statusPurchased";
            this.statusPurchased.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            resources.ApplyResources(this.statusPurchased, "statusPurchased");
            // 
            // statusRegistered
            // 
            this.statusRegistered.Name = "statusRegistered";
            this.statusRegistered.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            resources.ApplyResources(this.statusRegistered, "statusRegistered");
            // 
            // languageToolStripStatusLabel
            // 
            this.languageToolStripStatusLabel.Name = "languageToolStripStatusLabel";
            this.languageToolStripStatusLabel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            resources.ApplyResources(this.languageToolStripStatusLabel, "languageToolStripStatusLabel");
            // 
            // systemUserToolStripStatusLabel
            // 
            this.systemUserToolStripStatusLabel.Name = "systemUserToolStripStatusLabel";
            this.systemUserToolStripStatusLabel.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            resources.ApplyResources(this.systemUserToolStripStatusLabel, "systemUserToolStripStatusLabel");
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            resources.ApplyResources(this.printPreviewDialog, "printPreviewDialog");
            this.printPreviewDialog.Name = "printPreviewDialog";
            // 
            // TaskActivitiesExplorer
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "TaskActivitiesExplorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskActivitiesExplorer_FormClosing);
            this.Load += new System.EventHandler(this.TaskActivitiesExplorer_Load);
            this.ResizeEnd += new System.EventHandler(this.TaskActivitiesExplorer_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ViewerForm_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.splitContainerDetail.Panel1.ResumeLayout(false);
            this.splitContainerDetail.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDetail)).EndInit();
            this.splitContainerDetail.ResumeLayout(false);
            this.contextMenuStripTree.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.toolStripNavigator.ResumeLayout(false);
            this.toolStripNavigator.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.contextMenuStripStatusBar.ResumeLayout(false);
            this.contextMenuStripStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem productFeedbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip toolStripNavigator;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainerDetail;
        private System.Windows.Forms.ToolStripSeparator toolsSepStart;
        private System.Windows.Forms.ToolStripMenuItem webSiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTree;
        private System.Windows.Forms.ToolStripMenuItem rebuildToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripStatusLabel systemUserToolStripStatusLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private ToolStripStatusLabel statusRegistered;
        private Panel panel;
        private ToolStripStatusLabel statusPurchased;
        private RecordedTasks recordedTasks1;
        private ContextMenuStrip contextMenuStripStatusBar;
        private ToolStripTextBox toolStripTextBoxNewStart;
        private ToolStripMenuItem commitToolStripMenuItem;
        private ActivityChart activityChart1;
        private ToolStripMenuItem copyToolStripMenuItem1;
        private ToolStripMenuItem pasteToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem graphOptionsToolStripMenuItem;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripStatusLabel languageToolStripStatusLabel;
        private ToolStripMenuItem taskDescriptionsExplorerToolStripMenuItem;
        private ToolStripSeparator toolsSepEnd;
        private PrintDialog printDialog;
        private PrintPreviewDialog printPreviewDialog;
        private ToolStripMenuItem registerToolStripMenuItem;
        private WebBrowser webBrowser;
    }
}

