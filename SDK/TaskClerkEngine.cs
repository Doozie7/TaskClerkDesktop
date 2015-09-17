//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 01st of Jan 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using BritishMicro.TaskClerk.Providers;
using ConfigurationSection=BritishMicro.TaskClerk.Settings.ConfigurationSection;
using System.Diagnostics;
using BritishMicro.TaskClerk.Plugins;
using System.Timers;
using System.ComponentModel;
using System.Collections.Generic;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// The TaskClerk engine, this class contains the startup and shutdown 
    /// application logic. It also contains the providers for the rest of the 
    /// application to use.
    /// </summary>
    [LicenseProvider(typeof(TaskClerkLicenseProvider))]
    [DebuggerStepThroughAttribute]
    public abstract class TaskClerkEngine
    {
        private License _license;
        private LicenseInfo _licenceInfo;

        private Timer _heartbeatTimer;
        private Timer _nagTimer;

        private DirectoryInfo _dataFolder;
        private DirectoryInfo _appFolder;
        
        private List<PluginService> _services;

        private IdentityProvider _identityProvider;
        private UserMemoryProvider _userMemoryProvider;
        private TaskDescriptionsProvider _taskDescriptionsProvider;
        private TaskActivitiesProvider _taskActivitiesProvider;
        private PluginsProvider _pluginsProvider;
        private UIProvider _uiProvider;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TaskClerkEngine"/> class.
        /// </summary>
        protected TaskClerkEngine()
        {
            InitaliseEngine();
        }

        /// <summary>
        /// Initalises the engine.
        /// </summary>
        private void InitaliseEngine()
        {
            LicenseManagement();

            _heartbeatTimer = new System.Timers.Timer();
            _heartbeatTimer.Enabled = false;
            _heartbeatTimer.Interval = int.Parse(Properties.Resources.HeartbeatInterval);
            _heartbeatTimer.Elapsed += InternalHeartbeatTimerElapsed;
            _nagTimer = new System.Timers.Timer();
            _nagTimer.Enabled = false;
            _nagTimer.Elapsed += InternalNagTimerElapsed;

            _appFolder = (new FileInfo(Assembly.GetExecutingAssembly().Location)).Directory;

            _dataFolder = new DirectoryInfo(System.Windows.Forms.Application.UserAppDataPath);
            if (_dataFolder.Exists == false)
            {
                _dataFolder.Create();
                _dataFolder.Refresh();
            }

            //Read Provider Configuration
            ConfigurationSection config =
                ConfigurationManager.GetSection(ConfigurationSection.SectionName)
                as ConfigurationSection;
            if (config != null)
            {
                if (config.Providers.Count == 0)
                {
                    Trace.TraceError(Properties.Resources.NoProvidersFoundInConfigurationFile);
                    throw new InvalidOperationException(Properties.Resources.NoProvidersFoundInConfigurationFile);
                }

                // The providers are created and initalised in a special order, 
                // first is the 
                // identity provider (work out who is using this application)
                _identityProvider =
                    InitializeProvider(config, "IdentityProvider") as IdentityProvider;
                _identityProvider.DiscoverIdentity();
                if(!_identityProvider.Principal.Identity.IsAuthenticated)
                {
                    Trace.TraceError(Properties.Resources.IdentityCouldNotBeEstablished);
                    throw new InvalidOperationException(Properties.Resources.IdentityCouldNotBeEstablished);
                }

                // Then comes the plugin provider
                _pluginsProvider =
                    InitializeProvider(config, "PluginsProvider") as PluginsProvider;
                _pluginsProvider.DiscoverPlugins();

                // then comes the TaskDescription and Task Activity providers
                _taskDescriptionsProvider =
                    InitializeProvider(config, "TaskDescriptionsProvider") as TaskDescriptionsProvider;
                _taskActivitiesProvider =
                    InitializeProvider(config, "TaskActivitiesProvider") as TaskActivitiesProvider;
                // The UI provider then gets created and initalised
                _uiProvider =
                    InitializeProvider(config, "UIProvider") as UIProvider;
                // Finnaly the Users memory provider
                _userMemoryProvider =
                    InitializeProvider(config, "UserMemoryProvider") as UserMemoryProvider;
            }
            else
            {
                Trace.TraceError(Properties.Resources.ConfigurationFileInvalid);
                throw new InvalidOperationException(Properties.Resources.ConfigurationFileInvalid);
            }
            
            Trace.TraceInformation("TaskClerkSDK:InitaliseEngine");
        }

        /// <summary>
        /// Licenses the management.
        /// </summary>
        private void LicenseManagement()
        {
            _licenceInfo = new LicenseInfo();
            LicenseManager.IsValid(typeof(TaskClerkEngine), this, out _license);
            if (_license != null)
            {
                _licenceInfo.Initalise(_license);
            }
        }

        /// <summary>
        /// The Nag event handler.
        /// </summary>
        public event EventHandler<TaskActivityEventArgs> Nag;

        /// <summary>
        /// The internals nag timer has elapsed, raise the NagEvent event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void InternalNagTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.CurrentActivity.IsEmpty())
            {
                this._nagTimer.Stop();
                return;
            }
            
            //RaiseNag Event
            EventHandler<TaskActivityEventArgs> temp = Nag;
            if (temp != null)
            {
                temp(this, new TaskActivityEventArgs(this.CurrentActivity));
            }
        }

        /// <summary>
        /// The heart beat of TaskClerk
        /// </summary>
        public event EventHandler<EventArgs> HeartBeat;

        /// <summary>
        /// The internal heartbeat timer has elapsed, raise the Heartbeat event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void InternalHeartbeatTimerElapsed(object sender, ElapsedEventArgs e)
        {
            this._userMemoryProvider.Set("HeartBeat", DateTime.Now, PersistHint.AcrossSessions);
            
            //RaiseHeartBeat Event
            EventHandler<EventArgs> temp = HeartBeat;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="config">The config section.</param>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns></returns>
        private TaskClerkProvider InitializeProvider(
            ConfigurationSection config,
            string providerName)
        {
            TaskClerkProvider provider = FindProvider(config.Providers, providerName);
            if (provider == null)
            {
                Trace.TraceError(Properties.Resources.UnableToFindConfigurationFile, providerName);
                throw new InvalidOperationException(
                    string.Format(CultureInfo.InvariantCulture, Properties.Resources.UnableToFindConfigurationFile,
                                  providerName));
            }
            return provider.Create(this);
        }

        /// <summary>
        /// Finds the provider.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        private static TaskClerkProvider FindProvider(Collection<TaskClerkProvider> collection, string p)
        {
            foreach (TaskClerkProvider provider in collection)
            {
                if (provider.ProviderName == p)
                {
                    return provider;
                }
            }
            return null;
        }

        /// <summary>
        /// The Applications name
        /// </summary>
        public static string SdkName
        {
            get
            {
                return Properties.Resources.SdkName;
            }
        }

        /// <summary>
        /// Gets the SDK version.
        /// </summary>
        /// <value>The SDK version.</value>
        public static string SdkVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// The directory where the application is stored in
        /// </summary>
        public DirectoryInfo ApplicationFolder
        {
            get { return _appFolder; }
            set { _appFolder = value; }
        }

        /// <summary>
        /// Provides access to application settings
        /// </summary>
        public UserMemoryProvider SettingsProvider
        {
            get { return _userMemoryProvider; }
        }

        /// <summary>
        /// Provides access to the provided identity
        /// </summary>
        public IdentityProvider IdentityProvider
        {
            get { return _identityProvider; }
        }

        /// <summary>
        /// Provides access to the applications TaskDescriptions
        /// </summary>
        public TaskDescriptionsProvider TaskDescriptionsProvider
        {
            get { return _taskDescriptionsProvider; }
        }

        /// <summary>
        /// Provides access to the applications TaskActivities
        /// </summary>
        public TaskActivitiesProvider TaskActivitiesProvider
        {
            get { return _taskActivitiesProvider; }
        }

        /// <summary>
        /// Provides access to the available plugis
        /// </summary>
        public PluginsProvider PluginsProvider
        {
            get { return _pluginsProvider; }
        }

        /// <summary>
        /// Provides access to the available ui components
        /// </summary>
        public UIProvider UIProvider
        {
            get { return _uiProvider; }
        }

        /// <summary>
        /// Tasks the clerk engine start.
        /// </summary>
        public virtual void StartEngine()
        {
            StartServices();

            SettingsProvider.Set("LastStopEngine", DateTime.MinValue, PersistHint.ThisSession);
            if ((DateTime)SettingsProvider.Get("HeartBeat", DateTime.MinValue) != DateTime.MinValue)
            {
                DateTime lastheartBeat = (DateTime)SettingsProvider.Get("HeartBeat", DateTime.MinValue);
                SettingsProvider.Set("LastStopEngine", lastheartBeat, PersistHint.ThisSession);
            }

            _heartbeatTimer.Enabled = true;
            _heartbeatTimer.Start();
        }

        /// <summary>
        /// Tasks the clerk engine end.
        /// </summary>
        public virtual void StopEngine()
        {
            _heartbeatTimer.Stop();
            SettingsProvider.Set("HeartBeat", DateTime.MinValue, PersistHint.AcrossSessions);

            StopServices();
        }

        /// <summary>
        /// Gets the current running activity.
        /// </summary>
        /// <value>The current activity.</value>
        public TaskActivity CurrentActivity
        {
            get
            {
                return (TaskActivity)SettingsProvider.Get(
                    "CurrentActivity", TaskActivity.Empty);
            }
        }

        /// <summary>
        /// Starts the services.
        /// </summary>
        private void StartServices()
        {
            _services = new List<PluginService>();
            foreach (LoadableItem li in PluginsProvider.Plugins)
            {
                if (li.IsSubclassOf(typeof(PluginService)))
                {
                    PluginService ps = (PluginService)li.CreateInstance();
                    _services.Add(ps);
                    ps.Engine = this;
                    ps.CallStartup();
                }
            }
            Trace.TraceInformation("TaskClerkSDK:StartServices");
        }

        /// <summary>
        /// Ends the services.
        /// </summary>
        private void StopServices()
        {
            for (int i = _services.Count; i > 0; i--)
            {
                PluginService ps = _services[(i-1)];
                ps.CallShutdown();
                ps = null;
            }
            Trace.TraceInformation("TaskClerkSDK:EndServices");
        }

        /// <summary>
        /// Gets or sets the licence information.
        /// </summary>
        /// <value>The licence information.</value>
        public LicenseInfo LicenceInformation
        {
            get { return _licenceInfo; }
            set { _licenceInfo = value; }
        }

        /// <summary>
        /// Lists the providers.
        /// </summary>
        /// <returns></returns>
        public string[] ListProviders()
        {
            string[] providers = new string[6];
            providers[0] = ProviderToString("IdentityProvider", _identityProvider);
            providers[1] = ProviderToString("PluginsProvider", _pluginsProvider);
            providers[2] = ProviderToString("TaskDescriptionsProvider", _taskDescriptionsProvider);
            providers[3] = ProviderToString("TaskActivitiesProvider",_taskActivitiesProvider);
            providers[4] = ProviderToString("UIProvider", _uiProvider);
            providers[5] = ProviderToString("UserMemoryProvider", _userMemoryProvider);
            return providers;
        }

        /// <summary>
        /// Providers to string.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="provider">The provider.</param>
        /// <returns></returns>
        private static string ProviderToString(string providerName, object provider)
        {
            return string.Format("The {0} is an instance of {1} loaded from {2}",
                            providerName,
                            provider.GetType().AssemblyQualifiedName,
                            provider.GetType().Module.FullyQualifiedName);
        }
    }
}