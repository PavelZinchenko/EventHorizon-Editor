using EditorDatabase.Storage;
using EditorDatabase;

namespace Analyzer
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
                var database = new Database(new DatabaseStorage(path));
                _statistics = new Analyzer.Statistics(database);
                _integrityChecker = new Analyzer.IntegrityChecker(database);
            
                console.AppendText("Database loaded\n");
                console.AppendText("Total weapons - " + _statistics.WeaponCount + "\n\n");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " " + e.StackTrace);
            }
        }

        private void ammunitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Clear();
            console.AppendText("Total score:\n");

            foreach (var item in _statistics.GetCannonStats().ToArray().OrderBy(item => item.name))
            {
                console.AppendText(ExpandString(item.name, 24));
                console.AppendText(" id:" + item.id + "\t");
                console.AppendText(item.special ? "*" : " ");
                console.AppendText("lvl:" + item.level.ToString("000") + "\t");
                console.AppendText("s:" + item.size + "\t");
                console.AppendText("dps:" + item.dps.ToString("0.0") + "\t");
                console.AppendText("eps:" + item.eps.ToString("0.0") + "\t");
                console.AppendText("e2d:" + (item.dps / item.eps).ToString("0.0") + "\t");
                console.AppendText("rng:" + item.range.ToString("0.00") + (item.homing ? "*" : " ") + "\t");
                console.AppendText("score:" + item.score.ToString("0.0") + "\t");

                console.AppendText("\n");
            }
        }

        private static string ExpandString(string str, int length)
        {
            if (length <= str.Length) return str.Substring(0, length);
            return str.PadRight(length, ' ');
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Clear();
            OpenDatabase(Directory.GetCurrentDirectory());
        }

        private Analyzer.IntegrityChecker _integrityChecker;
        private Analyzer.Statistics _statistics;

        private void deadLinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            console.Clear();
            console.AppendText("Dead links:\n");

            foreach (var item in _integrityChecker.LookForDeadLinks())
                console.AppendText(item + "\n");
        }
    }
}
