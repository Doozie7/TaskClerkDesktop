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
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

namespace BritishMicro.Windows
{
    /// <summary>
    /// Control for creating an OS level hot key.
    /// </summary>
    public partial class OSHotkey : Control
    {
        /// <summary>
        /// Event raised when the OS level hot key combination has been pressed.
        /// </summary>
        public event EventHandler HotkeyPress;

        private bool _isHotkeySet;

        /// <summary>
        /// Initializes a new instance of the <see cref="OSHotkey"/> class.
        /// </summary>
        public OSHotkey()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OSHotkey"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public OSHotkey(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// Registers the hot key.
        /// </summary>
        /// <param name="keyCategory">The key category.</param>
        /// <param name="key">The key.</param>
        public void RegisterHotkey(KeyCategories keyCategory, Keys key)
        {
            // TODO: Make this call a single parameter call with "Keys" as the param.

            if (_isHotkeySet)
            {
                UnregisterHotkey();
            }

            _isHotkeySet = NativeMethods.RegisterHotkey(this, keyCategory, key);
        }

        /// <summary>
        /// Unregisters the hot key.
        /// </summary>
        public void UnregisterHotkey()
        {
            if (_isHotkeySet)
            {
                _isHotkeySet = !NativeMethods.UnregisterHotkey(this);
            }
        }

        /// <summary>
        /// Called when  the hot key is pressed.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnHotkeyPress(EventArgs e)
        {
            EventHandler handler = HotkeyPress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"></see> to process.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_HOTKEY:
                    OnHotkeyPress(EventArgs.Empty);
                    break;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Gets a value indicating whether the hotkey is set.
        /// </summary>
        /// <value><c>true</c> if the hotkey is set; otherwise, <c>false</c>.</value>
        public bool IsHotkeySet
        {
            get { return _isHotkeySet; }
        }
    }
}