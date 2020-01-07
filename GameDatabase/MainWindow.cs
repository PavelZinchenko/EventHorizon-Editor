using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GameDatabase.EditorModel;
using GameDatabase.Enums;
using GameDatabase.GameDatabase.Helpers;
using GameDatabase.GameDatabase.Model;
using GameDatabase.GameDatabase.Serializable;
using GameDatabase.Model;
using GameDatabase.Properties;
using GameDatabase.Serializable;
using Newtonsoft.Json;

namespace GameDatabase
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            OppenedWindows = new Dictionary<string, Form>();

            closeConfrmationToolStripMenuItem.Checked = Settings.Default.ClosingConfirmation;

            ChangeSorting(Settings.Default.SortingType);
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
                CloseAllChilds();
                _database = new Database(path, true); // TODO:
                BuildFilesTree(path, DatabaseTreeView.Nodes);
                _lastDatabasePath = path;
                BuildTemplates();
            }
            catch (EditorException e)
            {
                MessageBox.Show(e.Message);
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
                    node.Tag = "Folder";
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

        private void BuildTemplates()
        {
            createToolStripMenuItem.DropDownItems.Clear();
            createToolStripMenuItem.DropDownItems.Add(folderToolStripMenuItem);
            ToolStripMenuItem item = new ToolStripMenuItem("Templates");
            item.Name = "Templates";
            createToolStripMenuItem.DropDownItems.Add(item);
            item = new ToolStripMenuItem("");
            item.Enabled = false;
            createToolStripMenuItem.DropDownItems.Add(item);
            foreach (var template in _database.Templates)
            {
                var lastNode = createToolStripMenuItem;
                var _name = template.Name;
                if (template.Name.Contains('/'))
                {
                    var path = template.Name.Split('/');
                    _name = path.Last();
                    for (var i= 0;i< path.Length - 1; i++){
                        var findResult = lastNode.DropDownItems.Find(path[i], false);
                        ToolStripMenuItem curNode;
                        if (findResult.Length==0 || (curNode = findResult[0] as ToolStripMenuItem) == null)
                        {
                            curNode = new ToolStripMenuItem(path[i]);
                            curNode.Name = path[i];
                            lastNode.DropDownItems.Add(curNode);
                        }
                        lastNode = curNode;
                    }
                }
                item = new ToolStripMenuItem(_name);
                lastNode.DropDownItems.Add(item);
                item.Tag = item.Name = template.Name;
                item.Click += TemplateMenuItem_Click;
            }
        }

        private void DatabaseTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowItemInfo(e.Node.Name);
        }

        private void DatabaseTreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode node = DatabaseTreeView.GetNodeAt(p);
                if (node != null)
                {
                    DatabaseTreeView.SelectedNode = node;
                    ShowItemInfo(node.Name);
                    contextMenuStrip1.Show(this, p);
                }
            }
        }

        private Dictionary<string, string> ghostFiles = new Dictionary<string, string>();

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
                string data;
                if (File.Exists(path))
                    data = File.ReadAllText(path);
                else
                    data = ghostFiles[path];
                var name = Helpers.FileName(path);
                var item = JsonConvert.DeserializeObject<SerializableItem>(data);
                item.FileName = name;
                item.FilePath = path;
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
            public NumericValue<int> FirstUnusedId;
        }

        private DirectoryInfoData GetDirectoryInfo(string path)
        {
            DirectoryInfoData data = new DirectoryInfoData
            {
                FilesCount = new NumericValue<int>(0, 0, int.MaxValue),
                ItemTypes = ItemType.Undefined,
                LastItemId = new NumericValue<int>(0, 0, int.MaxValue),
                FirstUnusedId = new NumericValue<int>(0, 0, int.MaxValue),
            };

            try
            {
                var ids = new HashSet<int>();
                var top = 0;
                foreach (var file in Directory.EnumerateFiles(path, "*.json", SearchOption.TopDirectoryOnly))
                {
                    var text = File.ReadAllText(file);
                    var item = JsonConvert.DeserializeObject<SerializableItem>(text);

                    data.FilesCount.Value++;

                    if (item.ItemType == ItemType.Undefined)
                        continue;

                    ids.Add(item.Id);
                    top = Math.Max(top, item.Id);

                    if (data.ItemTypes == ItemType.Undefined)
                        data.ItemTypes = item.ItemType;
                }

                data.LastItemId.Value = top;
                var index = 0;
                while (ids.Contains(index)) index++;
                data.FirstUnusedId.Value = index > 0 ? index : top+1;
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

        public static Dictionary<string, Form> OppenedWindows;

        private void EditButton_Click(object sender, EventArgs e)
        {
            var item = GetItem();
            if (item == null)
                return;
            if (OppenedWindows.ContainsKey(_selectedItem.FilePath))
            {
                return;
            }
            Form window;
            switch ((ItemType)_selectedItem.ItemType)
            {
                case ItemType.Component:
                    (window=new ComponentEditorDialog(_database, (Component)item)).Show();
                    break;
                case ItemType.Satellite:
                case ItemType.Ship:
                    (window = new ShipEditorDialog(_database, item, _selectedItem.FileName)).Show();
                    break;
                default:
                    (window = new EditorDialog(_database, item, _selectedItem.FileName)).Show();
                    break;
            }
            OppenedWindows.Add(_selectedItem.FilePath, window);
        }

        private object GetItem()
        {
            switch (_selectedItem.ItemType)
            {
                case ItemType.Component:
                    return _database.GetComponent(_selectedItem.Id);
                case ItemType.Device:
                    return _database.GetDevice(_selectedItem.Id);
                case ItemType.Weapon:
                    return _database.GetWeapon(_selectedItem.Id);
                case ItemType.Ammunition:
                    return _database.GetAmmunition(_selectedItem.Id);
                case ItemType.AmmunitionObsolete:
                    return _database.GetAmmunitionObsolete(_selectedItem.Id);
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
                case ItemType.ShipSettings:
                    return _database.ShipSettings;
                case ItemType.GalaxySettings:
                    return _database.GalaxySettings;
                case ItemType.Faction:
                    return _database.GetFaction(_selectedItem.Id);
                case ItemType.Quest:
                    return _database.GetQuest(_selectedItem.Id);
                case ItemType.Loot:
                    return _database.GetLoot(_selectedItem.Id);
                case ItemType.Fleet:
                    return _database.GetFleet(_selectedItem.Id);
                case ItemType.Character:
                    return _database.GetCharacter(_selectedItem.Id);
                case ItemType.QuestItem:
                    return _database.GetQuestItem(_selectedItem.Id);
                case ItemType.BulletPrefab:
                    return _database.GetBulletPrefab(_selectedItem.Id);
                case ItemType.VisualEffect:
                    return _database.GetVisualEffect(_selectedItem.Id);
                default:
                    return null;
            }
        }

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
                ghostFiles.Clear();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void save()
        {
            if (!string.IsNullOrWhiteSpace(_lastDatabasePath))
            {
                _database.SaveAs(_lastDatabasePath);
                ghostFiles.Clear();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ( _lastDatabasePath != null && Settings.Default.ClosingConfirmation)
            {
                var result = MessageBox.Show("Do you want to save database before closing?", "About to exit program?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        save();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void createModMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_lastDatabasePath))
                return;

            try
            {
                string name, guid;
                if (!ModBuilder.TryReadSignature(_lastDatabasePath, out name, out guid))
                {
                    var dialog = new NameForm();
                    if (dialog.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(dialog.Name))
                        return;

                    name = dialog.Name;
                    guid = Guid.NewGuid().ToString();

                    File.WriteAllText(Path.Combine(_lastDatabasePath, ModBuilder.SignatureFileName), name + '\n' + guid);
                }

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                _database.SaveAs(_lastDatabasePath);
                var builder = ModBuilder.Create(_lastDatabasePath);
                builder.Build((FileStream)saveFileDialog.OpenFile());
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private SerializableItem _selectedItem;
        private Database _database;
        private string _lastDatabasePath;

        private void reloadDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDatabase(_lastDatabasePath);
        }

        private void loadAsDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDatabase(DatabaseTreeView.SelectedNode.Name);
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var name = Prompt.ShowDialog("Input folder name", "");
            if (name == "") return;
            var _node = DatabaseTreeView.SelectedNode;
            if (Path.GetInvalidFileNameChars().Any(name.Contains))
            {
                MessageBox.Show("Folder name contains invalid characters");
                return;
            }
            if (_node.Tag == null || _node.Tag.ToString() != "Folder")
            {
                _node = _node.Parent;
            }

            var _path = Path.Combine(_node.Name, name);
            if (Directory.Exists(_path))
            {
                MessageBox.Show("Folder with same name aready exists");
                return;
            }
            var _folder = new TreeNode(name);
            _folder.Name = _path;
            _folder.Tag = "Folder";
            _node.Nodes.Insert(0, _folder);
        }

        private void TemplateMenuItem_Click(object sender, EventArgs e)
        {
            ItemFromTemplate(sender, e, "", "");
        }

        private void ItemFromTemplate(object sender, EventArgs e, string name, string __id)
        {
            if (sender is ToolStripMenuItem)
            {

                var template = _database.GetTemplate((sender as ToolStripMenuItem).Tag.ToString());

                name = Prompt.ShowDialog(template.NameDialog, "", name);
                if (name == "") return;
                var _node = DatabaseTreeView.SelectedNode;
                if (Path.GetInvalidFileNameChars().Any(name.Contains))
                {
                    MessageBox.Show("File Name contains invalid characters");
                    ItemFromTemplate(sender, e, "", __id);
                    return;
                }
                if (_node.Tag == null || _node.Tag.ToString() != "Folder")
                {
                    _node = _node.Parent;
                }
                var result = Prompt.ShowDialog(template.IdDialog, "", __id);
                if (result == "") return;
                int id;
                try
                {
                    id = Int32.Parse(result);
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Id is too big");
                    ItemFromTemplate(sender, e, name, "");
                    return;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.GetType());
                    MessageBox.Show("Invalid Id");
                    ItemFromTemplate(sender, e, name, "");
                    return;
                }

                try
                {
                    var items = template.Items;
                    Dictionary<string, Newtonsoft.Json.Linq.JObject> files = new Dictionary<string, Newtonsoft.Json.Linq.JObject>();
                    Dictionary<TreeNode, TreeNode> nodes = new Dictionary<TreeNode, TreeNode>();
                    HashSet<string> _files = new HashSet<string>();
                    HashSet<string> _dirs = new HashSet<string>();

                    void FormatNumberField(Newtonsoft.Json.Linq.JObject target, string field, int placeholder = -1)
                    {
                        var _field = target.GetValue(field);
                        if (_field != null)
                        {
                            _field = (int)new DataTable().Compute(String.Format((string)_field, id), null);
                            target.Remove(field);
                            target.Add(field, _field);
                        }
                        else
                        {
                            if (placeholder != -1)
                                target.Add(field, placeholder);
                        }
                    }

                    bool error = false;

                    void BuildTemplateItems(SerializableTemplateItem[] _items, TreeNode _parentNode)
                    {
                        if (error) return;
                        for (int i = 0; i < _items.Length; i++)
                        {
                            var item = _items[i];
                            var path = Path.Combine(_parentNode.Name, String.Format(item.Filename, id, name));
                            TreeNode _newNode;
                            if (item.Type == "Folder")
                            {
                                _newNode = new TreeNode(path.Substring(path.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase) + 1))
                                {
                                    Name = path
                                };
                                _newNode.Tag = "Folder";
                                nodes.Add(_newNode, _parentNode);
                                BuildTemplateItems(item.Items, _newNode);
                                continue;
                            }
                            path += ".json";
                            string filename = Helpers.FileName(path);
                            _newNode = new TreeNode(filename)
                            {
                                Name = path
                            };
                            nodes.Add(_newNode, _parentNode);
                            _files.Add(path);
                            var content = files[path] = (Newtonsoft.Json.Linq.JObject)_items[i].Content.DeepClone();

                            FormatNumberField(content, "Id");
                            var _id = (int)content.GetValue("Id");

                            if (File.Exists(path) || ghostFiles.ContainsKey(path))
                            {
                                MessageBox.Show("File " + path + " already exists");
                                error = true;
                                return;
                            }
                            var _name = (string)content.GetValue("Name");
                            if (_name != null)
                            {
                                _name = String.Format(_name, id, name);
                                content.Remove("Name");
                                content.Add("Name", _name);
                            }
                            var type = (ItemType)(int)content.GetValue("ItemType");
                            switch (type)
                            {
                                case ItemType.Component:
                                    if (_database.GetComponent(_id) != null) error = true;
                                    FormatNumberField(content, "WeaponId");
                                    FormatNumberField(content, "AmmunitionId");
                                    FormatNumberField(content, "DroneBayId");
                                    FormatNumberField(content, "DroneId");
                                    FormatNumberField(content, "DeviceId");
                                    FormatNumberField(content, "ComponentStatsId", _database.ComponentStatsIds.First().Id);
                                    break;
                                case ItemType.Device:
                                    if (_database.GetDevice(_id) != null) error = true;
                                    break;
                                case ItemType.Weapon:
                                    if (_database.GetWeapon(_id) != null) error = true;
                                    break;
                                case ItemType.AmmunitionObsolete:
                                    if (_database.GetAmmunitionObsolete(_id) != null) error = true;
                                    break;
                                case ItemType.DroneBay:
                                    if (_database.GetDroneBay(_id) != null) error = true;
                                    break;
                                case ItemType.Ship:
                                    if (_database.GetShip(_id) != null) error = true;
                                    break;
                                case ItemType.Satellite:
                                    if (_database.GetSatellite(_id) != null) error = true;
                                    break;
                                case ItemType.ShipBuild:
                                    if (_database.GetShipBuild(_id) != null) error = true;
                                    FormatNumberField(content, "ShipId", _database.ShipIds.First().Id);
                                    break;
                                case ItemType.SatelliteBuild:
                                    if (_database.GetSatelliteBuild(_id) != null) error = true;
                                    FormatNumberField(content, "SatelliteId", _database.SatelliteIds.First().Id);
                                    break;
                                case ItemType.Technology:
                                    if (_database.GetTechnology(_id) != null) error = true;
                                    FormatNumberField(content, "ItemId");
                                    if (content.GetValue("ItemId") == null)
                                    {
                                        switch ((TechType)(int)content.GetValue("Type"))
                                        {
                                            case TechType.Component:
                                                content.Add("ItemId", _database.ComponentIds.First().Id);
                                                break;
                                            case TechType.Ship:
                                                content.Add("ItemId", _database.ShipIds.First().Id);
                                                break;
                                            case TechType.Satellite:
                                                content.Add("ItemId", _database.SatelliteIds.First().Id);
                                                break;
                                        }
                                    }
                                    break;
                                case ItemType.ComponentStats:
                                    if (_database.GetComponentStats(_id) != null) error = true;
                                    break;
                                case ItemType.ComponentMod:
                                    if (_database.GetComponentMods(_id) != null) error = true;
                                    break;
                                case ItemType.Skill:
                                    if (_database.GetSkill(_id) != null) error = true;
                                    break;
                                case ItemType.Faction:
                                    if (_database.GetFaction(_id) != null && _database.GetFaction(_id) != Faction.Undefined) error = true;
                                    break;
                                case ItemType.Quest:
                                    if (_database.GetQuest(_id) != null) error = true;
                                    break;
                                case ItemType.Loot:
                                    if (_database.GetLoot(_id) != null) error = true;
                                    break;
                                case ItemType.Fleet:
                                    if (_database.GetFleet(_id) != null) error = true;
                                    break;
                                case ItemType.Character:
                                    if (_database.GetCharacter(_id) != null) error = true;
                                    break;
                                case ItemType.QuestItem:
                                    if (_database.GetQuestItem(_id) != null) error = true;
                                    break;
                                case ItemType.Ammunition:
                                    if (_database.GetAmmunition(_id) != null) error = true;
                                    FormatNumberField(content.GetValue("Body") as Newtonsoft.Json.Linq.JObject, "BulletPrefab");
                                    break;
                                case ItemType.VisualEffect:
                                    if (_database.GetVisualEffect(_id) != null) error = true;
                                    break;
                                case ItemType.BulletPrefab:
                                    if (_database.GetBulletPrefab(_id) != null) error = true;
                                    break;
                            }
                            if (error)
                            {
                                MessageBox.Show(type.ToString() + " with Id " + _id + " already exists");
                                return;
                            }
                        }
                    }

                    BuildTemplateItems(template.Items, _node);

                    if (error)
                    {
                        ItemFromTemplate(sender, e, name, id.ToString());
                        return;
                    }

                    foreach (string path in _files)
                    {
                        _database.DeserializeItem(files[path].ToString(), _lastDatabasePath, path);
                        ghostFiles.Add(path, files[path].ToString());
                    }

                    foreach (TreeNode node in nodes.Keys)
                    {
                        nodes[node].Nodes.Add(node);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unexpected error happened, most likely due to invalid template file.\nIf yoy sure your template file is correct, then report stacktrace bellow\n\n" + ex);
                    return;
                }
            }
        }

        private void closeConfrmationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.ClosingConfirmation = closeConfrmationToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void CloseAllChilds()
        {
            foreach(var key in new List<string>(OppenedWindows.Keys))
            {
                if (!OppenedWindows.ContainsKey(key)) continue;
                OppenedWindows[key].Close();
            }
            OppenedWindows = new Dictionary<string, Form>();
        }

        private void ChangeSorting(int type)
        {
            byFolderToolStripMenuItem.Checked = type == 0;
            byNameToolStripMenuItem.Checked = type == 1;
            byIdToolStripMenuItem.Checked = type == 2;
            Settings.Default.SortingType = type;
            Settings.Default.Save();
        }

        private void byFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSorting(0);
        }

        private void byNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSorting(1);
        }

        private void byIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSorting(2);
        }
    }
}
