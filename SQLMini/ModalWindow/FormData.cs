using SQLMini.Klasy;
using System;
using System.Data;
using System.Windows.Forms;

namespace SQLMini.ModalWindow
{
    public partial class FormData : Form
    {
        int ex, ey;
        public FormData(Query selectedRow)
        {
            InitializeComponent();
            dg.SetStyle();
            DataTable dt = classData.WypelnijDane(selectedRow);
            if (dt != null)
                this.Text = selectedRow.Name + " ilosc:" + dt.Rows.Count;
            dg.DataSource = dt;
            AddContextMenuStrip("Koszyk", KoszykAdmin);
        }
        private CellSelect CellPos(object sender)
        {
            ToolStripMenuItem tt = (ToolStripMenuItem)sender;


            int wiersz = dg.HitTest(ex, ey).RowIndex;
            int kolumna = dg.HitTest(ex, ey).ColumnIndex;

            return new CellSelect() { wiersz = wiersz, kolumna = kolumna, Tag = tt.Tag?.ToString() };
        }
        private void KoszykAdmin(object sender, EventArgs e)
        {
            string url = "https://admin2k.onninen.it/pages/baskets?find={0}";
            var cell = CellPos(sender);




            int wiersz = cell.wiersz;
            int kolumna = cell.kolumna;




        }

        private void dg_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var dgv = (DataGridView)sender;
            ContextMenuStrip cms = null;
            ex = e.X;
            ey = e.Y;
            var hit = dgv.HitTest(e.X, e.Y);
            switch (hit.Type)
            {

                case DataGridViewHitTestType.Cell:
                    cms = contextMenuStrip1; break;
            }
            if (cms != null) cms.Show(dgv, e.Location);
        }

        private void AddContextMenuStrip(string caption, EventHandler eh)
        {
            int ilosc = contextMenuStrip1.Items.Count;
            ToolStripMenuItem t = new ToolStripMenuItem();
            t.Name = "menuStrip" + ilosc;
            t.Text = caption;

            t.Click += eh;
            contextMenuStrip1.Items.Add(t);
        }
    }
}
