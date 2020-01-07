using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Conrols;
using GameDatabase.Controls;
using GameDatabase.EditorModel;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.ShipLayout;

namespace GameDatabase
{
    public partial class LayoutEditor : UserControl
    {
        public LayoutEditor()
        {
            InitializeComponent();

            _layout = new LayoutModel();
            _layout.DataChangedEvent += OnLayoutChanged;

            _categories = new Dictionary<char, Color> { { '0', Color.LightGray }, { '1', Color.Blue } };
            SelectedCategory = _categories.Last().Key;

            _selectedBrush = _defaultBrush;

        }

        [Description("Layout"), Category("Margins")]
        public int BorderSize { get; set; }

        [Description("Image"), Category("Data")]
        public Image Image { get; set; }

        [Description("Layout"), Category("Data")]
        public new string Layout
        {
            get { return _layout.Data; }
            set { _layout.Data = value; }
        }

        [Serializable]
        public struct BarrelData
        {
            public float X;
            public float Y;
            public float Rotation;
            public float Offset;
            public float Arc;
            public string Text;
        }

        [Serializable]
        public struct EngineData
        {
            public float X;
            public float Y;
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

        [Description("Barrels"), Category("Data")]
        public IList<EngineData> Engines
        {
            get { return _engines; }
            set
            {
                _engines = value;
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

        [Description("BarrelsCollectionEditor"), Category("Data")]
        public BarrelsCollectionEditor BarrelsCollection { get; set; }

        public event EventHandler ValueChanged;

        public void OnValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        private void DrawLayout(PaintData data)
        {
            data.Graphics.FillRectangle(GetBrush(_categories[(char)CellType.Empty]), new Rectangle(BorderSize, BorderSize, data.CanvasSize, data.CanvasSize));

            if (Image != null && showImageToolStripMenuItem.Checked)
                data.Graphics.DrawImage(Image, new Rectangle(BorderSize, BorderSize, data.CanvasSize, data.CanvasSize));

            if (!showLayoutToolStripMenuItem.Checked) return;

            for (var i = 0; i < data.LayoutSize; ++i)
            {
                for (var j = 0; j < data.LayoutSize; ++j)
                {
                    var x = BorderSize + j * data.CanvasSize / data.LayoutSize;
                    var y = BorderSize + i * data.CanvasSize / data.LayoutSize;

                    var cell = _layout[j, i];

                    if (cell == (char)CellType.Empty)
                        continue;

                    Color color;
                    if (!_categories.TryGetValue(cell, out color))
                        color = Color.Black;

                    data.Graphics.FillRectangle(GetBrush(color), x, y, data.CellSize, data.CellSize);
                }
            }
        }

        private void DrawGrid(PaintData data)
        {
            if (!showGridToolStripMenuItem.Checked) return;

            var pen = new Pen(Color.Black);
            for (var i = 0; i <= data.LayoutSize; ++i)
            {
                var x = BorderSize + i * data.CanvasSize / data.LayoutSize;
                data.Graphics.DrawLine(pen, x, BorderSize, x, BorderSize + data.CanvasSize);
                data.Graphics.DrawLine(pen, BorderSize, x, BorderSize + data.CanvasSize, x);
            }
        }

        private void DrawAxis(PaintData data)
        {
            if (!data.MirrorHorizontal && !data.MirrorVertical) return;

            var pen = new Pen(Color.Purple, 3);

            if (data.MirrorVertical)
                data.Graphics.DrawLine(pen, BorderSize + data.CanvasSize / 2, BorderSize, BorderSize + data.CanvasSize / 2, BorderSize + data.CanvasSize);
            if (data.MirrorHorizontal)
                data.Graphics.DrawLine(pen, BorderSize, BorderSize + data.CanvasSize / 2, BorderSize + data.CanvasSize, BorderSize + data.CanvasSize / 2);
        }

        private void DrawBarrels(PaintData data)
        {
            if (!showBarrelsToolStripMenuItem.Checked || _barrels == null) return;


            if (data.CanvasSize < 0) return;

            for (int i = 0; i < _barrels.Count; i++)
            {
                var pen = new Pen(Color.Black, 2);
                var widePen = new Pen(Color.Black, 3);

                if (barrelsEditingModeToolStripMenuItem.Checked && BarrelsCollection.GetSelectedItemId() == i)
                {
                    pen = new Pen(Color.DarkRed, 2);
                    widePen = new Pen(Color.DarkRed, 3);
                }

                var barrel = _barrels.ElementAt(i);
                var x = BorderSize + (1 - barrel.X) * data.CanvasSize / 2;
                var y = BorderSize + (1 - barrel.Y) * data.CanvasSize / 2;

                var offset = barrel.Offset * data.CanvasSize / 2;
                var radius = Math.Max(data.CellSize / 4, offset);


                data.Graphics.DrawEllipse(widePen, x - radius, y - radius, radius * 2, radius * 2);

                var length = radius + data.CellSize / 2;
                data.Graphics.DrawLine(pen, x - length, y, x + length, y);
                data.Graphics.DrawLine(pen, x, y - length, x, y + length);

                var startAngle = barrel.Rotation - barrel.Arc / 2f;
                var endAngle = startAngle + barrel.Arc;
                var dir = RotationHelpers.Direction(-startAngle);
                var p0 = new Point((int)(x + dir.x * offset), (int)(y + dir.y * offset));
                var p1 = new Point((int)(x + dir.x * (offset + data.CellSize)), (int)(y + dir.y * (offset + data.CellSize)));
                var d = new Point((int)(dir.x * data.CellSize / 2f), (int)(dir.y * data.CellSize / 2f));
                var p2 = new Point(p1.X + d.X, p1.Y + d.Y);
                data.Graphics.DrawLine(widePen, p0, p2);
                data.Graphics.DrawLine(pen, p2.X, p2.Y, p1.X - d.Y, p1.Y + d.X);

                dir = RotationHelpers.Direction(-endAngle);
                p0 = new Point((int)(x + dir.x * offset), (int)(y + dir.y * offset));
                p1 = new Point((int)(x + dir.x * (offset + data.CellSize)), (int)(y + dir.y * (offset + data.CellSize)));
                d = new Point((int)(dir.x * data.CellSize / 2f), (int)(dir.y * data.CellSize / 2f));
                p2 = new Point(p1.X + d.X, p1.Y + d.Y);
                data.Graphics.DrawLine(widePen, p0, p2);
                data.Graphics.DrawLine(pen, p2.X, p2.Y, p1.X + d.Y, p1.Y - d.X);

                data.Graphics.DrawArc(pen, x - radius - data.CellSize, y - radius - data.CellSize, radius * 2 + data.CellSize * 2f, radius * 2 + data.CellSize * 2f, -barrel.Rotation - barrel.Arc / 2 - 90, barrel.Arc);

                if (showBarrelsNumbersToolStripMenuItem.Checked)
                {
                    float fontSize = Math.Max(24f,radius*7f/4f);
                    float r = fontSize * 4f / 7f;
                    SolidBrush myBrush = new SolidBrush(Color.White);
                    data.Graphics.FillEllipse(myBrush, x - r, y - r, r * 2, r * 2);
                    data.Graphics.DrawEllipse(widePen, x - r, y - r, r * 2, r * 2);

                    StringFormat format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    Font drawFont = new Font("Arial", fontSize, GraphicsUnit.Pixel);
                    SolidBrush textBrush = new SolidBrush(Color.Black);
                    data.Graphics.DrawString((i+1).ToString(), drawFont, textBrush, x, y, format);
                }
            }
        }

        private void DrawEngines(PaintData data)
        {
            if (!showEnginesToolStripMenuItem.Checked || _engines == null) return;

            var pen = new Pen(Color.DarkOrange, 2);
            var widePen = new Pen(Color.DarkOrange, 3);

            foreach(var engine in _engines)
            {
                var x = BorderSize + (1 - engine.X) * data.CanvasSize / 2;
                var y = BorderSize + (1 - engine.Y) * data.CanvasSize / 2;

                var length = data.CellSize/2;
                data.Graphics.DrawLine(pen, x - length, y, x + length, y);
                data.Graphics.DrawLine(widePen, x, y - length, x, y + length*3);
                data.Graphics.DrawEllipse(pen, x - length, y - length, length* 2, length* 2);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var layoutSize = _layout.Size;
            var canvasSize = Math.Min(Width, Height) - BorderSize*2 - 1;
            var cellSize = canvasSize / layoutSize + (canvasSize % layoutSize > 0 ? 1 : 0);
            var paintData = new PaintData(e.Graphics, layoutSize, canvasSize, BorderSize, cellSize, _layout.MirrorHorizontal, _layout.MirrorVertical);

            DrawLayout(paintData);
            _selectedBrush.OnDraw(paintData);

            DrawGrid(paintData);
            DrawAxis(paintData);
            DrawBarrels(paintData);
            DrawEngines(paintData);
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

        public Vector2 PointToBarrelPos(float x, float y)
        {
            var canvasSize = Math.Min(Width, Height) - BorderSize * 2 - 1;
            var ret = new Vector2();
            ret.x = (canvasSize - 2f * (x - BorderSize)) / canvasSize;
            ret.y = (canvasSize - 2f * (y - BorderSize)) / canvasSize;
            return ret;
        }
        public Vector2 BarrelPosToPoint(Vector2 pos)
        {
            var canvasSize = Math.Min(Width, Height) - BorderSize * 2 - 1;
            var ret = new Vector2();
            ret.x = BorderSize + (1 - pos.x) * canvasSize / 2;
            ret.y = BorderSize + (1 - pos.y) * canvasSize / 2;
            return ret;
        }

        private void BarrelPlace(MouseEventArgs e)
        {
            if (BarrelsCollection != null)
            {
                switch (ModifierKeys)
                {
                    case Keys.Alt:
                        BarrelsCollection.clickClone();
                        var barrel = BarrelsCollection.SelectedBarrel();
                        barrel.Position = PointToBarrelPos(e.X, e.Y);
                        BarrelsCollection.SetSelectedObject(barrel);
                        _barrelsEditor.SetBarrel(barrel);
                        break;
                    case Keys.Control:
                        barrel = BarrelsCollection.SelectedBarrel();
                        barrel.Position = PointToBarrelPos(e.X, e.Y);
                        BarrelsCollection.SetSelectedObject(barrel);
                        _barrelsEditor.UpdateBarrel();
                        break;
                    case Keys.Shift:
                        var closestId = 0;
                        var closestSqareDist = float.MaxValue;
                        var barrels = BarrelsCollection.GetBarrels().ToArray();
                        for (int i = 0; i < barrels.Length; i++)
                        {
                            var pos = BarrelPosToPoint(barrels[i].Position);
                            var dx = pos.x - e.X;
                            var dy = pos.y - e.Y;
                            if (dx * dx + dy * dy < closestSqareDist)
                            {
                                closestSqareDist = dx * dx + dy * dy;
                                closestId = i;
                            }
                        }
                        BarrelsCollection.SetSelectedId(closestId);
                        _barrelsEditor.SetBarrel(barrels[closestId]);
                        Invalidate();
                        break;
                }
                return;
            }
        }

        private void LayoutEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsDragging) return;
            
            if (barrelsEditingModeToolStripMenuItem.Checked)
            {
                BarrelPlace(e);
                return;
            }

            var cellIndex = PointToCellIndex(e.X, e.Y);
            if (cellIndex < 0) return;

            if (e.Button.HasFlag(MouseButtons.Middle))
            {
                SelectedCategory = _layout[cellIndex];
                return;
            }

            var leftButtonPressed = e.Button.HasFlag(MouseButtons.Left);
            var rightButtonPressed = e.Button.HasFlag(MouseButtons.Right);

            if (!leftButtonPressed && !rightButtonPressed) return;

            _mouseButton = leftButtonPressed ? MouseButtons.Left : MouseButtons.Right;
            var celltype = leftButtonPressed ? SelectedCategory : EraseCategory;
            var position = new MousePosition(e.X, e.Y, cellIndex);

            switch (ModifierKeys)
            {
                case Keys.Shift:
                    _selectedBrush = _lineBrush; break;
                case Keys.Control:
                    _selectedBrush = _rectangleBrush; break;
                case Keys.Alt:
                    _selectedBrush = _circleBrush; break;
                case Keys.Shift | Keys.Control:
                    _selectedBrush = _fillBrush; break;
                default:
                    _selectedBrush = _defaultBrush; break;
            }

            _selectedBrush.OnMouseDown(position, _layout, celltype);
        }

        private void LayoutEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDragging) return;
            var celltype = _mouseButton == MouseButtons.Left ? SelectedCategory : EraseCategory;
            var cellIndex = PointToCellIndex(e.X, e.Y);
            var position = new MousePosition(e.X, e.Y, cellIndex);

            _selectedBrush.OnMouseDrag(position, _layout, celltype);
            Invalidate();
        }

        private void LayoutEditor_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsDragging)
            {
                if (e.Button.HasFlag(MouseButtons.Right))
                {
                    var p = PointToScreen(new Point(e.X, e.Y));

                    if (_barrels == null)
                    {
                        showBarrelsToolStripMenuItem.Enabled = showBarrelsToolStripMenuItem.Checked = false;

                        barrelsEditingModeToolStripMenuItem.Enabled = barrelsEditingModeToolStripMenuItem.Checked = false;
                        showBarrelsNumbersToolStripMenuItem.Enabled = showBarrelsNumbersToolStripMenuItem.Checked = false;
                    }
                    if (Image == null) showImageToolStripMenuItem.Enabled = showImageToolStripMenuItem.Checked = false;
                    if (_engines == null) showEnginesToolStripMenuItem.Enabled = showEnginesToolStripMenuItem.Checked = false;

                    contextMenuStrip1.Show(p);
                }
                return;
            }

            var celltype = _mouseButton == MouseButtons.Left ? SelectedCategory : EraseCategory;
            var cellIndex = PointToCellIndex(e.X, e.Y);
            var position = new MousePosition(e.X, e.Y, cellIndex);
            _selectedBrush.OnMouseUp(position, _layout, celltype);
            _mouseButton = MouseButtons.None;
            Invalidate();
        }

        private int PointToCellIndex(int mouseX, int mouseY)
        {
            var layoutSize = _layout.Size;
            var size = Math.Min(Width, Height) - 1 - BorderSize * 2;

            var x = layoutSize * (mouseX - BorderSize) / size;
            var y = layoutSize * (mouseY - BorderSize) / size;

            if (x < 0 || x >= layoutSize)
                return -1;
            if (y < 0 || y >= layoutSize)
                return -1;

            return x + y * layoutSize;
        }

        private void LayoutEditor_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private char EraseCategory { get { return _categories.First().Key; } }

        private void OnLayoutChanged()
        {
            Invalidate();
            OnValueChanged(this, EventArgs.Empty);
        }

        private void horizontalSymmetryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _layout.MirrorHorizontal = horizontalSymmetryToolStripMenuItem.Checked;            
            Invalidate();
        }

        private void verticalSymmetryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _layout.MirrorVertical = verticalSymmetryToolStripMenuItem.Checked;
            Invalidate();
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void showLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void showBarrelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showBarrelsNumbersToolStripMenuItem.Enabled = showBarrelsToolStripMenuItem.Checked;
            Invalidate();
        }

        private void showBarrelsNumbersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void showEnginesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void showImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Invalidate();
        }

        private bool IsDragging { get { return _mouseButton != MouseButtons.None; } }

        private MouseButtons _mouseButton;

        private ILayoutBrush _selectedBrush;
        private IList<BarrelData> _barrels;
        private IList<EngineData> _engines;

        private readonly Dictionary<Color, Brush> _brushes = new Dictionary<Color, Brush>();
        private readonly Dictionary<char, Color> _categories;
        private readonly LayoutModel _layout;

        private static readonly ILayoutBrush _defaultBrush = new DefaultBrush();
        private static readonly ILayoutBrush _rectangleBrush = new RectangleBrush();
        private static readonly ILayoutBrush _lineBrush = new LineBrush();
        private static readonly ILayoutBrush _circleBrush = new CircleBrush();
        private static readonly ILayoutBrush _fillBrush = new FillBrush();

        [Serializable]
        public class CellCategory
        {
            public char Id { get; set; }
            public Color Color { get; set; }
        }

        private int lastId = -1;

        private void Parent_ResizeEnd(object sender, EventArgs e)
        {
            ResizeCanvas(lastId);
        }

        private void ResizeCanvas(int id)
        {
            if(lastId==-1 && Parent!=null)
                Parent.Resize += Parent_ResizeEnd;

            lastId = id;

            var items = new ToolStripMenuItem[] { defaultToolStripMenuItem,
                xToolStripMenuItem,
                xToolStripMenuItem1,
                xToolStripMenuItem2,
                xToolStripMenuItem3,
                xToolStripMenuItem4
            };
            for(var i = 0; i < items.Length; i++)
            {
                items[i].Checked = false;
            }
            items[id].Checked = true;

            var sizes = new int[]{ -1,20,40,60,80,100};
            if (id == 0)
            {
                Dock = DockStyle.Fill;
            } else
            {
                Dock = DockStyle.None;
                Width = this.Height = _layout.Size * sizes[id] + BorderSize * 2 + 1;
                if (Width < Parent.Width)
                {
                    Width = Parent.Width;
                } else if (Height < Parent.Height)
                {
                    Height = Parent.Height;
                } else
                {
                    Width += Parent.Width / 10;
                }
            }
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResizeCanvas(0);
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResizeCanvas(1);
        }

        private void xToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Dock = DockStyle.None;
            this.Width = this.Height = _layout.Size * 20 + BorderSize * 2 + 1;
            ResizeCanvas(2);
        }

        private void xToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ResizeCanvas(3);
        }

        private void xToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ResizeCanvas(4);
        }

        private void xToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ResizeCanvas(5);
        }

        private BarrelsEditor _barrelsEditor;

        private void barrelsEditingModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (barrelsEditingModeToolStripMenuItem.Checked)
            {
                BarrelsCollection.Visible = false;
                _barrelsEditor = new BarrelsEditor(BarrelsCollection);
                _barrelsEditor.FormClosed += barrelEditor_Closed;
                _barrelsEditor.Show();
                _barrelsEditor.Owner = FindForm();
                if (BarrelsCollection.Data == null || BarrelsCollection.Data.Length == 0)
                {
                    BarrelsCollection.Data = new Barrel[] { new Barrel() };
                }
                BarrelsCollection.SetSelectedId(0);
            } else
            {
                _barrelsEditor?.Close();
                BarrelsCollection.Visible = true;
                _barrelsEditor = null;
            }
        }

        private void barrelEditor_Closed(object sender, EventArgs e)
        {
            BarrelsCollection.Visible = true;
            barrelsEditingModeToolStripMenuItem.Checked = false;
            _barrelsEditor = null;
        }
    }
}
