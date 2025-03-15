using SQLMini.Klasy;
using SQLMini.ModalWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SQLMini
{
    public partial class Form1 : Form
    {
        List<Server> serwery = new List<Server>();
        List<Query> dOrg;
        List<Query> d_filtr;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;
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










        private void TextForm(int count)
        {
            this.Text = "Liczba wierszy " + count.ToString();
        }



        private void dgQuery_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var SelectedRow = dgQuery.CurrentRow<Query>();

            using (FormData form = new FormData(SelectedRow))
            {
                form.ShowDialog(this);
            } // 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var SelectedRow = dataGridView1.CurrentRow<Server>();
            classMessage.PopUp("wybrano " + SelectedRow.Opis);
            List<Query> zapytania = classFun.Tabele(SelectedRow.Pol);
            TextForm(zapytania.Count);
            dgQuery.DataSource = zapytania;
            dOrg = zapytania;
            dgQuery.Columns[0].Visible = false;
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            string searchValue = txtName.Text.ToLower();
            if (string.IsNullOrEmpty(searchValue))
            {
                dgQuery.DataSource = dOrg;
                return;
            }
            int rowIndex = -1;

            List<int> searchid = new List<int>();
            dgQuery.DataSource = dOrg;

            try
            {
                foreach (DataGridViewRow row in dgQuery.Rows)
                {
                    for (int i = 1; i < row.Cells.Count; i++)
                    {

                        if (row.Cells[i].Value.ToString().ToLower().Contains(searchValue))
                        {




                            rowIndex = row.Index;
                            searchid.Add(row.Index);

                        }
                    }
                }

                searchid = searchid.Distinct().ToList();
                d_filtr = new List<Query>();
                foreach (int k in searchid)
                {
                    d_filtr.Add(dOrg[k]);
                }
                dgQuery.DataSource = d_filtr;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnNewServer_Click(object sender, EventArgs e)
        {
            using (CreatorConn form = new CreatorConn())
            {
                var result = form.ShowDialog(this);
                if (result == DialogResult.OK) // check if the user accepted  his input
                {
                    string zapytanie = form.wartosc; // obtain our customly-created property's value

                    serwery = classFun.SerwerList();
                    dataGridView1.DataSource = serwery;
                }
            } // 
        }
    }
}
