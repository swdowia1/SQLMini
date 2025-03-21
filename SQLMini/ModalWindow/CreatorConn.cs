using SQLMini.Klasy;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace SQLMini.ModalWindow
{
    public partial class CreatorConn : Form
    {
        public string wartosc;
        string connectionString;

        public CreatorConn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false; ;
            connectionString = $"Server={txtServer.Text};Database={txtDatabase.Text};User Id={txtUsername.Text};Password={txtPassword.Text};";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Połączenie udane!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNameCatalog.Text = txtDatabase.Text;
                    txtDescribe.Text = "baza " + txtDatabase.Text;


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd połączenia: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            button1.Enabled = true;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.wartosc = "nowy";
            this.DialogResult = DialogResult.OK;
            string catalog = "Serwery\\" + txtNameCatalog.Text;
            Directory.CreateDirectory(catalog);
            File.WriteAllText(classFun.PolPlik(catalog), txtDescribe.Text + classConst.EOL + connectionString);
            this.Close();
        }
    }
}
