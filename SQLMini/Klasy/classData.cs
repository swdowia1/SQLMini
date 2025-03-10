﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace SQLMini.Klasy
{
    internal class classData
    {
        public static DataTable WypelnijDane(string zapytanie, string pol, bool showerror = false)
        {



            try
            {
                using (SqlConnection con = new SqlConnection(pol))
                {


                    SqlDataAdapter dataAdapter = new SqlDataAdapter(zapytanie, con);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    var dane = dataSet.Tables[0];
                    if (dane.Rows.Count > 1)
                        return dane;
                    if (dane.Rows.Count == 1)
                    {
                        DataTable MyTable = new DataTable(); // 1
                        MyTable.Columns.Add("Id", typeof(int));
                        MyTable.Columns.Add("Kolumna", typeof(string));
                        MyTable.Columns.Add("Wartosc", typeof(string));

                        for (int i = 0; i < dane.Columns.Count; i++)
                        {
                            MyTable.Rows.Add(i + 1, dane.Columns[i].ColumnName, dane.Rows[0][i].ToString());

                        }
                        return MyTable;


                    }
                    return null;
                }
            }
            catch (Exception ee)
            {
                if (showerror)
                {
                    classMessage.ShowError(ee.Message);
                }
                classLog.LogError(ee, "classFun.SyberiaZapytanie");
                return null;

            }
        }
    }
}
