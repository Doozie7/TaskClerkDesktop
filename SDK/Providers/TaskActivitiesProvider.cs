//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// Provides the foundation for all TaskActivity prividers
    /// </summary>
    public abstract class TaskActivitiesProvider : TaskClerkProvider
    {

        /// <summary>
        /// When overridden in a provider, it discovers the date metrics stored in the users data area.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>
        protected abstract Collection<DateTime> ProviderDiscoverDateMetrics(MetricQuestion question);

        /// <summary>
        /// When overridden in a provider, it loads and returns the activities based on the supplied parameters.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="allowedTaskDescriptions">The allowed task descriptions.</param>
        /// <returns></returns>
        protected abstract Collection<TaskActivity> ProviderLoadActivities(DateTime start, DateTime end, Collection<TaskDescription> allowedTaskDescriptions);

        /// <summary>
        /// When overridden in a provider, it saves the activities.
        /// </summary>
        /// <param name="activities">The activities.</param>
        protected abstract void ProviderSaveActivities(Collection<TaskActivity> activities);

        /// <summary>
        /// When overridden in a provider, it adds an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected abstract void ProviderBeginActivity(TaskActivity activity);

        /// <summary>
        /// When overridden in a provider, it completes an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected abstract void ProviderCompleteActivity(TaskActivity activity);


        /// <summary>
        /// When overridden in a provider, it removes an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected abstract void ProviderRemoveActivity(TaskActivity activity);

        /// <summary>
        /// When overridden in a provider, it performs an archive action on the activity data.
        /// </summary>
        protected abstract void ProviderArchive();


        /// <summary>
        /// Initializes a new instance of the <see cref="T:TaskActivityProvider"/> class.
        /// </summary>
        protected TaskActivitiesProvider()
        {
        }

        /// <summary>
        /// Discover data store metrics, eg what days are stored in the data store.
        /// </summary>
        /// <returns></returns>
        public Collection<DateTime> DiscoverDateMetrics(MetricQuestion question)
        {
            TaskActivityDiscoverDateMetricsEventArgs tasdea 
                = new TaskActivityDiscoverDateMetricsEventArgs(ProviderDiscoverDateMetrics(question));
            OnDiscoveringDateMetricsEvent(tasdea);
            return tasdea.StoredDays;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum MetricQuestion
        {
            /// <summary>
            /// No operation
            /// </summary>
            Noop = 0,
            /// <summary>
            /// Return the available days in the data store
            /// </summary>
            AvailableDays = 1,
            /// <summary>
            /// Return the available months in the data store
            /// </summary>
            AvailableMonths = 2,
            /// <summary>
            /// Return the available years in the data store
            /// </summary>
            AvailableYears = 3,
        }

        /// <summary>
        /// Loads the activities for a day.
        /// </summary>
        /// <param name="focusDay">The focus day.</param>
        /// <returns></returns>
        public Collection<TaskActivity> LoadActivities(DateTime focusDay)
        {
            DateTime start = new DateTime(focusDay.Year, focusDay.Month, focusDay.Day, 0, 0, 1);
            DateTime end = new DateTime(focusDay.Year, focusDay.Month, focusDay.Day, 23, 59, 59);
            return LoadActivities(start, end);
        }

        /// <summary>
        /// Loads the activities for a period.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public Collection<TaskActivity> LoadActivities(DateTime start, DateTime end)
        {
            return LoadActivities(start, end, new Collection<TaskDescription>());
        }

        /// <summary>
        /// Loads the activities for a period.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="allowedTaskDescriptions">The allowed task descriptions.</param>
        /// <returns></returns>
        public Collection<TaskActivity> LoadActivities(DateTime start, DateTime end, Collection<TaskDescription> allowedTaskDescriptions)
        {
            if (allowedTaskDescriptions == null)
            {
                throw new ArgumentNullException("allowedTaskDescriptions");
            }

            DateTime timerStart = DateTime.Now;
            Collection<TaskActivity> activities = ProviderLoadActivities(start, end, allowedTaskDescriptions);
            RaiseTimingEvent(MethodInfo.GetCurrentMethod(), timerStart);
            
            TaskActivityLoadEventArgs talea = new TaskActivityLoadEventArgs(start, end, allowedTaskDescriptions, activities);
            OnLoadingActivitiesEvent(talea);            
            return activities;
        }

        /// <summary>
        /// Saves the activities.
        /// </summary>
        /// <param name="activities">The activities.</param>
        public void SaveActivities(Collection<TaskActivity> activities)
        {
            if (activities == null)
            {
                throw new ArgumentNullException("activities");
            }

            TaskActivitySaveEventArgs tasea = new TaskActivitySaveEventArgs(activities);
            OnSavingActivitiesEvent(new TaskActivitySaveEventArgs(activities));
            
            DateTime timerStart = DateTime.Now;
            ProviderSaveActivities(tasea.TaskActivities);
            RaiseTimingEvent(MethodInfo.GetCurrentMethod(), timerStart);

            OnTaskActivitiesChangedEvent(TaskActivityEventArgs.Empty);
        }

        /// <summary>
        /// Starts an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void BeginActivity(TaskActivity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }
            TaskActivityEventArgs taea = new TaskActivityEventArgs(activity);
            OnBeginActivityEvent(taea);
            if (taea.Cancel == false)
            {
                DateTime timerStart = DateTime.Now;
                ProviderBeginActivity(taea.TaskActivity);
                RaiseTimingEvent(MethodInfo.GetCurrentMethod(), timerStart);

                OnTaskActivitiesChangedEvent(taea);
            }
        }

        /// <summary>
        /// Completes the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        public void CompleteActivity(TaskActivity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }
            CompleteActivity(activity, activity.StartDate);
        }

        /// <summary>
        /// Completes the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="effectiveDate">The effective date.</param>
        public void CompleteActivity(TaskActivity activity, DateTime effectiveDate)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            TaskActivityEventArgs taea = new TaskActivityEventArgs(activity, effectiveDate);
            OnCompletingActivityEvent(taea);
            if (taea.Cancel == false)
            {
                DateTime timerStart = DateTime.Now;
                ProviderCompleteActivity(taea.TaskActivity);
                RaiseTimingEvent(MethodInfo.GetCurrentMethod(), timerStart);

                OnTaskActivitiesChangedEvent(new TaskActivityEventArgs(activity));
            }
        }

        /// <summary>
        /// Removes an activity.
        /// </summary>
        /// <param name="activity"></param>
        public void RemoveActivity(TaskActivity activity)
        {
            TaskActivityEventArgs taea = new TaskActivityEventArgs(activity);
            OnRemovingActivityEvent(taea);
            if (taea.Cancel == false)
            {
                DateTime timerStart = DateTime.Now;
                ProviderRemoveActivity(activity);
                RaiseTimingEvent(MethodInfo.GetCurrentMethod(), timerStart);

                OnTaskActivitiesChangedEvent(new TaskActivityEventArgs(activity));
            }
        }

        /// <summary>
        /// Backs up the task activities.
        /// </summary>
        /// <returns></returns>
        public bool Backup()
        {
            OnBackingUpTaskActivitiesEvent(EventArgs.Empty);

            DateTime timerStart = DateTime.Now;
            ProviderArchive();
            RaiseTimingEvent(MethodInfo.GetCurrentMethod(), timerStart);

            return true;
        }

        /// <summary>
        /// Backs up the taskactivities.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <param name="lastBackup">The last backup.</param>
        /// <returns></returns>
        public bool Backup(string frequency, DateTime lastBackup)
        {
            bool backupOccurred = false;
            TimeSpan diff = DateTime.Now - lastBackup;
            switch (frequency)
            {
                case "Daily":
                    if (diff.TotalDays > 1)
                    {
                        backupOccurred = Backup();
                    }
                    break;
                case "Weekly":
                    if (diff.TotalDays > 7)
                    {
                        backupOccurred = Backup();
                    }
                    break;
                case "Monthly":
                    if ((lastBackup.Year < DateTime.Now.Year) && (lastBackup.Month < DateTime.Now.Month))
                    {
                        backupOccurred = Backup();
                    }
                    break;
                default:
                    break;
            }
            return backupOccurred;
        }

        #region Backup Event

        /// <summary>
        /// This event is fired when activities are being backed up
        /// </summary>
        public event EventHandler<EventArgs> BackinUpTaskActivities;

        /// <summary>
        /// Raises the <see cref="E:BackingUpTaskActivitiesEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnBackingUpTaskActivitiesEvent(EventArgs e)
        {
            EventHandler<EventArgs> handler = BackinUpTaskActivities;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region OnDiscoveringDateMetrics Event

        /// <summary>
        /// This event is fired when discovering activities
        /// </summary>
        public event EventHandler<TaskActivityDiscoverDateMetricsEventArgs> DiscoveringDateMetrics;

        /// <summary>
        /// Raises the <see cref="E:DiscoveringDateMetricsEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.TaskActivityDiscoverDateMetricsEventArgs"/> instance containing the event data.</param>
        private void OnDiscoveringDateMetricsEvent(TaskActivityDiscoverDateMetricsEventArgs e)
        {
            EventHandler<TaskActivityDiscoverDateMetricsEventArgs> handler = DiscoveringDateMetrics;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Load TaskActivities Event

        /// <summary>
        /// This event is fired when loading activities
        /// </summary>
        public event EventHandler<TaskActivityLoadEventArgs> LoadingActivities;

        /// <summary>
        /// Raises the <see cref="E:LoadingActivitiesEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.TaskActivityLoadEventArgs"/> instance containing the event data.</param>
        private void OnLoadingActivitiesEvent(TaskActivityLoadEventArgs e)
        {
            EventHandler<TaskActivityLoadEventArgs> handler = LoadingActivities;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Save TaskActivities Event

        /// <summary>
        /// This event is fired when saving activities
        /// </summary>
        public event EventHandler<TaskActivitySaveEventArgs> SavingActivities;

        /// <summary>
        /// Raises the <see cref="E:SavingActivitiesEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.TaskActivitySaveEventArgs"/> instance containing the event data.</param>
        private void OnSavingActivitiesEvent(TaskActivitySaveEventArgs e)
        {
            EventHandler<TaskActivitySaveEventArgs> handler = SavingActivities;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Starts an TaskActivity

        /// <summary>
        /// This event is fired when beginning activities
        /// </summary>
        public event EventHandler<TaskActivityEventArgs> BeginningActivity;

        /// <summary>
        /// Raises the <see cref="E:StartingActivityEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.TaskActivityEventArgs"/> instance containing the event data.</param>
        private void OnBeginActivityEvent(TaskActivityEventArgs e)
        {
            EventHandler<TaskActivityEventArgs> handler = BeginningActivity;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Completing TaskActivity Event

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TaskActivityEventArgs> CompletingActivity;

        /// <summary>
        /// Raises the <see cref="E:CompletingActivityEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.TaskActivityEventArgs"/> instance containing the event data.</param>
        private void OnCompletingActivityEvent(TaskActivityEventArgs e)
        {
            EventHandler<TaskActivityEventArgs> handler = CompletingActivity;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Removing TaskActivity Event

        /// <summary>
        /// This event is fired when removing activities
        /// </summary>
        public event EventHandler<TaskActivityEventArgs> RemovingActivity;

        /// <summary>
        /// Raises the <see cref="E:RemovingActivityEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.TaskActivityEventArgs"/> instance containing the event data.</param>
        private void OnRemovingActivityEvent(TaskActivityEventArgs e)
        {
            EventHandler<TaskActivityEventArgs> handler = RemovingActivity;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region TaskActivities Changed Event

        /// <summary>
        /// This event is fired when TaskActivities are sent to the data store
        /// </summary>
        public event EventHandler<TaskActivityEventArgs> TaskActivitiesChanged;

        /// <summary>
        /// Raises the <see cref="E:TaskActivitiesChangedEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.TaskActivityEventArgs"/> instance containing the event data.</param>
        private void OnTaskActivitiesChangedEvent(TaskActivityEventArgs e)
        {
            EventHandler<TaskActivityEventArgs> handler = TaskActivitiesChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        /// <summary>
        /// Rollups the by category.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static Collection<TaskActivity> RollupByTaskDescription(Collection<TaskActivity> collection)
        {
            Collection<TaskActivity> rollup = new Collection<TaskActivity>();
            foreach (TaskActivity ta in collection)
            {
                bool found = false;
                foreach (TaskActivity rta in rollup)
                {
                    if (rta.TaskDescription != null)
                    {
                        if (rta.TaskDescription.Name == ta.TaskDescription.Name)
                        {
                            rta.Duration = rta.Duration + ta.Duration;
                            found = true;
                            break;
                        }
                    }
                }
                if ((found == false) && (ta.Duration > 0))
                {
                    rollup.Add(ta);
                }
            }
            return rollup;
        }
    }
}