using GameDatabase.EditorModel;
using GameDatabase.GameDatabase;
using GameDatabase.ShipLayout;
using System;
using System.Windows.Forms;

namespace GameDatabase.Controls
{
    public partial class LayoutInfo : Form
    {

        public LayoutEditor _layout { get; set; }
        public Ship _shipData { get; set; }
        public Database _database { get; set; }

        private Label CellsNum;
        private Label EngineSize;
        private Label BaseArmor;
        private Label BaseWeigth;
        private Label MinWeigth;
        private Label BaseEnergyResistance;
        private Label BaseKineticResistance;
        private Label BaseHeatResistance;
        private Label CreditsCost;
        private Label StarCost;
        private Label MinSpawnDistance;

        public LayoutInfo()
        {
            InitializeComponent();
            tableLayoutPanel.RowCount = 11;

            tableLayoutPanel.SuspendLayout();
            for (var i = 0; i <= tableLayoutPanel.RowCount; ++i)
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var lastRow = 0;

            CreateLabel("Cells Number", 0, lastRow);
            CellsNum = CreateLabel("-", 1, lastRow++);

            CreateLabel("Engine Size", 0, lastRow);
            EngineSize = CreateLabel("-", 1, lastRow++);

            CreateLabel("HP", 0, lastRow);
            BaseArmor = CreateLabel("-", 1, lastRow++);

            CreateLabel("Default Weigth", 0, lastRow);
            BaseWeigth = CreateLabel("-", 1, lastRow++);

            CreateLabel("Minimal Weigth", 0, lastRow);
            MinWeigth = CreateLabel("-", 1, lastRow++);

            CreateLabel("Energy Resistance", 0, lastRow);
            BaseEnergyResistance = CreateLabel("-", 1, lastRow++);

            CreateLabel("Kinetic Resistance", 0, lastRow);
            BaseKineticResistance = CreateLabel("-", 1, lastRow++);

            CreateLabel("Heat Resistance", 0, lastRow);
            BaseHeatResistance = CreateLabel("-", 1, lastRow++);

            CreateLabel("Crafting Credits Cost", 0, lastRow);
            CreditsCost = CreateLabel("-", 1, lastRow++);

            CreateLabel("Crafting Stars Cost", 0, lastRow);
            StarCost = CreateLabel("-", 1, lastRow++);

            CreateLabel("Minimal Wandering Ship Distance", 0, lastRow);
            MinSpawnDistance = CreateLabel("-", 1, lastRow++);

            tableLayoutPanel.ResumeLayout();
        }

        public void OnLayoutChanged()
        {
            tableLayoutPanel.SuspendLayout();

            string data = _layout.Layout;

            int size = data.Replace("0", "").Length;
            CellsNum.Text = size.ToString();

            EngineSize.Text = (data.Replace("0", "").Length - data.Replace("0", "").Replace("5", "").Length).ToString();
            var armor = _database.ShipSettings.BaseArmorPoints.Value + _database.ShipSettings.ArmorPointsPerCell.Value * size;
            BaseArmor.Text = armor.ToString("0.00");

            BaseWeigth.Text = (_database.ShipSettings.DefaultWeightPerCell.Value * size * (1+_shipData.BaseWeightModifier.Value)).ToString("0.0");

            MinWeigth.Text = (_database.ShipSettings.MinimumWeightPerCell.Value * size * (1+_shipData.BaseWeightModifier.Value)).ToString("0.0");

            BaseEnergyResistance.Text = CalculateResistances(_shipData.EnergyResistance.Value).ToString("0.00");
            BaseKineticResistance.Text = CalculateResistances(_shipData.KineticResistance.Value).ToString("0.00");
            BaseHeatResistance.Text = CalculateResistances(_shipData.HeatResistance.Value).ToString("0.00");

            long cost;
            if (_shipData.ShipCategory == Enums.ShipCategory.Flagship) cost = checked(15 * size * size);
            else cost = checked(5 * size * size);

            CreditsCost.Text = cost.ToString();

            long starcost;
            switch (_shipData.ShipCategory)
            {
                case Enums.ShipCategory.Common:
                    starcost = cost / 48000;
                    break;
                case Enums.ShipCategory.Rare:
                    starcost = cost / 6000;
                    break;
                case Enums.ShipCategory.Flagship:
                    starcost = (int)Math.Floor(armor / 5);
                    break;
                default:
                    starcost = -1;
                    break;
            }
            if (starcost >= 0)
            {
                StarCost.Text = starcost.ToString();
            }
            else
            {
                StarCost.Text = "-";
            }

            MinSpawnDistance.Text = Math.Max((size - 55) / 2, 0).ToString();

            tableLayoutPanel.ResumeLayout();
        }

        private float CalculateResistances(float number)
        {
            return 100-100/(number+1);
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
    }
}
