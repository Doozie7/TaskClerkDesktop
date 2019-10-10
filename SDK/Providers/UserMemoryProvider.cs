//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

// Note
// This provider allows the developer to store named value pairs in a local
// memory store called the users session memory, one of the key aspects of this store 
// is not to store null values.

using System;
using System.Collections;
using System.Diagnostics;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// The default UserMemoryProvider stores user settings in a hash table in memory.
    /// The default behaviour does not persist the stored settings between application
    /// sessions.
    /// </summary>
    public abstract class UserMemoryProvider : TaskClerkProvider, IEnumerable
    {
        private Hashtable _usersSessionMemory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMemoryProvider"/> class.
        /// </summary>
        protected UserMemoryProvider()
        {
            _usersSessionMemory = new Hashtable();
        }

        /// <summary>
        /// Gets the typed value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public virtual T GetTypedValue<T>(object key, T defaultValue)
        {
            return (T)Get(key, defaultValue);
        }

        /// <summary>
        /// Provides access to settings
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public virtual object Get(object key, object defaultValue)
        {
            if (key == null)
            {
                Trace.TraceError("The key cannot be null in the UserMemory Get method.");
                throw new InvalidOperationException(
                    "The Get method in the UserMemory provider does not allow the key property to be null.");
            }

            if (defaultValue == null)
            {
                Trace.TraceError("The key [{0}] cannot have its defaultValue set to null.", key);
                throw new InvalidOperationException(
                    "The Get method in the UserMemory provider does not allow the defaultValue property to be null.");
            }

            if ((_usersSessionMemory.ContainsKey(key) == false) && (_usersSessionMemory[key] == null))
            {
                _usersSessionMemory[key] = defaultValue;
                InternalGetComplete(key, _usersSessionMemory[key]);
            }

            return _usersSessionMemory[key];
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public virtual object Get(object key)
        {
            if (key == null)
            {
                Trace.TraceError("The key cannot be null in the UserMemory Get method.");
                throw new InvalidOperationException(
                    "The Get method in the UserMemory provider does not allow the key property to be null.");
            }

            if (_usersSessionMemory.ContainsKey(key) == false)
            {
                Trace.TraceError("The key [{0}] was not found in the UserMemory store.", key);
                throw new InvalidOperationException("A requested UserMemory key was not found in the UserMemory store.");
            }

            //InternalGetComplete(key, _usersSessionMemory[key]);

            return _usersSessionMemory[key];
        }

        /// <summary>
        /// Tries to get a specified key value based on the TryParse pattern.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGet<T>(object key, out T value) where T : class
        {
            value = null;

            if (key == null || !_usersSessionMemory.ContainsKey(key))
            {
                return false;
            }

            value = (T)Get(key);
            return true;
        }

        /// <summary>
        /// Get Event is raised after the data is collected from the data store
        /// but befor it is returned to the caller.
        /// </summary>
        public event EventHandler<InformationEventArgs> GetComplete;

        /// <summary>
        /// The internal GetComplete, raise the SettingGetComplete event.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="keyValue">The key value.</param>
        private void InternalGetComplete(object key, object keyValue)
        {
            //GetComplete Event
            GetComplete?.Invoke(this, new InformationEventArgs("Information", key.ToString(), keyValue));
        }

        /// <summary>
        /// Allows settings to be stored in the UsersSessionMemory,
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="hint">The persistance hint.</param>
        public virtual void Set(object key, object value, PersistHint hint)
        {
            if (key == null)
            {
                Trace.TraceError("The key cannot be null in the UserMemory Set method.");
                throw new InvalidOperationException(
                    "The Set method in the UserMemory provider does not allow the key property to be null.");
            }

            if (value == null)
            {
                Trace.TraceError("The key [{0}] cannot have its value set to null.", key);
                throw new InvalidOperationException(
                    "The Set method in the UserMemory provider does not allow the value property to be null.");
            }

            InternalSetBegin(key, value);

            if (_usersSessionMemory.ContainsKey(key))
            {
                _usersSessionMemory[key] = value;
            }
            else
            {
                _usersSessionMemory.Add(key, value);
            }
        }

        /// <summary>
        /// Set Event is raised before data is set in the data store
        /// </summary>
        public event EventHandler<InformationEventArgs> SetBegin;

        /// <summary>
        /// The internal SetComplete, raise the SettingGetComplete event.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="keyValue">The key value.</param>
        private void InternalSetBegin(object key, object keyValue)
        {
            //SetCommence Event
            SetBegin?.Invoke(this, new InformationEventArgs("Information", key.ToString(), keyValue));
        }

        /// <summary>
        /// Gets or users session memory.
        /// </summary>
        /// <value>The users session memory.</value>
        protected Hashtable UsersSessionMemory
        {
            get { return _usersSessionMemory; }
        }

        /// <summary>
        /// Replaces the user session memory.
        /// </summary>
        /// <param name="hash">The hash.</param>
        protected void ReplaceUsersSessionMemory(Hashtable hash)
        {
            if (hash != null)
            {
                Trace.TraceInformation("The user session memory hash has been replaced");
                _usersSessionMemory = hash;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this._usersSessionMemory.GetEnumerator();
        }
    }
}