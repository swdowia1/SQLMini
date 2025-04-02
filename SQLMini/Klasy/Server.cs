namespace SQLMini.Klasy
{
    public class Server
    {


        public Server(string[] linie, string katalogSQL)
        {
            Opis = linie[0];
            Pol = linie[1];
            KatalogSQL = katalogSQL;
        }
        public string KatalogSQL { get; set; }
        public string Opis { get; set; }
        public string Pol { get; set; }
    }
}
