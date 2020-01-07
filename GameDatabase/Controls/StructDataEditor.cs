﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Cyotek.Windows.Forms;
using GameDatabase.Controls;
using GameDatabase.EditorModel;
using GameDatabase.EditorModel.Quests;
using GameDatabase.GameDatabase;
using GameDatabase.GameDatabase.Helpers;
using GameDatabase.Model;

namespace GameDatabase
{
    public partial class StructDataEditor : UserControl
    {
        [Description("Data"), Category("Data")]
        public IDataAdapter Data
        {
            get { return _data; }
            set
            {
                _data = value;
                if (_data == null)
                {
                    Cleanup();
                    return;
                }

                _data.DataChangedEvent += OnDataChanged;
                _data.LayoutChangedEvent += BuildLayout;
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

            tableLayoutPanel.SuspendLayout();

            var fields = _data.Properties.ToArray();

            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = fields.Length + 1;

            for (var i = 0; i <= tableLayoutPanel.RowCount; ++i)
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _ignoreEvents = true;

            var rowId = 0;
            foreach (var item in fields)
            {
                var name = item.Name;

                if (_exclusions.Contains(name))
                    continue;

                CreateLabel(name, 0, rowId);

                var value = item.Value;
                var control = item.IsReadOnly ? null : CreateControl(value, item.Type, rowId, item);
                if (control != null)
                    _binding.Add(control, item);
                else
                    CreateLabel(value?.ToString() ?? "[empty]", 1, rowId);

                rowId++;
            }

            _ignoreEvents = false;

            tableLayoutPanel.ResumeLayout();
        }
        
        public void UpdateControls()
        {
            foreach(var bind in _binding)
            {
                var control = bind.Key;
                var value = bind.Value;
                if (bind.Value.Type.IsEnum)
                {
                    ((ComboBox)control).SelectedItem = value.Value;
                }
                else if (value.Type == typeof(NumericValue<int>))
                {
                    var numVal = (NumericUpDown)control;
                    numVal.Value = ((NumericValue<int>)value.Value).Value;
                }
                else if (value.Type == typeof(NumericValue<float>))
                {
                    var numVal = (NumericUpDown)control;
                    numVal.Value = (decimal)((NumericValue<float>)value.Value).Value;
                }
                else if (value.Type == typeof(string))
                {
                    ((TextBox)control).Text = (string)value.Value;
                }
                else if (value.Type == typeof(bool))
                {
                    ((CheckBox)control).Checked = (bool)value.Value;
                }
                else if (value.Type.IsArray)
                {
                    ((CollectionEditor)control).UpdateControls();
                }
                else if (value.Type == typeof(Vector2))
                {
                    ((VectorEditor)control).Value = (Vector2)value.Value;
                }
                else if (typeof(IItemId).IsAssignableFrom(value.Type))
                {
                    ((ComboBox)value).SelectedValue = value.Value;
                }
                else if (typeof(IDataAdapter).IsAssignableFrom(value.Type))
                {
                    ((StructDataEditor)control).UpdateControls();
                }
                else if (value.Type.IsClass)
                {
                    ((StructDataEditor)control).UpdateControls();
                }
            }
        }

        private Control CreateControl(object value, Type type, int rowId, IProperty property)
        {
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
                return CreateTextBox((string)value, 1, rowId, property);

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

            Control result;
            if ((result = TryCreateIdItem(value, type, _database.ComponentIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.WeaponIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.DeviceIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.AmmunitionIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.AmmunitionObsoleteIds, 1, rowId)) != null)
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
            if ((result = TryCreateIdItem(value, type, _database.FactionIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.QuestIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.LootIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.FleetIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.CharacterIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.QuestItemIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.BulletPerfabIds, 1, rowId)) != null)
                return result;
            if ((result = TryCreateIdItem(value, type, _database.VisualEffectIds, 1, rowId)) != null)
                return result;

            if (typeof(IDataAdapter).IsAssignableFrom(type))
                return CreateStructEditor((IDataAdapter)value, 1, rowId);

            if (type.IsClass)
                return CreateStructEditor(new DataAdapter(value), 1, rowId);

            return null;
        }

        private Control TryCreateIdItem<T>(object value, Type type, IEnumerable<ItemId<T>> items, int column, int row)
        {
            if (type != typeof(ItemId<T>) && (value==null || value.GetType()!=typeof(ItemId<T>)))
                return null;

            var itemlist = Enumerable.Repeat(ItemId<T>.Empty, 1).Concat(items);
            if (Properties.Settings.Default.SortingType == 1)
                itemlist=itemlist.OrderBy(o => o.Name).ToList();
            else if (Properties.Settings.Default.SortingType == 2)
                itemlist = itemlist.OrderBy(o => o.Id).ToList();
            return CreateComboBox(itemlist.Cast<object>(), value, column, row);
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

        private TextBox CreateTextBox(string text, int column, int row, IProperty iProperty = null)
        {
            var property = iProperty as Property;
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();
            if (property != null)
            {
                var attributes = property.GetCustomAttributes(typeof(AutoCompleteAtribute), false);
                bool addImages = false;
                for (int i = 0; i < attributes.Length; i++) { 
                    AutoCompleteAtribute customAttribute = attributes[i] as AutoCompleteAtribute;
                    if (customAttribute != null)
                    {
                        autoCompleteStringCollection.AddRange(customAttribute.getCompletion());
                        if (customAttribute.AddImages())
                        {
                            addImages = true;
                        }
                    }
                }
                if (addImages)
                {
                    autoCompleteStringCollection.AddRange(Database.GetImages().Keys.ToArray<string>());
                }
            }

            var textbox = new TextBox()
            {
                Text = text,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                AutoCompleteCustomSource = autoCompleteStringCollection,
                AutoCompleteMode = AutoCompleteMode.Suggest,
                AutoCompleteSource = AutoCompleteSource.CustomSource
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

            var sizeControl = CreateNumericContol(layout.Size, 1, 1024, 1, 0, 1, row);
            sizeControl.ValueChanged += OnLayoutSizeChanged;

            panel.Controls.Add(layoutEditor, 0, 0);
            panel.Controls.Add(sizeControl, 1, 0);

            _layouts.Add(sizeControl, layoutEditor);
            tableLayoutPanel.Controls.Add(panel, column, row);

            panel.ResumeLayout();

            return layoutEditor;
        }

        private Control CreateCollection(Array value, int column, int row)
        {

            var collection = new CollectionEditor
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                Database = _database,
                ContentAutoScroll = false,
            };

            collection.Data = value;
            collection.CollectionChanged += OnCollectionChanged;
            collection.DataChanged += OnCollectionChanged;

            tableLayoutPanel.Controls.Add(collection, column, row);

            return collection;
        }

        private Control CreateStructEditor(IDataAdapter data, int column, int row)
        {
            var editor = new StructDataEditor
            {
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                AutoSize = true,
                Database = _database,
                Data = data,
                ContentAutoScroll = false,
            };

            editor.DataChanged += DataChanged;
            tableLayoutPanel.Controls.Add(editor, column, row);
            return editor;
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
                Value = value < min ? min : value > max ? max:value,
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
                _binding[sender].Value = colorDialog.Color;
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

            _binding[sender].Value = new Layout(((LayoutEditor)sender).Layout);
        }

        private void OnComboBoxValueChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            _binding[sender].Value = ((ComboBox)sender).SelectedItem;
        }

        private void OnTextBoxValueChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            var newValue = ((TextBox)sender).Text;
            _binding[sender].Value = string.IsNullOrWhiteSpace(newValue) ? null : newValue;
        }

        private void OnNumericValueChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            IProperty property;
            if (!_binding.TryGetValue(sender, out property))
                return;

            var value = ((NumericUpDown)sender).Value;
            var oldValue = property.Value;
            property.Value = ConvertDecimal(value, oldValue);
        }

        private void OnVectorValueChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            IProperty property;
            if (!_binding.TryGetValue(sender, out property))
                return;

            var value = ((VectorEditor)sender).Value;
            property.Value = value;
        }

        private void OnCheckedChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            _binding[sender].Value = ((CheckBox)sender).Checked;
        }

        private void OnCollectionChanged(object sender, EventArgs args)
        {
            if (_ignoreEvents) return;

            _binding[sender].Value = ((CollectionEditor)sender).Data;
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

        private void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        private static void DisableMouseWheel(object sender, EventArgs args)
        {
            ((HandledMouseEventArgs)args).Handled = true;
        }

        private bool _ignoreEvents;
        private IDataAdapter _data;
        private Database _database;
        private HashSet<string> _exclusions = new HashSet<string>();
        private readonly Dictionary<object, IProperty>  _binding = new Dictionary<object, IProperty>();
        private readonly Dictionary<object, LayoutEditor> _layouts = new Dictionary<object, LayoutEditor>();
    }
}
