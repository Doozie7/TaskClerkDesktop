//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Dec 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------
using System;

namespace BritishMicro.TaskClerk.Plugins
{
    /// <summary>
    /// The BasePlugin provides a hint for the plugin engine so that it does not
    /// load the plugin into the engine to do work, Plugins decorated with this attribute
    /// are use to provide classes of basic functionality
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TaskClerkPluginAttribute : Attribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskClerkPluginAttribute"/> class.
        /// </summary>
        public TaskClerkPluginAttribute()
        {
        }
    }
}