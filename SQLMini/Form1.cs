using SQLMini.Klasy;
using SQLMini.ModalWindow;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLMini
{
    public partial class Form1 : Form
    {
        List<Server> serwery = new List<Server>();
        Server SelectedRow;
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
            } 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

           
            SelectedRow = dataGridView1.CurrentRow<Server>();
            classMessage.PopUp("wybrano " + SelectedRow.Opis);
            List<Query> zapytania = classFun.Tabele(SelectedRow.Pol,SelectedRow.KatalogSQL);
            TextForm(zapytania.Count);
            dgQuery.DataSource = zapytania;
            dOrg = zapytania;
            dgQuery.Columns[0].Visible = false; 
            }
            catch (Exception ex)
            {

                classMessage.Show(ex.Message);
            }
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
                if (result == DialogResult.OK)
                {
                    string zapytanie = form.wartosc; 

                    serwery = classFun.SerwerList();
                    dataGridView1.DataSource = serwery;
                }
            } // 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path=SelectedRow.KatalogSQL;
            if (Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
            else
            {
                MessageBox.Show("Katalog nie istnieje.");
            }
        }

        private void btnConnProperty_Click(object sender, EventArgs e)
        {
            string pol = SelectedRow.Pol;
            var builder = new SqlConnectionStringBuilder(pol);
            StringBuilder sb = new StringBuilder();
         
            string eol = classConst.EOL;
            sb.AppendLine(pol);
            sb.Append("Serwer\t\t:" + builder.DataSource+eol);
            sb.Append("Baza danych\t:" + builder.InitialCatalog + eol);
            sb.Append("Użytkownik\t:" + builder.UserID + eol);
            sb.Append("Hasło\t\t:" + builder.Password + eol);
            File.WriteAllText("dane.txt", sb.ToString());
            Process.Start(classConfig.edytor, "dane.txt");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (QueryForm form = new QueryForm(SelectedRow))
            {
                form.ShowDialog(this);
            } //
        }
    }
}
