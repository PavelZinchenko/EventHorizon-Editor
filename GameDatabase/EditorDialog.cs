using System;
using System.Windows.Forms;

namespace GameDatabase
{
    public partial class EditorDialog : Form
    {
        public EditorDialog(Database database, object item, string name)
        {
            _item = item;
            _database = database;
            _dialogName = name;

            InitializeComponent();
        }

        private void EditorDialog_Load(object sender, EventArgs e)
        {
            Text = _dialogName;
            structDataEditor1.Database = _database;
            structDataEditor1.Data = _item;
        }

        private readonly string _dialogName;
        private readonly Object _item;
        private readonly Database _database;
    }
}
