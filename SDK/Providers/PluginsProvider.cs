//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using BritishMicro.TaskClerk.Plugins;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// The Plugin provider
    /// </summary>
    public abstract class PluginsProvider : TaskClerkProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsProvider"/> class.
        /// </summary>
        protected PluginsProvider()
        {
            _plugins = new Collection<LoadableItem>();
        }

        /// <summary>
        /// The GetPlugis method is called during the OnIny event.
        /// </summary>
        protected override void OnInit()
        {
            base.OnInit();
        }

        /// <summary>
        /// Discovers the plugins. Only called by the TaskClerk engine.
        /// </summary>
        internal void DiscoverPlugins()
        {
            ProviderDiscoverPlugins();
            OnDiscoveredPlugins(EventArgs.Empty);
        }

        /// <summary>
        /// Discover the plugins available to the application.
        /// </summary>
        public abstract void ProviderDiscoverPlugins();

        private Collection<LoadableItem> _plugins;

        /// <summary>
        /// The Plugins available to the application
        /// </summary>
        public Collection<LoadableItem> Plugins
        {
            get { return _plugins; }
        }

        /// <summary>
        /// Counts the number of types in the plugins collection.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public int CountOfType(Type type)
        {
            return GetPlugisOfSubcalss(type).Count;
        }

        /// <summary>
        /// Provides a mechanisim for geting a type specific list of plugins
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Collection<LoadableItem> GetPlugisOfSubcalss(Type type)
        {
            Collection<LoadableItem> typedCollection
                = new Collection<LoadableItem>();
            foreach (LoadableItem loadableItem in _plugins)
            {
                if (loadableItem.IsSubclassOf(type) && loadableItem.Enabled)
                {
                    typedCollection.Add(loadableItem);
                }
            }
            return typedCollection;
        }

        /// <summary>
        /// Lists the plugins.
        /// </summary>
        /// <returns></returns>
        public string[] ListPlugins()
        {
            string[] result = new string[_plugins.Count];
            int index = 0;
            foreach (LoadableItem loadableItem in _plugins)
            {
                result[index++] = PluginToString(loadableItem.ReturnType());
            }

            return result;
        }

        /// <summary>
        /// Plugins to string.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns></returns>
        private static string PluginToString(Type plugin)
        {
            return string.Format("Plugin {0} loaded from {1}",
                            plugin.AssemblyQualifiedName,
                            plugin.Module.FullyQualifiedName);
        }

        #region Discovered Plugins Event

        /// <summary>
        /// This event is fired when the provider descovers the plugins
        /// </summary>
        public event EventHandler<EventArgs> DiscoveredPlugins;

        /// <summary>
        /// Raises the <see cref="E:DiscoveredPlugins"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnDiscoveredPlugins(EventArgs e)
        {
            EventHandler<EventArgs> handler = DiscoveredPlugins;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }
}