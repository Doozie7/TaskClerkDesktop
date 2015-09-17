//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TaskDescriptionsProvider : TaskClerkProvider
    {

        private Collection<TaskDescription> _taskDescriptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDescriptionsProvider"/> class.
        /// </summary>
        protected TaskDescriptionsProvider()
        {
            _taskDescriptions = new Collection<TaskDescription>();
        }

        /// <summary>
        /// This method provides the implimenter of a TaskDescriptionsProvider the first access 
        /// to the TaskClerk Engine, and should be where most of the providers setup should
        /// happen.
        /// </summary>
        protected override void OnInit()
        {
            base.OnInit();
        }

        /// <summary>
        /// When overridden in a provider, it loads and returns the descriptions.
        /// </summary>
        /// <returns></returns>
        protected abstract Collection<TaskDescription> ProviderLoadDescriptions();

        /// <summary>
        /// When overridden in a provider, it saves the descriptions.
        /// </summary>
        /// <param name="descriptions">The descriptions.</param>
        protected abstract void ProviderSaveDescriptions(Collection<TaskDescription> descriptions);

        /// <summary>
        /// When overridden in a provider, adds a description.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        /// <param name="parent">The parent.</param>
        protected abstract void ProviderAddDescription(TaskDescription taskDescription, TaskDescription parent);

        /// <summary>
        /// When overridden in a provider, removes a description.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        protected abstract void ProviderRemoveDescription(TaskDescription taskDescription);

        /// <summary>
        /// When overridden in a provider, archives the descriptions.
        /// </summary>
        protected abstract void ProviderArchive();

        #region Properties

        /// <summary>
        /// The collection of available task descriptions
        /// </summary>
        public Collection<TaskDescription> TaskDescriptions
        {
            get { return _taskDescriptions; }
            protected set { _taskDescriptions = value; }
        }

        #endregion

        /// <summary>
        /// Loads all items.
        /// </summary>
        public void LoadDescriptions()
        {
            TaskDescriptionsEventArgs tdea = new TaskDescriptionsEventArgs(ProviderLoadDescriptions());
            OnLoadingAllItemsEvent(tdea);
            if (tdea.Cancel == false)
            {
                TaskDescriptions = tdea.TaskDescriptions;
            }
            OnTaskDescriptionsChangedEvent(EventArgs.Empty);
        }

        /// <summary>
        /// Saves all items.
        /// </summary>
        public void SaveDescriptions()
        {
            TaskDescriptionsEventArgs tdea = new TaskDescriptionsEventArgs(TaskDescriptions);
            OnSavingAllItemsEvent(tdea);
            if (tdea.Cancel == false)
            {
                ProviderSaveDescriptions(tdea.TaskDescriptions);
            }
            OnTaskDescriptionsChangedEvent(EventArgs.Empty);
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        /// <param name="parent">The parent.</param>
        public void AddDescription(TaskDescription taskDescription, TaskDescription parent)
        {
            TaskDescriptionEventArgs tdea = new TaskDescriptionEventArgs(taskDescription, parent);
            OnAddingItemEvent(new TaskDescriptionEventArgs(taskDescription, parent));
            if (tdea.Cancel == false)
            {
                parent.Children.Add(taskDescription);
            }
            OnTaskDescriptionsChangedEvent(EventArgs.Empty);
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="description">The description.</param>
        public void RemoveDescription(TaskDescription description)
        {
            TaskDescription parent = null;
            foreach(TaskDescription child in TaskDescriptions)
            {
                parent = FindParentInHierarchy(child, description);
                if (parent != null)
                {
                    break;
                }
            }
            if (parent != null)
            {
                TaskDescriptionEventArgs tdea = new TaskDescriptionEventArgs(description, parent);
                OnRemovingItemEvent(tdea);
                if (tdea.Cancel == false)
                {
                    parent.Children.Remove(description);
                    ProviderRemoveDescription(description);
                }
                OnTaskDescriptionsChangedEvent(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Finds the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public TaskDescription FindDescription(Guid id)
        {
            TaskDescription test = new TaskDescription();
            test.Id = id;
            test = InternalFindById(test, TaskDescriptions);
            if (test == null)
            {
                test = TaskDescription.UnknownTaskDescription;
            }
            return test;
        }

        /// <summary>
        /// Finds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public TaskDescription FindDescription(string name)
        {
            TaskDescription test = new TaskDescription();
            test.Name = name;
            test = InternalFindByName(test, TaskDescriptions);
            if (test == null)
            {
                test = TaskDescription.UnknownTaskDescription;
            }
            return test;
        }

        /// <summary>
        /// Internals the find by id.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <param name="taskDescriptions">The task descriptions.</param>
        /// <returns></returns>
        private static TaskDescription InternalFindById(
            TaskDescription test, Collection<TaskDescription> taskDescriptions)
        {
            TaskDescription result = null;
            foreach (TaskDescription child in taskDescriptions)
            {
                if (result != null)
                {
                    return result;
                }
                else
                {
                    if (child.Id == test.Id)
                    {
                        return child;
                    }
                    result = InternalFindById(test, child.Children);
                }
            }
            return result;
        }

        /// <summary>
        /// Internals the name of the find by.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <param name="taskDescriptions">The task descriptions.</param>
        /// <returns></returns>
        protected static TaskDescription InternalFindByName(
            TaskDescription test, Collection<TaskDescription> taskDescriptions)
        {
            TaskDescription result = null;
            foreach (TaskDescription child in taskDescriptions)
            {
                if (result != null)
                {
                    return result;
                }
                else
                {
                    if (child.Name == test.Name)
                    {
                        return child;
                    }
                    result = InternalFindById(test, child.Children);
                }
            }
            return result;
        }

        /// <summary>
        /// Finds a TaskDescription in the hierarchy using the Id.
        /// </summary>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static TaskDescription FindChildInHierarchy(Collection<TaskDescription> descriptions, Guid id)
        {
            TaskDescription toFind = new TaskDescription();
            toFind.Id = id;
            return FindChildInHierarchy(descriptions, toFind);
        }

        /// <summary>
        /// Finds the TaskDescription in the hierarchy using the Id.
        /// </summary>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static TaskDescription FindChildInHierarchy(Collection<TaskDescription> descriptions, TaskDescription description)
        {
            TaskDescription result = null;
            foreach (TaskDescription child in descriptions)
            {
                if (result != null)
                {
                    return result;
                }
                else
                {
                    if (child.Id == description.Id)
                    {
                        return child;
                    }
                    result = FindChildInHierarchy(child.Children, description);
                }
            }
            return result;
        }

        /// <summary>
        /// Finds the description in hierarchy.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static TaskDescription FindParentInHierarchy(TaskDescription parent, TaskDescription description)
        {
            TaskDescription result = null;
            foreach (TaskDescription child in parent.Children)
            {
                if (result != null)
                {
                    return result;
                }
                else
                {
                    if (child.Id == description.Id)
                    {
                        return parent;
                    }
                    result = FindParentInHierarchy(child, description);
                }
            }
            return result;
        }

        /// <summary>
        /// Ancestorses the specified task description.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        /// <returns></returns>
        public Queue<TaskDescription> Ancestors(TaskDescription taskDescription)
        {
            Queue<TaskDescription> ancestors = new Queue<TaskDescription>();
            InternalFindAndQueue(taskDescription, TaskDescriptions, ancestors);
            return ancestors;
        }

        /// <summary>
        /// Internals the find and queue.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="queue">The queue.</param>
        /// <returns></returns>
        private static bool InternalFindAndQueue(
            TaskDescription taskDescription,
            Collection<TaskDescription> descriptions,
            Queue<TaskDescription> queue)
        {
            bool result = false;
            foreach (TaskDescription child in descriptions)
            {
                if (child.Equals(taskDescription))
                {
                    //queue.Enqueue(child);
                    return true;
                }
                else
                {
                    result = InternalFindAndQueue(taskDescription, child.Children, queue);
                    if (result == true)
                    {
                        queue.Enqueue(child);
                        return result;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Categories from task description.
        /// </summary>
        /// <param name="child">The child.</param>
        /// <returns></returns>
        public TaskDescription CategoryFromTaskDescription(TaskDescription child)
        {
            TaskDescription result = null;
            Stack<TaskDescription> stack = new Stack<TaskDescription>();
            FindAndStack(child, TaskDescriptions, stack);
            foreach (TaskDescription td in stack.ToArray())
            {
                if (td.IsCategory)
                {
                    result = td;
                }
            }
            if (result == null)
            {
                result = TaskDescription.UnknownTaskDescription;
            }
            return result;
        }

        /// <summary>
        /// Finds the and stack.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        /// <param name="descriptions">The descriptions.</param>
        /// <param name="stack">The stack.</param>
        /// <returns></returns>
        private static bool FindAndStack(TaskDescription taskDescription,
                                  Collection<TaskDescription> descriptions,
                                  Stack<TaskDescription> stack)
        {
            bool result = false;
            foreach (TaskDescription child in descriptions)
            {
                if (child.Equals(taskDescription))
                {
                    stack.Push(child);
                    return true;
                }
                else
                {
                    result = FindAndStack(taskDescription, child.Children, stack);
                    if (result == true)
                    {
                        stack.Push(child);
                        return result;
                    }
                }
            }
            return result;
        }

        #region Load All TaskDescriptions Event

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TaskDescriptionsEventArgs> LoadingAllItems;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnLoadingAllItemsEvent(TaskDescriptionsEventArgs e)
        {
            EventHandler<TaskDescriptionsEventArgs> handler = LoadingAllItems;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Save All TaskDescriptions Event

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TaskDescriptionsEventArgs> SavingAllItems;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnSavingAllItemsEvent(TaskDescriptionsEventArgs e)
        {
            EventHandler<TaskDescriptionsEventArgs> handler = SavingAllItems;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Add Item TaskDescription Event

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TaskDescriptionEventArgs> AddingItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnAddingItemEvent(TaskDescriptionEventArgs e)
        {
            EventHandler<TaskDescriptionEventArgs> handler = AddingItem;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Remove Item TaskDescription Event

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TaskDescriptionEventArgs> RemovingItem;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void OnRemovingItemEvent(TaskDescriptionEventArgs e)
        {
            EventHandler<TaskDescriptionEventArgs> handler = RemovingItem;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Task Descriptions Changed Event

        /// <summary>
        /// This event is fired when TaskDescriptions are sent to the data store
        /// </summary>
        public event EventHandler<EventArgs> TaskDescriptionsChangedEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTaskDescriptionsChangedEvent(EventArgs e)
        {
            EventHandler<EventArgs> handler = TaskDescriptionsChangedEvent;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

    }
}