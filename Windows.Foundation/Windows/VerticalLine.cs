using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BritishMicro.Windows
{
    using HANDLE = IntPtr;
    using HWND = IntPtr;

    /// <summary>
    /// Implements a standard windows vertical rule.
    /// </summary>
    [Description("A vertical rule.")]
    public class VerticalLine : Control
    {
        private const int SS_ETCHEDHORZ = 0x00000010;
        private const int SS_ETCHEDVERT = 0x00000011;
        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const int SWP_DRAWFRAME = SWP_FRAMECHANGED;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOCOPYBITS = 0x100;

        [DllImport("user32")]
        private static extern int SetWindowPos(HWND hwnd, HWND hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32")]
        private static extern int CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, int dwStyle,
                                                 int x, int y, int nWidth, int nHeight, HWND hwndParent, HANDLE hMenu,
                                                 HANDLE hInstance, IntPtr lpParam);

        private HANDLE m_hwndRule;
        private const int FixedWidth = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalLine"/> class.
        /// </summary>
        public VerticalLine()
        {
            // The width of a vertical rule is a constant.
            Width = FixedWidth;

            // Create the native window that will display as a vertical rule.
            m_hwndRule = (IntPtr) CreateWindowEx(
                                      0, "static", "",
                                      WS_CHILD | WS_VISIBLE | SS_ETCHEDVERT,
                                      0, 0, Width, Height, Handle,
                                      HANDLE.Zero, HANDLE.Zero, HANDLE.Zero
                                      );
        }

        /// <summary>
        /// Override of <see cref="Control.OnSizeChanged"/>.
        /// </summary>
        protected override void OnSizeChanged(EventArgs args)
        {
            if (m_hwndRule != HANDLE.Zero)
            {
                // The width of a vertical rule is a constant.
                Width = FixedWidth;

                SetWindowPos(
                    m_hwndRule, HANDLE.Zero, 0, 0, Width, Height,
                    SWP_NOCOPYBITS | SWP_DRAWFRAME
                    );
            }

            base.OnSizeChanged(args);
        }
    }
}