using BritishMicro.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    internal partial class SelectedTaskDescriptions : WizardUserControl
    {
        private readonly List<TaskDescription> _exportList;
        private readonly ExportForm _mainForm;

        public SelectedTaskDescriptions(ExportForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _exportList = new List<TaskDescription>();
            ReBuildTree(AppContext.Current.TaskDescriptionsProvider.TaskDescriptions);
            _mainForm.SelectedTaskDescriptions = _exportList;

            labelCount.Text = string.Format(
                CultureInfo.InvariantCulture,
                "There are {0} TaskDescriptions currently selected.", _exportList.Count);
        }

        private void ReBuildTree(Collection<TaskDescription> rootDescriptions)
        {
            //clear the tree
            treeviewTaskDescriptions.Nodes.Clear();

            //create a root
            TreeNode root = new TreeNode("Task Descriptions")
            {
                Tag = rootDescriptions
            };
            treeviewTaskDescriptions.Nodes.Add(root);

            foreach (TaskDescription taskDescription in rootDescriptions)
            {
                TreeNode treeNode = new TreeNode(taskDescription.Name)
                {
                    ToolTipText = taskDescription.Description,
                    Tag = taskDescription,
                    Checked = !taskDescription.IsPrivate
                };
                if (treeNode.Checked)
                {
                    _exportList.Add(taskDescription);
                }
                BuildTree(taskDescription, treeNode);
                treeviewTaskDescriptions.Nodes[0].Nodes.Add(treeNode);
                treeNode.EnsureVisible();
            }
        }

        private void BuildTree(TaskDescription parent, TreeNode rootNode)
        {
            foreach (TaskDescription child in parent.Children)
            {
                TreeNode node = new TreeNode(child.Name)
                {
                    Tag = child,
                    ToolTipText = child.Description,
                    Checked = !child.IsPrivate
                };
                if (node.Checked)
                {
                    _exportList.Add(child);
                }
                rootNode.Nodes.Add(node);
                if (child.Children.Count > 0)
                {
                    BuildTree(child, node);
                }
            }
        }

        private void UpdateExportList(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag.GetType() == typeof(TaskDescription))
            {
                if (e.Node.Tag is TaskDescription taskDescription)
                {
                    if (e.Node.Checked == true)
                    {
                        InsertItemInList(taskDescription);
                    }
                    else
                    {
                        RemoveItemFromList(taskDescription);
                    }
                    CheckChildren(e.Node);
                }
            }
            labelCount.Text =
                string.Format(
                    CultureInfo.InvariantCulture,
                    "There are {0} TaskDescriptions currently selected.", _exportList.Count);
        }

        private void CheckChildren(TreeNode parent)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                if (child.Checked != parent.Checked)
                {
                    child.Checked = parent.Checked;
                    TaskDescription taskDescription = child.Tag as TaskDescription;
                    if (child.Checked)
                    {
                        InsertItemInList(taskDescription);
                    }
                    else
                    {
                        RemoveItemFromList(taskDescription);
                    }
                }
                if (child.Nodes != null)
                {
                    CheckChildren(child);
                }
            }
        }

        private void InsertItemInList(TaskDescription taskDescription)
        {
            if (!_exportList.Contains(taskDescription))
            {
                _exportList.Add(taskDescription);
            }
        }

        private void RemoveItemFromList(TaskDescription taskDescription)
        {
            if (_exportList.Contains(taskDescription))
            {
                _exportList.Remove(taskDescription);
            }
        }
    }
}