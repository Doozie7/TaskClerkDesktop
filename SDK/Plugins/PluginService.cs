//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 01st of Jan 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System.ComponentModel;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk.Plugins
{
    /// <summary>
    /// The plugin service provides TaskClerk with its basic plugin architecture.
    /// </summary>
    [TaskClerkPlugin]
    public abstract class PluginService
    {
        private TaskClerkEngine _engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginService"/> class.
        /// </summary>
        protected PluginService()
        {
        }

        /// <summary>
        /// The TaskClerk Engine
        /// </summary>
        /// <value>The TaskClerk engine.</value>
        [Browsable(false)]
        [XmlIgnore]
        public TaskClerkEngine Engine
        {
            get { return _engine; }
            set { _engine = value; }
        }

        /// <summary>
        /// The OnInit method provides implimenters with an opertunity to
        /// setup their services after a valid taskclerk engine has been asigned to
        /// the Engine property.
        /// </summary>
        protected virtual void OnStartup()
        {
        }

        /// <summary>
        /// The OnUnload method provides implimenters with an opertunity to
        /// shut down their services.
        /// </summary>
        protected virtual void OnShutdown()
        {
        }


        // The following methods are called by the SDK engine which intern call the 
        // StartUp and Shutdown functions

        /// <summary>
        /// Calls the OnStartup operation.
        /// </summary>
        protected internal void CallStartup()
        {
            OnStartup();
        }

        /// <summary>
        /// Calls the OnShutdown operation.
        /// </summary>
        protected internal void CallShutdown()
        {
            OnShutdown();
        }
    }
}