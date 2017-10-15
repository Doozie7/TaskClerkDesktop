using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace BritishMicro.TaskClerk.ToDoPlugin
{
    public partial class ToDoExplorer : Form
    {
        private TaskClerkEngine _engine;
        private List<ToDoTask> _todoTasks;
        private ToDoOptions _options;

        public ToDoExplorer(TaskClerkEngine engine)
        {
            InitializeComponent();
            _engine = engine;
            _todoTasks = _engine.SettingsProvider.Get("ToDoTasks", new List<ToDoTask>()) as List<ToDoTask>;
            _options = _engine.SettingsProvider.Get("ToDoOptions", new ToDoOptions()) as ToDoOptions;
            listView.Font = _engine.SettingsProvider.Get("GeneralFont", SystemFonts.DefaultFont) as Font;
            toolStripStatusLang.Text = Thread.CurrentThread.CurrentCulture.Name;

            GetFormSizeAndLocation();
            CreateGroupsInListView();
            PopulateListView();

            this.monthCalendar.DateSelected += 
                new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
            this.listView.ItemSelectionChanged += 
                new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
        }

        private void CreateGroupsInListView()
        {
            listView.Groups.Clear();
            foreach (string group in _options.GroupStrings.Split(
                new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                ListViewGroup lvg = new ListViewGroup(group.Replace(" ", "_"), group);
                listView.Groups.Add(lvg);
            }
        }

        private void PopulateListView()
        {
            this.listView.Items.Clear();
            List<DateTime> dates = new List<DateTime>();
            _todoTasks.Sort(ToDoTask.CompareOnRemindDate);
            foreach (ToDoTask task in _todoTasks)
            {
                if ((task != null) && (task.TaskDescription != null))
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Tag = task;
                    if (task.Progress == 100)
                    {
                        lvi.Font = new Font(this.listView.Font, FontStyle.Strikeout);
                    }                    
                    lvi.SubItems.AddRange(
                        new string[] { 
                        task.Description, 
                        task.TaskDescription.Name, 
                        task.RemindType.ToString(), 
                        task.RemindDate.ToString(), 
                        task.PopupAlarm.ToString() });

                    lvi.Group = SetGroup(task);
                    
                    this.listView.Items.Add(lvi);
                    dates.Add(task.RemindDate);
                }
            }
            monthCalendar.BoldedDates = dates.ToArray();
            toolStripStatusLabelCount.Text = listView.Items.Count.ToString();
        }

        private ListViewGroup SetGroup(ToDoTask task)
        {
            if (task.RemindDate == DateTime.MinValue)
            {
                return listView.Groups[3];
            }
            if (task.RemindDate.Date == DateTime.Today)
            {
                return listView.Groups[1];
            }
            if (task.RemindDate.Date < DateTime.Today)
            {
                return listView.Groups[0];
            }
            return listView.Groups[2];
        }

        private void newToDoTask(object sender, EventArgs e)
        {
            using (ToDoItemDialog dialog = new ToDoItemDialog(this._engine))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.ToDoTask != null && dialog.ToDoTask.TaskDescription != null)
                    {
                        _todoTasks.Add(dialog.ToDoTask);
                        _engine.SettingsProvider.Set("ToDoTasks", _todoTasks, PersistHint.AcrossSessions);
                        PopulateListView();
                    }
                }
            }
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.monthCalendar.DateSelected -= 
                new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
            ToDoTask task = e.Item.Tag as ToDoTask;
            if (task != null)
            {
                monthCalendar.SelectionStart = task.RemindDate;
                monthCalendar.SelectionEnd = task.RemindDate;
            }
            this.monthCalendar.DateSelected += 
                new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
        }

        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.listView.ItemSelectionChanged -= 
                new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
            foreach (ListViewItem lvi in listView.Items)
            {
                lvi.Selected = false;
                ToDoTask task = lvi.Tag as ToDoTask;
                if ((task != null) && (task.RemindDate.Date == e.Start.Date))
                {
                    lvi.Selected = true;
                }
            }
            this.listView.ItemSelectionChanged += 
                new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_ItemSelectionChanged);
        }

        private void closeStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deleteToDoTask(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView.Items)
            {
                if (lvi.Selected)
                {
                    ToDoTask t = lvi.Tag as ToDoTask;
                    if (t != null)
                    {
                        _todoTasks.Remove(t);
                        lvi.Remove();
                    }
                }
            }
            toolStripStatusLabelCount.Text = listView.Items.Count.ToString();
        }

        private void completeToolStripButton_Click(object sender, EventArgs e)
        {
            SetSelectedToComplete();
        }

        private void completeStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSelectedToComplete();
        }

        private void completeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSelectedToComplete();
        }

        private void SetSelectedToComplete()
        {
            foreach (ListViewItem lvi in listView.Items)
            {
                if (lvi.Selected == true)
                {
                    ToDoTask task = lvi.Tag as ToDoTask;
                    if (task != null)
                    {
                        task.Progress = 100;
                        lvi.Font = new Font(lvi.Font, FontStyle.Strikeout);
                    }
                }
            }
        }

        private void optionsStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ToDoOptionsDialog dialog = new ToDoOptionsDialog(new ToDoOptions(_options)))
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    _options = dialog.Options;
                    _engine.SettingsProvider.Set("ToDoOptions", _options, PersistHint.AcrossSessions);
                }
            }
            CreateGroupsInListView();
            PopulateListView();
        }

        private void listView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteToDoTask(sender, EventArgs.Empty);
                return;
            }
            if (e.KeyCode == Keys.Insert)
            {
                newToDoTask(sender, EventArgs.Empty);
                return;
            }
        }

        private void ToDoExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetFormSizeAndLocation();
        }

        private void GetFormSizeAndLocation()
        {
            this.ClientSize = (Size)_engine.SettingsProvider.Get(
                $"{this.GetType().Name} ClientSize",
                                         this.Size);
        }

        private void SetFormSizeAndLocation()
        {
            _engine.SettingsProvider.Set(
                $"{this.GetType().Name} ClientSize",
                this.ClientSize,
                PersistHint.AcrossSessions);
        }

        private void editToDoTask(object sender, EventArgs e)
        {
            ToDoTask toDoTask = listView.Items[listView.SelectedIndices[0]].Tag as ToDoTask;
            using (ToDoItemDialog dialog = new ToDoItemDialog(this._engine, toDoTask))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (dialog.ToDoTask != null && dialog.ToDoTask.TaskDescription != null)
                    {
                        _todoTasks.Add(dialog.ToDoTask);
                        _engine.SettingsProvider.Set("ToDoTasks", _todoTasks, PersistHint.AcrossSessions);
                        PopulateListView();
                    }
                }
            }
        }
    }
}