//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 01st of Jan 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk
{

    //[XmlRoot("TaskDescription", Namespace = "http://schemas.britishmicro.com/taskclerk")]
    /// <summary>
    /// A TaskDescription contains the identification and description information about types of Tasks not
    /// instance of Tasks.
    /// </summary>
    [Serializable]
    public class TaskDescription : INotifyPropertyChanged
    {
        /// <summary>
        /// The empty TaskDescription.
        /// </summary>
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly TaskDescription Empty
            = new TaskDescription(new Guid("{90F23F7A-2156-4051-9BBF-AAA095E1BA64}"), "Empty");

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDescription"/> class,
        /// Using the supplied GUID
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="description">The description.</param>
        private TaskDescription(Guid id, string description) : this()
        {
            this.id = id;
            this.description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDescription"/> class.
        /// </summary>
        public TaskDescription()
        {
            id = Guid.NewGuid();
            enabled = true;
        }

        private Guid id;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [ReadOnly(true)]
        [Category("Identification")]
        [Description("The Id that uniquely identifies this Task Description.")]
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

        private string server;

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        [ReadOnly(true)]
        [Category("Identification")]
        [Description("The server that owns this Task Description.")]
        public string Server
        {
            get { return server; }
            set
            {
                if (server != value)
                {
                    server = value;
                    OnPropertyChanged("Server");
                }
            }
        }

        private string name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Category("Appearance")]
        [Description("The name of the task.")]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string description;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [Category("Appearance")]
        [Editor(
            "System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            , typeof(UITypeEditor)), SettingsBindable(true)]
        [Description("A full description.")]
        public virtual string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private int noNagMinutes;

        /// <summary>
        /// Gets or sets the no nag minutes.
        /// </summary>
        /// <value>The no nag minutes.</value>
        [Category("Behaviour")]
        [Description(
            "The number of minutes the system waits before it pops up a nag message asking if you have completed the task. Setting this value to 0 means you don't want the system to nage you."
            )]
        public int NoNagMinutes
        {
            get { return noNagMinutes; }
            set
            {
                if (noNagMinutes != value)
                {
                    noNagMinutes = value;
                    OnPropertyChanged("NoNagMinutes");
                }
            }
        }

        private Color color;

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        [Category("Appearance")]
        [TypeConverter(typeof(ColorConverter))]
        [XmlIgnore()]
        [Description(
            "The color that represents this Task Description, you can use the same color for other Task Descriptions.")]
        public virtual Color Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;
                    OnPropertyChanged("Color");
                }
            }
        }

        /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        /// <value>The colour.</value>
        [XmlElement("Colour")]
        [Browsable(false)]
        public string Colour
        {
            get => SerializeColor(Color);
            set
            {
                Color = DeserializeColor(value);
                OnPropertyChanged("Colour");
            }
        }

        /// <summary>
        /// Serializes the color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static string SerializeColor(Color color)
        {
            if (color.IsNamedColor)
                return string.Format(CultureInfo.InvariantCulture,
                                     "NAME:{0}", color.Name);
            else
                return string.Format(CultureInfo.InvariantCulture,
                                     "ARGB:{0}:{1}:{2}:{3}",
                                     color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Deserializes the color.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        private static Color DeserializeColor(string color)
        {
            string[] pieces = color.Split(new char[] { ':' });

            if (pieces.Length == 2)
            {
                //"NAME"
                return Color.FromName(pieces[1]);
            }
            else if (pieces.Length == 5)
            {
                byte a, r, g, b;
                //"ARGB"
                a = byte.Parse(pieces[1], CultureInfo.InvariantCulture);
                r = byte.Parse(pieces[2], CultureInfo.InvariantCulture);
                g = byte.Parse(pieces[3], CultureInfo.InvariantCulture);
                b = byte.Parse(pieces[4], CultureInfo.InvariantCulture);

                return Color.FromArgb(a, r, g, b);
            }
            return Color.Empty;
        }

        private int customFlags;

        /// <summary>
        /// Gets or sets the custom flags.
        /// </summary>
        /// <value>The custom flags.</value>
        [Category("CustomData")]
        [Description("A custom flag data field.")]
        public int CustomFlags
        {
            get { return customFlags; }
            set
            {
                if (customFlags != value)
                {
                    customFlags = value;
                    OnPropertyChanged("CustomFlags");
                }
            }
        }

        private bool isPrivate;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        [Category("CustomData")]
        [Description("This allows the user to have private task description that get seen as Private on other systems.")
        ]
        public bool IsPrivate
        {
            get { return isPrivate; }
            set
            {
                if (isPrivate != value)
                {
                    isPrivate = value;
                    OnPropertyChanged("IsPrivate");
                }
            }
        }

        private string groupName;

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        [Category("Classification")]
        [Description(
            "A Task Description can be associated with a group, a group is a simple string representing a named group.")
        ]
        public string GroupName
        {
            get { return groupName; }
            set
            {
                if (groupName != value)
                {
                    groupName = value;
                    OnPropertyChanged("GroupName");
                }
            }
        }

        private int menuColumn;

        /// <summary>
        /// Gets or sets the menu column.
        /// </summary>
        /// <value>The menu column.</value>
        [Category("Behaviour")]
        [Description(
            "The menu column this description will be displayed in, this allows users to group descriptions into columnes."
            )]
        public int MenuColumn
        {
            get { return menuColumn; }
            set
            {
                if (menuColumn != value)
                {
                    menuColumn = value;
                    OnPropertyChanged("MenuColumn");
                }
            }
        }

        private int sequence;

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        [Category("Behaviour")]
        [Description("The sequence used to order the task description in the menus.")]
        public int Sequence
        {
            get { return sequence; }
            set
            {
                if (sequence != value)
                {
                    sequence = value;
                    OnPropertyChanged("Sequence");
                }
            }
        }

        private bool isEvent;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is event.
        /// </summary>
        /// <value><c>true</c> if this instance is event; otherwise, <c>false</c>.</value>
        [Description(
            "The IsEvent details if the TaskDescription is an activity (having a duration) or an Event (not having a duration)."
            )]
        [Category("Behaviour")]
        public bool IsEvent
        {
            get { return isEvent; }
            set
            {
                if (isEvent != value)
                {
                    isEvent = value;
                    OnPropertyChanged("IsEvent");
                }
            }
        }

        private bool isCategory;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is category.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is category; otherwise, <c>false</c>.
        /// </value>
        [Category("Behaviour")]
        [Description("A Task Description can also be designated a category and can be used in category views.")]
        public bool IsCategory
        {
            get { return isCategory; }
            set
            {
                if (isCategory != value)
                {
                    isCategory = value;
                    OnPropertyChanged("IsCategory");
                }
            }
        }

        private string url;

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [Category("CustomData")]
        [Description(
            "The Url can be used as a link to the project website or a wiki page, this provides an extensibility point for users of the system."
            )]
        public string Url
        {
            get { return url; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }

                if (value.Length > 0)
                {
                    if (Uri.IsWellFormedUriString(value, UriKind.Absolute) == false)
                    {
                        throw new InvalidOperationException(value + " is not a valid url");
                    }
                }
                if (url != value)
                {
                    url = value;
                    OnPropertyChanged("Url");
                }
            }
        }

        private DateTime validFromDate;

        /// <summary>
        /// Gets or sets the valid from date.
        /// </summary>
        /// <value>The valid from date.</value>
        [Category("Limits")]
        [Description(
            "The ValidFromDate can be used to limit the use of a task between valid dates; if this date is not set then valid dates are ignored."
            )]
        public DateTime ValidFromDate
        {
            get { return validFromDate; }
            set
            {
                if (validFromDate != value)
                {
                    validFromDate = value;
                    OnPropertyChanged("ValidFromDate");
                }
            }
        }

        private DateTime validToDate;

        /// <summary>
        /// Gets or sets the valid to date.
        /// </summary>
        /// <value>The valid to date.</value>
        [Category("Limits")]
        [Description(
            "The ValidToDate can be used to limit the use of a task between valid dates; if this date is not set then valid dates are ignored."
            )]
        public DateTime ValidToDate
        {
            get { return validToDate; }
            set
            {
                if (validToDate != value)
                {
                    validToDate = value;
                    OnPropertyChanged("ValidToDate");
                }
            }
        }

        private bool enabled;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TaskDescription"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [Category("Limits")]
        [Description("The enabled setting allows task descriptions to be temporary disabled for use.")]
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }

        /// <summary>
        /// Determines whether this instance is valid.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            return IsDateBetweenValidPeriod(DateTime.Now) && Enabled;
        }

        /// <summary>
        /// Determines whether date is between a valid period.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>
        /// 	<c>true</c> if date is between a valid period; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDateBetweenValidPeriod(DateTime dateTime)
        {
            bool result = true;
            if (ValidFromDate != ValidToDate)
            {
                if (!(ValidFromDate <= dateTime) || !(ValidToDate >= dateTime))
                {
                    result = false;
                }
            }
            return result;
        }

        private Collection<TaskDescription> children;

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>The children.</value>
        [Browsable(false)]
        public Collection<TaskDescription> Children
        {
            get
            {
                if (children == null)
                {
                    children = new Collection<TaskDescription>();
                }
                return children;
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} [{1}]", name, Color.Name);
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
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType()) return false;
            TaskDescription working = (TaskDescription)obj;
            return (Id == working.Id);
        }

        /// <summary>
        /// Compares the task description on sequence.
        /// </summary>
        /// <param name="taskDescriptionA">The task description A.</param>
        /// <param name="taskDescriptionB">The task description B.</param>
        /// <returns></returns>
        public static int CompareTaskDescriptionOnSequence(TaskDescription taskDescriptionA,
                                                           TaskDescription taskDescriptionB)
        {
            if (taskDescriptionA == null)
            {
                throw new ArgumentNullException("taskDescriptionA");
            }
            if (taskDescriptionB == null)
            {
                throw new ArgumentNullException("taskDescriptionB");
            }

            return taskDescriptionA.Sequence.CompareTo(taskDescriptionB.Sequence);
        }

        /// <summary>
        /// Compares the name of the task description on.
        /// </summary>
        /// <param name="taskDescriptionA">The task description A.</param>
        /// <param name="taskDescriptionB">The task description B.</param>
        /// <returns></returns>
        public static int CompareTaskDescriptionOnName(TaskDescription taskDescriptionA,
                                                       TaskDescription taskDescriptionB)
        {
            if (taskDescriptionA == null)
            {
                throw new ArgumentNullException("taskDescriptionA");
            }
            if (taskDescriptionB == null)
            {
                throw new ArgumentNullException("taskDescriptionB");
            }

            return taskDescriptionA.Name.CompareTo(taskDescriptionB.Name);
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return this.Equals(TaskDescription.Empty);
        }

        /// <summary>
        /// Determines whether this instance is not empty.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if is not empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsNotEmpty()
        {
            return !this.Equals(TaskDescription.Empty);
        }

        #region INotifyPropertyChanged Members

        private event PropertyChangedEventHandler Changed;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="property">The property.</param>
        protected void OnPropertyChanged(string property)
        {
            Changed?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Provides the public Property change event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { Changed += value; }
            remove { Changed -= value; }
        }

        #endregion

        /// <summary>
        /// Gets the unknown task description.
        /// </summary>
        /// <value>The unknown task description.</value>
        public static TaskDescription UnknownTaskDescription
        {
            get
            {
                TaskDescription result = new TaskDescription
                {
                    id = new Guid("AE73E2D8-C698-48ec-B125-C703A21DEA42"),
                    color = Color.Black,
                    description = "This task description is used when the system cannot find the requested task description, this could happen when a task description has been deleted.",
                    name = "Unknown",
                    Server = "Unknown"
                };
                return result;
            }
        }

    }
}