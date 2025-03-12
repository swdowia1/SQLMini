﻿using System;
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
        /// <summary>
        /// Lista tabel w wybranej bazie
        /// </summary>
        /// <param name="pol"></param>
        /// <returns></returns>
        internal static List<Query> Tabele(string pol)
        {
            List<Query> wynik = new List<Query>();
            try
            {
                string zap = @"SELECT      SCHEMA_NAME(A.schema_id) + '.' +
       
          A.Name as Tabela, AVG(B.rows) AS 'Ilosc'
FROM        sys.objects A
INNER JOIN sys.partitions B ON A.object_id = B.object_id
WHERE       A.type = 'U'
GROUP BY    A.schema_id, A.Name";
                // DataTable dt = classData.WypelnijDane("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", pol);
                DataTable dt = classData.WypelnijDane(zap, pol);
                foreach (DataRow item in dt.Rows)
                {
                    string tabela = item["Tabela"].ToString();
                    Query q = new Query() { POL = pol, QueryText = "select * from " + tabela, Name = tabela, Ilosc = int.Parse(item["Ilosc"].ToString()) };
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
