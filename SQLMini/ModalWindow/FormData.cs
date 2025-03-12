using SQLMini.Klasy;
using System.Data;
using System.Windows.Forms;

namespace SQLMini.ModalWindow
{
    public partial class FormData : Form
    {
        public FormData(Query selectedRow)
        {
            InitializeComponent();
            dg.SetStyle();
            DataTable dt = classData.WypelnijDane(selectedRow);
            this.Text = selectedRow.Name + " ilosc:" + dt.Rows.Count;
            dg.DataSource = dt;
        }
    }
}
