using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Conrols;

namespace GameDatabase
{
    public partial class LayoutEditor : UserControl
    {
        public LayoutEditor()
        {
            InitializeComponent();
            _layout = new StringBuilder(_defaultLayout);

            _categories = new Dictionary<char, Color> { { '0', Color.LightGray }, { '1', Color.Blue } };
            SelectedCategory = _categories.Last().Key;
        }

        [Description("Layout"), Category("Margins")]
        public int BorderSize { get; set; }

        [Description("Layout"), Category("Data")]
        public string Layout
        {
            get { return _layout.ToString(); }
            set
            {
                _layout.Clear();

                var size = value.Length;
                if (size <= 0)
                {
                    _layout.Append(_defaultLayout);
                    return;
                }

                var x = (int)Math.Sqrt(size);
                if (x*x != size)
                {
                    var defaultValue = _categories.First().Key;
                    _layout.Append(value);
                    _layout.Append(defaultValue, (x + 1)*(x + 1) - value.Length);
                    return;
                }

                _layout.Append(value);
            }
        }

        [Serializable]
        public struct BarrelData
        {
            public float X;
            public float Y;
            public float Rotation;
            public float Offset;
            public string Text;
        }

        [Description("Barrels"), Category("Data")]
        public IList<BarrelData> Barrels
        {
            get { return _barrels; }
            set
            {
                _barrels = value;
                Invalidate();
            }
        }

        [Description("Colors"), Category("Data")]
        public IDictionary<char, Color> Colors
        {
            get { return _categories; }
        }

        [Description("SelectedCategory"), Category("Data")]
        public char SelectedCategory { get; set; }

        public event EventHandler ValueChanged;

        protected void OnValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var layoutSize = (int)Math.Sqrt(_layout.Length);
            var size = Math.Min(Width, Height) - BorderSize*2 - 1;
            var cellSize = size/layoutSize + (size % layoutSize > 0 ? 1 : 0);

            for (var i = 0; i < layoutSize; ++i)
            {
                for (var j = 0; j < layoutSize; ++j)
                {
                    var x = BorderSize + j*size/layoutSize;
                    var y = BorderSize + i*size/layoutSize;

                    var cell = _layout[i*layoutSize + j];
                    Color color;
                    if (!_categories.TryGetValue(cell, out color))
                        color = Color.Black;

                    e.Graphics.FillRectangle(GetBrush(color), x, y, cellSize, cellSize);
                }
            }

            var pen = new Pen(Color.Black);
            for (var i = 0; i <= layoutSize; ++i)
            {
                var x = BorderSize + i * size / layoutSize;
                e.Graphics.DrawLine(pen, x, BorderSize, x, BorderSize + size );
                e.Graphics.DrawLine(pen, BorderSize, x, BorderSize + size, x);
            }

            if (_barrels == null)
                return;

            pen.Width = 2;
            var widePen = new Pen(Color.Black, 3);
            foreach (var barrel in _barrels)
            {
                var x = BorderSize + (1 - barrel.X) * size / 2;
                var y = BorderSize + (1 - barrel.Y) * size / 2;

                var offset = barrel.Offset*size/2;
                var radius = Math.Max(cellSize/4, offset);

                e.Graphics.DrawEllipse(widePen, x - radius, y - radius, radius*2, radius*2);

                var length = radius + cellSize/2;
                e.Graphics.DrawLine(pen, x - length, y, x + length, y);
                e.Graphics.DrawLine(pen, x, y - length, x, y + length);

                var dir = RotationHelpers.Direction(-barrel.Rotation);
                var p0 = new Point((int)(x + dir.x*offset), (int)(y + dir.y*offset));
                var p1 = new Point((int)(x + dir.x*(offset + cellSize)), (int)(y + dir.y*(offset + cellSize)));
                var d = new Point((int)(dir.x * cellSize / 2f), (int)(dir.y * cellSize / 2f));
                var p2 = new Point(p1.X + d.X, p1.Y + d.Y);

                e.Graphics.DrawLine(widePen, p0, p2);
                e.Graphics.DrawLine(pen, p2.X, p2.Y, p1.X - d.Y, p1.Y + d.X);
                e.Graphics.DrawLine(pen, p2.X, p2.Y, p1.X + d.Y, p1.Y - d.X);
            }
        }

        public Brush GetBrush(Color color)
        {
            Brush brush;
            if (!_brushes.TryGetValue(color, out brush))
            {
                brush = new SolidBrush(color);
                _brushes.Add(color, brush);
            }

            return brush;
        }

        private readonly Dictionary<Color, Brush> _brushes = new Dictionary<Color, Brush>();
        private readonly Dictionary<char, Color> _categories;
        private readonly StringBuilder _layout;
        private const string _defaultLayout = "000000000";

        [Serializable]
        public class CellCategory
        {
            public char Id { get; set; }
            public Color Color { get; set; }
        }

        private void LayoutEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
                Draw(e.X, e.Y, SelectedCategory);
            else if (e.Button.HasFlag(MouseButtons.Right))
                Draw(e.X, e.Y, _categories.First().Key);
        }

        private void LayoutEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
                Draw(e.X, e.Y, SelectedCategory);
            else if (e.Button.HasFlag(MouseButtons.Right))
                Draw(e.X, e.Y, _categories.First().Key);
        }

        private void Draw(int mouseX, int mouseY, char value)
        {
            var layoutSize = (int)Math.Sqrt(_layout.Length);
            var size = Math.Min(Width, Height) - 1 - BorderSize*2;

            var x = layoutSize * (mouseX - BorderSize) / size;
            var y = layoutSize * (mouseY - BorderSize) / size;

            if (x < 0 || x >= layoutSize)
                return;
            if (y < 0 || y >= layoutSize)
                return;

            var oldValue = _layout[y * layoutSize + x];
            if (oldValue == value)
                return;

            _layout[y * layoutSize + x] = value;
            Invalidate();

            OnValueChanged(this, EventArgs.Empty);
        }

        private void LayoutEditor_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private IList<BarrelData> _barrels;
    }
}
