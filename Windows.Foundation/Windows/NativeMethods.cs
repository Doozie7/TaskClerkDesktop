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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BritishMicro.Windows
{
    /// <summary>
    /// Contains all the native methods required for the plug-in.
    /// </summary>
    public static class NativeMethods
    {
        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="hWnd">The handle to the window that requests to be the foreground window.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return : MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Sets the focus to the window specified.
        /// </summary>
        /// <param name="hWnd">The handle of the window to set focus to.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);


        /// <summary>
        /// Registers the hot key.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="id">The id.</param>
        /// <param name="fsModifiers">The fs modifiers.</param>
        /// <param name="vk">The vk.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterHotKey")]
        [return : MarshalAs(UnmanagedType.Bool)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyCategories fsModifiers, Keys vk);

        /// <summary>
        /// Unregisters the hot key.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "UnregisterHotKey")]
        [return : MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 
        /// </summary>
        public const int WM_HOTKEY = 0x0312;

        private const int UniqueHotkeyId = 100; //  ...has to be unqiue for the calling thread

        /// <summary>
        /// Sets the foreground window.
        /// </summary>
        /// <param name="bringToForeground">The control to bring to the foreground.</param>
        /// <returns>true if successful; otherwise false.</returns>
        public static bool SetForegroundWindow(Control bringToForeground)
        {
            return SetForegroundWindow(bringToForeground.Handle);
        }

        /// <summary>
        /// Sets the focus.
        /// </summary>
        /// <param name="setFocusTo">The control to set focus to.</param>
        public static void SetFocus(Control setFocusTo)
        {
            SetFocus(setFocusTo.Handle);
        }

        /// <summary>
        /// Registers the hotkey.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="category">The category.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool RegisterHotkey(Control container, KeyCategories category, Keys key)
        {
            return RegisterHotKey(container.Handle, UniqueHotkeyId, category, key);
        }

        /// <summary>
        /// Unregisters the hotkey.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns></returns>
        public static bool UnregisterHotkey(Control container)
        {
            return UnregisterHotKey(container.Handle, UniqueHotkeyId);
        }
    }
}