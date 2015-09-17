using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Permissions;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BritishMicro.TaskClerk.Windows
{
    /// <summary>
    /// The owner drawn list view
    /// </summary>
    public class OwnerDrawnListView : ListView
    {
        // Fields
        private bool _backgroundDirty = false;
        private int _cellSize;
        private ImageList _imageList;
        private bool _paintBackground = true;
        private Point _topItemPos = new Point(0, 0);
        private const int DefaultCellSize = 90;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerDrawnListView"/> class.
        /// </summary>
        public OwnerDrawnListView()
        {
            this.CellSize = 90;
        }

        /// <summary>
        /// Draws the item.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="index">The index.</param>
        protected virtual new void DrawItem(Graphics g, int index)
        {
        }

        private bool IsItemVisible(int index)
        {
            if (this.Items.Count <= index)
            {
                return false;
            }
            Rectangle itemRect = this.GetItemRect(index);
            return (this.DisplayRectangle.Contains(itemRect.Left, itemRect.Top) | this.DisplayRectangle.Contains(itemRect.Right, itemRect.Bottom));
        }

        private bool ProcessBackground()
        {
            bool flag = true;
            if (((!this._backgroundDirty && !this._paintBackground) && (this.Items.Count > 0)) && !this._topItemPos.Equals(this.GetItemRect(0).Location))
            {
                this._topItemPos = this.GetItemRect(0).Location;
                this._paintBackground = true;
            }
            if (!this._paintBackground && !this._backgroundDirty)
            {
                flag = false;
            }
            this._backgroundDirty = false;
            return flag;
        }

        private bool ProcessListCustomDraw(ref Message m)
        {
            IntPtr ptr;
            bool flag = false;
            Win32.NMCUSTOMDRAW lParam = (Win32.NMCUSTOMDRAW)m.GetLParam(typeof(Win32.NMCUSTOMDRAW));
            switch (lParam.dwDrawStage)
            {
                case 1:
                    ptr = new IntPtr(0x20);
                    m.Result = ptr;
                    return flag;

                case 0x10001:
                    ptr = new IntPtr(4);
                    m.Result = ptr;
                    if (this.IsItemVisible(lParam.dwItemSpec))
                    {
                        Graphics g = Graphics.FromHdc(lParam.hdc);
                        try
                        {
                            this.DrawItem(g, lParam.dwItemSpec);
                            flag = true;
                        }
                        finally
                        {
                            g.Dispose();
                        }
                        return flag;
                    }
                    return true;
            }
            ptr = new IntPtr(0);
            m.Result = ptr;
            return flag;
        }

        /// <summary>
        /// Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)"></see>.
        /// </summary>
        /// <param name="m"></param>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x85)
            {
                this._backgroundDirty = true;
            }
            if ((m.Msg != 20) || this.ProcessBackground())
            {
                if (m.Msg == 0x204e)
                {
                    Win32.NMHDR lParam = (Win32.NMHDR)m.GetLParam(typeof(Win32.NMHDR));
                    if (lParam.code == -101)
                    {
                        this._paintBackground = false;
                    }
                    if (lParam.hwndFrom.Equals(this.Handle) & (lParam.code == -12))
                    {
                        this._paintBackground = true;
                        if (this.ProcessListCustomDraw(ref m))
                        {
                            return;
                        }
                    }
                }
                base.WndProc(ref m);
            }
        }

        /// <summary>
        /// Gets or sets the size of the cell.
        /// </summary>
        /// <value>The size of the cell.</value>
        [Description("The size of each cell."), DefaultValue(90), Category("Appearance")]
        public int CellSize
        {
            get
            {
                return this._cellSize;
            }
            set
            {
                this._cellSize = value;
                this._imageList = new ImageList();
                Size size = new Size(this._cellSize, this._cellSize);
                this._imageList.ImageSize = size;
                this.LargeImageList = this._imageList;
                this.Invalidate();
            }
        }

        // Nested Types
        private class Win32
        {
            // Nested Types
            public enum Consts
            {
                CDDS_ITEM = 0x10000,
                CDDS_ITEMPREPAINT = 0x10001,
                CDDS_PREPAINT = 1,
                CDRF_DODEFAULT = 0,
                CDRF_NOTIFYITEMDRAW = 0x20,
                CDRF_SKIPDEFAULT = 4,
                LVN_ITEMCHANGED = -101,
                NM_CUSTOMDRAW = -12,
                NM_SETFOCUS = -7,
                OCM_BASE = 0x2000,
                OCM_NOTIFY = 0x204e,
                WM_ERASEBKGND = 20,
                WM_NCPAINT = 0x85,
                WM_NOTIFY = 0x4e
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct NMCUSTOMDRAW
            {
                public OwnerDrawnListView.Win32.NMHDR hdr;
                public int dwDrawStage;
                public IntPtr hdc;
                public OwnerDrawnListView.Win32.RECT rc;
                public int dwItemSpec;
                public int uItemState;
                public IntPtr lItemlParam;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct NMHDR
            {
                public IntPtr hwndFrom;
                public int idFrom;
                public int code;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
        }
    }
}
