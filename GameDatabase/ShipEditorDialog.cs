﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameDatabase.Controls;
using GameDatabase.EditorModel;
using GameDatabase.Enums;
using GameDatabase.GameDatabase;
using GameDatabase.Model;
using GameDatabase.Properties;

namespace GameDatabase
{
    public partial class ShipEditorDialog : Form
    {
        public ShipEditorDialog(Database database, object item, string name)
        {
            _item = item;
            _database = database;
            _dialogName = name;

            InitializeComponent();
        }

        public LayoutInfo layoutInfo;

        private void ShipEditorDialog_Load(object sender, EventArgs e)
        {
            _ignoreEvents = true;

            Text = _dialogName;
            structDataEditor1.Exclusions = new List<string>() { "Layout", "Barrels"};
            structDataEditor1.Database = _database;
            structDataEditor1.Data = _item as IDataAdapter ?? new DataAdapter(_item);
            layoutEditor1.Colors.Clear();
            layoutEditor1.Colors.Add((char)CellType.Empty, Color.LightGray);
            layoutEditor1.Colors.Add((char)CellType.Engine, Color.FromArgb(192, 255,255,0));
            layoutEditor1.Colors.Add((char)CellType.Weapon, Color.FromArgb(192, 255, 0, 0));
            layoutEditor1.Colors.Add((char)CellType.Inner, Color.FromArgb(192, 0, 255, 0));
            layoutEditor1.Colors.Add((char)CellType.InnerOuter, Color.FromArgb(192, 0, 255, 255));
            layoutEditor1.Colors.Add((char)CellType.Outer, Color.FromArgb(192, 0, 128, 255));


            barrelCollection.Database = _database;

            Layout layout;
            if (_item is Ship)
            {
                var ship = (Ship)_item;
                layout = ship.Layout;
                barrelCollection.Data = ship.Barrels;
                layoutEditor1.Image = _database.GetImage(ship.ModelImage);
            }
            else if (_item is Satellite)
            {
                button1.Hide();
                button1.Enabled = false;
                var satellite = (Satellite)_item;
                layout = satellite.Layout;
                barrelCollection.Data = satellite.Barrels;
                layoutEditor1.Image = _database.GetImage(satellite.ModelImage);
            }
            else
                throw new ArgumentException();

            layoutSize.Value = (this.oldLayoutSize = layout.Size);
            layoutEditor1.Layout = layout.Data;

            _ignoreEvents = false;

            UpdateBarrels();
            UpdateEngines();

            splitContainer1.SplitterDistance = Settings.Default.ShipEditorHorizontalSplitter;
            splitContainer2.SplitterDistance = Settings.Default.ShipEditorVerticalSplitter;
        }

        private void ShipEditorDialog_Shown(object sender, EventArgs e)
        {
        }

        private void structDataEditor1_DataChanged(object sender, EventArgs e)
        {
            if(_item is Ship)
            {
                layoutInfo?.OnLayoutChanged();
                UpdateEngines();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (_ignoreEvents) return;

            layoutEditor1.SelectedCategory = (char)CellType.Empty;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (_ignoreEvents) return;

            layoutEditor1.SelectedCategory = (char)CellType.Engine;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (_ignoreEvents) return;

            layoutEditor1.SelectedCategory = (char)CellType.Weapon;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (_ignoreEvents) return;

            layoutEditor1.SelectedCategory = (char)CellType.Inner;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (_ignoreEvents) return;

            layoutEditor1.SelectedCategory = (char)CellType.InnerOuter;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (_ignoreEvents) return;

            layoutEditor1.SelectedCategory = (char)CellType.Outer;
        }

        private void layoutEditor1_ValueChanged(object sender, EventArgs e)
        {
            if (_ignoreEvents) return;

            if (_item is Ship)
            {
                ((Ship)_item).Layout.Data = layoutEditor1.Layout;
                layoutInfo?.OnLayoutChanged();
            }
            else if (_item is Satellite)
                ((Satellite)_item).Layout.Data = layoutEditor1.Layout;
        }

        private readonly string _dialogName;
        private readonly Object _item;
        private readonly Database _database;

        private static bool warned = false;
        private decimal oldLayoutSize;
        private void layoutSize_ValueChanged(object sender, EventArgs e)
        {
            if (_ignoreEvents) return;

            if (!ShipEditorDialog.warned && this.oldLayoutSize > 32)
            {
                ShipEditorDialog.warned = true;
            }
            if (!ShipEditorDialog.warned && this.layoutSize.Value > 32)
            {
                ShipEditorDialog.warned = true;
                MessageBox.Show("Large grid sizes can cause low performance.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            modifyLayout((int)layoutSize.Value, 0, 0);
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            modifyLayout((int)layoutSize.Value, 1, 0);
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            modifyLayout((int)layoutSize.Value, -1, 0);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            modifyLayout((int)layoutSize.Value, 0, 1);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            modifyLayout((int)layoutSize.Value, 0, -1);
        }

        private void modifyLayout(int size, int dx, int dy)
        {
            var layout = new Layout(layoutEditor1.Layout);
            layout.Size = size;
            layout.Shift(dx, dy);
            layoutEditor1.Layout = layout.Data;
            layoutEditor1.Invalidate();

            if (_item is Ship)
                ((Ship)_item).Layout = layout;
            else if (_item is Satellite)
                ((Satellite)_item).Layout = layout;
        }

        private bool _ignoreEvents;

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Settings.Default.ShipEditorHorizontalSplitter = splitContainer1.SplitterDistance;
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Settings.Default.ShipEditorVerticalSplitter = splitContainer2.SplitterDistance;
        }

        private void barrelCollection_CollectionChanged(object sender, EventArgs e)
        {
            if (_item is Ship)
            {
                ((Ship)_item).Barrels = (Barrel[])barrelCollection.Data;
            }
            else if (_item is Satellite)
            {
                ((Satellite)_item).Barrels = (Barrel[])barrelCollection.Data;
            }
            else
                throw new ArgumentException();

            UpdateBarrels();
        }

        private void barrelCollection_DataChanged(object sender, EventArgs e)
        {
            UpdateBarrels();
        }

        private void UpdateBarrels()
        {
            layoutEditor1.Barrels = ((Barrel[])barrelCollection.Data).Select(item => new LayoutEditor.BarrelData
            {
                X = item.Position.x,
                Y = item.Position.y,
                Text = item.WeaponClass,
                Offset = item.Offset.Value,
                Rotation = item.Rotation.Value,
                Arc = item.AutoAimingArc.Value > 0 ? item.AutoAimingArc.Value*2 :
                    item.PlatformType == PlatformType.AutoTarget ? 360 :
                    item.PlatformType == PlatformType.AutoTargetFrontal ? 160 :
                    item.PlatformType == PlatformType.TargetingUnit ? 40 : 0
            }).ToArray();
        }

        private void UpdateEngines()
        {
            if (!(_item is Ship)) return;

            var ship = _item as Ship;

            Ship.Engine[] engines = ship.Engines ?? new Ship.Engine[0];
            if (ship.EngineSize.Value > 0)
            {
                engines = engines.Concat(new Ship.Engine[] {
                    new Ship.Engine() {
                        Position = ship.EnginePosition,
                        Size = ship.EngineSize
                    }
                }).ToArray();
            }
            

            layoutEditor1.Engines = (engines).Select(item => new LayoutEditor.EngineData
            {
                X = item.Position.y,
                Y = item.Position.x
            }).ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (layoutInfo != null && layoutInfo.Visible)
            {
                return;
            }
            layoutInfo = new LayoutInfo();
            layoutInfo._database = _database;
            layoutInfo._layout = layoutEditor1;
            layoutInfo._shipData = _item as Ship;
            layoutInfo.Owner = this;
            layoutInfo.OnLayoutChanged();
            layoutInfo.Show();
        }

        private void ShipEditorDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var key in new List<string>(MainWindow.OppenedWindows.Keys))
            {
                if (MainWindow.OppenedWindows[key] == this)
                {
                    MainWindow.OppenedWindows.Remove(key);
                    return;
                }
            }
        }
    }
}
