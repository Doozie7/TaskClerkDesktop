using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BritishMicro.TaskClerk.UI
{
    //[Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    internal partial class RoundedPanel : UserControl
    {
        public RoundedPanel()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Rectangle client = ClientRectangle;
            client.Location = new Point(2, 2);
            client.Inflate(-4, -4);
            using (GraphicsPath path = RoundedRectandlePath(client, 5))
            {
                e.Graphics.DrawPath(new Pen(new SolidBrush(Color.DarkGray), 1), path);
            }
        }

        private GraphicsPath RoundedRectandlePath(Rectangle rectangle, int radius)
        {
            int diameter = 2*radius;
            Rectangle arcRectangle = new Rectangle(rectangle.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRectangle, 180, 90);
            arcRectangle.X = rectangle.Right - diameter;
            path.AddArc(arcRectangle, 270, 90);
            arcRectangle.Y = rectangle.Bottom - diameter;
            path.AddArc(arcRectangle, 0, 90);
            arcRectangle.X = rectangle.Left;
            path.AddArc(arcRectangle, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}