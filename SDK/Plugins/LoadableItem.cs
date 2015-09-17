//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.IO;

namespace BritishMicro.TaskClerk.Plugins
{
    /// <summary>
    /// Provides quick access to loaded types
    /// </summary>
    [Serializable]
    public class LoadableItem
    {
        private FileInfo _assemblyFile;
        private RuntimeTypeHandle _typeHandle;
        private bool _enabled;
        private DateTime _loadTime;
        /// <summary>
        /// description
        /// </summary>
        private string _description;
        /// <summary>
        /// local display name
        /// </summary>
        private string _displayName;

        /// <summary>
        /// Hidden constructor
        /// </summary>
        private LoadableItem()
        {
        }

        /// <summary>
        /// Initanises a new instance of LoadableItem
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="type"></param>
        /// <param name="enabled"></param>
        public LoadableItem(FileInfo assemblyFile, Type type, bool enabled)
        {
            if (assemblyFile == null)
            {
                throw new ArgumentNullException("assemblyFile");
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            _assemblyFile = assemblyFile;
            _typeHandle = type.TypeHandle;
            _enabled = enabled;
            _loadTime = DateTime.Now;

            DescriptionAttribute[] descriptions =
                type.GetCustomAttributes(typeof (DescriptionAttribute), false) as DescriptionAttribute[];

            if (descriptions != null)
            {
                if (descriptions.Length == 1)
                {
                    _description = descriptions[0].Description;
                }
            }

            DisplayNameAttribute[] diaplayNames =
                type.GetCustomAttributes(typeof(DisplayNameAttribute), false) as DisplayNameAttribute[];

            if (diaplayNames != null)
            {
                if (diaplayNames.Length == 1)
                {
                    _displayName = diaplayNames[0].DisplayName;
                }
            }
        }

        /// <summary>
        /// The assembly file name where this type was loaded from
        /// </summary>
        public FileInfo AssemblyFile
        {
            get { return _assemblyFile; }
        }

        /// <summary>
        /// The type
        /// </summary>
        /// <returns>Type</returns>
        public Type ReturnType()
        {
            if (_enabled == false)
            {
                throw new InvalidOperationException("Plugin not enabled");
            }
            return Type.GetTypeFromHandle(_typeHandle);
        }

        /// <summary>
        /// Is the plugin enabled
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        /// <summary>
        /// The date and time the type was loaded by .Net
        /// </summary>
        public DateTime LoadTime
        {
            get { return _loadTime; }
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get { return _displayName; }
        }

        /// <summary>
        /// Helper for creating an instance of the loadable type
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object CreateInstance(params object[] parameters)
        {
            if (_enabled == false)
            {
                throw new InvalidOperationException("Plugin not enabled");
            }
            return Activator.CreateInstance(ReturnType(), parameters);
        }

        /// <summary>
        /// Checks if the loadable type is of a specific subClass
        /// </summary>
        /// <param name="subclass"></param>
        /// <returns></returns>
        public bool IsSubclassOf(Type subclass)
        {
            if (subclass == null)
            {
                throw new ArgumentNullException("subclass");
            }

            bool result = false;
            Type type = Type.GetTypeFromHandle(_typeHandle);
            if (!type.IsAbstract)
            {
                result = type.IsSubclassOf(subclass);
            }
            return result;
        }
    }
}