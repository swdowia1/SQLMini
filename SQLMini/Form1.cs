using SQLMini.Klasy;
using System.Windows.Forms;

namespace SQLMini
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = classConfig.context;
            classLog.LogInfo(this.Text);
            var ff = classData.WypelnijDane("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'",
                "Server=10.1.0.136;Database=OnnShop20DEV;User Id=onnshop20adm;Password=00c1edf6eedf52g34h7diecjc5k9elc5m95n62ob0p16q96r;MultipleActiveResultSets=true;TrustServerCertificate=True;Max Pool Size=400;");
            dataGridView1.SetStyle();
            dataGridView1.DataSource = ff;
        }
    }
}
