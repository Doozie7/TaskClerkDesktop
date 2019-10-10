using System;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.ToDoPlugin
{
    public partial class ToDoItemDialog : Form
    {
        private readonly TaskClerkEngine _engine;
        private ToDoTask _todoTask;

        public ToDoItemDialog(TaskClerkEngine engine) : this(engine, new ToDoTask())
        {
        }

        public ToDoItemDialog(TaskClerkEngine engine, ToDoTask toDoTask)
        {
            InitializeComponent();
            this._engine = engine;
            this._todoTask = toDoTask;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            _todoTask = this.toDoItem1.ToDoTask;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        public ToDoTask ToDoTask
        {
            get { return _todoTask; }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}