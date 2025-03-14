using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace SQLMini.Klasy
{
    internal class classMessage
    {
        public static void PopUp(string tresc, int wielkosc = 100)
        {
            PopupNotifier popup = new PopupNotifier();
            //if (tresc.Length > 30)
            //{
            //    tresc = tresc.Substring(tresc.Length - wielkosc);
            //}
            popup.TitleText = "Informacja";
            popup.ContentText = tresc;
            popup.Popup(); // show
        }
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
