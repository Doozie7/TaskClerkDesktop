//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 20th of Feb 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using BritishMicro.TaskClerk.Plugins;
using BritishMicro.TaskClerk.Properties;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// The reflecting plugins provider loads loadable items by 
    /// reflecting on asemblies in the plugins folder of the application.
    /// </summary>
    public class ReflectingPluginsProvider : PluginsProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectingPluginsProvider"/> class.
        /// </summary>
        public ReflectingPluginsProvider()
        {
        }

        /// <summary>
        /// Discover the plugins available to the application.
        /// </summary>
        public override void ProviderDiscoverPlugins()
        {
            DirectoryInfo directoryInfo = Engine.ApplicationFolder;

            StringBuilder traceMessage = new StringBuilder();
            traceMessage.AppendLine("ReflectingPluginsProvider:GetPlugins");
            traceMessage.AppendLine(string.Format("Plugin directory {0}", directoryInfo.FullName));
            try
            {
                foreach (FileInfo fileInfo in GetPluginAssemblyFiles(directoryInfo))
                {
                    Assembly foundAssembly = GetAssembly(fileInfo);
                    if (foundAssembly != null)
                    {
                        Type pluginAttributeType = typeof(TaskClerkPluginAttribute);

                        foreach (Type type in foundAssembly.GetTypes())
                        {
                            if(!type.IsDefined(pluginAttributeType, true) ||
                                type.IsAbstract ||
                                Resources.ExcludePluginList.Contains(type.Name))
                            {
                                continue;
                            }

                            object[] plugins = type.GetCustomAttributes(pluginAttributeType, true);
                            if (plugins.Length > 0)
                            {
                                Plugins.Add(new LoadableItem(fileInfo, type, true));
                                traceMessage.AppendLine(
                                    string.Format("Plugin {0} found in {1}.", type.Name, fileInfo.Name));
                            }
                        }
                    }
                }
            }
            finally
            {
                Trace.TraceInformation("{0}", traceMessage.ToString());
            }
        }

        private FileInfo[] GetPluginAssemblyFiles(DirectoryInfo directoryInfo)
        {
            List<FileInfo> files = new List<FileInfo>();
            files.AddRange(directoryInfo.GetFiles("*.dll", SearchOption.AllDirectories));
            files.AddRange(directoryInfo.GetFiles("*.exe", SearchOption.AllDirectories));
            List<FileInfo> removeFileList = new List<FileInfo>();
            foreach (FileInfo fi in files)
            {
                if (Resources.ExcludeFileList.Contains(fi.Name))
                {
                    removeFileList.Add(fi);
                }
            }
            foreach (FileInfo fi in removeFileList)
            {
                files.RemoveAll(
                    delegate(FileInfo fileInfo) { return fi.Name == fileInfo.Name; }
                    );
            }
            return files.ToArray();
        }

        private static Assembly GetAssembly(FileInfo fileInfo)
        {
            Assembly assembly = null;
            if (".dll.exe".IndexOf(fileInfo.Extension) >= 0)
            {
                if (fileInfo.Exists)
                {
                    try
                    {
                        assembly = Assembly.LoadFile(fileInfo.FullName);
                    }
                    catch (BadImageFormatException)
                    {
                        Trace.TraceWarning(
                            "ReflectingPluginsProvider:GetAssembly[Please remove the file {0} from the Plugins folder.",
                            fileInfo.FullName);
                    }
                }
            }
            return assembly;
        }
    }
}