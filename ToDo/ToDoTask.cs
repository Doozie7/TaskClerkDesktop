using System;

namespace BritishMicro.TaskClerk.ToDoPlugin
{
    /// <summary>
    /// The ToDoTask class, this class is used in the storage of ToDoTask items.
    /// </summary>
    [Serializable]
    public class ToDoTask
    {

        private string _description;
        private int _priority;
        private int _progress;
        private string _remindType;
        private DateTime _remindDate;
        private bool _popupAlarm;
        private TaskDescription _taskDescription;
        private DateTime _createdDate;
        private string _createdBy;
        private string _createdFor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToDoTask"/> class.
        /// </summary>
        public ToDoTask()
        {
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        /// <summary>
        /// Gets or sets the progress.
        /// </summary>
        /// <value>The progress.</value>
        public int Progress
        {
            get { return _progress; }
            set { _progress = value; }
        }

        /// <summary>
        /// Gets or sets the type of the remind.
        /// </summary>
        /// <value>The type of the remind.</value>
        public string RemindType
        {
            get { return _remindType; }
            set { _remindType = value; }
        }

        /// <summary>
        /// Gets or sets the remind date.
        /// </summary>
        /// <value>The remind date.</value>
        public DateTime RemindDate
        {
            get { return _remindDate; }
            set { _remindDate = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [popup alarm].
        /// </summary>
        /// <value><c>true</c> if [popup alarm]; otherwise, <c>false</c>.</value>
        public bool PopupAlarm
        {
            get { return _popupAlarm; }
            set { _popupAlarm = value; }
        }

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>The task description.</value>
        public TaskDescription TaskDescription
        {
            get { return _taskDescription; }
            set { _taskDescription = value; }
        }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        /// <summary>
        /// Gets or sets the identity of the creator.
        /// </summary>
        /// <value>The type of the remind.</value>
        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        /// <summary>
        /// Gets or sets the identity of the target of the todo item.
        /// </summary>
        /// <value>The type of the remind.</value>
        public string CreatedFor
        {
            get { return _createdFor; }
            set { _createdFor = value; }
        }

        /// <summary>
        /// Compares on the RemindDate of the todotask.
        /// </summary>
        /// <param name="taskDescriptionA">The toDoTask  A.</param>
        /// <param name="taskDescriptionB">The toDoTask B.</param>
        /// <returns></returns>
        public static int CompareOnRemindDate(ToDoTask toDoTaskA,
                                                       ToDoTask toDoTaskB)
        {
            if (toDoTaskA == null)
            {
                throw new ArgumentNullException("toDoTaskA");
            }
            if (toDoTaskB == null)
            {
                throw new ArgumentNullException("toDoTaskB");
            }

            return toDoTaskA.RemindDate.CompareTo(toDoTaskB.RemindDate);
        }

    }
}
