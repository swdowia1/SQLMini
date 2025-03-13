using SQLMini.Klasy;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SQLMini.ModalWindow
{
    public partial class FormData : Form
    {
        int ex, ey;
        DataTable datatableORG;
        DataView dataviewORG;
        DataTable d_filtr;
        Query _selectedRow;
        public FormData(Query selectedRow)
        {
            InitializeComponent();
            _selectedRow = selectedRow;
            dg.SetStyle();
            datatableORG = classData.WypelnijDane(selectedRow);
            if (datatableORG != null)
                this.Text = selectedRow.Name + " ilosc:" + datatableORG.Rows.Count;

            dataviewORG = datatableORG.DefaultView;
            dg.DataSource = dataviewORG;


            AddContextMenuStrip("Kopia wiersz", WierszCSV);
            AddContextMenuStrip("Kopia kolumn", KolumnaCSV);
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
            var d = dg.CurrentRow<DataRowView>();
            classMessage.Show($"wiersz {wiersz} kolumna {kolumna}");


        }
        //KolumnaCSV
        private void KolumnaCSV(object sender, EventArgs e)
        {
            var cell = CellPos(sender);


            classMessage.Show($"kolumna{cell.kolumna}");


            StringBuilder sb = new StringBuilder();
            string columnName = datatableORG.Columns[cell.kolumna].ColumnName;
            sb.Append(columnName + classConst.EOL);
            sb.Append("---------------------------------" + classConst.EOL);

            foreach (DataRowView rowView in dataviewORG)
            {
                sb.Append(rowView[cell.kolumna].ToString() + classConst.EOL);
            }
            classFun.StringBuilderToFile(sb, "kolumna.txt");


        }

        //WierszCSV
        private void WierszCSV(object sender, EventArgs e)
        {

            StringBuilder sb = new StringBuilder();

            DataRowView rowView = dg.CurrentRow<DataRowView>();
            foreach (DataColumn column in rowView.Row.Table.Columns)
            {
                sb.Append($"{column.ColumnName}: {rowView[column.ColumnName]}" + classConst.EOL);
            }


            classFun.StringBuilderToFile(sb, "sb.txt");
            classMessage.Show("zapisano do pliku sb.txt");

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

        private void btnCSV_Click(object sender, EventArgs e)
        {
            string filename = classConst.KatalogCSV + "test.csv";
            string filenameorg = classConst.KatalogCSV + _selectedRow.Name + ".csv";
            dg.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            // Select all the cells
            dg.SelectAll();
            // Copy selected cells to DataObject
            DataObject dataObject = dg.GetClipboardContent();
            // Get the text of the DataObject, and serialize it to a file
            File.WriteAllText(filename, dataObject.GetText(TextDataFormat.CommaSeparatedValue));
            File.WriteAllText(filenameorg, dataObject.GetText(TextDataFormat.CommaSeparatedValue));
            dg.ClearSelection();

            if (classMessage.Pytanie("Czy otworzyc edytor"))
                Process.Start(classConfig.edytor, filenameorg);
            return;
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
