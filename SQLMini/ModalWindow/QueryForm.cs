using SQLMini.Klasy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLMini.ModalWindow
{
    public partial class QueryForm : Form
    {
        Server _server;
        public QueryForm(Server server)
        {
            InitializeComponent();
            _server = server;
            this.Text = "Query on " + _server.Opis;
            List<string> zapytania = classFun.TabeleTylko(_server.Pol);
            dgTable.SetStyle();
            dgTable.DataSource = zapytania
       .Select(nazwa => new Dane(nazwa))
       .ToList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Dane aa = dgTable.CurrentRow<Dane>();

            rt.Text = "delete from " +aa.Wartosc;


            this.Text = "Usuwanie tabeli " + aa;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            string res=classFun.UsunRekordy(_server.Pol, rt.Text);
            this.Text = res;
        }
    }
}
