//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 11th of May 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk.Settings
{
    /// <summary>
    /// Provides the custom section handler for the application configuration information
    /// </summary>
    internal sealed class SectionHandler : IConfigurationSectionHandler
    {
        /// <summary>
        /// Initalizes a new SectionHandler
        /// </summary>
        public SectionHandler()
        {
        }

        #region IConfigurationSectionHandler Members

        /// <summary>
        /// Called by the ConfigurationManager.GetSection method and creates
        /// the custom configuration object.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public object Create(object parent, object configContext, XmlNode section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("section");
            }

            ConfigurationSection config;

            using (Stream memory = new MemoryStream(Encoding.UTF8.GetBytes(section.OuterXml)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof (ConfigurationSection));
                config = (ConfigurationSection) serializer.Deserialize(memory);
            }

            //Debug.Assert(config == null, "The config section is null, this will cause configuration faults.");

            return config;
        }

        #endregion
    }
}