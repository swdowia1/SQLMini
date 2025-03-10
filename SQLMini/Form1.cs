using SQLMini.Klasy;
using System.Windows.Forms;

namespace SQLMini
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = classConfig.context;
        }
    }
}
