//----------------------------------------------------------------------
//	Developed by:
//          BritishMicro
//          United Kingdom
//
//  Copyright (c) BritishMicro 2008
//
//  Date  : 26th February 2008
//	Author: John Powell (john.powell@britishmicro.com)
//          Architect
//----------------------------------------------------------------------

using System;
using System.Windows.Forms;
using BritishMicro.TaskClerk.Plugins;
using BritishMicro.Windows;

namespace BritishMicro.TaskClerk.InstantNotes
{
    /// <summary>
    /// Represents a service within TaskClerk that waits for HotKey combination then renders
    /// the Instant Access functionality within the application.
    /// </summary>
    public partial class InstantNotesService : PluginService, IDisposable
    {
        private bool _isWindowOpen;
        private OSHotkey _osHotKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:InstantAccess"/> class.
        /// </summary>
        public InstantNotesService()
        {
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="InstantAccessService"/> is reclaimed by garbage collection.
        /// </summary>
        ~InstantNotesService()
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
            this._osHotKey.RegisterHotkey(KeyCategories.AltKey, Keys.F11);
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

                using (InstantNotesDialog iaf = new InstantNotesDialog())
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