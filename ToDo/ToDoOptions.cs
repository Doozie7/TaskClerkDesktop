using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BritishMicro.TaskClerk.ToDoPlugin
{
    [Serializable]
    public class ToDoOptions
    {

        private string _reminderStrings;
        private string _groupStrings;
        private TaskDescription _defaultTaskDescription;

        public ToDoOptions()
        {
            if (string.IsNullOrEmpty(_reminderStrings))
            {
                _reminderStrings = "Start by;End by;Should Start by;Should End by;Milestone;None";
            }
            if (string.IsNullOrEmpty(_groupStrings))
            {
                _groupStrings = "Over Due;Due Today;Due in the future, No due date";
            }
            _defaultTaskDescription = TaskDescription.Empty;
        }

        public ToDoOptions(ToDoOptions fromOptions)
        {
            if (fromOptions != null)
            {
                _reminderStrings = fromOptions.ReminderStrings;
                _groupStrings = fromOptions.GroupStrings;
                _defaultTaskDescription = fromOptions.DefaultTaskDescription;
            }            
        }

        public string ReminderStrings
        {
            get { return _reminderStrings; }
            set { _reminderStrings = value; }
        }

        

        public string GroupStrings
        {
            get { return _groupStrings; }
            set { _groupStrings = value; }
        }

        public TaskDescription DefaultTaskDescription
        {
            get { return _defaultTaskDescription; }
            set { _defaultTaskDescription = value; }
        }
	
	
	
    }
}
        