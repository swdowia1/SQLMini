using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SQLMini.Klasy
{
    public class classFun
    {
        public static List<Server> SerwerList()
        {
            List<Server> wynik = new List<Server>();


            try
            {
                string katalog = classConst.Katalog + "Serwery";
                string[] serwer = Directory.GetDirectories(katalog);
                foreach (string s in serwer)
                {
                    string polPlik = PolPlik(s);
                    string[] linie = File.ReadAllLines(polPlik);
                    Server ss = new Server(linie);
                    wynik.Add(ss);
                }

            }
            catch (Exception ee)
            {

                classLog.LogError(ee, "classFun.SerwerList");


            }
            return wynik;
        }

        public static string PolPlik(string s)
        {
            return s + "\\pol.txt";
        }

        internal static List<Query> Tabele(string pol)
        {
            List<Query> wynik = new List<Query>();
            try
            {
                DataTable dt = classData.WypelnijDane("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", pol);
                foreach (DataRow item in dt.Rows)
                {
                    string tabela = item["TABLE_NAME"].ToString();
                    Query q = new Query() { POL = pol, QueryText = "select * from " + tabela, Name = tabela };
                    wynik.Add(q);
                }
            }
            catch (Exception ee)
            {

                classLog.LogError(ee, "classFun.Tabele");


            }
            return wynik;

        }
    }
}
