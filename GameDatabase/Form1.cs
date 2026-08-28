namespace GameDatabase
{
    public partial class NameForm : Form
    {
        public NameForm()
        {
            InitializeComponent();
            Name = string.Empty;
        }

        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new string Name { get; set; }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            Name = nameBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
