using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TasksNavigator : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TasksNavigator"/> class.
        /// </summary>
        public TasksNavigator()
        {
            InitializeComponent();
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //RefreshViews(e.Node.Tag);
        }
    }
}