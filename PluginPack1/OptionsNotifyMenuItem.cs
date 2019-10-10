//----------------------------------------------------------------------
//	Developed by:
//          BritishMicro
//          United Kingdom
//
//  Copyright (c) BritishMicro 2006
//
//  Date  : 30th November 2006
//	Author: Paul Jackson (BritishMicro@messagelabs.com)
//          Architect
//----------------------------------------------------------------------

using BritishMicro.TaskClerk.Plugins;
using System;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Represents an Options menu selection direct from the Notify Icon.
    /// </summary>
    public class OptionsNotifyMenuItem : PluginNotifyMenuItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsNotifyMenuItem"/> class.
        /// </summary>
        public OptionsNotifyMenuItem()
        {
            MenuText = "Options...";
        }

        /// <summary>
        /// Raises the <see cref="E:Click"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public override void OnClick(EventArgs e)
        {
            this.MenuItem.Enabled = false;
            Engine.UIProvider.ShowOptionsExplorer();
            this.MenuItem.Enabled = true;
        }
    }
}