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
using System.Windows.Forms;
using BritishMicro.TaskClerk.Plugins;
using BritishMicro.Windows;

namespace BritishMicro.TaskClerk.InstantAccess
{
    /// <summary>
    /// Represents a service within TaskClerk that waits for HotKey combination then renders
    /// the Instant Access functionality within the application.
    /// </summary>
    public partial class InstantAccessService : PluginService, IDisposable
    {
        private bool _isWindowOpen;
        private OSHotkey _osHotKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:InstantAccess"/> class.
        /// </summary>
        public InstantAccessService()
        {
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="InstantAccessService"/> is reclaimed by garbage collection.
        /// </summary>
        ~InstantAccessService()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> [disposing].</param>
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(this._osHotKey != null)
                {
                    this._osHotKey.Dispose();
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Init"/> event.
        /// </summary>
        protected override void OnStartup()
        {
            this._osHotKey = new OSHotkey();
            this._osHotKey.HotkeyPress += this.osHotKey_HotKeyPress;
            this._osHotKey.RegisterHotkey(KeyCategories.AltKey, Keys.F12);
        }

        /// <summary>
        /// Handles the HotKeyPress event of the osHotKey control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void osHotKey_HotKeyPress(object sender, EventArgs e)
        {
            if(this._isWindowOpen)
                return;

            try
            {
                this._isWindowOpen = true;

                using(InstantAccessDialog iaf = new InstantAccessDialog())
                {
                    NativeMethods.SetForegroundWindow(iaf);
                    NativeMethods.SetFocus(iaf);
                    iaf.ShowDialog();
                }
            }
            finally
            {
                this._isWindowOpen = false;
            }
        }
    }
}