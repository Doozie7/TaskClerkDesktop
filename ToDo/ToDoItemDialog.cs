using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BritishMicro.TaskClerk;

namespace BritishMicro.TaskClerk.ToDoPlugin
{
    public partial class ToDoItemDialog : Form
    {
        private TaskClerkEngine _engine;
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