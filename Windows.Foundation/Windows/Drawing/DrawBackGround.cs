using System.Drawing;

namespace BritishMicro.Windows.Drawing
{
    internal class DrawBackGround : IDrawingElement
    {
        private Color _backgroundColor;
        private Brush _backgroundBrush;
        private Image _backgroundImage;
        private RectangleF _rectangle;

        public DrawBackGround(RectangleF rectangle)
        {
            _rectangle = rectangle;
            _backgroundColor = Color.White;
            _backgroundBrush = new SolidBrush(_backgroundColor);
        }

        public DrawBackGround(Color color, RectangleF rectangle)
        {
            _rectangle = rectangle;
            _backgroundColor = color;
            _backgroundBrush = new SolidBrush(_backgroundColor);
        }

        public DrawBackGround(Brush brush, RectangleF rectangle)
        {
            _rectangle = rectangle;
            _backgroundColor = Color.Transparent;
            _backgroundBrush = brush;
        }

        public DrawBackGround(Image image, RectangleF rectangle)
        {
            _rectangle = rectangle;
            _backgroundColor = Color.Transparent;
            _backgroundImage = image;
        }

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set { _backgroundBrush = value; }
        }

        public Image BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; }
        }

        public RectangleF Rectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        #region IDrawingElement Members

        public void Render(Graphics graphics)
        {
            if (_backgroundImage == null)
            {
                graphics.FillRectangle(_backgroundBrush, _rectangle);
            }
            else
            {
                graphics.DrawImage(_backgroundImage, _rectangle);
            }
        }

        #endregion
    }
}