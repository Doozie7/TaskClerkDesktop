//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 01st of Jan 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using BritishMicro.TaskClerk.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk
{
    //[XmlRoot("TaskActivity", Namespace = "http://schemas.britishmicro.com/taskclerk")]
    /// <summary>
    /// The TaskActivity class provides all the information about a recorded
    /// activity or event, this is a primary object in the system.
    /// </summary>
    [Serializable]
    public class TaskActivity : INotifyPropertyChanged
    {
        /// <summary>
        /// The empty TaskActivity
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly TaskActivity Empty
            = new TaskActivity(new Guid("{B3C43E24-DB23-4b6b-A610-DA4CCABEF312}"));

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskActivity"/> class,
        /// Using the supplied GUID.
        /// </summary>
        /// <param name="id">The id.</param>
        private TaskActivity(Guid id) : this()
        {
            Id = id;
            TaskDescription = TaskDescription.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TaskActivity"/> class.
        /// </summary>
        public TaskActivity()
        {
            endDate = DateTime.Now;
            startDate = DateTime.Now;
            originalDate = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TaskActivity"/> class.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        /// <param name="userId">The user id.</param>
        public TaskActivity(TaskDescription taskDescription, string userId)
        {
            id = Guid.NewGuid();
            this.taskDescription = taskDescription;
            startDate = DateTime.Now;
            endDate = DateTime.MinValue;
            originalDate = DateTime.Now;
            UserId = userId;
            sourceMachine = Environment.MachineName;
        }

        private Guid id;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [ReadOnly(true)]
        [Category("Identification")]
        [Description("The unique id for the task.")]
        public Guid Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        private string sourceMachine;

        /// <summary>
        /// Gets or sets the SourceMachine.
        /// </summary>
        /// <value>The SourceMachine.</value>
        [ReadOnly(true)]
        [Category("Identification")]
        [Description("The machine this activity was created on.")]
        public string SourceMachine
        {
            get { return sourceMachine; }
            set
            {
                if (sourceMachine != value)
                {
                    sourceMachine = value;
                    OnPropertyChanged("SourceMachine");
                }
            }
        }

        private DateTime startDate;

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [Category("Time")]
        [Description("The date and time the task started.")]
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = StripSeconds(value);
                    if (TaskDescription != null)
                    {
                        //If this is an event type change the end date.
                        if (TaskDescription.IsEvent)
                        {
                            endDate = startDate;
                        }
                    }
                    OnPropertyChanged("StartDate");
                }
            }
        }

        private DateTime endDate;

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [Category("Time")]
        [Description("The date and time the task ended.")]
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value; // StripSeconds(value);
                    if (TaskDescription != null)
                    {
                        //If this is an event type change the start date.
                        if (TaskDescription.IsEvent && startDate != endDate)
                        {
                            startDate = endDate;
                        }
                    }
                    OnPropertyChanged("EndDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        [Category("Time")]
        [ReadOnly(true)]
        [Description("The duration in minutes the task lasted.")]
        [XmlIgnore]
        public decimal Duration
        {
            get
            {
                DateTime end = StripSeconds(EndDate);
                if (end == DateTime.MinValue)
                {
                    end = DateTime.Now;
                }
                TimeSpan ts = end.Subtract(StripSeconds(StartDate));
                if (ts.TotalMinutes < 0)
                {
                    ts = end.Subtract(end);
                }
                return Math.Abs((decimal)ts.TotalMinutes);
            }
            set
            {
                if (value >= 0)
                {
                    if (TaskDescription != null)
                    {
                        if (TaskDescription.IsEvent == false)
                        {
                            EndDate = StartDate.AddMinutes((double)value);
                        }
                    }
                }
            }
        }

        private TaskDescription taskDescription;

        /// <summary>
        /// Gets or sets the task description.
        /// </summary>
        /// <value>The task description.</value>
        [Category("Information")]
        public TaskDescription TaskDescription
        {
            get { return taskDescription; }
            set
            {
                if (taskDescription != value)
                {
                    taskDescription = value;
                    OnPropertyChanged("TaskDescription");
                }
            }
        }

        private TaskDescription crosstabTaskDescription;

        /// <summary>
        /// Gets or sets the crosstab task description.
        /// </summary>
        /// <value>The crosstab task description.</value>
        [Category("Information")]
        public TaskDescription CrosstabTaskDescription
        {
            get { return crosstabTaskDescription; }
            set
            {
                if (crosstabTaskDescription != value)
                {
                    crosstabTaskDescription = value;
                    OnPropertyChanged("CrosstabTaskDescription");
                }
            }
        }

        private string remarks;

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>The remarks.</value>
        [Description("The users remarks associated with the task.")]
        [Category("Information")]
        public string Remarks
        {
            get { return remarks; }
            set
            {
                if (remarks != value)
                {
                    remarks = value;
                    OnPropertyChanged("Remarks");
                }
            }
        }

        private string userId;

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        [ReadOnly(true)]
        [Description("The user who created the task.")]
        [Category("System Information")]
        public string UserId
        {
            get { return userId; }
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        private int customFlags;

        /// <summary>
        /// Gets or sets the custom flags.
        /// </summary>
        /// <value>The custom flags.</value>
        [Description("A custom flag data field")]
        [ReadOnly(true)]
        [Category("CustomData")]
        public int CustomFlags
        {
            get => customFlags;
            set
            {
                if (customFlags != value)
                {
                    customFlags = value;
                    OnPropertyChanged("CustomFlags");
                }
            }
        }

        private DateTime originalDate;

        /// <summary>
        /// Gets or sets the original date.
        /// </summary>
        /// <value>The original date.</value>
        [ReadOnly(true)]
        [Category("System Information")]
        [Description("The date and time this record was original created.")]
        public DateTime OriginalDate
        {
            get
            {
                if (originalDate == DateTime.MinValue)
                {
                    originalDate = DateTime.Now;
                }
                return originalDate;
            }
            set
            {
                if (originalDate != value)
                {
                    originalDate = value;
                    OnPropertyChanged("OrigionalDate");
                }
            }
        }

        /// <summary>
        /// Toes the summary string.
        /// </summary>
        /// <returns></returns>
        //[DebuggerStepThrough()]
        public string ToSummaryString()
        {
            DateTime end = EndDate;
            if (end == DateTime.MinValue)
            {
                end = DateTime.Now;
            }
            TimeSpan ts = end.Subtract(StartDate);

            string result = string.Empty;
            if (TaskDescription.IsNotEmpty())
            {
                if (TaskDescription.IsEvent)
                {
                    result = string.Format(CultureInfo.InvariantCulture,
                                           Resources.EventSummaryString,
                                           TaskDescription.Name,
                                           StartDate);
                }
                else
                {
                    result = string.Format(CultureInfo.InvariantCulture,
                                           Resources.ActivitySummaryString,
                                           TaskDescription.Name,
                                           StartDate,
                                           ts.TotalMinutes);
                }
            }
            return result;
        }


        /// <summary>
        /// Changes the date.
        /// </summary>
        /// <param name="date">The date.</param>
        public void ChangeDate(DateTime date)
        {
            StartDate =
                new DateTime(date.Year, date.Month, date.Day, StartDate.Hour, StartDate.Minute, 0);
            EndDate =
                new DateTime(date.Year, date.Month, date.Day, EndDate.Hour, EndDate.Minute, 0);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"></see> to compare with the current <see cref="T:System.Object"></see>.</param>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"></see> is equal to the current <see cref="T:System.Object"></see>; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            TaskActivity working = (TaskActivity)obj;
            bool result = (id == working.Id);
            return result;
        }

        /// <summary>
        /// Strips the seconds from a provided date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime StripSeconds(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
        }

        /// <summary>
        /// Aligns the end to start.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        public static void AlignEndToStart(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            if (taskActivityA == null)
            {
                throw new ArgumentNullException("taskActivityA");
            }
            if (taskActivityB == null)
            {
                throw new ArgumentNullException("taskActivityB");
            }

            taskActivityA.EndDate = taskActivityB.StartDate.AddSeconds(-1);
        }

        /// <summary>
        /// Merges the specified task activity into A.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        public static void Merge(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            if (taskActivityA == null)
            {
                throw new ArgumentNullException("taskActivityA");
            }
            if (taskActivityB == null)
            {
                throw new ArgumentNullException("taskActivityB");
            }

            if ((taskActivityA.TaskDescription.IsEvent) ||
                (taskActivityB.TaskDescription.IsEvent))
            {
                throw new InvalidOperationException(Resources.CannotMergeEvents);
            }
            else
            {
                taskActivityA.EndDate = taskActivityB.EndDate;
            }
        }

        /// <summary>
        /// Compares the tasks on start date.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        /// <returns></returns>
        public static int CompareTasksOnStartDate(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            if (taskActivityA == null)
            {
                throw new ArgumentNullException("taskActivityA");
            }
            if (taskActivityB == null)
            {
                throw new ArgumentNullException("taskActivityB");
            }

            return taskActivityA.StartDate.CompareTo(taskActivityB.StartDate);
        }

        /// <summary>
        /// Compares the tasks on end date.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        /// <returns></returns>
        public static int CompareTasksOnEndDate(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            if (taskActivityA == null)
            {
                throw new ArgumentNullException("taskActivityA");
            }
            if (taskActivityB == null)
            {
                throw new ArgumentNullException("taskActivityB");
            }

            return taskActivityA.EndDate.CompareTo(taskActivityB.EndDate);
        }

        /// <summary>
        /// Compares the tasks on duration.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        /// <returns></returns>
        public static int CompareTasksOnDuration(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            if (taskActivityA == null)
            {
                throw new ArgumentNullException("taskActivityA");
            }
            if (taskActivityB == null)
            {
                throw new ArgumentNullException("taskActivityB");
            }

            return taskActivityA.Duration.CompareTo(taskActivityB.Duration);
        }

        /// <summary>
        /// Compares the tasks on task description.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        /// <returns></returns>
        public static int CompareTasksOnTaskName(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            if (taskActivityA == null)
            {
                throw new ArgumentNullException("taskActivityA");
            }
            if (taskActivityB == null)
            {
                throw new ArgumentNullException("taskActivityB");
            }
            if ((taskActivityA.TaskDescription.Name == null) || (taskActivityB.TaskDescription.Name == null))
            {
                return 0;
            }

            return taskActivityA.TaskDescription.Name.CompareTo(taskActivityB.TaskDescription.Name);
        }

        /// <summary>
        /// Reverse Compares the tasks on start date.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        /// <returns></returns>
        public static int ReverseCompareTasksOnStartDate(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            if (taskActivityA == null)
            {
                throw new ArgumentNullException("taskActivityA");
            }
            if (taskActivityB == null)
            {
                throw new ArgumentNullException("taskActivityB");
            }

            return taskActivityB.StartDate.CompareTo(taskActivityA.StartDate);
        }

        /// <summary>
        /// Reverse Compares the tasks on end date.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        /// <returns></returns>
        public static int ReverseCompareTasksOnEndDate(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            if (taskActivityA == null)
            {
                throw new ArgumentNullException("taskActivityA");
            }
            if (taskActivityB == null)
            {
                throw new ArgumentNullException("taskActivityB");
            }

            return taskActivityB.EndDate.CompareTo(taskActivityA.EndDate);
        }

        /// <summary>
        /// Reverse Compares the tasks on duration.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        /// <returns></returns>
        public static int ReverseCompareTasksOnDuration(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            return -CompareTasksOnDuration(taskActivityB, taskActivityA);
        }

        /// <summary>
        /// Reverse Compares the tasks on task description.
        /// </summary>
        /// <param name="taskActivityA">The task activity A.</param>
        /// <param name="taskActivityB">The task activity B.</param>
        /// <returns></returns>
        public static int ReverseCompareTasksOnTaskName(TaskActivity taskActivityA, TaskActivity taskActivityB)
        {
            if (taskActivityA == null)
            {
                throw new ArgumentNullException("taskActivityA");
            }
            if (taskActivityB == null)
            {
                throw new ArgumentNullException("taskActivityB");
            }
            if ((taskActivityA.TaskDescription.Name == null) || (taskActivityB.TaskDescription.Name == null))
            {
                return 0;
            }

            return taskActivityB.TaskDescription.Name.CompareTo(taskActivityA.TaskDescription.Name);
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return this.Equals(TaskActivity.Empty);
        }

        /// <summary>
        /// Determines whether this instance is not empty.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is not empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNotEmpty()
        {
            return !this.Equals(TaskActivity.Empty);
        }

        #region INotifyPropertyChanged Members

        private event PropertyChangedEventHandler _changed;

        /// <summary>
        /// the OnPropertyChanged operation.
        /// </summary>
        /// <param name="property"></param>
        protected void OnPropertyChanged(string property)
        {
            _changed?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// The PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _changed += value; }
            remove { _changed -= value; }
        }

        #endregion
    }
}