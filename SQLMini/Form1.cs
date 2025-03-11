using SQLMini.Klasy;
using SQLMini.ModalWindow;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SQLMini
{
    public partial class Form1 : Form
    {
        List<Server> serwery = new List<Server>();
        public Form1()
        {
            InitializeComponent();
            serwery = classFun.SerwerList();
            this.Text = classConfig.context;
            classLog.LogInfo(this.Text);
            //var ff = classData.WypelnijDane("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'",
            //    "Server=10.1.0.136;Database=OnnShop20DEV;User Id=onnshop20adm;Password=00c1edf6eedf52g34h7diecjc5k9elc5m95n62ob0p16q96r;MultipleActiveResultSets=true;TrustServerCertificate=True;Max Pool Size=400;");
            dataGridView1.SetStyle(false);
            dgQuery.SetStyle();
            dataGridView1.DataSource = serwery;
        }

        private void dataGridView1_Click(object sender, System.EventArgs e)
        {
            var SelectedRow = dataGridView1.CurrentRow<Server>();
            List<Query> zapytania = classFun.Tabele(SelectedRow.Pol);
            TextForm(zapytania.Count);
            dgQuery.DataSource = zapytania;
            dgQuery.Columns[0].Visible = false;
        }

        private void TextForm(int count)
        {
            this.Text = "Liczba wierszy " + count.ToString();
        }

        private void dgQuery_Click(object sender, System.EventArgs e)
        {
            var SelectedRow = dgQuery.CurrentRow<Query>();

            using (FormData form = new FormData(SelectedRow))
            {
                form.ShowDialog(this);
            } // 
        }
    }
}
