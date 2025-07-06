using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Obecnosc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitDataGridView();
            ObliczSumeGodzin();

        }

        private void InitDataGridView()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var dayColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Dzień",
                ReadOnly = true
            };

            var hoursColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Godziny (np. 7h30)"
            };

            dataGridView1.Columns.Add(dayColumn);
            dataGridView1.Columns.Add(hoursColumn);

            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month-1;
            var culture = new CultureInfo("pl-PL");

            for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            {
                DateTime date = new DateTime(year, month, day);
                string dayText = $"{day} {culture.DateTimeFormat.GetMonthName(month)} {culture.DateTimeFormat.GetDayName(date.DayOfWeek)}";

                string godziny = "";

                bool isWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
                bool isThursday = date.DayOfWeek == DayOfWeek.Thursday;

                if (isThursday)
                    godziny = "7";

                int rowIndex = dataGridView1.Rows.Add(dayText, godziny);

                if (isWeekend)
                {
                    // Szary kolor + zablokowanie edycji godziny
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.Cells[1].ReadOnly = true;
                }
            }

            dataGridView1.CellValidating += DataGridView1_CellValidating;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);           // Komórki
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold); // Nagłówki
        }
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1) // kolumna "Godziny"
            {
                ObliczSumeGodzin();
            }
        }
        private void ObliczSumeGodzin()
        {
            int sumaMinut = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                var cell = row.Cells[1];
                if (cell.ReadOnly) continue; // pomijamy weekendy

                string wartosc = cell.Value?.ToString().Trim() ?? "";

                if (string.IsNullOrEmpty(wartosc)) continue;

                try
                {
                    int minuty = ParsujGodziny(wartosc);
                    sumaMinut += minuty;
                }
                catch
                {
                    cell.Style.BackColor = Color.LightCoral; // zaznacz błędne pole
                }
            }

            int godziny = sumaMinut / 60;
            int minutyReszta = sumaMinut % 60;
this.Text = $"Suma: {godziny}h{minutyReszta:D2}";
        }
        private int ParsujGodziny(string input)
        {
            int suma = 0;
            if (string.IsNullOrEmpty(input))
                return 0;
            input=input.ToLower();
            string[] kol=input.Split('h');
            if (kol.Length==1)
            {
                suma = 60 * int.Parse(kol[0]);
            }
            else
                suma = 60 * int.Parse(kol[0])+ int.Parse(kol[1]);
            return suma;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sprawdź, czy kliknięto w kolumnę "Godziny"
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (!cell.ReadOnly)
                {
                    cell.Value = "7";
                    ObliczSumeGodzin();
                }
            }
        }
        private void DataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1 && !dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
            {
                string input = e.FormattedValue.ToString();
                if (!IsValidTimeFormat(input) && input!="")
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightCoral;
                    MessageBox.Show("Wpisz godziny w formacie np. 7h, 7h30, 0h45", "Nieprawidłowy format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }
        }

        private bool IsValidTimeFormat(string input)
        {
            if(input.ToLower().Contains("h"))
            return Regex.IsMatch(input, @"^\d{1,2}h(\d{1,2})?$");
            else
                return Regex.IsMatch(input, @"^\d{1,2}?$");
        }
    }
}

