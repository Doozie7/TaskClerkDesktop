//----------------------------------------------------------------------
//	Developed by:
//          compilewith.net
//          United Kingdom
//
//  Copyright (c) compilewith.net 2007
//
//  Date  : 1st June 2007
//	Author: Paul Jackson (paul@compilewith.net)
//          Architect
//----------------------------------------------------------------------
using System;
using System.Collections.Generic;

using BritishMicro.TaskClerk.Plugins;
using BritishMicro.TaskClerk.Providers;
using System.Collections;

namespace BritishMicro.TaskClerk.InstantAccess
{
    /// <summary>
    /// Represents a service that monitiors the most recently used TaskDescriptions
    /// within the application. As a user changes activities the description being
    /// used is remembered and put to the top of the list, duplicates are removed.
    /// </summary>
    public class MostRecentlyUsedService : PluginService
    {
        private static MostRecentlyUsedService _current;

        private List<MostRecentlyUsedItem> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="MostRecentlyUsedService"/> class.
        /// </summary>
        public MostRecentlyUsedService()
        {
            MostRecentlyUsedService._current = this;
            this._items = new List<MostRecentlyUsedItem>();
        }

        /// <summary>
        /// Finds the top task <c>n</c> descriptions, where n is a value between
        /// <c>0</c> and <c>maximum</c>.
        /// </summary>
        /// <param name="maximum">The maximum.</param>
        /// <returns></returns>
        public static TaskDescription[] FindTopMostRecentlyUsed(int maximum)
        {
            MostRecentlyUsedService service = MostRecentlyUsedService._current;
            if(service == null)
                return new TaskDescription[] { };

            return service.InternalFindTopMostRecentlyUsed(maximum);
        }

        /// <summary>
        /// Finds the top most frequently used.
        /// </summary>
        /// <param name="maximum">The maximum.</param>
        /// <returns></returns>
        public static TaskDescription[] FindTopMostFrequentlyUsed(int maximum)
        {
            MostRecentlyUsedService service = MostRecentlyUsedService._current;
            if(service == null)
                return new TaskDescription[] { };

            return service.InternalFindTopMostFrequentlyUsed(maximum);
        }


        /// <summary>
        /// Internals the find top most recently used.
        /// </summary>
        /// <param name="maximum">The maximum.</param>
        /// <returns></returns>
        internal TaskDescription[] InternalFindTopMostRecentlyUsed(int maximum)
        {
            TaskDescriptionsProvider provider = this.Engine.TaskDescriptionsProvider;
            List<TaskDescription> localList = new List<TaskDescription>(maximum);

            foreach(MostRecentlyUsedItem mruItem in this._items)
            {
                TaskDescription description = provider.FindDescription(mruItem.Id);
                if(description != null)
                    localList.Add(description);

                if(localList.Count == maximum)
                    break;
            }

            return localList.ToArray();
        }

        /// <summary>
        /// Internals the find top most frequently used.
        /// </summary>
        /// <param name="maximum">The maximum.</param>
        /// <returns></returns>
        internal TaskDescription[] InternalFindTopMostFrequentlyUsed(int maximum)
        {
            TaskDescriptionsProvider provider = this.Engine.TaskDescriptionsProvider;
            List<TaskDescription> localList = new List<TaskDescription>(maximum);

            //  ...sort by usage
            MostRecentlyUsedItem[] items = this._items.ToArray();
            Array.Sort(items, MostRecentlyUsedItem.UsageComparer);

            foreach(MostRecentlyUsedItem mruItem in items)
            {
                TaskDescription description = provider.FindDescription(mruItem.Id);
                if(description != null)
                    localList.Add(description);

                if(localList.Count == maximum)
                    break;
            }

            return localList.ToArray();
        }

        /// <summary>
        /// The OnInit method provides implimenters with an opertunity to
        /// setup their services after a valid taskclerk engine has been asigned to
        /// the Engine property.
        /// </summary>
        protected override void OnStartup()
        {
            base.OnStartup();

            this._items = new List<MostRecentlyUsedItem>
                (this.Engine.SettingsProvider.GetTypedValue("MRUList", new MostRecentlyUsedItem[] { }));

            this.Engine.TaskActivitiesProvider.BeginningActivity
                += TaskActivitiesProvider_BeginningActivity;
        }

        /// <summary>
        /// The OnUnload method provides implimenters with an opertunity to
        /// shut down their services.
        /// </summary>
        protected override void OnShutdown()
        {
            this.Engine.TaskActivitiesProvider.BeginningActivity
                -= TaskActivitiesProvider_BeginningActivity;

            this.Engine.SettingsProvider.Set("MRUList", this._items.ToArray(), PersistHint.AcrossSessions);

            base.OnShutdown();
        }

        /// <summary>
        /// Handles the BeginningActivity event of the TaskActivitiesProvider control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.TaskActivityEventArgs"/> instance containing the event data.</param>
        void TaskActivitiesProvider_BeginningActivity(object sender, TaskActivityEventArgs e)
        {
            TaskDescription description = e.TaskActivity.TaskDescription;
            MostRecentlyUsedItem targetItem = this._items.Find(
                delegate(MostRecentlyUsedItem item)
                {
                    return item.Id.Equals(description.Id);
                });

            if(targetItem == null)
                targetItem = new MostRecentlyUsedItem(description.Id);

            this._items.Remove(targetItem);
            this._items.Insert(0, targetItem);
            targetItem.Usage++;
        }

        /// <summary>
        /// Represents a single MRU instance.
        /// </summary>
        [Serializable]
        private class MostRecentlyUsedItem
        {
            private static readonly IComparer<MostRecentlyUsedItem> _usageComparer = new MruUsageComparer();

            private Guid _id;
            private int _usage;

            /// <summary>
            /// Initializes a new instance of the <see cref="MostRecentlyUsedItem"/> class.
            /// </summary>
            public MostRecentlyUsedItem()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="MostRecentlyUsedItem"/> class.
            /// </summary>
            /// <param name="id">The id.</param>
            public MostRecentlyUsedItem(Guid id)
            {
                this._id = id;
            }

            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            /// <value>The id.</value>
            public Guid Id
            {
                get { return this._id; }
            }

            /// <summary>
            /// Gets or sets the usage.
            /// </summary>
            /// <value>The usage.</value>
            public int Usage
            {
                get { return this._usage; }
                set { this._usage = value; }
            }

            /// <summary>
            /// Gets the usage comparer.
            /// </summary>
            /// <value>The usage comparer.</value>
            public static IComparer<MostRecentlyUsedItem> UsageComparer
            {
                get { return _usageComparer; }
            }

            /// <summary>
            /// Represents a comparer capable of comparing two objects via Usage.
            /// This is used for sorting purposes not equality.
            /// </summary>
            private class MruUsageComparer : IComparer<MostRecentlyUsedItem>
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="UsageComparer"/> class.
                /// </summary>
                public MruUsageComparer()
                {
                }

                /// <summary>
                /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
                /// </summary>
                /// <param name="x">The first object to compare.</param>
                /// <param name="y">The second object to compare.</param>
                /// <returns>
                /// Value Condition Less than zerox is less than y.Zerox equals y.Greater than zerox is greater than y.
                /// </returns>
                public int Compare(MostRecentlyUsedItem x, MostRecentlyUsedItem y)
                {
                    MostRecentlyUsedItem lhs = (MostRecentlyUsedItem)x;
                    MostRecentlyUsedItem rhs = (MostRecentlyUsedItem)y;

                    return rhs.Usage - lhs.Usage;
                }
            }
        }
    }
}