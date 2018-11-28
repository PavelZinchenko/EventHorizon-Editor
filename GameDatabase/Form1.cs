using System;
using System.Windows.Forms;

namespace GameDatabase
{
    public partial class NameForm : Form
    {
        public NameForm()
        {
            InitializeComponent();
            Name = string.Empty;
        }

        public string Name { get; set; }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            Name = nameBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
