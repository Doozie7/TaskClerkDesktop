//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.Xml.Serialization;
using BritishMicro.TaskClerk.Providers;

namespace BritishMicro.TaskClerk.Settings
{
    /// <summary>
    /// Provides the confighraion setion class
    /// </summary>
    [XmlRoot("TaskClerk.Configuration")]
    public sealed class ConfigurationSection
    {
        /// <summary>
        /// The configuration element used in the application configuration file.
        /// </summary>
        public const string SectionName = "TaskClerk.Configuration";

        private Collection<TaskClerkProvider> _providers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSection"/> class.
        /// </summary>
        public ConfigurationSection()
        {
        }

        /// <summary>
        /// Gets or sets the providers.
        /// </summary>
        /// <value>The providers.</value>
        [XmlArray("Providers")]
        [XmlArrayItem("Provider")]
        public Collection<TaskClerkProvider> Providers
        {
            get { return _providers; }
            set { _providers = value; }
        }
    }
}