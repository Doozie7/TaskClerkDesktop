using System.Drawing;
using System.Windows.Forms;

namespace BritishMicro.Windows
{
    /// <summary>
    /// The blue panel provides a blue backgrpound planel
    /// </summary>
    public class BluePanel : Panel
    {
        /// <summary>
        /// Raises the <see cref="E:PaintBackground"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle r = new Rectangle(0, 0, Bounds.Width - 2, Bounds.Height - 2);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Blue), 1), r);
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.DarkGray), 1),
                                2, Bounds.Height - 1, Bounds.Width, Bounds.Height - 1);
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.DarkGray), 1),
                                Bounds.Width - 1, 2, Bounds.Width - 1, Bounds.Height);
        }
    }
}