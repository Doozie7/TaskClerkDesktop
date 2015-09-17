using System;
using System.Collections.Generic;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Provided by EG
    /// </summary>
    public sealed class EventSet
    {
        // The private dictionary used to maintain EventKey -> Delegate mappings
        private Dictionary<object, Delegate> m_events =
            new Dictionary<object, Delegate>();

        // Adds an EventKey -> Delegate mapping if it doesn't exist or 
        // combines a delegate to an existing EventKey
        /// <summary>
        /// Adds the specified event key.
        /// </summary>
        /// <param name="eventKey">The event key.</param>
        /// <param name="handler">The handler.</param>
        public void Add(object eventKey, Delegate handler)
        {
            lock (m_events)
            {
                Delegate d;
                m_events.TryGetValue(eventKey, out d);
                m_events[eventKey] = Delegate.Combine(d, handler);
            }
        }

        // Removes a delegate from an EventKey (if it exists) and 
        // removes the EventKey -> Delegate mapping the last delegate is removed
        /// <summary>
        /// Removes the specified event key.
        /// </summary>
        /// <param name="eventKey">The event key.</param>
        /// <param name="handler">The handler.</param>
        public void Remove(object eventKey, Delegate handler)
        {
            lock (m_events)
            {
                // Do not throw an exception if attempting to remove 
                // a delegate from an EventKey not in the set
                Delegate d;
                if (m_events.TryGetValue(eventKey, out d))
                {
                    d = Delegate.Remove(d, handler);

                    // If a delegate remains, set the new head else remove the EventKey
                    if (d != null) m_events[eventKey] = d;
                    else m_events.Remove(eventKey);
                }
            }
        }

        // Raies the event for the indicated EventKey
        /// <summary>
        /// Raises the specified event key.
        /// </summary>
        /// <param name="eventKey">The event key.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void Raise(object eventKey, Object sender, EventArgs e)
        {
            // Don't throw an exception if the EventKey is not in the set
            Delegate d;
            lock (m_events)
            {
                m_events.TryGetValue(eventKey, out d);
            }
            if (d != null)
            {
                // Because the dictionary can contain several different delegate types,
                // it is impossible to construct a type-safe call to the delegate at 
                // compile time. So, I call the System.Delegate type’s DynamicInvoke 
                // method, passing it the callback method’s parameters as an array of 
                // objects. Internally, DynamicInvoke will check the type safety of the 
                // parameters with the callback method being called and call the method.
                // If there is a type mismatch, then DynamicInvoke will throw an exception.
                d.DynamicInvoke(new Object[] {sender, e});
            }
        }
    }
}