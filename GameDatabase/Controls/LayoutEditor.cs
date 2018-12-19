using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Conrols;
using GameDatabase.Enums;

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

        [Description("Image"), Category("Data")]
        public Image Image { get; set; }

        [Description("Layout"), Category("Data")]
        public new string Layout
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

        public void OnValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var layoutSize = (int)Math.Sqrt(_layout.Length);
            var size = Math.Min(Width, Height) - BorderSize*2 - 1;
            var cellSize = size/layoutSize + (size % layoutSize > 0 ? 1 : 0);

            e.Graphics.FillRectangle(GetBrush(_categories[(char)CellType.Empty]), new Rectangle(BorderSize, BorderSize, size, size));

            if (Image != null)
                e.Graphics.DrawImage(Image, new Rectangle(BorderSize, BorderSize, size, size));

            if (this.showLayoutToolStripMenuItem.Checked)
            {
                for (var i = 0; i < layoutSize; ++i)
                {
                    for (var j = 0; j < layoutSize; ++j)
                    {
                        var x = BorderSize + j * size / layoutSize;
                        var y = BorderSize + i * size / layoutSize;

                        var cell = _layout[i * layoutSize + j];

                        if (cell == (char)CellType.Empty)
                            continue;

                        Color color;
                        if (!_categories.TryGetValue(cell, out color))
                            color = Color.Black;

                        e.Graphics.FillRectangle(GetBrush(color), x, y, cellSize, cellSize);
                    }
                }
                this.curBrush.draw(e, this);
            }

            var pen = new Pen(Color.Black);
            if (this.showGridToolStripMenuItem.Checked)
            {
                for (var i = 0; i <= layoutSize; ++i)
                {
                    var x = BorderSize + i * size / layoutSize;
                    e.Graphics.DrawLine(pen, x, BorderSize, x, BorderSize + size);
                    e.Graphics.DrawLine(pen, BorderSize, x, BorderSize + size, x);
                }
            }

            pen = new Pen(Color.Purple);
            pen.Width = 3;
            if (mirror_vertical)
            {
                e.Graphics.DrawLine(pen, BorderSize + size / 2, BorderSize, BorderSize + size / 2, BorderSize + size);
            }
            if (mirror_horizontal)
            {
                e.Graphics.DrawLine(pen, BorderSize, BorderSize + size / 2, BorderSize + size, BorderSize + size / 2);
            }

            if (this.showBarrelsToolStripMenuItem.Checked && _barrels != null)
            {
                pen = new Pen(Color.Black);
                pen.Width = 2;
                var widePen = new Pen(Color.Black, 3);
                foreach (var barrel in _barrels)
                {
                    var x = BorderSize + (1 - barrel.X) * size / 2;
                    var y = BorderSize + (1 - barrel.Y) * size / 2;

                    var offset = barrel.Offset * size / 2;
                    var radius = Math.Max(cellSize / 4, offset);

                    e.Graphics.DrawEllipse(widePen, x - radius, y - radius, radius * 2, radius * 2);

                    var length = radius + cellSize / 2;
                    e.Graphics.DrawLine(pen, x - length, y, x + length, y);
                    e.Graphics.DrawLine(pen, x, y - length, x, y + length);

                    var dir = RotationHelpers.Direction(-barrel.Rotation);
                    var p0 = new Point((int)(x + dir.x * offset), (int)(y + dir.y * offset));
                    var p1 = new Point((int)(x + dir.x * (offset + cellSize)), (int)(y + dir.y * (offset + cellSize)));
                    var d = new Point((int)(dir.x * cellSize / 2f), (int)(dir.y * cellSize / 2f));
                    var p2 = new Point(p1.X + d.X, p1.Y + d.Y);

                    e.Graphics.DrawLine(widePen, p0, p2);
                    e.Graphics.DrawLine(pen, p2.X, p2.Y, p1.X - d.Y, p1.Y + d.X);
                    e.Graphics.DrawLine(pen, p2.X, p2.Y, p1.X + d.Y, p1.Y - d.X);
                }
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

        public BrushType curBrush = BrushType.DEFAULT;

        private void LayoutEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.curButton != MouseButtons.Left && this.curButton != MouseButtons.Right)
            {
                return;
            }
            this.curBrush.onMouseDrag(e, this);
        }

        private void LayoutEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.curButton != MouseButtons.None)
            {
                return;
            }
            int num = this.PointToCell(e.X, e.Y);
            if (num == -1)
            {
                return;
            }
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                this.curButton = MouseButtons.Left;
            }
            else if (e.Button.HasFlag(MouseButtons.Right))
            {
                this.curButton = MouseButtons.Right;
            }
            else if (e.Button.HasFlag(MouseButtons.Middle))
            {
                this.SelectedCategory = this._layout[num];
                return;
            }
            if (Control.ModifierKeys == Keys.Shift)
            {
                this.curBrush = BrushType.LINE;
            }
            else if (Control.ModifierKeys == Keys.Control)
            {
                this.curBrush = BrushType.RECT;
            }
            else if (Control.ModifierKeys == Keys.Alt)
            {
                this.curBrush = BrushType.ELLIPSE;
            }
            else if (Control.ModifierKeys == (Keys.Shift | Keys.Control))
            {
                this.curBrush = BrushType.FILL;
            }
            else
            {
                this.curBrush = BrushType.DEFAULT;
            }
            this.curBrush.onMouseDown(e, this);
        }

        private void LayoutEditor_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.curButton == MouseButtons.Left || this.curButton == MouseButtons.Right)
            {
                this.curBrush.onMouseUp(e, this);
                this.curButton = MouseButtons.None;
                this.Refresh();
                /*if (this.history)
                {
                    ShipEditorDialog.EditHistory.Snapshot();
                    return;
                }*/
            }
            else if (e.Button.HasFlag(MouseButtons.Right))
            {
                Point p = PointToScreen(new Point(e.X, e.Y) ) ;
                this.contextMenuStrip1.Show(p);
            }
        }

        public void Draw(int mouseX, int mouseY, char value)
        {
            int cell = this.PointToCell(mouseX, mouseY);
            if (cell == -1)
            {
                return;
            }
            this.Draw(cell, value);
        }

        public void Draw(int cell, char value)
        {
            if (this._layout[cell] == value)
            {
                return;
            }
            this.PaintCell(cell, value);
            base.Invalidate();
            this.OnValueChanged(this, EventArgs.Empty);
        }

        //Next 3 functions was taken from decompilled code
        public void DrawLine(int x, int y, int x2, int y2, char value)
        {
            int num = this.PointToCell(x, y);
            x = num % this.LayoutWidth;
            y = num / this.LayoutWidth;
            int num2 = this.PointToCell(x2, y2);
            x2 = num2 % this.LayoutWidth;
            y2 = num2 / this.LayoutWidth;
            int num3 = x2 - x;
            int num4 = y2 - y;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            if (num3 < 0)
            {
                num5 = -1;
            }
            else if (num3 > 0)
            {
                num5 = 1;
            }
            if (num4 < 0)
            {
                num6 = -1;
            }
            else if (num4 > 0)
            {
                num6 = 1;
            }
            if (num3 < 0)
            {
                num7 = -1;
            }
            else if (num3 > 0)
            {
                num7 = 1;
            }
            int num9 = Math.Abs(num3);
            int num10 = Math.Abs(num4);
            if (num9 <= num10)
            {
                num9 = Math.Abs(num4);
                num10 = Math.Abs(num3);
                if (num4 < 0)
                {
                    num8 = -1;
                }
                else if (num4 > 0)
                {
                    num8 = 1;
                }
                num7 = 0;
            }
            int num11 = num9 >> 1;
            for (int i = 0; i <= num9; i++)
            {
                this.PaintCell(x + y * this.LayoutWidth, value);
                num11 += num10;
                if (num11 >= num9)
                {
                    num11 -= num9;
                    x += num5;
                    y += num6;
                }
                else
                {
                    x += num7;
                    y += num8;
                }
            }
        }

        public void DrawEllipse(int x0, int y0, int x1, int y1, char value)
        {
            int num = this.PointToCell(Math.Min(x0, x1), Math.Min(y0, y1));
            int num2 = this.PointToCell(Math.Max(x0, x1), Math.Max(y0, y1));
            int x2 = num % this.LayoutWidth;
            int y2 = num / this.LayoutWidth;
            int x3 = num2 % this.LayoutWidth;
            int y3 = num2 / this.LayoutWidth;
            this.DrawEllipseAligned(x2, y2, x3, y3, value);
        }

        private void DrawEllipseAligned(int x0, int y0, int x1, int y1, char value)
        {
            decimal d = (x0 + x1) / 2.0m;
            decimal d2 = (y0 + y1) / 2.0m;
            decimal num = (x1 - x0) / 2.0m + 0.5m;
            decimal num2 = (y1 - y0) / 2.0m + 0.5m;
            for (int i = x0; i <= x1; i++)
            {
                for (int j = y0; j <= y1; j++)
                {
                    if ((i - d) * (i - d) / (num * num) + (j - d2) * (j - d2) / (num2 * num2) <= 1m)
                    {
                        this.PaintCell(i + j * this.LayoutWidth, value);
                    }
                }
            }
        }


        private void LayoutEditor_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        public int PointToCell(int mouseX, int mouseY)
        {
            int width = (int)Math.Sqrt((double)this._layout.Length);
            int cellSize = Math.Min(base.Width, base.Height) - 1 - this.BorderSize * 2;
            int x = width * (mouseX - this.BorderSize) / cellSize;
            int y = width * (mouseY - this.BorderSize) / cellSize;
            if (x < 0 || x >= width)
            {
                return -1;
            }
            if (y < 0 || y >= width)
            {
                return -1;
            }
            return y * width + x;
        }

        public void PaintCell(int cell, char value)
        {
            this.PaintCell(cell % this.LayoutWidth, cell / this.LayoutWidth, value);
        }

        public void PaintCell(int x, int y, char value)
        {
            foreach (int index in this.GetMirrorCells(x, y))
            {
                this._layout[index] = value;
            }
        }

        public List<int> GetMirrorCells(int cell)
        {
            return this.GetMirrorCells(cell % this.LayoutWidth, cell / this.LayoutWidth);
        }

        public List<int> GetMirrorCells(int x, int y)
        {
            List<int> list = new List<int>();
            int layoutWidth = this.LayoutWidth;
            int num = layoutWidth - 1;
            list.Add(x + y * layoutWidth);
            if (this.mirror_horizontal)
            {
                list.Add(num - x + y * layoutWidth);
            }
            if (this.mirror_vertical)
            {
                list.Add(x + (num - y) * layoutWidth);
            }
            if (this.mirror_horizontal && this.mirror_vertical)
            {
                list.Add(num - x + (num - y) * layoutWidth);
            }
            return list;
        }

        private IList<BarrelData> _barrels;
        public MouseButtons curButton;

        public int LayoutWidth
        {
            get
            {
                return (int)Math.Sqrt((double)this._layout.Length);
            }
        }

        public char EraseCategory
        {
            get
            {
                return this._categories.First<KeyValuePair<char, Color>>().Key;
            }
        }

        public enum AlignSide
        {
            Center,
            TopLeft,
            BottomRight
        }

        public Point Align(int mouseX, int mouseY, LayoutEditor.AlignSide side)
        {
            return this.Align(this.PointToCell(mouseX, mouseY), side);
        }

        public Point Align(int cell, LayoutEditor.AlignSide side)
        {
            int num = (int)Math.Sqrt((double)this._layout.Length);
            int num2 = Math.Min(base.Width, base.Height) - this.BorderSize * 2 - 1;
            int num3 = num2 / num + ((num2 % num > 0) ? 1 : 0);
            int num4 = cell / num;
            int num5 = cell % num;
            int num6 = this.BorderSize + num5 * num2 / num;
            int num7 = this.BorderSize + num4 * num2 / num;
            if (side == LayoutEditor.AlignSide.Center)
            {
                return new Point(num6 + num3 / 2, num7 + num3 / 2);
            }
            if (side != LayoutEditor.AlignSide.BottomRight)
            {
                return new Point(num6, num7);
            }
            return new Point(num6 + num3, num7 + num3);
        }

        private bool mirror_horizontal = false;

        private bool mirror_vertical = false;

        private void horizontalSymmetryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mirror_horizontal = this.horizontalSymmetryToolStripMenuItem.Checked;
            Refresh();
        }

        private void verticalSymmetryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mirror_vertical = this.verticalSymmetryToolStripMenuItem.Checked;
            Refresh();
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void showLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void showBarrelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Refresh();
        }
    }

    public abstract class BrushType
    {
        public BrushType()
        {
        }

        public virtual void onMouseDown(MouseEventArgs e, LayoutEditor editor)
        {
        }

        public virtual void onMouseDrag(MouseEventArgs e, LayoutEditor editor)
        {
        }

        public virtual void onMouseUp(MouseEventArgs e, LayoutEditor editor)
        {
        }

        public virtual void draw(PaintEventArgs e, LayoutEditor editor)
        {
        }

        public static readonly BrushType DEFAULT = new BrushType.DefaultBrush();

        public static readonly BrushType RECT = new BrushType.RectangleBrush();

        public static readonly BrushType LINE = new BrushType.LineBrush();

        public static readonly BrushType FILL = new BrushType.FillBrush();

        public static readonly BrushType ELLIPSE = new BrushType.CircleBrush();

        private class DefaultBrush : BrushType
        {
            public override void onMouseDown(MouseEventArgs e, LayoutEditor editor)
            {
                int cell = editor.PointToCell(e.X, e.Y);
                if (cell != -1)
                {
                    editor.Draw(cell, (editor.curButton == MouseButtons.Left) ? editor.SelectedCategory : editor.EraseCategory);
                }
            }

            public override void onMouseDrag(MouseEventArgs e, LayoutEditor editor)
            {
                int cell = editor.PointToCell(e.X, e.Y);
                if (cell != -1)
                {
                    editor.Draw(cell, (editor.curButton == MouseButtons.Left) ? editor.SelectedCategory : editor.EraseCategory);
                }
            }
        }

        private class RectangleBrush : BrushType
        {
            public override void onMouseDown(MouseEventArgs e, LayoutEditor editor)
            {
                this._x0 = (this.curX = e.X);
                this._y0 = (this.curY = e.Y);
            }

            public override void onMouseDrag(MouseEventArgs e, LayoutEditor editor)
            {
                int num = editor.PointToCell(e.X, e.Y);
                if (num != -1 && this.lastCell != num)
                {
                    this.curX = e.X;
                    this.curY = e.Y;
                    editor.Refresh();
                    this.lastCell = num;
                }
            }

            public override void onMouseUp(MouseEventArgs e, LayoutEditor editor)
            {
                if (!e.Button.HasFlag(MouseButtons.Left) && !e.Button.HasFlag(MouseButtons.Right))
                {
                    return;
                }
                int num = Math.Min(this._x0, this.curX);
                int num2 = Math.Max(this._x0, this.curX);
                int num3 = Math.Min(this._y0, this.curY);
                int num4 = Math.Max(this._y0, this.curY);
                this.lastCell = -1;
                for (int i = num; i <= num2; i++)
                {
                    for (int j = num3; j <= num4; j++)
                    {
                        editor.PaintCell(editor.PointToCell(i, j), (editor.curButton == MouseButtons.Left) ? editor.SelectedCategory : editor.EraseCategory);
                    }
                }
                editor.Invalidate();
                editor.OnValueChanged(editor, EventArgs.Empty);
            }

            public override void draw(PaintEventArgs e, LayoutEditor editor)
            {
                if (editor.curButton != MouseButtons.None)
                {
                    int cell = editor.PointToCell(this._x0, this._y0);
                    int cell2 = editor.PointToCell(this.curX, this.curY);
                    List<int> mirrorCells = editor.GetMirrorCells(cell);
                    List<int> mirrorCells2 = editor.GetMirrorCells(cell2);
                    int num = Math.Min(mirrorCells.Count, mirrorCells2.Count);
                    for (int i = 0; i < num; i++)
                    {
                        this.doDraw(e, editor, mirrorCells[i], mirrorCells2[i]);
                    }
                }
            }

            public virtual void doDraw(PaintEventArgs e, LayoutEditor editor, int c1, int c2)
            {
                int layoutWidth = editor.LayoutWidth;
                int num = Math.Min(c1 % layoutWidth, c2 % layoutWidth);
                int num2 = Math.Max(c1 % layoutWidth, c2 % layoutWidth);
                int num3 = Math.Min(c1 / layoutWidth, c2 / layoutWidth);
                int num4 = Math.Max(c1 / layoutWidth, c2 / layoutWidth);
                Point point = editor.Align(num + num3 * layoutWidth, LayoutEditor.AlignSide.TopLeft);
                Point point2 = editor.Align(num2 + num4 * layoutWidth, LayoutEditor.AlignSide.BottomRight);
                Pen pen = new Pen(Color.Red, 3f);
                e.Graphics.DrawRectangle(pen, point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
            }

            protected int _x0;

            protected int _y0;

            protected int curX;

            protected int curY;

            protected int lastCell = -1;
        }

        private class LineBrush : BrushType.RectangleBrush
        {
            public override void onMouseUp(MouseEventArgs e, LayoutEditor editor)
            {
                if (!e.Button.HasFlag(MouseButtons.Left) && !e.Button.HasFlag(MouseButtons.Right))
                {
                    return;
                }
                this.lastCell = -1;
                Point point = editor.Align(this._x0, this._y0, LayoutEditor.AlignSide.Center);
                Point point2 = editor.Align(this.curX, this.curY, LayoutEditor.AlignSide.Center);
                editor.DrawLine(point.X, point.Y, point2.X, point2.Y, (editor.curButton == MouseButtons.Left) ? editor.SelectedCategory : editor.EraseCategory);
                editor.Invalidate();
                editor.OnValueChanged(editor, EventArgs.Empty);
            }

            public override void doDraw(PaintEventArgs e, LayoutEditor editor, int c1, int c2)
            {
                Point point = editor.Align(c1, LayoutEditor.AlignSide.Center);
                Point point2 = editor.Align(c2, LayoutEditor.AlignSide.Center);
                Pen pen = new Pen(Color.Red, 3f);
                e.Graphics.DrawLine(pen, point.X, point.Y, point2.X, point2.Y);
            }
        }

        private class FillBrush : BrushType
        {
            public override void onMouseDown(MouseEventArgs e, LayoutEditor editor)
            {
                int num = editor.PointToCell(e.X, e.Y);
                if (num == -1)
                {
                    return;
                }
                char c = editor.Layout[num];
                char c2 = (editor.curButton == MouseButtons.Left) ? editor.SelectedCategory : editor.EraseCategory;
                if (c == c2)
                {
                    return;
                }
                int x = num % editor.LayoutWidth;
                int y = num / editor.LayoutWidth;
                this.filled = 0;
                try
                {
                    this.recursiveFill(x, y, editor, c, c2);
                }
                catch (Exception ex)
                {
                    if (!(ex.Message == "10,000"))
                    {
                        throw ex;
                    }
                    //ShipEditorDialog.EditHistory.restore();
                    MessageBox.Show("Can't fill more than 10,000 tiles.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    editor.curButton = MouseButtons.None;
                }
                editor.Draw(num, c2);
            }

            public void recursiveFill(int x, int y, LayoutEditor editor, char oldValue, char newValue)
            {
                int num = x + editor.LayoutWidth * y;
                if (x < 0 || y < 0 || x >= editor.LayoutWidth || y >= editor.LayoutWidth || editor.Layout[num] != oldValue)
                {
                    return;
                }
                if (this.filled > 10000)
                {
                    throw new Exception("10,000");
                }
                this.filled++;
                editor.PaintCell(num, newValue);
                this.recursiveFill(x - 1, y, editor, oldValue, newValue);
                this.recursiveFill(x + 1, y, editor, oldValue, newValue);
                this.recursiveFill(x, y - 1, editor, oldValue, newValue);
                this.recursiveFill(x, y + 1, editor, oldValue, newValue);
            }

            private int filled;
        }

        private class CircleBrush : BrushType.RectangleBrush
        {
            public override void onMouseUp(MouseEventArgs e, LayoutEditor editor)
            {
                if (!e.Button.HasFlag(MouseButtons.Left) && !e.Button.HasFlag(MouseButtons.Right))
                {
                    return;
                }
                int x = Math.Min(this._x0, this.curX);
                int x2 = Math.Max(this._x0, this.curX);
                int y = Math.Min(this._y0, this.curY);
                int y2 = Math.Max(this._y0, this.curY);
                this.lastCell = -1;
                editor.DrawEllipse(x, y, x2, y2, (editor.curButton == MouseButtons.Left) ? editor.SelectedCategory : editor.EraseCategory);
                editor.Invalidate();
                editor.OnValueChanged(editor, EventArgs.Empty);
            }
            public override void doDraw(PaintEventArgs e, LayoutEditor editor, int c1, int c2)
            {
                int layoutWidth = editor.LayoutWidth;
                int num = Math.Min(c1 % layoutWidth, c2 % layoutWidth);
                int num2 = Math.Max(c1 % layoutWidth, c2 % layoutWidth);
                int num3 = Math.Min(c1 / layoutWidth, c2 / layoutWidth);
                int num4 = Math.Max(c1 / layoutWidth, c2 / layoutWidth);
                Point point = editor.Align(num + num3 * layoutWidth, LayoutEditor.AlignSide.TopLeft);
                Point point2 = editor.Align(num2 + num4 * layoutWidth, LayoutEditor.AlignSide.BottomRight);
                Pen pen = new Pen(Color.Red, 3f);
                e.Graphics.DrawEllipse(pen, point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
            }
        }
    }
}
