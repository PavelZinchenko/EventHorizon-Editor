using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GameDatabase.EditorModel;
using GameDatabase.GameDatabase;

namespace GameDatabase
{
    public partial class ComponentEditorDialog : Form
    {
        public ComponentEditorDialog(Database database, Component component)
        {
            _component = component;
            _database = database;
            
            InitializeComponent();
        }

        private void ComponentEditorDialog_Load(object sender, EventArgs e)
        {
            Text = _component.ItemId.Name;
            structDataEditor1.Database = _database;
            structDataEditor1.Data = new DataAdapter(_component);
        }

        private readonly Component _component;
        private readonly Database _database;

        private void ComponentEditorDialog_FormClosed(object sender, FormClosedEventArgs e)
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
