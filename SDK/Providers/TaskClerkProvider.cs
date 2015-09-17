//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 01st of Jan 2006
//	Author: John Powell (john.powell@britishmicro.com)
//  Change: Created
//
//  Date  : 31st May 2007
//	Author: Paul Jackson (paul@compilewith.net)
//  Change: Added extensibility to the XML Configuration for providers.
//----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// The base provider type for the TaskClerk application, this
    /// base object calls the OnInit method once the requered setup has been done.
    /// It must be noted that providers must carry out their initalisation process
    /// with the most basic of constructs and should not rely on any other providers
    /// being available.
    /// </summary>
    public class TaskClerkProvider
    {
        private TaskClerkEngine _engine;
        private string _name;
        private string _providerType;
        private XmlElement[] _metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskClerkProvider"/> class.
        /// </summary>
        protected TaskClerkProvider()
        {
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute("name")]
        public string ProviderName
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the type of the provider.
        /// </summary>
        /// <value>The type of the provider.</value>
        [XmlAttribute("providerType")]
        public string ProviderType
        {
            get { return _providerType; }
            set { _providerType = value; }
        }

        /// <summary>
        /// Gets or sets the provider metadata.
        /// </summary>
        /// <value>The provider metadata.</value>
        [XmlAnyElement]
        public XmlElement[] ProviderMetaData
        {
            get { return _metadata; }
            set { _metadata = value; }
        }

        /// <summary>
        /// Creates the specified engine.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <returns></returns>
        internal TaskClerkProvider Create(TaskClerkEngine engine)
        {
            Regex expression =
                new Regex(BritishMicro.TaskClerk.Properties.Resources.TypeNameAndAssemblyRegEx);
            Match m = expression.Match(_providerType);
            Assembly targetAssembly;
            try
            {
                targetAssembly = Assembly.Load(m.Groups["AssemblyName"].Value);
            }
            catch (FileNotFoundException fnf)
            {
                Trace.TraceError(
                    "TaskClerkProvider:Create[Unable to find the file {0}, specified in the providers section of the application configuration file.]",
                    fnf.FileName);
                throw new FileNotFoundException(
                    "Unable to find the file specified in the providers section of the application configuration file.",
                    fnf);
            }
            Type targetType = targetAssembly.GetType(m.Groups["TypeName"].Value);
            if (targetType == null)
            {
                Trace.TraceError("TaskClerkProvider:Create[Could not find type [{0}] in assembly [{1}].]",
                                 m.Groups["TypeName"].Value, targetAssembly.FullName);
                throw new InvalidOperationException(
                    string.Format(CultureInfo.InvariantCulture, "Could not find type [{0}] in assembly [{1}].",
                                  m.Groups["TypeName"].Value,
                                  targetAssembly.FullName));
            }
            TaskClerkProvider provider = Activator.CreateInstance(targetType) as TaskClerkProvider;
            if (provider != null)
            {
                provider._engine = engine;
                provider._metadata = this._metadata;
                provider.OnInit();
            }
            return provider;
        }

        #region Init 

        /// <summary>
        /// The OnInit method provides implimenters with an opertunity to
        /// setup their providers after a valid taskclerk engine has been asigned to
        /// the Engine property.
        /// </summary>
        protected virtual void OnInit()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance has metadata.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has metadata; otherwise, <c>false</c>.
        /// </value>
        protected internal bool HasMetaData
        {
            get { return (_metadata != null && _metadata.Length > 0); }
        }

        /// <summary>
        /// The providers context
        /// </summary>
        /// <value>The engine.</value>
        protected TaskClerkEngine Engine
        {
            get { return _engine; }
        }

        #endregion

        #region events

        /// <summary>
        /// Raises the timing event for monitoring the performance of providers.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="timerStart">The timer start.</param>
        protected void RaiseTimingEvent(MethodBase method, DateTime timerStart)
        {
            TimingEventArgs tcpta = new TimingEventArgs(method, timerStart, DateTime.Now);
            OnTimingDone(tcpta);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<TimingEventArgs> ProviderTimer;

        /// <summary>
        /// Raises the <see cref="E:TaskClerkProviderDone"/> event.
        /// </summary>
        /// <param name="e">The <see cref="BritishMicro.TaskClerk.Providers.TimingEventArgs"/> instance containing the event data.</param>
        private void OnTimingDone(TimingEventArgs e)
        {
            EventHandler<TimingEventArgs> handler = ProviderTimer;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region static methods

        /// <summary>
        /// Discovers the connection string for the provider.
        /// </summary>
        public static ConnectionStringSettings DiscoverConnectionStringSettings(TaskClerkProvider provider)
        {
            if(provider == null)
                throw new ArgumentNullException("provider");
            
            string connectionStringName = "BritishMicro.TaskClerk.Properties.Settings.DefaultDataStore";
            if (provider.HasMetaData)
            {
                connectionStringName = provider.ProviderMetaData[0].GetAttribute("name");
            }
            return ConfigurationManager.ConnectionStrings[connectionStringName];
        }

        /// <summary>
        /// Tries the get a value from settings.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string TryGetFromSettings(TaskClerkProvider provider, string key, string defaultValue)
        {
            if(provider == null)
                throw new ArgumentNullException("provider");

            string foundValue = null;
            if (provider.HasMetaData)
            {
                foundValue = provider.ProviderMetaData[0].GetAttribute(key);
            }
            if (string.IsNullOrEmpty(foundValue))
            {
                foundValue = defaultValue;
            }
            return foundValue;
        }

        /// <summary>
        /// Handles the AssemblyResolve event of the CurrentDomain control.
        /// </summary>
        /// <param name="args">The <see cref="System.ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        protected static Assembly AssemblyResolve(ResolveEventArgs args)
        {
            if(args == null)
                throw new ArgumentNullException("args");
            
            try
            {
                string[] assemblyName = args.Name.Split(',');
                string pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
                string assemblyPath = Path.Combine(pluginPath, assemblyName[0] + ".dll");

                if(File.Exists(assemblyPath))
                {
                    Assembly targetAssembly = Assembly.LoadFile(assemblyPath);
                    if(targetAssembly != null)
                        return targetAssembly;
                }
            }
            catch(Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }

            return Assembly.GetExecutingAssembly();
        }

        #endregion
    }
}