using System;
using System.IO;
using System.Windows.Forms;
using GameDatabase.EditorModel;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;
using Newtonsoft.Json;

namespace GameDatabase
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs eventArgs)
        {
            OpenDatabase(Directory.GetCurrentDirectory());
        }

        private void OpenDatabase(string path)
        {
            try
            {
                DatabaseTreeView.Nodes.Clear();
                _database = new Database(path, true); // TODO:
                BuildFilesTree(path, DatabaseTreeView.Nodes);
                _lastDatabasePath = path;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " " + e.StackTrace);
            }
        }

        private void BuildFilesTree(string path, TreeNodeCollection nodeCollection)
        {
            try
            {
                foreach (var directory in Directory.EnumerateDirectories(path))
                {
                    var name = directory.Substring(directory.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase) + 1);
                    var node = nodeCollection.Add(directory, name);
                    BuildFilesTree(directory, node.Nodes);
                }

                foreach (var file in Directory.EnumerateFiles(path, "*.json", SearchOption.TopDirectoryOnly))
                    nodeCollection.Add(file, Helpers.FileName(file));
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        private void DatabaseTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowItemInfo(e.Node.Name);
        }

        private void ShowItemInfo(string path)
        {
            try
            {
                ItemTypeText.Text = @"-";
                _selectedItem = new SerializableItem();
                EditButton.Enabled = false;

                if (Directory.Exists(path))
                {
                    ItemTypeText.Text = @"Directory";
                    structDataView1.Data = GetDirectoryInfo(path);
                    return;
                }

                var data = File.ReadAllText(path);
                var name = Helpers.FileName(path);
                var item = JsonConvert.DeserializeObject<SerializableItem>(data);
                item.FileName = name;
                if ((ItemType)item.ItemType == ItemType.Undefined)
                    return;

                ItemTypeText.Text = ((ItemType)item.ItemType).ToString();
                _selectedItem = item;

                structDataView1.Database = _database;
                structDataView1.Data = GetItem();

                EditButton.Enabled = true;
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        private struct DirectoryInfoData
        {
            public NumericValue<int> FilesCount;
            public ItemType ItemTypes;
            public NumericValue<int> LastItemId;
        }

        private DirectoryInfoData GetDirectoryInfo(string path)
        {
            DirectoryInfoData data = new DirectoryInfoData
            {
                FilesCount = new NumericValue<int>(0, 0, int.MaxValue),
                LastItemId = new NumericValue<int>(0, 0, int.MaxValue),
                ItemTypes = ItemType.Undefined
            };

            try
            {
                foreach (var file in Directory.EnumerateFiles(path, "*.json", SearchOption.TopDirectoryOnly))
                {
                    var text = File.ReadAllText(file);
                    var item = JsonConvert.DeserializeObject<SerializableItem>(text);

                    data.FilesCount.Value++;

                    if (item.ItemType == ItemType.Undefined)
                        continue;

                    if (data.ItemTypes == ItemType.Undefined)
                        data.ItemTypes = item.ItemType;
                    if (item.Id > data.LastItemId.Value)
                        data.LastItemId.Value = item.Id;
                }
            }
            catch (Exception e)
            {
            }

            return data;
        }

        private void DatabaseTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditButton_Click(sender, e);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var item = GetItem();
            if (item == null)
                return;

            switch ((ItemType)_selectedItem.ItemType)
            {
                case ItemType.Component:
                    new ComponentEditorDialog(_database, (Component)item).ShowDialog();
                    break;
                case ItemType.Satellite:
                case ItemType.Ship:
                    new ShipEditorDialog(_database, item, _selectedItem.FileName).ShowDialog();
                    break;
                default:
                    new EditorDialog(_database, item, _selectedItem.FileName).ShowDialog();
                    break;
            }
        }

        private object GetItem()
        {
            switch ((ItemType)_selectedItem.ItemType)
            {
                case ItemType.Component:
                    return _database.GetComponent(_selectedItem.Id);
                case ItemType.Device:
                    return _database.GetDevice(_selectedItem.Id);
                case ItemType.Weapon:
                    return _database.GetWeapon(_selectedItem.Id);
                case ItemType.Ammunition:
                    return _database.GetAmmunition(_selectedItem.Id);
                case ItemType.DroneBay:
                    return _database.GetDroneBay(_selectedItem.Id);
                case ItemType.Ship:
                    return _database.GetShip(_selectedItem.Id);
                case ItemType.ShipBuild:
                    return _database.GetShipBuild(_selectedItem.Id);
                case ItemType.Satellite:
                    return _database.GetSatellite(_selectedItem.Id);
                case ItemType.SatelliteBuild:
                    return _database.GetSatelliteBuild(_selectedItem.Id);
                case ItemType.Technology:
                    return _database.GetTechnology(_selectedItem.Id);
                case ItemType.Skill:
                    return _database.GetSkill(_selectedItem.Id);
                case ItemType.ComponentStats:
                    return _database.GetComponentStats(_selectedItem.Id);
                case ItemType.ComponentMod:
                    return _database.GetComponentMods(_selectedItem.Id);
                case ItemType.ShipBuilderSettings:
                    return _database.GetShipBuilderSettings(_selectedItem.Id);
                default:
                    return null;
            }
        }

        private SerializableItem _selectedItem;
        private Database _database;

        private void loadMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenDatabase(folderBrowserDialog1.SelectedPath);
            }
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _database.SaveAs(folderBrowserDialog1.SelectedPath);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_lastDatabasePath))
                _database.SaveAs(_lastDatabasePath);
        }

        private string _lastDatabasePath;
    }
}
