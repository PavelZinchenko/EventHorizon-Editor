using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using GameDatabase.Controls;
using GameDatabase.Model;

namespace GameDatabase
{
    public partial class StructDataEditor : UserControl
    {
        [Description("Data"), Category("Data")]
        public object Data
        {
            get { return _data; }
            set
            {
                _data = value;
                BuildLayout();
            }
        }

        [Description("Database"), Category("Data")]
        public Database Database
        {
            get { return _database; }
            set { _database = value; }
        }

        [Description("ContentAutoScroll"), Category("Layout")]
        public bool ContentAutoScroll
        {
            get { return tableLayoutPanel.AutoScroll; }
            set { tableLayoutPanel.AutoScroll = value; }
        }

        [Description("Exclusions"), Category("Data")]
        public List<string> Exclusions
        {
            get { return _exclusions.ToList(); }
            set { _exclusions = new HashSet<string>(value); }
        }

        [Description("CellBorderStyle"), Category("Layout")]
        public TableLayoutPanelCellBorderStyle CellBorderStyle
        {
            get { return tableLayoutPanel.CellBorderStyle; }
            set { tableLayoutPanel.CellBorderStyle = value; }
        }

        public event EventHandler DataChanged;

        public StructDataEditor()
        {
            InitializeComponent();
        }

        private void Cleanup()
        {
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowStyles.Clear();
            _layouts.Clear();
            _binding.Clear();
        }

        private void BuildLayout()
        {
            Cleanup();

            if (_data == null)
                return;

            var type = _data.GetType();
            var fields = type.GetFields().Where(f => f.IsPublic && !f.IsStatic).ToDictionary(field => field.Name);

            var rowCount = fields.Count;
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = rowCount+1;

            tableLayoutPanel.SuspendLayout();
            for (var i = 0; i <= tableLayoutPanel.RowCount; ++i)
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _ignoreEvents = true;

            var rowId = 0;
            foreach (var item in fields)
            {
                if (_exclusions.Contains(item.Key))
                    continue;

                var value = item.Value.GetValue(_data);

                var control = CreateControls(item.Key, value, item.Value.FieldType, rowId);
                if (control != null)
                    _binding.Add(control, item.Value);
                else
                    CreateLabel(value != null ? value.ToString() : "null", 1, rowId);

                rowId++;
            }

            _ignoreEvents = false;

            tableLayoutPanel.ResumeLayout();
        }

        private object CreateControls(string name, object value, Type type, int rowId)
        {
            CreateLabel(name, 0, rowId);

            if (type.IsEnum)
            {
                var items = Enum.GetValues(type).OfType<object>();
                return CreateComboBox(items, value, 1, rowId);
            }

            if (type == typeof(NumericValue<int>))
            {
                var numeric = (NumericValue<int>)value;
                return CreateNumericContol(numeric.Value, numeric.Min, numeric.Max, 1, 0, 1, rowId);
            }

            if (type == typeof(NumericValue<float>))
            {
                var numeric = (NumericValue<float>)value;
                return CreateNumericContol((decimal)numeric.Value, (decimal)numeric.Min, (decimal)numeric.Max, (decimal)0.1f, 5, 1, rowId);
            }

            if (type == typeof(string))
                return CreateTextBox((string)value, 1, rowId);

            if (type == typeof(bool))
                return CreateCheckBox((bool)value, 1, rowId);

            if (type == typeof(Color))
                return CreateColorButton((Color)value, 1, rowId);

            if (type == typeof(Layout))
                return CreateLayout((Layout)value ?? new Layout(null), 1, rowId);

            if (type.IsArray)
                return CreateCollection((Array)value ?? (Array)Activator.CreateInstance(type, 0), 1, rowId);

            if (type == typeof(Vector2))
                return CreateVectorEditor((Vector2)value, 1, rowId);

            object result;
            if ((result = TryCreateIdItem(value, type, _database.ComponentIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.WeaponIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.DeviceIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.AmmunitionIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.DroneBayIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.ShipIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.ShipBuildIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.SatelliteIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.SatelliteBuildIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.TechnologyIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.ComponentStatsIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.ComponentModIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.Factions, 1, rowId)) != null)
                return result;

            return null;
        }

        private object TryCreateIdItem<T>(object value, Type type, IEnumerable<ItemId<T>> items, int column, int row)
        {
            if (type != typeof(ItemId<T>))
                return null;

            if (_data.GetType() == typeof(T))
                return CreateLabel(((ItemId<T>)value).Id.ToString(), column, row);

            return CreateComboBox(items.Cast<object>(), value, column, row);
        }

        private Label CreateLabel(string text, int column, int row)
        {
            var label = new Label()
            {
                Text = text,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                AutoSize = true,
            };

            tableLayoutPanel.Controls.Add(label, column, row);
            return label;
        }

        private TextBox CreateTextBox(string text, int column, int row)
        {
            var textbox = new TextBox()
            {
                Text = text,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
            };

            textbox.TextChanged += OnTextBoxValueChanged;

            tableLayoutPanel.Controls.Add(textbox, column, row);
            return textbox;
        }

        private ComboBox CreateComboBox(IEnumerable<object> items, object value, int column, int row)
        {
            var comboBox = new ComboBox()
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
            };

            comboBox.Items.AddRange(items.ToArray());
            comboBox.SelectedItem = value;
            comboBox.SelectedValueChanged += OnComboBoxValueChanged;
            comboBox.MouseWheel += DisableMouseWheel;

            tableLayoutPanel.Controls.Add(comboBox, column, row);
            return comboBox;
        }

        private CheckBox CreateCheckBox(bool value, int column, int row)
        {
            var check = new CheckBox()
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                Dock = DockStyle.Fill,
                Checked = value,
            };

            check.CheckedChanged += OnCheckedChanged;

            tableLayoutPanel.Controls.Add(check, column, row);
            return check;
        }


        private Button CreateColorButton(Color color, int column, int row)
        {
            var button = new Button()
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                Dock = DockStyle.Fill,
                BackColor = color,
                Text = string.Empty,
            };

            button.Click += OnColorButtonClicked;

            tableLayoutPanel.Controls.Add(button, column, row);
            return button;
        }

        private LayoutEditor CreateLayout(Layout layout, int column, int row)
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                ColumnCount = 2,
                RowCount = 1,
            };

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            panel.SuspendLayout();

            var layoutEditor = new LayoutEditor
            {
                Dock = DockStyle.Fill,
                Layout = layout.Data,
                Height = layout.Size*16,
            };

            layoutEditor.ValueChanged += OnLayoutChanged;

            var sizeControl = CreateNumericContol(layout.Size, 1, 32, 1, 0, 1, row);
            sizeControl.ValueChanged += OnLayoutSizeChanged;

            panel.Controls.Add(layoutEditor, 0, 0);
            panel.Controls.Add(sizeControl, 1, 0);

            _layouts.Add(sizeControl, layoutEditor);
            tableLayoutPanel.Controls.Add(panel, column, row);

            panel.ResumeLayout();

            return layoutEditor;
        }

        private object CreateCollection(Array value, int column, int row)
        {
            if (value.Length > 15)
            {
                return CreateLabel("List is too long", column, row);
            }

            var collection = new CollectionEditor
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Database = _database,
                ContentAutoScroll = false,
            };

            collection.Data = value;
            collection.CollectionChanged += OnCollectionChanged;

            tableLayoutPanel.Controls.Add(collection, column, row);

            return collection;
        }

        private VectorEditor CreateVectorEditor(Vector2 value, int column, int row)
        {
            var vector = new VectorEditor()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Value = value,
            };


            tableLayoutPanel.Controls.Add(vector, column, row);
            vector.ValueChanged += OnVectorValueChanged;

            return vector;
        }

        private NumericUpDown CreateNumericContol(decimal value, decimal min, decimal max, decimal increment, int decimalPlaces, int column, int row)
        {
            var numeric = new NumericUpDown()
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Maximum = max,
                Minimum = min,
                Increment = increment,
                Value = value,
                DecimalPlaces = decimalPlaces,
            };

            numeric.ValueChanged += OnNumericValueChanged;
            numeric.MouseWheel += DisableMouseWheel;

            tableLayoutPanel.Controls.Add(numeric, column, row);
            return numeric;
        }

        private void OnColorButtonClicked(object sender, EventArgs eventArgs)
        {
            if (_ignoreEvents) return;

            var button = (Button)sender;

            var colorDialog = new ColorPickerDialog();
            colorDialog.Color = button.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                button.BackColor = colorDialog.Color;
                _binding[sender].SetValue(_data, colorDialog.Color);

                if (DataChanged != null) DataChanged.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnLayoutSizeChanged(object sender, EventArgs eventArgs)
        {
            if (_ignoreEvents) return;

            LayoutEditor layoutEditor;
            if (!_layouts.TryGetValue(sender, out layoutEditor))
                return;

            var layout = new Layout(layoutEditor.Layout);
            layout.Size = (int)((NumericUpDown)sender).Value;

            layoutEditor.Layout = layout.Data;
            layoutEditor.Height = layout.Size*24;
            layoutEditor.Invalidate();

            OnLayoutChanged(layoutEditor, EventArgs.Empty);
        }

        private void OnLayoutChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            _binding[sender].SetValue(_data, new Layout(((LayoutEditor)sender).Layout));

            if (DataChanged != null) DataChanged.Invoke(this, EventArgs.Empty);
        }

        private void OnComboBoxValueChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            _binding[sender].SetValue(_data, ((ComboBox)sender).SelectedItem);

            if (DataChanged != null) DataChanged.Invoke(this, EventArgs.Empty);
        }

        private void OnTextBoxValueChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            var newValue = ((TextBox)sender).Text;
            _binding[sender].SetValue(_data, string.IsNullOrWhiteSpace(newValue) ? null : newValue);

            if (DataChanged != null) DataChanged.Invoke(this, EventArgs.Empty);
        }

        private void OnNumericValueChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            FieldInfo fieldInfo;
            if (!_binding.TryGetValue(sender, out fieldInfo))
                return;

            var value = ((NumericUpDown)sender).Value;
            var oldValue = fieldInfo.GetValue(_data);
            fieldInfo.SetValue(_data, ConvertDecimal(value, oldValue));

            if (DataChanged != null) DataChanged.Invoke(this, EventArgs.Empty);
        }

        private void OnVectorValueChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            FieldInfo fieldInfo;
            if (!_binding.TryGetValue(sender, out fieldInfo))
                return;

            var value = ((VectorEditor)sender).Value;
            fieldInfo.SetValue(_data, value);

            if (DataChanged != null) DataChanged.Invoke(this, EventArgs.Empty);
        }

        private void OnCheckedChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            _binding[sender].SetValue(_data, ((CheckBox)sender).Checked);

            if (DataChanged != null) DataChanged.Invoke(this, EventArgs.Empty);
        }

        private void OnCollectionChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            _binding[sender].SetValue(_data, ((CollectionEditor)sender).Data);

            if (DataChanged != null) DataChanged.Invoke(this, EventArgs.Empty);
        }

        private static object ConvertDecimal(decimal value, object oldValue)
        {
            if (oldValue is NumericValue<int>)
            {
                var numeric = (NumericValue<int>)oldValue;
                numeric.Value = (int)value;
                return numeric;
            }

            if (oldValue is NumericValue<float>)
            {
                var numeric = (NumericValue<float>)oldValue;
                numeric.Value = (float)value;
                return numeric;
            }

            return value;
        }

        private static void DisableMouseWheel(object sender, EventArgs args)
        {
            ((HandledMouseEventArgs)args).Handled = true;
        }

        private bool _ignoreEvents;
        private object _data;
        private Database _database;
        private HashSet<string> _exclusions = new HashSet<string>();
        private readonly Dictionary<object, FieldInfo>  _binding = new Dictionary<object, FieldInfo>();
        private readonly Dictionary<object, LayoutEditor> _layouts = new Dictionary<object, LayoutEditor>();
    }
}
