using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SQLMini.ModalWindow
{
    public partial class CreatorConn : Form
    {
        public string wartosc;

        public CreatorConn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = $"Server={txtServer.Text};Database={txtDatabase.Text};User Id={txtUsername.Text};Password={txtPassword.Text};";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Połączenie udane!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.wartosc = "ss";
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd połączenia: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
