//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 01st of Jan 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using BritishMicro.TaskClerk.Properties;

namespace BritishMicro.TaskClerk
{
    internal static class Program
    {
        private static Mutex applicationMutex;

        /// <summary>
        /// The entry point for TaskClerk.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            Trace.TraceInformation(Resources.AppStarting);
            string mutexName = Application.UserAppDataPath.Replace(@"\", "_");
            applicationMutex = new Mutex(false, mutexName);
            if (applicationMutex.WaitOne(1, true))
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    
                    AppContext.Current.StartEngine();
                    Application.Run(new HiddenForm(args));
                    AppContext.Current.StopEngine();
                }
                finally
                {
                    if (applicationMutex != null)
                    {
                        applicationMutex.ReleaseMutex();
                    }

                    AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
                }
            }
            else
            {
                Trace.TraceWarning(Resources.IsRunningMessage);
                MessageBox.Show(
                    Resources.IsRunningMessage,
                    Assembly.GetExecutingAssembly().GetName().Name,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly
                    );
            }
            Trace.TraceInformation(Resources.AppEnding);
        }

        /// <summary>
        /// Called when an unhandled exception is encountered.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if(ex != null)
                Trace.TraceError(ex.ToString());
        }
    }
}