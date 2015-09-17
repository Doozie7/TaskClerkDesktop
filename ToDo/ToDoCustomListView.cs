using System;
using System.Collections.Generic;
using System.Text;
using BritishMicro.TaskClerk.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace ToDo
{
    public class ToDoCustomListView : OwnerDrawnListView
    {

        //private Font _font;
        private StringFormat _format;

        public ToDoCustomListView()
        {
            //this._font = SystemFonts.DefaultFont;
            this._format = new StringFormat();
        }

        public StringFormat StringFormat
        {
            get { return _format; }
            set { _format = value; }
        }

        protected override void DrawItem(System.Drawing.Graphics g, int index)
        {
            base.DrawItem(g, index);
            
        }
    }
}
