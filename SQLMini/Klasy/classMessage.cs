using System.Windows.Forms;

namespace SQLMini.Klasy
{
    internal class classMessage
    {
        internal static bool Pytanie(string v)
        {
            DialogResult dialogResult = MessageBox.Show(v, "Pytanie", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        internal static void Show(string value)
        {
            MessageBox.Show(value, "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static void ShowError(string value, string Title = "Error")
        {

            MessageBox.Show(value, Title,
      MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
