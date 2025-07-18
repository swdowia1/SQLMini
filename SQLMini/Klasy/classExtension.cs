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
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 255, 230);
            dg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dg.ReadOnly = true;
        }

        public static void SetStyleWitchoutZebra(this DataGridView dg)
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dg.AutoGenerateColumns = true;
            dg.AllowUserToAddRows = false;
            dg.AllowUserToDeleteRows = false;
            dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 255, 230);
           // dg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dg.ReadOnly = true;
        }

        public static T CurrentRow<T>(this DataGridView dg)
        {
            object item = dg.CurrentRow?.DataBoundItem;

            if (item is T)
                return (T)item;

            // Gdy np. string, a chcemy tylko T == string
            if (typeof(T) == typeof(string) && item != null)
                return (T)(object)item.ToString();

            return default;

        }
    }
}
