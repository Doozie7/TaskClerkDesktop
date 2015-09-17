//----------------------------------------------------------------------
//	Developed by:
//          BritishMicro
//          United Kingdom
//
//  Copyright (c) BritishMicro 2006
//
//  Date  : 30th November 2006
//	Author: Paul Jackson (paul.jackson@britishmicro.com)
//          Architect
//----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using BritishMicro.TaskClerk.Providers;

namespace BritishMicro.TaskClerk.InstantAccess
{
    /// <summary>
    /// Represents the data for the InstantAccessManager form.
    /// </summary>
    [XmlRoot("InstantAccess", Namespace = "http://britishmicro.com/2006/11/InstantAccess")]
    public class InstantAccessData : IXmlSerializable
    {
        private const string InstantAccessDataFileName = "IAData.xml";

        private List<TaskDescription> _pinnedTaskDescriptions;
        private bool _isDataAvailable;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstantAccessData"/> class.
        /// </summary>
        public InstantAccessData()
        {
            _pinnedTaskDescriptions = new List<TaskDescription>();
        }

        /// <summary>
        /// Creates an instance of the InstantAccessData class.
        /// </summary>
        /// <returns>A valid instance of the InstantAccessData class.</returns>
        public static InstantAccessData Create()
        {
            InstantAccessData target = new InstantAccessData();
            DirectoryInfo dataFolder = new DirectoryInfo(System.Windows.Forms.Application.UserAppDataPath);
            string path = Path.Combine(dataFolder.FullName, InstantAccessDataFileName);
            if (File.Exists(path))
            {
                using (Stream s = new FileStream(path, FileMode.OpenOrCreate))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof (InstantAccessData));
                    target = (InstantAccessData) serializer.Deserialize(s);
                    target._isDataAvailable = true;
                }
            }

            return target;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        private void Save()
        {
            DirectoryInfo dataFolder = new DirectoryInfo(System.Windows.Forms.Application.UserAppDataPath);
            string path = Path.Combine(dataFolder.FullName, InstantAccessDataFileName);
            using (Stream s = new FileStream(path, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof (InstantAccessData));
                serializer.Serialize(s, this);
                _isDataAvailable = true;
            }
        }

        /// <summary>
        /// This property is reserved, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"></see> to the class instead.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"></see> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"></see> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"></see> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"></see> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            TaskDescriptionsProvider provider = AppContext.Current.TaskDescriptionsProvider;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.LocalName)
                    {
                        case "TaskDescription":
                            AppendTaskDescription(reader, provider);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"></see> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            //<?xml version="1.0" encoding="utf-8"?>
            //<InstantAccess version="1.0" xmlns="http://britishmicro.com/2006/11/InstantAccess">
            //  <Configuration />
            //  <PinnedItems>
            //    <TaskDescription id="0f5c233a-fae1-4ddc-8d37-097a0e679b29" />
            //    <TaskDescription id="a9bb7c12-d14e-4c10-b4c2-1f9bc0e7a164" />
            //  </PinnedItems>
            //</InstantAccess>

            writer.WriteElementString("Configuration", string.Empty);
            writer.WriteStartElement("PinnedItems");
            foreach (TaskDescription description in _pinnedTaskDescriptions)
            {
                writer.WriteStartElement("TaskDescription");
                writer.WriteAttributeString("id", description.Id.ToString("D"));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        /// <summary>
        /// Appends the task description.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="provider">The provider.</param>
        private void AppendTaskDescription(XmlReader reader, TaskDescriptionsProvider provider)
        {
            Guid descriptionId = new Guid(reader.GetAttribute("id"));
            TaskDescription description = provider.FindDescription(descriptionId);
            if (description != null)
            {
                // TODO: Check for duplicates and remove them
                _pinnedTaskDescriptions.Add(description);
            }
        }

        /// <summary>
        /// Adds the pinned task description, duplicates are ignored.
        /// </summary>
        /// <param name="description">The description.</param>
        public void AddPinnedTaskDescription(TaskDescription description)
        {
            foreach (TaskDescription pinnedDescription in PinnedTaskDescriptions)
            {
                if (pinnedDescription.Id.Equals(description.Id))
                {
                    //  ...item already exists in the list
                    return;
                }
            }

            _pinnedTaskDescriptions.Add(description);
            Save();
        }

        /// <summary>
        /// Removes the pinned task description.
        /// </summary>
        /// <param name="description">The description.</param>
        public void RemovePinnedTaskDescription(TaskDescription description)
        {
            if (_pinnedTaskDescriptions.Contains(description))
            {
                _pinnedTaskDescriptions.Remove(description);
                Save();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is data available.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is data available; otherwise, <c>false</c>.
        /// </value>
        public bool IsDataAvailable
        {
            get { return _isDataAvailable; }
        }

        /// <summary>
        /// Gets the pinned task descriptions.
        /// </summary>
        /// <value>The pinned task descriptions.</value>
        public ReadOnlyCollection<TaskDescription> PinnedTaskDescriptions
        {
            get { return new ReadOnlyCollection<TaskDescription>(_pinnedTaskDescriptions); }
        }
    }
}