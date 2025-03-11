namespace SQLMini.Klasy
{
    public class Server
    {


        public Server(string[] linie)
        {
            Opis = linie[0];
            Pol = linie[1];
        }

        public string Opis { get; set; }
        public string Pol { get; set; }
    }
}
