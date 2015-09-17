using System;

namespace BritishMicro.TaskClerk.Plugins
{
    /// <summary>
    /// The plugin Exporter provides the basic exporter features.
    /// </summary>
    [TaskClerkPlugin]
    public class PluginExporter : PluginComponent
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private TaskActivity[] _taskActivities;
        private TaskDescription[] _taskDescriptions;

        /// <summary>
        /// Default constructor for a Basic Exporter
        /// </summary>
        protected PluginExporter()
        {
        }

        /// <summary>
        /// This method get handed the start and end date selected by the exporter user
        /// and a copy of all the TaskActivities that fit into the users export selection
        /// and a copy of all the TaskDescriptions the export user selected
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="taskActivities"></param>
        /// <param name="taskDescriptions"></param>
        public void AcceptData(DateTime startDate,
                               DateTime endDate, TaskActivity[] taskActivities,
                               TaskDescription[] taskDescriptions)
        {
            _startDate = startDate;
            _endDate = endDate;
            _taskActivities = taskActivities;
            _taskDescriptions = taskDescriptions;
        }

        /// <summary>
        /// The date the export user selected as the start of the export
        /// </summary>
        public virtual DateTime StartDate
        {
            get { return _startDate; }
        }

        /// <summary>
        /// The date the export user selected as the end of the export
        /// </summary>
        public virtual DateTime EndDate
        {
            get { return _endDate; }
        }

        /// <summary>
        /// The array of task activities handed to the exporter
        /// </summary>
        public virtual TaskActivity[] ProvidedTaskActivities()
        {
            return _taskActivities;
        }

        /// <summary>
        /// The array of task description handed to the exporter
        /// </summary>
        public virtual TaskDescription[] ProvidedTaskDescriptions()
        {
            return _taskDescriptions;
        }

        /// <summary>
        /// This method is called by the export engine prior to executing the export,
        /// The export builder has the opertunity to discribe to the user
        /// what the export will do. If an empty string is returned the summary view 
        /// will not be displayed - Exporter builders can instanciate their own form
        /// at this point to describe what the exporter will do.
        /// </summary>
        /// <returns></returns>
        public virtual string BeforeExecuteSummary()
        {
            return string.Empty;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public virtual void Execute()
        {
            return;
        }
    }
}