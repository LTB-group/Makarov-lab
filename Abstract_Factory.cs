using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public interface ICircle
        {
            void Draw(Graphics g, Point location);
        }

        public interface ISquare
        {
            void Draw(Graphics g, Point location);
        }

        public interface ITriangle
        {
            void Draw(Graphics g, Point location);
        }

        public interface IShapeFactory
        {
            ICircle CreateCircle();
            ISquare CreateSquare();
            ITriangle CreateTriangle();
        }

        public class RedCircle : ICircle
        {
            public void Draw(Graphics g, Point location)
            {
                using (Brush brush = new SolidBrush(Color.Red))
                {
                    g.FillEllipse(brush, location.X, location.Y, 50, 50);
                }
            }
        }

        public class RedSquare : ISquare
        {
            public void Draw(Graphics g, Point location)
            {
                using (Brush brush = new SolidBrush(Color.Red))
                {
                    g.FillRectangle(brush, location.X, location.Y, 50, 50);
                }
            }
        }

        public class RedTriangle : ITriangle
        {
            public void Draw(Graphics g, Point location)
            {
                using (Brush brush = new SolidBrush(Color.Red))
                {
                    Point[] points = { new Point(location.X + 25, location.Y), new Point(location.X, location.Y + 50), new Point(location.X + 50, location.Y + 50) };
                    g.FillPolygon(brush, points);
                }
            }
        }

        public class BlueCircle : ICircle
        {
            public void Draw(Graphics g, Point location)
            {
                using (Brush brush = new SolidBrush(Color.Blue))
                {
                    g.FillEllipse(brush, location.X, location.Y, 50, 50);
                }
            }
        }

        public class BlueSquare : ISquare
        {
            public void Draw(Graphics g, Point location)
            {
                using (Brush brush = new SolidBrush(Color.Blue))
                {
                    g.FillRectangle(brush, location.X, location.Y, 50, 50);
                }
            }
        }

        public class BlueTriangle : ITriangle
        {
            public void Draw(Graphics g, Point location)
            {
                using (Brush brush = new SolidBrush(Color.Blue))
                {
                    Point[] points = { new Point(location.X + 25, location.Y), new Point(location.X, location.Y + 50), new Point(location.X + 50, location.Y + 50) };
                    g.FillPolygon(brush, points);
                }
            }
        }

        public class RedFactory : IShapeFactory
        {
            public ICircle CreateCircle() => new RedCircle();
            public ISquare CreateSquare() => new RedSquare();
            public ITriangle CreateTriangle() => new RedTriangle();
        }

        public class BlueFactory : IShapeFactory
        {
            public ICircle CreateCircle() => new BlueCircle();
            public ISquare CreateSquare() => new BlueSquare();
            public ITriangle CreateTriangle() => new BlueTriangle();
        }

        private ComboBox shapeComboBox;
        private ComboBox colorComboBox;
        private IShapeFactory currentFactory;
        private string selectedShape;

        public Form1()
        {
            InitializeComponent();
            SetupComboBoxes();
            currentFactory = new RedFactory();
            this.Paint += new PaintEventHandler(OnPaint);
        }

        private void SetupComboBoxes()
        {
            shapeComboBox = new ComboBox();
            shapeComboBox.Items.AddRange(new string[] { "Circle", "Square", "Triangle" });
            shapeComboBox.Location = new Point(10, 10);
            shapeComboBox.SelectedIndexChanged += OnShapeChanged;
            Controls.Add(shapeComboBox);

            colorComboBox = new ComboBox();
            colorComboBox.Items.AddRange(new string[] { "Red", "Blue" });
            colorComboBox.Location = new Point(150, 10);
            colorComboBox.SelectedIndexChanged += OnColorChanged;
            Controls.Add(colorComboBox);
        }

        private void OnShapeChanged(object sender, EventArgs e)
        {
            selectedShape = shapeComboBox.SelectedItem.ToString();
            this.Invalidate();
        }

        private void OnColorChanged(object sender, EventArgs e)
        {
            switch (colorComboBox.SelectedItem.ToString())
            {
                case "Red":
                    currentFactory = new RedFactory();
                    break;
                case "Blue":
                    currentFactory = new BlueFactory();
                    break;
            }
            this.Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (currentFactory == null || string.IsNullOrEmpty(selectedShape)) return;

            ICircle circle = null;
            ISquare square = null;
            ITriangle triangle = null;

            switch (selectedShape)
            {
                case "Circle":
                    circle = currentFactory.CreateCircle();
                    circle?.Draw(e.Graphics, new Point(50, 50));
                    break;
                case "Square":
                    square = currentFactory.CreateSquare();
                    square?.Draw(e.Graphics, new Point(50, 50));
                    break;
                case "Triangle":
                    triangle = currentFactory.CreateTriangle();
                    triangle?.Draw(e.Graphics, new Point(50, 50));
                    break;
            }
        }
    }
}
