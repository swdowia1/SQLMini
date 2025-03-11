using System.Drawing;
using System.Windows.Forms;

namespace SQLMini.Klasy
{
    public static class classExtension
    {
        public static void SetStyle(this DataGridView dg, bool autoGenerate = true)
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dg.AutoGenerateColumns = autoGenerate;
            dg.AllowUserToAddRows = false;
            dg.AllowUserToDeleteRows = false;


            dg.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            foreach (DataGridViewColumn column in dg.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }


            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 224, 192);
            dg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dg.ReadOnly = true;
        }

        public static T CurrentRow<T>(this DataGridView dg)
        {
            return (T)(dg.CurrentRow.DataBoundItem);
        }
    }
}
