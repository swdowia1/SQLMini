using SQLMini.Klasy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SQLMini.ModalWindow
{
    public partial class FormData : Form
    {
        int ex, ey;
    
        private string[] keywords = { "SELECT", "FROM", "WHERE", "INSERT", "UPDATE", "DELETE", "JOIN", "INNER", "LEFT", "RIGHT", "ON", "ORDER", "GROUP", "BY", "HAVING",
        "LIKE","TOP"};

        DataTable datatableORG;
        DataView dataviewORG;
        DataTable d_filtr;
        Query _selectedRow;
        public FormData(Query selectedRow)
        {
            InitializeComponent();
            _selectedRow = selectedRow;


            richTextBox1.Font = new Font("Consolas", 12);
            //richTextBox1.TextChanged += RichTextBox_TextChanged;
            richTextBox1.Text = _selectedRow.QueryText;
            RichTextBox_TextChanged();


            dg.SetStyle();
            FillGrid();

            AddContextMenuStrip("Kopia wiersz", WierszCSV);
            AddContextMenuStrip("Kopia kolumn", KolumnaCSV);
            AddContextMenuStrip("Filtr równy", FiltrRowny);
            AddContextMenuStrip("Filtr like", KolumnaCSV);
        }

        private void FillGrid()
        {
            datatableORG = classData.WypelnijDane(_selectedRow);

            if (datatableORG != null)
            {
                this.Text = _selectedRow.Name + " ilosc:" + datatableORG.Rows.Count;

                dataviewORG = datatableORG.DefaultView;
                dg.DataSource = dataviewORG;

            }
        }

        private void RichTextBox_TextChanged(object sender=null, EventArgs e=null)
        {
            int selectionStart = richTextBox1.SelectionStart;
            richTextBox1.SuspendLayout();

            // Resetowanie kolorów
            richTextBox1.SelectAll();
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectionStart = selectionStart;
            richTextBox1.SelectionLength = 0;

            // Podświetlanie słów kluczowych SQL
            foreach (string keyword in keywords)
            {
                HighlightWord(keyword, Color.Blue);
            }

            // Podświetlanie komentarzy (--) oraz łańcuchów znaków ('...')
            HighlightRegex(@"--.*", Color.Green);  // Komentarze SQL
            HighlightRegex(@"'[^']*'", Color.Brown);  // Stringi w SQL
            HighlightRegex(@"\b\d+\b", Color.YellowGreen);

            richTextBox1.ResumeLayout();
        }

        private void HighlightWord(string word, Color color)
        {
            Regex regex = new Regex(@"\b" + word + @"\b", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(richTextBox1.Text);

            foreach (Match match in matches)
            {
                richTextBox1.Select(match.Index, match.Length);
                richTextBox1.SelectionColor = color;
            }
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.SelectionColor = Color.Black;
        }

        private void HighlightRegex(string pattern, Color color)
        {
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(richTextBox1.Text);

            foreach (Match match in matches)
            {
                richTextBox1.Select(match.Index, match.Length);
                richTextBox1.SelectionColor = color;
            }
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.SelectionColor = Color.Black;
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
        private void FiltrRowny(object sender, EventArgs e)
        {
            var cell = CellPos(sender);
            string value = datatableORG.Rows[cell.wiersz][cell.kolumna].ToString();
            var type = datatableORG.Columns[cell.kolumna].DataType.Name.ToLower();
            if (!txtFilter.Text.TrimEnd().EndsWith("and"))
                txtFilter.Text = WstawFil(type, cell.kolumna, value);
            else
                txtFilter.Text += WstawFil(type, cell.kolumna, value);
        }
        private string WstawFil(string type, int kolumna, string value)
        {

            string rresult = dg.Columns[kolumna].Name + "=";
            switch (type)
            {
                case "string":
                    value = "'" + value + "'"; break;
                default:
                    break;
            }

            return rresult + value;
        }
        //KolumnaCSV
        private void KolumnaCSV(object sender, EventArgs e)
        {
            var cell = CellPos(sender);


        


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
            string filenameorg = classConst.KatalogCSV + _selectedRow.Name.Replace(" ","_") + ".csv";
            dg.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            // Select all the cells
            dg.SelectAll();
            // Copy selected cells to DataObject
            DataObject dataObject = dg.GetClipboardContent();
            // Get the text of the DataObject, and serialize it to a file
            File.WriteAllText(filename, dataObject.GetText(TextDataFormat.CommaSeparatedValue));
            File.WriteAllText(filenameorg, dataObject.GetText(TextDataFormat.CommaSeparatedValue));
            dg.ClearSelection();

            if (classMessage.Question("Czy otworzyc edytor"))
                Process.Start(classConfig.edytor, filenameorg);
            return;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FilterGrid();
        }
        private void FilterGrid()
        {
            string filter = txtFilter.Text;
            dg.DataSource = datatableORG;
            try
            {

                if (!filter.Contains("="))
                {
                    List<int> searchid = new List<int>();
                    foreach (DataGridViewRow row in dg.Rows)
                    {
                        for (int i = 0; i < row.Cells.Count; i++)
                        {

                            if (row.Cells[i].Value.ToString().ToLower().Contains(filter))
                            {





                                searchid.Add(row.Index);

                            }
                        }
                    }
                    searchid = searchid.Distinct().ToList();
                    d_filtr = datatableORG.Clone();
                    foreach (var item in searchid)
                    {
                        d_filtr.ImportRow(datatableORG.Rows[item]);
                    }
                    dg.DataSource = d_filtr;
                    return;
                }
                dataviewORG = datatableORG.DefaultView;
                if (!string.IsNullOrEmpty(filter))
                {
                    dataviewORG.RowFilter = string.Format(filter);
                }
                else
                    dataviewORG.RowFilter = null;



                dg.DataSource = dataviewORG;

                this.Text = "Filtr ilosc:" + txtFilter.Text + " " + dataviewORG.Count;
            }
            catch (Exception)
            {


            }
        }

        private void btnFilterClear_Click(object sender, EventArgs e)
        {
            txtFilter.Text = "";
            FilterGrid();
        }

        private void dg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;
            if (dg.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                string val = dg.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                DoSchowka(val);
            }
        }

        private void DoSchowka(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Clipboard.SetText(value);
                classMessage.PopUp("zapisano do schowka " + value);
            }

        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            _selectedRow.QueryText = richTextBox1.Text;
            FillGrid();
            tabControl1.SelectedIndex = 0;
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
