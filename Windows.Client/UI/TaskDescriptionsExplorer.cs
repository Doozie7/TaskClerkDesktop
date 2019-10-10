using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk.UI
{
    public partial class TaskDescriptionsExplorer : Form
    {
        private TaskDescription _currentTaskDescription;
        private TreeNode _currentNode;
        private readonly bool _showTreeFaults;
        private readonly bool _throwTreeFaults;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDescriptionsExplorer"/> class.
        /// </summary>
        public TaskDescriptionsExplorer()
        {
            InitializeComponent();
            _showTreeFaults = true;
            _throwTreeFaults = false;
        }

        private void TaskDescriptionsExplorer_Load(object sender, EventArgs e)
        {
            treeView.Font = (Font)AppContext.Current.SettingsProvider.Get("GeneralFont", Font);
            propertyGrid.Font = (Font)AppContext.Current.SettingsProvider.Get("GeneralFont", Font);

            //clear the context variable
            AppContext.Current.SettingsProvider.Set(
                "SelectedTaskDescription",
                TaskDescription.Empty,
                PersistHint.AcrossSessions);

            TaskActivity currentTaskactivity =
                (TaskActivity)AppContext.Current.SettingsProvider.Get("CurrentActivity", TaskActivity.Empty);
            if (currentTaskactivity.IsNotEmpty())
            {
                _currentTaskDescription = currentTaskactivity.TaskDescription;
            }

            // Set Owner Draw Mode to Text
            if ((null == Site) || (!Site.DesignMode))
            {
                if ((bool)AppContext.Current.SettingsProvider.Get("Advanced Draw on Descriptions Tree", true))
                {
                    treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
                }
            }
            ReBuildTree(AppContext.Current.TaskDescriptionsProvider.TaskDescriptions);

            buttonUse.Visible = !(bool)AppContext.Current.SettingsProvider.Get("HideUseButton", false);

            Activate();
        }

        private void ReBuildTree(Collection<TaskDescription> rootDescriptions)
        {
            //clear the tree
            treeView.Nodes.Clear();

            //create a root
            TreeNode root = new TreeNode("Task Descriptions", imageList.Images.Count - 1, imageList.Images.Count - 1)
            {
                Tag = rootDescriptions
            };
            treeView.Nodes.Add(root);

            foreach (TaskDescription taskDescription in rootDescriptions)
            {
                TreeNode treeNode = new TreeNode(taskDescription.Name)
                {
                    ToolTipText = taskDescription.Description,
                    Tag = taskDescription
                };
                BuildTree(taskDescription, treeNode);
                treeView.Nodes[0].Nodes.Add(treeNode);
                treeNode.EnsureVisible();
                if (taskDescription.Enabled == false)
                {
                    treeNode.ForeColor = Color.Gray;
                }
                if (_currentNode != null)
                {
                    treeView.SelectedNode = _currentNode;
                    _currentNode.EnsureVisible();
                }
            }
        }

        private void BuildTree(TaskDescription parent, TreeNode rootNode)
        {
            foreach (TaskDescription child in parent.Children)
            {
                if (child != null)
                {
                    TreeNode node = new TreeNode(child.Name)
                    {
                        Tag = child,
                        ToolTipText = child.Description
                    };
                    rootNode.Nodes.Add(node);
                    if (child.Children != null)
                    {
                        if (child.Children.Count > 0)
                        {
                            BuildTree(child, node);
                        }
                    }

                    if ((_currentTaskDescription != null) && (_currentTaskDescription.Id == child.Id))
                    {
                        _currentNode = node;
                    }
                }
            }
        }

        private void SelectAndView(object sender, TreeViewEventArgs e)
        {
            treeView.SuspendLayout();
            if (treeView.SelectedNode != null)
            {
                if (treeView.SelectedNode.Tag != null)
                {
                    if (treeView.SelectedNode.Tag.GetType() == typeof(TaskDescription))
                    {
                        if (treeView.SelectedNode.Tag is TaskDescription td)
                        {
                            propertyGrid.SelectedObject = td;
                            treeView.SelectedNode.Text = td.Name;
                            treeView.SelectedNode.ToolTipText = td.Description;
                            buttonUse.Enabled = false;
                            if (td.IsDateBetweenValidPeriod(DateTime.Now))
                            {
                                buttonUse.Enabled = true;
                            }
                            // This line ensures users cannot modify server created records
                            if (!string.IsNullOrEmpty(td.Server))
                            {
                                propertyGrid.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        propertyGrid.SelectedObject = null;
                    }
                }
            }
            treeView.ResumeLayout();
        }

        private void SelectAndClose(object sender, EventArgs e)
        {
            if ((treeView.SelectedNode != null) && (treeView.SelectedNode.Nodes.Count == 0))
            {
                if ((treeView.SelectedNode.Tag != null) &&
                    (treeView.SelectedNode.Tag.GetType() == typeof(TaskDescription)))
                {
                    if (treeView.SelectedNode.Tag is TaskDescription td)
                    {
                        if (td.IsDateBetweenValidPeriod(DateTime.Now) == false)
                        {
                            buttonUse.Enabled = false;
                        }
                        else
                        {
                            AppContext.Current.SettingsProvider.Set(
                                "SelectedTaskDescription",
                                td,
                                PersistHint.AcrossSessions);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                }
            }
        }

        private void AddNewTaskDescription(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                NewTaskDescriptionDialog ntdd
                    = new NewTaskDescriptionDialog(treeView.SelectedNode.Tag as TaskDescription);
                if (ntdd.ShowDialog(this) == DialogResult.OK)
                {
                    TaskDescription taskDescription = new TaskDescription
                    {
                        Id = Guid.NewGuid(),
                        Name = ntdd.TaskDescription.Name,
                        Color = ntdd.TaskDescription.Color,
                        NoNagMinutes = ntdd.TaskDescription.NoNagMinutes,
                        IsEvent = false
                    };

                    TreeNode treeNode = new TreeNode(taskDescription.Name)
                    {
                        Tag = taskDescription
                    };
                    if ((treeView.SelectedNode.Tag != null) &&
                        (treeView.SelectedNode.Tag.GetType() == typeof(TaskDescription)))
                    {
                        if (treeView.SelectedNode.Tag is TaskDescription td)
                        {
                            td.Children.Add(taskDescription);
                            treeView.SelectedNode.Nodes.Add(treeNode);
                            treeView.SelectedNode = treeNode;
                        }
                    }
                    else
                    {
                        treeView.Nodes[0].Nodes.Add(treeNode);
                        treeView.SelectedNode = treeNode;
                        ((Collection<TaskDescription>)treeView.Nodes[0].Tag).Add(taskDescription);
                    }
                    AppContext.Current.TaskDescriptionsProvider.SaveDescriptions();
                }
            }
        }

        private void PropertyGridUpdateComplete(object s, PropertyValueChangedEventArgs e)
        {
            if (propertyGrid.SelectedObject != null)
            {
                if (propertyGrid.SelectedObject.GetType() == typeof(TaskDescription))
                {
                    TaskDescription currentTaskDescription = (TaskDescription)propertyGrid.SelectedObject;
                    treeView.SelectedNode.Text = currentTaskDescription.Name;
                    AppContext.Current.TaskDescriptionsProvider.SaveDescriptions();
                    if (currentTaskDescription.IsDateBetweenValidPeriod(DateTime.Now) == false)
                    {
                        buttonUse.Enabled = false;
                    }
                    else
                    {
                        buttonUse.Enabled = true;
                    }
                }
            }
        }

        private void DeleteTaskDescription(object sender, EventArgs e)
        {
            if (treeView.SelectedNode.Nodes.Count > 0)
            {
                MessageBox.Show(this, "Please remove all the sub Task Descriptions first.",
                                "Remove Condition",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                return;
            }

            if (treeView.SelectedNode.Tag != null)
            {
                TreeNode parentNode = treeView.SelectedNode.Parent;
                if (treeView.SelectedNode.Tag.GetType() == typeof(TaskDescription))
                {
                    AppContext.Current.TaskDescriptionsProvider.RemoveDescription(
                        (TaskDescription)treeView.SelectedNode.Tag);
                    treeView.SelectedNode.Remove();
                    ReBuildTree(AppContext.Current.TaskDescriptionsProvider.TaskDescriptions);
                    treeView.SelectedNode = FindTaskDescription(parentNode.Text, treeView.Nodes);
                    treeView.SelectedNode.Expand();
                }
            }
        }

        private void RefreshTree(object sender, EventArgs e)
        {
            ReBuildTree(AppContext.Current.TaskDescriptionsProvider.TaskDescriptions);
        }

        private void CloseForm(object sender, EventArgs e)
        {
            Close();
        }

        private void SynchroniseWithServer(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchForTaskDescriptions(object sender, EventArgs e)
        {
            if (searchToolStripButton.Checked == false)
            {
                searchToolStripButton.Checked = !searchToolStripButton.Checked;
                searchToolStripTextBox.Visible = searchToolStripButton.Checked;
            }
            searchToolStripTextBox.Focus();
            searchToolStripTextBox.SelectAll();
        }

        private static TreeNode FindTaskDescription(string search, TreeNodeCollection nodes)
        {
            TreeNode found = null;
            foreach (TreeNode treeNode in nodes)
            {
                if (treeNode.Text.StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                {
                    return treeNode;
                }
                else
                {
                    found = FindTaskDescription(search, treeNode.Nodes);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return found;
        }

        private void TaskSelectorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                CloseForm(sender, new EventArgs());
                return;
            }

            if ((e.KeyCode == Keys.Enter) && (searchToolStripTextBox.Focused))
            {
                TreeNode foundNode = FindTaskDescription(searchToolStripTextBox.Text, treeView.Nodes);
                if (foundNode != null)
                {
                    treeView.SelectedNode = foundNode;
                    foundNode.EnsureVisible();
                    treeView.Focus();
                }
                return;
            }

            if (e.KeyCode == Keys.F3)
            {
                SearchForTaskDescriptions(this, EventArgs.Empty);
            }

            if (e.KeyCode == Keys.Enter)
            {
                SelectAndClose(this, EventArgs.Empty);
                return;
            }
        }

        #region Tree drag drop

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            TreeNode destination = ((TreeView)sender).GetNodeAt(pt);
            TreeNode movingNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

            if (MoveNode(movingNode, destination, false))
            {
                AppContext.Current.TaskDescriptionsProvider.RemoveDescription((TaskDescription)movingNode.Tag);
                if (destination.Tag.GetType() == typeof(TaskDescription))
                {
                    ((TaskDescription)destination.Tag).Children.Add((TaskDescription)movingNode.Tag);
                }
                else
                {
                    AppContext.Current.TaskDescriptionsProvider.TaskDescriptions.Add((TaskDescription)movingNode.Tag);
                }
                AppContext.Current.TaskDescriptionsProvider.SaveDescriptions();
            }
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private bool MoveNode(TreeNode nodeToMove, TreeNode destination, bool linking)
        {
            bool result = false;
            if (destination == null)
            {
                if (_showTreeFaults)
                {
                    MessageBox.Show(this,
                                    "The destination node cannot be null.",
                                    "Drag Fault",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
                if (_throwTreeFaults)
                {
                    throw new InvalidOperationException("The destination node cannot be null.");
                }
                return result;
            }

            if (HasAnsester(destination, nodeToMove))
            {
                if (_showTreeFaults)
                {
                    MessageBox.Show(this,
                                    "The destination node cannot be parented by the node been dragged.",
                                    "Drag Fault",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
                if (_throwTreeFaults)
                {
                    throw new InvalidOperationException(
                        "The destination node cannot be parented by the node been dragged.");
                }
                return result;
            }

            if (nodeToMove == destination)
            {
                return result;
            }

            //if (destination != null)
            //{
            TreeNode clonedNode = (TreeNode)nodeToMove.Clone();
            clonedNode.Tag = nodeToMove.Tag;
            clonedNode.Checked = false;
            destination.Nodes.Add(clonedNode);
            if (linking == false)
            {
                nodeToMove.Remove();
            }
            clonedNode.EnsureVisible();
            clonedNode.TreeView.SelectedNode = clonedNode;
            result = true;
            //}

            return result;
        }

        private static bool HasAnsester(TreeNode node, TreeNode ansester)
        {
            if (node == ansester)
            {
                return true;
            }
            else
            {
                if (node.Parent == null)
                {
                    return false;
                }
                else
                {
                    return HasAnsester(node.Parent, ansester);
                }
            }
        }

        #endregion

        private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Font font = treeView.Font;
            Color color = SystemColors.WindowText;
            Color highlightColor = SystemColors.HighlightText;

            TaskDescription td = e.Node.Tag as TaskDescription;
            if (td != null)
            {
                if (td.IsCategory)
                {
                    font = new Font(font, FontStyle.Bold);
                }

                if (td.IsEvent)
                {
                    font = new Font(font, FontStyle.Italic);
                }

                if (td.Enabled == false)
                {
                    color = Color.Gray;
                }
            }
            else
            {
                e.DrawDefault = true;
                return;
            }

            int width = TextRenderer.MeasureText((e.Node.Text + "  "), font).Width;
            if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
            {
                Rectangle rectangle2 = e.Bounds;

                Size size1 = TextRenderer.MeasureText(e.Node.Text, font);
                Point point1 = new Point(rectangle2.X, rectangle2.Y);
                rectangle2 = new Rectangle(point1, new Size(size1.Width, rectangle2.Height));
                e.Graphics.FillRectangle(SystemBrushes.Highlight, rectangle2);
                ControlPaint.DrawFocusRectangle(e.Graphics, rectangle2, color, SystemColors.Highlight);
                TextRenderer.DrawText(
                    e.Graphics, e.Node.Text, font, rectangle2, highlightColor, TextFormatFlags.GlyphOverhangPadding);
                TextRenderer.DrawText(
                    e.Graphics, "(" + td.Color.Name + ")", font, new Point(e.Bounds.X + width, e.Bounds.Y), td.Color);
            }
            else
            {
                if (e.Bounds.X > 0)
                {
                    if ((e.Node != null) && (e.Node.Tag != null) && (e.Node.Tag.GetType() == typeof(TaskDescription)))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(treeView.BackColor), e.Bounds);
                        TextRenderer.DrawText(e.Graphics, td.Name, font, new Point(e.Bounds.X, e.Bounds.Y), color);
                        TextRenderer.DrawText(e.Graphics, "(" + td.Color.Name + ")", font,
                                              new Point(e.Bounds.X + width, e.Bounds.Y), td.Color);
                    }
                }
            }
            e.DrawDefault = false;
            font.Dispose();
        }

        private void searchToolStripTextBox_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void searchToolStripTextBox_Leave(object sender, EventArgs e)
        {
            AcceptButton = buttonUse;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode.Tag is TaskDescription taskDescription)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);
                    try
                    {
                        using (FileStream fileStream = fileInfo.Open(FileMode.Create, FileAccess.Write))
                        {
                            XmlSerializer s = new XmlSerializer(typeof(TaskDescription));
                            s.Serialize(fileStream, taskDescription);
                            if (DialogResult.OK == MessageBox.Show(
                                                       "Task Descriptions saved to\n" + fileInfo.FullName +
                                                       "\n\nwould you like to view the new file?",
                                                       "File Save Complete",
                                                       MessageBoxButtons.OKCancel,
                                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign))
                            {
                                Process.Start(fileInfo.FullName);
                            }
                        }
                    }
                    catch (IOException ioe)
                    {
                        MessageBox.Show(
                            ioe.Message,
                            "File Save Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    }
                }
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                using (FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer s = new XmlSerializer(typeof(TaskDescription));
                    TaskDescription imported = (TaskDescription)s.Deserialize(fileStream);
                    if (treeView.SelectedNode.Tag is TaskDescription taskDescription)
                    {
                        taskDescription.Children.Add(imported);
                        AppContext.Current.TaskDescriptionsProvider.SaveDescriptions();
                        MessageBox.Show(
                            "Task Descriptions imported from " + fileInfo.FullName,
                            "File Import Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                        ReBuildTree(AppContext.Current.TaskDescriptionsProvider.TaskDescriptions);
                    }
                }
            }
        }
    }
}